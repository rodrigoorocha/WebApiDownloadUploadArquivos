namespace WebApiDownloadUploadArquivos.Models
{
    public class FileModel
    {
        

        public int Id { get; private set; } 
        public string Path { get; private set; }
        public string Nome { get; private set; }
        public DateTime UploadDate { get; private set; }

        public FileModel( string path, string nome, DateTime uploadDate)
        {
            Path = path;
            Nome = nome;
            UploadDate = uploadDate;
        }

    }
}
