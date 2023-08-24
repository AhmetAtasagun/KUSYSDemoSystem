using KUSYS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KUSYS.DataAccess.Configurations
{
    public class UserEntityConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Username).HasMaxLength(50);
            builder.Property(x => x.Password).HasMaxLength(200);
            builder.Property(x => x.PasswordSalt).HasMaxLength(200);
            builder.Property(x => x.RoleId);
            builder.HasOne(x => x.Role).WithMany(x => x.Users).HasForeignKey(x => x.RoleId);
            builder.Property(x => x.CreateDate);
        }
    }
}
