
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace Infrastucture.Services
{
    public class FileUploadService
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public FileUploadService(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }
        public string Image(IFormFile ImagePath, string uploadFolderPath)
        {
            if (ImagePath != null && ImagePath.Length > 0)
            {
                var uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, uploadFolderPath);
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = DateTime.Now.ToString("ddMMyyyyhhmmssfff")+1 + "_" + ImagePath.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    ImagePath.CopyToAsync(stream);
                }

                return uploadFolderPath + uniqueFileName;
            }

            return "Error";
        }
    }

    public static class FileUploadType
    {
        public static readonly string ProductImage = "ProductImages/";
    }
}
