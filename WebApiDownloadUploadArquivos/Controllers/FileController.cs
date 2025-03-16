using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using WebApiDownloadUploadArquivos.DTO;
using WebApiDownloadUploadArquivos.Models;
using WebApiDownloadUploadArquivos.Services.Interfaces;

namespace WebApiDownloadUploadArquivos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        // Método de Upload
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file, [FromForm] UploadDto uploadDto )
        {
            var result = await _fileService.UploadFileAsync(file, uploadDto);
            return Ok(result);
        }

        // Método de Download
        [HttpGet("download/{fileName}")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            var result = await _fileService.DownloadFileAsync(fileName);

            // Verifica se o resultado foi bem-sucedido
            if (!result.Success)
            {
                return NotFound(result.Message); // Ou outro tratamento de erro apropriado
            }

            // Converte result.Data para byte[]
            byte[] fileBytes = result.Data as byte[]; // Tenta converter para byte[]

            // Verifica se a conversão foi bem-sucedida
            if (fileBytes == null)
            {
                return StatusCode(500, "Unable to convert file data to byte array.");
            }

            return File(fileBytes, "application/octet-stream", fileName);
        }

        private string GetContentType(string path)
        {
            var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(path, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }
}