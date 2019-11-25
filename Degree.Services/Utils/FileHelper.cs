using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Degree.Services.Utils
{
    public static class FileHelper
    {
        public static async Task WriteFile(string filePath, string content)
        {
            using (StreamWriter writetext = new StreamWriter(filePath))
            {
                await writetext.WriteAsync(content);
            }
        }

        public static async Task<string> ReadFile(string filePath)
        {
            using (StreamReader readtext = new StreamReader(filePath))
            {
                var text = await readtext.ReadToEndAsync();
                return text;
            }
        }
    }
}
