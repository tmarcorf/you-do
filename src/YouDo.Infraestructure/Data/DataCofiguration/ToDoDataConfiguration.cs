using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouDo.Core.Entities;

namespace YouDo.Infraestructure.Data.DataCofiguration
{
    public class ToDoDataConfiguration : IEntityTypeConfiguration<ToDo>
    {
        public void Configure(EntityTypeBuilder<ToDo> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("ID").HasMaxLength(40);
            builder.Property(x => x.Title).HasColumnName("TITLE").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Details).HasColumnName("DETAILS").HasMaxLength(500);
            builder.Property(x => x.Completed).HasColumnName("COMPLETED").HasDefaultValue(false);
            builder.Property(x => x.UserId).HasColumnName("USERID").HasMaxLength(40).IsRequired();
        }
    }
}
