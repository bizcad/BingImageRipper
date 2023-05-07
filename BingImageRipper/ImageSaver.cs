using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;


namespace BingImageRipper
{
    public static class ImageSaver
    {
        
        public static async Task<string> SaveImageAsync(string url, string title) 
        {
            string address = @"E:\OneDrive - Personal\OneDrive\Pictures\Big Saved Pictures\";
            HttpClient client = new();
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {

                var imageBytes = await response.Content.ReadAsByteArrayAsync();

                using var stream = new MemoryStream(imageBytes);
                using Task<SixLabors.ImageSharp.Image> image = SixLabors.ImageSharp.Image.LoadAsync(stream);
                SixLabors.ImageSharp.Image i = image.Result;

                string filename = $"{address}{title}";

                if (!File.Exists(filename))
                {

                    Thread.Sleep(50);
                    await i.SaveAsJpegAsync(filename);
                    //image.Save(filename);
                    Thread.Sleep(50);
                }
                else
                {
                    return $"image already exists: {title}";
                    
                }
                return $"image saved: {title}";
            }
            return $"Response: {response.StatusCode} {response.ReasonPhrase}";
        }
    }
}
