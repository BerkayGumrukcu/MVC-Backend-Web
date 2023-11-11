using Microsoft.AspNetCore.Mvc;

namespace UI.Areas.Admin.Utility
{
    public enum FileResult
    {
        Succeeded = 1,
        Error = 2
    }

    public interface IFileUploader
    {
        public FileUploadResult Upload(IFormFile file);
    }

    public class FileUploadResult
    {
        public string FileUrl { get; set; }
        public string OriginalName { get; set; }
        public string Base64 { get; set; }
        public string Message { get; set; }
        public FileResult FileResult { get; set; }
    }
}
