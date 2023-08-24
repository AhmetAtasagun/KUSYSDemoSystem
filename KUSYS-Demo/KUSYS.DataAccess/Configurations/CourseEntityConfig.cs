using KUSYS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KUSYS.DataAccess.Configurations
{
    internal class CourseEntityConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name);//.HasMaxLength(80); => çalışmadığından DataAnotation Attribute ile yapıldı.
            builder.Property(x => x.CreateDate);
        }
    }
}
