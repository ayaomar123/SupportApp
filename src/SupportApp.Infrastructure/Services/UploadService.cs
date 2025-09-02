using System.Globalization;
using SupportApp.Application.Common.Interfaces;
using SupportApp.Application.Features.Categories.Dtos;

namespace SupportApp.Infrastructure.Services
{
    public sealed class UploadService : IFileStorage
    {
        private readonly string _root;

        public UploadService(string root)
        {
            _root = root;
        }

        public async Task<string> UploadAsync(FileUpload file, string folder, CancellationToken ct)
        {
            var folderPath = Path.Combine(_root, folder);
            Directory.CreateDirectory(folderPath);

            var safeName = Path.GetFileNameWithoutExtension(file.FileName);
            var ext = Path.GetExtension(file.FileName);
            var unique = $"{safeName}_{DateTime.UtcNow.Ticks.ToString(CultureInfo.InvariantCulture)}{ext}";

            var fullPath = Path.Combine(folderPath, unique);
            using (var fs = new FileStream(fullPath, FileMode.CreateNew, FileAccess.Write, FileShare.None, 81920, useAsync: true))
            {
                await file.Content.CopyToAsync(fs, ct);
            }

            return Path.Combine(folder, unique).Replace('\\', '/');
        }
    }
}
