using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebApiDownloadUploadArquivos.Models;

namespace WebApiDownloadUploadArquivos.Data
{
    public class FileServerContext : DbContext
    {
        public FileServerContext(DbContextOptions<FileServerContext> options) : base(options) { 
        }
        
        public FileServerContext()
        {
            
        }

        public DbSet <FileModel> FilesModels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");

                optionsBuilder.EnableSensitiveDataLogging();
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}
