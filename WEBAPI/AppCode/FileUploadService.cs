
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Azure.Core;
using System.Net.Http.Headers;
using Domain.Entities;

namespace Infrastucture.Services
{
    public class FileUploadService
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;

        public FileUploadService(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor=httpContextAccessor;
        }
        public Response UploadImage(IFormFile ImagePath, string uploadFolderPath)
        {
            var res = new Response() { ResponseText = "Error in Ulpoad Image !" ,StatusCode = ResponseStatus.Failed };
            if (ImagePath != null && ImagePath.Length > 0)
            {
                var uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, uploadFolderPath);
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
				var filename = ContentDispositionHeaderValue.Parse(ImagePath.ContentDisposition).FileName.Trim('"');
				string originalExt = Path.GetExtension(filename).ToLower();
				string[] Extensions = { ".png", ".jpeg", ".jpg", ".webp", ".pdf" };
				if (!Extensions.Contains(originalExt))
				{
					res.ResponseText = "You can only upload JPEG, JPG, and PNG files.";
					return res;
				}

				var uniqueFileName = DateTime.Now.ToString("ddMMyyyyhhmmssfff")+1 + "_" + originalExt;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    ImagePath.CopyToAsync(stream);
                }
                var request = httpContextAccessor.HttpContext.Request;
                var baseUrl = $"{request.Scheme}://{request.Host}/";

                res.ResponseText = baseUrl + uploadFolderPath + uniqueFileName;
                res.StatusCode = ResponseStatus.Success;
				return res;
            }

            return res;
        }
    }

    public static class FileUploadPath
    {
        public static readonly string ProductImage = "ProductImages/";
        public static readonly string CategoryImage = "CategoryImages/";
        public static readonly string ProductVarient = "ProductVarientImages/";
    }
}
