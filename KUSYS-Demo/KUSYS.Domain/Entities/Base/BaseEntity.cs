using KUSYS.Core.Abstracts;

namespace KUSYS.Domain.Entities.Base
{
    public class BaseEntity : IEntity
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    }
}
