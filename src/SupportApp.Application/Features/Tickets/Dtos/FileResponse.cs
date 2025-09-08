namespace SupportApp.Application.Features.Tickets.Dtos
{
    public sealed class FileResponse
    {
        public required string FileName { get; init; }
        public required string ContentType { get; init; }
        public required byte[] Bytes { get; init; }
    }
}
