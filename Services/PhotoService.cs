using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using JournalistTierAPI.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace JournalistTierAPI.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;
        public PhotoService(IOptions<CloudinarySettings> config)
        {
            var acc = new Account(config.Value.CloudName,
            config.Value.ApiKey,
            config.Value.ApiSecret);
            _cloudinary = new Cloudinary(acc);
        }
        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var imgUpdResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var imgUpdParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.FileName, stream),
                        Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                    };
                    imgUpdResult = await _cloudinary.UploadAsync(imgUpdParams);
                }
            }
            return imgUpdResult;
        }
        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deletionParams = new DeletionParams(publicId);
            var deletionResult = await _cloudinary.DestroyAsync(deletionParams);
            return deletionResult;
        }
    }
}

