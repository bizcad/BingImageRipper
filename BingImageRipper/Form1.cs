using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.IO;

namespace BingImageRipper
{
    public partial class Form1 : Form
    {
        private string title = string.Empty;
        private string url = "https://www.bing.com/";

        public Form1()
        {
            InitializeComponent();
        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            await FetchBackgroundImage();
        }

        private async Task FetchBackgroundImage()
        {
            labelFileName.Text = $"Fetching Image";
            Refresh();
            using HttpClient client = new();
            var contents = await client.GetAsync(url).Result.Content.ReadAsStringAsync();
            // for development, get a 
            //Clipboard.SetText(contents);
            int startingIndex = contents.IndexOf("th?");
            startingIndex = contents.IndexOf("th?", startingIndex + 3);  // get the second one

            int endingIndex;
            StringBuilder sb = new();
            if (startingIndex > 0)
            {
                string c = "1080";
                char e = '"';

                endingIndex = contents.IndexOf(c, startingIndex);
                endingIndex = contents.IndexOf(e, endingIndex);

                sb = new();
                sb.Append(@"https://bing.com/");
                sb.Append(contents.AsSpan(startingIndex, endingIndex - startingIndex));
                url = sb.ToString();
                url = url[..url.IndexOf('&')];
            }
            if (url.Length == 0)
            {
                throw new Exception("url not found");
            }
            //else
            //{

            //}
            //startingIndex -= 125;
            //startingIndex = contents.IndexOf($"th?", startingIndex);
            //int endingIndex = contents.IndexOf(".jpg", startingIndex) + ".jpg".Length;
            //StringBuilder sb = new();
            //sb.Append(@"https://bing.com/");
            //sb.Append(contents.AsSpan(startingIndex, endingIndex - startingIndex));
            //url = sb.ToString();

            // get a filename from the title in the og:title meta tag
            string find = @"<meta property=""og:title"" content=";
            startingIndex = contents.IndexOf(find, 0) + 1 + find.Length;
            endingIndex = contents.IndexOf(@"/>", startingIndex) - 2;
            title = contents[startingIndex..endingIndex];
            title += ".jpg";
            title = title.Replace("?", string.Empty);
            textBoxTitle.Text = title;

        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await FetchBackgroundImage();
            //Wallpaper.Set(this.label1.Text, Wallpaper.Style.Stretched);
        }

        private async void ButtonSetBackground_Click(object sender, EventArgs e)
        {
            string filename = await ImageSaver.SaveImageAsync(url, textBoxTitle.Text);
            labelFileName.Text = filename;
            FileInfo info = new FileInfo(filename);
            textBoxTitle.Text = info.Name;
            Refresh();
            Thread.Sleep(1000);
            Wallpaper.Set(this.labelFileName.Text, Wallpaper.Style.Stretched);
            System.Threading.Thread.Sleep(2000);
            this.Close();
            //Wallpaper.Set(this.label1.Text, Wallpaper.Style.Stretched);
        }
    }
}