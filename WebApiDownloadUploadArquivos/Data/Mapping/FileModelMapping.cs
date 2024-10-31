using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiDownloadUploadArquivos.Models;

namespace WebApiDownloadUploadArquivos.Data.Mapping
{
    public class FileModelMapping : IEntityTypeConfiguration<FileModel>
    {
        public void Configure(EntityTypeBuilder<FileModel> builder)
        {
            builder.ToTable(nameof(FileModel));

            // Configuração da chave primária
            builder.HasKey(f => f.Id);
           

            // Configuração do campo Id
            builder.Property(f => f.Id)
                .ValueGeneratedOnAdd() 
                .IsRequired();

            // Configuração do campo Path
            builder.Property(f => f.Path)
                .IsRequired() 
                .HasMaxLength(255); 

            // Configuração do campo Name
            builder.Property(f => f.Nome)
                .IsRequired() 
                .HasMaxLength(100); 

            // Configuração do campo UploadDate
            builder.Property(f => f.UploadDate)
                .IsRequired(); 
        }
    }
}
