using System;
using System.IO;
using System.Threading.Tasks;

namespace BingImageRipper
{
    internal static class FileReader
    {


        // The path of the file to read
        static async Task<string> GetContents(string filePath = "Bing.html")
        {
            // The variable to store the contents of the file
            string contents = string.Empty;

            // Try to read the file asynchronously and catch any exception
            try
            {
                // Use the ReadTextAsync method of the File class to read the file as a string
                contents = await File.ReadAllTextAsync(filePath);

                // Print the contents of the file to the console
                Console.WriteLine(contents);
            }
            catch (Exception e)
            {
                // Print the error message to the console
                Console.WriteLine(e.Message);
            }
            return contents;
        }
    }
}

