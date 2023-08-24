using KUSYS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KUSYS.DataAccess.Configurations
{
    public class UserTokenEntityConfig : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserId);
            builder.HasOne(x => x.User).WithMany(x => x.Tokens).HasForeignKey(x => x.UserId);
            builder.Property(x => x.Token);
            builder.Property(x => x.TokenExpireDate);
            builder.Property(x => x.CreateDate);
        }
    }
}
