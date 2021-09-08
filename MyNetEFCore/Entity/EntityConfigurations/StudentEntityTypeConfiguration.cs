
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyNetEFCore
{
    class StudentEntityTypeConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(p => p.Id);
            builder.ToTable("Student");
    
            builder.Property(p => p.Name).HasMaxLength(30);
           
        }
    }
}
