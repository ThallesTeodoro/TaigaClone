using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Taiga.Api.Utilities
{
    public class FileManagement
    {
        public static string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                    + "_"
                    + DateTime.Now.ToString("yyyyMMddHHmm")
                    + Path.GetExtension(fileName);
        }

        public static async Task<string> SaveFile(IFormFile file, string path)
        {
            var imageName = GetUniqueFileName(file.FileName);
            var uploads = path;
            var filePath = Path.Combine(uploads,imageName);
            using (var steam = System.IO.File.Create(filePath))
            {
                await file.CopyToAsync(steam);
            }

            return imageName;
        }
    }
}
