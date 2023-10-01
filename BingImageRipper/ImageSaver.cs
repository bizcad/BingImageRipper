using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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

                //var tag = new SixLabors.ImageSharp.Metadata.Profiles.Exif.IExifValue<string>();

                //i.Metadata.ExifProfile.SetValue<string>("Comment", "test");

                //foreach (var value in i.Metadata.ExifProfile.Values)
                //{
                //    var v = value.GetValue().ToString();
                //    var t = value.GetType().FullName;
                //    var tag = value.Tag.ToString();
                //    string output = ($"{tag} {t} {v}");
                //    Debug.WriteLine(output);
                //}

                string filename = $"{address}{title}";
                if (!filename.EndsWith(".jpg"))
                {
                    filename += ".jpg";                    
                }
                if(File.Exists(filename))
                {
                    File.Delete(filename);
                }
                Thread.Sleep(50);
                if (!File.Exists(filename))
                {
                    Thread.Sleep(50);
                    await i.SaveAsJpegAsync(filename);
                    //image.Save(filename);
                    Thread.Sleep(50);
                }
                else
                {
                    
                    return $"{filename}";
                    
                }
                return $"{filename}";
            }
            return $"Response: {response.StatusCode} {response.ReasonPhrase}";
        }
    }
}
