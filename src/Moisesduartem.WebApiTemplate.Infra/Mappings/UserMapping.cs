using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moisesduartem.WebApiTemplate.Domain.V1.Aggregates.Users.Entities;

namespace Moisesduartem.WebApiTemplate.Infra.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasColumnType("VARCHAR")
                   .HasMaxLength(50);

            builder.Property(x => x.Email)
                   .IsRequired()
                   .HasColumnType("VARCHAR")
                   .HasMaxLength(50);

            builder.Property(x => x.Username)
                   .IsRequired()
                   .HasColumnType("VARCHAR")
                   .HasMaxLength(25);

            builder.Property(x => x.PasswordHash)
                   .IsRequired()
                   .HasColumnType("CHAR")
                   .HasMaxLength(72);
            
            builder.Property(x => x.Role)
                   .IsRequired()
                   .HasColumnType("TINYINT");

            builder.Property(x => x.CreateDate)
                   .IsRequired()
                   .HasColumnType("SMALLDATETIME");

            builder.Property(x => x.UpdateDate)
                   .IsRequired()
                   .HasColumnType("SMALLDATETIME");

            builder.HasIndex(x => x.Email, "IX_User_Email").IsUnique();
        }
    }
}
