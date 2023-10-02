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
        private const string Message = "Could not find the headline.";
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

            // Create a descriptive filename from the ImageContent in the black heading box 
            int startingIndex = contents.IndexOf(@"ImageContent", 0);
            StringBuilder sb = new StringBuilder();
            // "Title":"
            sb.Append('"');
            sb.Append("Title");
            sb.Append('"');
            sb.Append(':');
            sb.Append('"');
            string f = sb.ToString();
            startingIndex = contents.IndexOf(f, startingIndex);   // find the title
            startingIndex = startingIndex + f.Length;               // advance the pointer to the end of the Title string
            int endingIndex = startingIndex + 1;                    // set the ending index to the pointer + 1 for the "
            endingIndex = contents.IndexOf('"', endingIndex);   // find the end of the quoted string

            headline = contents[startingIndex..endingIndex];
            if (headline.Length < 1)
            {
                throw new Exception(Message);
            }
            headline += ".jpg";
            headline = string.Join("_", headline.Split(Path.GetInvalidFileNameChars()));
            headline = headline.Replace("?", string.Empty)
                .Replace(",", string.Empty)
                .Replace("'", string.Empty);

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