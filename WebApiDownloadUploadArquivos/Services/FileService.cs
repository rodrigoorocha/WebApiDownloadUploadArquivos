using Microsoft.EntityFrameworkCore;
using System;
using WebApiDownloadUploadArquivos.Data;
using WebApiDownloadUploadArquivos.DTO;
using WebApiDownloadUploadArquivos.Models;
using WebApiDownloadUploadArquivos.Services.Interfaces;

namespace WebApiDownloadUploadArquivos.Services
{
    public class FileService : IFileService
    {

        private readonly FileServerContext _context;

        public FileService(FileServerContext context)
        {
            _context = context;
        }

        
        //public string PathRoot { get; set; } = "C:\\Users\\Rodrigo\\source\\repos\\WebApiDownloadUploadArquivos\\WebApiDownloadUploadArquivos\\Files";
        public string PathRoot { get; set; } = "C:\\fileRoot";


        public async Task<GenericResult> UploadFileAsync(IFormFile file, UploadDto uploadDto)
        {
            try{

                if (!Directory.Exists(PathRoot))
                    Directory.CreateDirectory(PathRoot);


                if (file == null || file.Length == 0)
                    return new GenericResult(false, "file not exist or empety", null);

                
                if (file.Length > 10 * 1024 * 1024)
                    return new GenericResult(false, "file size exceeds the 10MB limit", null);

                if ((Path.GetExtension(file.FileName) == ".exe") ||
                    (Path.GetExtension(file.FileName) == ".bat"))
                    return new GenericResult(false, "extesion denied", null);

                var dirPath = Path.Combine(PathRoot, uploadDto.FolderDesteny);

                if (!Directory.Exists(dirPath))
                    Directory.CreateDirectory(dirPath);

                var pathComplete = Path.Combine(dirPath, file.FileName);


                if (File.Exists(pathComplete))
                    return new GenericResult(false, "file already exists", null);

                using (var stream = new FileStream(pathComplete, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var fileModel = new FileModel(dirPath, file.FileName, DateTime.Now);
                _context.FilesModels.Add(fileModel);
                await _context.SaveChangesAsync();

                return new GenericResult(true, "file created", null);

            }
            catch(Exception ex)
            {
                return new GenericResult(false, ex.Message , ex);
            }
          
        }

        public async Task<GenericResult> DownloadFileAsync(string fileName)
        {

            try {
               var fileModel = await _context.FilesModels
                    .Where(x => x.Nome == fileName)
                    .FirstOrDefaultAsync();
                if (fileModel == null)
                    return new GenericResult(false, "file name not exist", null);

                var pathComplete = Path.Combine(fileModel.Path, fileModel.Nome);

                // Verifica se o arquivo existe no caminho especificado
                if (!File.Exists(pathComplete))
                    return new GenericResult(false, "file not found on disk", null);

                // Lê o conteúdo do arquivo
                var fileBytes = await File.ReadAllBytesAsync(pathComplete);


                return new GenericResult(true, "file downloaded successfully", fileBytes);
            }
            catch (Exception ex)
            {
                return new GenericResult(false, ex.Message, ex);
            }
           
        }

    }
}
