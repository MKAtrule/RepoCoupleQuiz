namespace RepoCoupleQuiz.Common.File.Interface
{
    public interface IFileUpload
    {
        Task<string> UploadImageAsync(IFormFile file);

    }
}
