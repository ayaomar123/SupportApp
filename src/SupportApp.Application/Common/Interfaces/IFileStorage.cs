using SupportApp.Application.Features.Categories.Dtos;

namespace SupportApp.Application.Common.Interfaces
{
    public interface IFileStorage
    {
        Task<string> UploadAsync(FileUpload file, string folder, CancellationToken ct = default);
    }
}
