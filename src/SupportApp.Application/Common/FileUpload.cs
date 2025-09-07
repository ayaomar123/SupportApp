namespace SupportApp.Application.Common
{
    public class FileUpload
    {
        public required Stream Content { get; init; }
        public required string FileName { get; init; }
        public required string ContentType { get; init; }
        public long Length { get; init; }
    }
}
