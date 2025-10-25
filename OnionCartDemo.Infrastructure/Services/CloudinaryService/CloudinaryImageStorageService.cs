
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using OnionCartDemo.Application.DTOs;
using OnionCartDemo.Application.Interfaces;
using OnionCartDemo.Infrastructure.Configurations;

namespace OnionCartDemo.Infrastructure.Services.CloudinaryService;

internal class CloudinaryImageStorageService : IImageStorageService
{
    private readonly Cloudinary _cloudinary;
    public CloudinaryImageStorageService(IOptions<CloudinarySettings> options)
    {
        var settings = options.Value;

        var account = new Account(settings.CloudName, settings.ApiKey, settings.ApiSecret);

        _cloudinary = new Cloudinary(account);
    }
    public async Task<string> UploadAsync(FileUploadDto file, CancellationToken cancellationToken = default)
    {
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, file.Content),
            Folder = "Products"
        };


        var result = await _cloudinary.UploadAsync(uploadParams, cancellationToken);

        if (result.StatusCode != System.Net.HttpStatusCode.OK)
            throw new Exception($"Cloudinary upload failed: {result.Error?.Message}");

        return result.SecureUrl.ToString();
    }
}
