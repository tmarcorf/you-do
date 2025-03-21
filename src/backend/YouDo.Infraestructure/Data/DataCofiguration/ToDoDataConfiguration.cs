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
            builder.Property(x => x.Id).HasMaxLength(40);
            builder.Property(x => x.Title).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Details).HasMaxLength(500);
            builder.Property(x => x.Completed).HasDefaultValue(false);
            builder.HasOne(x => x.User).WithMany(x => x.ToDos);
        }
    }
}
