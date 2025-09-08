using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace OnlineBoutiqueCoreLayer.Services.File
{
    public class FileService : IFileService
    {
        private readonly string _imagePath;

        public FileService(IConfiguration configuration)
        {
            _imagePath = configuration["FileStorage:ImagePath"];
        }

        public async Task<string> SaveFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            if (!Directory.Exists(_imagePath))
                Directory.CreateDirectory(_imagePath);

            var extension = Path.GetExtension(file.FileName);
            var uniqueName = $"{Guid.NewGuid()}{extension}";
            var fullPath = Path.Combine(_imagePath, uniqueName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return uniqueName;
        }

        public bool DeleteFile(string imageName)
        {
            var fullPath = Path.Combine(_imagePath, imageName);
            //var fullPath = Path.Combine(Directory.GetCurrentDirectory(), imageName.TrimStart('/').Replace("/", "\\"));
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
                return true;
            }
            return false;
        }
    }


}
