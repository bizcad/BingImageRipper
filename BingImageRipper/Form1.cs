using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.IO;
using System.Windows;
using System.Diagnostics;

namespace BingImageRipper
{
    public partial class Form1 : Form
    {
        private string title = string.Empty;
        private string headline = string.Empty;
        private string url = "https://www.bing.com/";
        private string htmlFile = @"C:\Users\bizca\Downloads\Bing.html";
        private string contents = string.Empty;

        public Form1()
        {
            InitializeComponent();
        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            await FetchImageUrlFromHTMLFile();
        }
        private async Task FetchImageUrlFromHTMLFile()
        {
            FileInfo info = new(htmlFile);
            if (!info.Exists) throw new FileNotFoundException("File not Found", htmlFile);
            contents = await File.ReadAllTextAsync(htmlFile);

            try
            {
                string imageName = string.Empty;
                int imageNameLocation = GetStartingIndex(ref contents, ref imageName);
                int imageNameEnd = imageNameLocation + imageName.Length;
                imageNameLocation = contents.IndexOf(@"/th?", imageNameEnd-128);
                imageNameLocation++;
                url += contents[imageNameLocation..imageNameEnd];
                Debug.WriteLine(url);
            } 
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Not Found Error");
            }
            GetHeadline();
        }

        private void GetHeadline()
        {
            labelFileName.Text = $"Getting Image Save To Filename."; 
            Refresh();
            //using HttpClient client = new();
            //var contents = await client.GetAsync(url).Result.Content.ReadAsStringAsync();
            //// for development, get a clipboard
            //System.Windows.Forms.Clipboard.SetText(contents);
            //int startingIndex = contents.IndexOf("th?");
            //startingIndex = contents.IndexOf("th?", startingIndex + 3);  // get the second one
            //int endingIndex;
            //StringBuilder sb = new();
            //if (startingIndex > 0)
            //{
            //    string c = "1080";
            //    char e = '"';

            //    endingIndex = contents.IndexOf(c, startingIndex);
            //    endingIndex = contents.IndexOf(e, endingIndex);

            //    sb = new();
            //    sb.Append(@"https://bing.com/");
            //    sb.Append(contents.AsSpan(startingIndex, endingIndex - startingIndex));
            //    url = sb.ToString();
            //    url = url[..url.IndexOf('&')];
            //}
            //if (url.Length == 0)
            //{
            //    throw new Exception("url not found");
            //}
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
            //string find = @"<meta property=""og:title"" content=";
            //int startingIndex = contents.IndexOf(find, 0) + 1 + find.Length;
            //int endingIndex = contents.IndexOf(@"/>", startingIndex) - 2;
            //title = contents[startingIndex..endingIndex];
            //title += ".jpg";
            //title = title.Replace("?", string.Empty);

            // Create a filename from the ImageContent.
            int startingIndex = contents.IndexOf(@"ImageContent", 0);
            startingIndex = contents.IndexOf(@"Title", startingIndex + 9);
            int endingIndex = contents.IndexOf('"', startingIndex + 12);
            startingIndex += @"Title".Length + 4;
            headline = contents[startingIndex..endingIndex];
            headline += ".jpg";
            headline = string.Join("_", headline.Split(Path.GetInvalidFileNameChars()));
            headline = headline.Replace("?", string.Empty)
                .Replace(",", string.Empty)
                .Replace("'", string.Empty);
            if (headline.Length < 4)
            {
                throw new Exception("Zero Length Filename from ImageContent");
            }
            textBoxTitle.Text = headline;
        }

        /// <summary>
        /// Finds the starting index of the first occurrence of a valid image filename
        /// </summary>
        /// <param name="contents">The contents of the saved bing web page</param>
        /// <param name="imageName">The name of the image file it found</param>
        /// <returns>the starting index of the image filename</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        static int GetStartingIndex(ref string contents, ref string imageName)
        {
            int startingIndex = -1;
            string[] arr = "1920x1080.webp,1920x1200.jpg,1920x1080.jpg,1920x1080.webp".Split(",");
            //string[] arr = "3920x1200.jpg".Split(",");
            foreach (string fn in arr)
            {
                startingIndex = contents.IndexOf(fn);
                if (startingIndex >= 0)
                {
                    imageName = fn;
                    break;
                }
            }
            if (startingIndex < 0)
            {
                imageName = string.Empty;
                throw new KeyNotFoundException("Cannot find reference to appropriate image.");
            }

            return startingIndex;
        }


        private async void Form1_Load(object sender, EventArgs e)
        {
            await FetchImageUrlFromHTMLFile();
        }

        private async void ButtonSetBackground_Click(object sender, EventArgs e)
        {

            labelFileName.Text = $"Getting Image Save To Filename.";
            Refresh();
            string filename = await ImageSaver.SaveImageAsync(url, textBoxTitle.Text);
            labelFileName.Text = filename;
            FileInfo info = new(filename);
            textBoxTitle.Text = info.Name;
            Refresh();
            Thread.Sleep(1000);
            Wallpaper.Set(this.labelFileName.Text, Wallpaper.Style.Stretched);
            System.Threading.Thread.Sleep(2000);
            this.Close();
            labelFileName.Text = string.Empty;
            Refresh();

        }
    }
}