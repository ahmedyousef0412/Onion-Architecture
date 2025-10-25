

namespace OnionCartDemo.Application.DTOs;

public class FileUploadDto
{
    public string FileName { get; set; } = default!;
    public Stream Content { get; set; } = default!;
}
