
using OnionCartDemo.Application.DTOs;

namespace OnionCartDemo.Application.Interfaces;



public interface IImageStorageService
{
    Task<string> UploadAsync(FileUploadDto file, CancellationToken cancellationToken = default);
}
