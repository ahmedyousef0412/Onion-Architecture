using FluentValidation;
using OnionCartDemo.Application.DTOs;
namespace OnionCartDemo.Application.Validation;

public class FileUploadDtoValidator:AbstractValidator<FileUploadDto>
{
    public FileUploadDtoValidator()
    {
        const long maxSize = 2 * 1024 * 1024; // 2 MB
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

        RuleFor(file => file)
            .NotNull()
            .WithMessage("Image file is required.");

        RuleFor(file => file.Content)
            .NotNull()
            .NotEmpty()
            .WithMessage("Image file is required.");

        RuleFor(file => file.Content.Length)
            .LessThanOrEqualTo(maxSize)
            .WithMessage("Image size cannot exceed 2 MB.");

        RuleFor(file => file.FileName)
            .Must(fileName => allowedExtensions.Contains(Path.GetExtension(fileName).ToLowerInvariant()))
            .WithMessage("Only JPG and PNG formats are allowed.");
    }
}
