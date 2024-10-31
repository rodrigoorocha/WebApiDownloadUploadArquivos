using Microsoft.AspNetCore.Mvc;
using WebApiDownloadUploadArquivos.DTO;

namespace WebApiDownloadUploadArquivos.Services.Interfaces
{
    public interface IFileService
    {

        Task <GenericResult> UploadFileAsync(IFormFile file, UploadDto uploadDto);
        Task <GenericResult> DownloadFileAsync(string fileName);
    }
}
