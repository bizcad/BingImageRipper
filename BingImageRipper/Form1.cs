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

        private async void button1_Click(object sender, EventArgs e)
        {
            await FetchBackgroundImage();
        }

        private async Task FetchBackgroundImage()
        {
            label1.Text = $"Fetching Image";
            Refresh();
            string url = "https://www.bing.com/";
            string address = @"E:\OneDrive - Personal\OneDrive\Pictures\Big Saved Pictures\";
            string filename = "image.jpg";

            using (HttpClient client = new())
            {
                string contents = GetLatestVersion(client, url);
                // for development, get a 
                //Clipboard.SetText(contents);
                int index = contents.IndexOf($"1080");
                index -= 125;
                index = contents.IndexOf($"th?", index);
                int endix = contents.IndexOf(".jpg", index) + ".jpg".Length;
                StringBuilder sb = new();
                sb.Append(@"https://bing.com/");
                sb.Append(contents.Substring(index, endix - index));
                url = sb.ToString();

                string find = @"<meta property=""og:title"" content=";
                index = contents.IndexOf(find, 0) + 1 + find.Length;
                endix = contents.IndexOf(@"/>", index) - 2;
                title = contents.Substring(index, endix - index);
                title += ".jpg";

                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var imageBytes = await response.Content.ReadAsByteArrayAsync();
                    using var stream = new MemoryStream(imageBytes);
                    Image image = Image.FromStream(stream);
                    
                    filename = $"{address}{title}";
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
        }

        private string GetLatestVersion(HttpClient client, string url)
        {
            return client.GetAsync(url).Result.Content.ReadAsStringAsync().Result;

        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await FetchBackgroundImage();
        }
    }
}