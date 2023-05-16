using System.Net.Http;
using System.Text;


namespace BingImageRipper
{
    public partial class Form1 : Form
    {
        private string title = string.Empty;
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
            label1.Text = $"Fetching Image";
            Refresh();
            string url = "https://www.bing.com/";
            using HttpClient client = new();
            string contents = GetLatestVersion(client, url);
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

            label1.Text = await ImageSaver.SaveImageAsync(url, title);
            Refresh();
            Thread.Sleep(1000);
            Wallpaper.Set(this.label1.Text, Wallpaper.Style.Stretched);

            this.Close();
        }

        private static string GetLatestVersion(HttpClient client, string url)
        {
            return client.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await FetchBackgroundImage();
            //Wallpaper.Set(this.label1.Text, Wallpaper.Style.Stretched);
        }

        private void ButtonSetBackground_Click(object sender, EventArgs e)
        {
            Wallpaper.Set(this.label1.Text, Wallpaper.Style.Stretched);
        }
    }
}