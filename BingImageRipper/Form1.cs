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
            string address = @"E:\OneDrive - Personal\OneDrive\Pictures\Big Saved Pictures\";
            using HttpClient client = new();
            string contents = GetLatestVersion(client, url);
            // for development, get a 
            //Clipboard.SetText(contents);
            int startingIndex = contents.IndexOf($"1080");
            startingIndex -= 125;
            startingIndex = contents.IndexOf($"th?", startingIndex);
            int endingIndex = contents.IndexOf(".jpg", startingIndex) + ".jpg".Length;
            StringBuilder sb = new();
            sb.Append(@"https://bing.com/");
            sb.Append(contents.AsSpan(startingIndex, endingIndex - startingIndex));
            url = sb.ToString();

            // get a filename from the title in the og:title meta tag
            string find = @"<meta property=""og:title"" content=";
            startingIndex = contents.IndexOf(find, 0) + 1 + find.Length;
            endingIndex = contents.IndexOf(@"/>", startingIndex) - 2;
            title = contents[startingIndex..endingIndex];
            title += ".jpg";

            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var imageBytes = await response.Content.ReadAsByteArrayAsync();
                using var stream = new MemoryStream(imageBytes);
                Image image = Image.FromStream(stream);

                string filename = $"{address}{title}";
                if (File.Exists(filename)) { File.Delete(filename); }
                Thread.Sleep(50);
                image.Save(filename);
                Thread.Sleep(50);
            }
            label1.Text = $"image saved: {title}";
            Refresh();
            Thread.Sleep(2000);
            this.Close();
        }

        private static string GetLatestVersion(HttpClient client, string url)
        {
            return client.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await FetchBackgroundImage();
        }
    }
}