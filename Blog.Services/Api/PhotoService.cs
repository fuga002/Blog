using Blog.Common.Models;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace Blog.Services.Api;

public class PhotoService
{
    private readonly ICloudinary _cloudinary;

    public PhotoService()
    {
        var account = new Account()
        {
            Cloud = CloudinaryOption.CloudName,
            ApiKey = CloudinaryOption.ApiKey,
            ApiSecret = CloudinaryOption.SecretKey
        };
        _cloudinary = new Cloudinary(account);
    }


    public async Task<ImageUploadResult> AddPhoto(IFormFile file)
    {
        var imageUpload = new ImageUploadResult();

        if (file.Length > 0)
        {
            await using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face"),
                Folder = "da-net7u"
            };
            imageUpload = await _cloudinary.UploadAsync(uploadParams);
        }

        return imageUpload;
    }

    public async Task<DeletionResult> DeletePhoto(string publicId)
    {
        var deletionResult = new DeletionResult();

        var delParams = new DeletionParams(publicId);

        deletionResult = await _cloudinary.DestroyAsync(delParams);

        return deletionResult;
    }
}