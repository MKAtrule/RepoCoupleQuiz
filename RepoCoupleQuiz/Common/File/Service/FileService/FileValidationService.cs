using RepoCoupleQuiz.Common.File.Interface;

namespace RepoCoupleQuiz.Common.File.Service.FileService
{
    public class FileValidationService : IFileValidation
    {
        private readonly string[] _allowedExtensions = { ".png", ".jpg", ".jpeg", ".svg", ".jfif", ".gif", ".webp", ".bmp", ".tiff" };
        public bool IsValidImageFile(string fileName)
        {
            var extension = Path.GetExtension(fileName);
            return _allowedExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase);
        }
    }

}
