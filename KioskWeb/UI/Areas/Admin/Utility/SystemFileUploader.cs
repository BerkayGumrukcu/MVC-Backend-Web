using Microsoft.AspNetCore.Mvc;

namespace UI.Areas.Admin.Utility
{
    public class SystemFileUploader : IFileUploader
    {
        private readonly string _filePath;

        public SystemFileUploader(string filePath)
        {
            this._filePath = filePath;
        }

        public SystemFileUploader()
        {
            this._filePath = "imgs";
        }

        public FileUploadResult Upload(IFormFile file)
        {
            FileUploadResult result = new FileUploadResult();
            result.FileResult = FileResult.Error;
            result.Message = "Dosya yüklenmesi sırasında hata meydana geldi";

            if (file.Length > 0)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

                result.OriginalName = file.FileName;

                var phsycalPath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/{_filePath}", fileName);

                if (File.Exists(phsycalPath))
                {
                    result.Message = "Dizin içerisinde böyle bir dosya mevcuttur!";
                }

                else
                {
                    result.FileUrl = $"/{_filePath}/{fileName}";
                    result.Base64 = null;
                    try
                    {
                        using var stream = new FileStream(phsycalPath, FileMode.Create);
                        file.CopyTo(stream);
                        result.FileResult = FileResult.Succeeded;
                        result.Message = "Dosya başarıyla kaydedildi.";
                    }
                    catch
                    {
                        result.Message = "Dosya kayıt işlemi sırasında hata meydana geldi.";
                        result.FileResult = FileResult.Error;
                    }
                }
            }
            return result;
        }
    }
}
