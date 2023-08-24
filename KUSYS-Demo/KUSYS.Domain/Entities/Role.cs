using KUSYS.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace KUSYS.Domain.Entities
{
    public class Role : BaseEntity
    {
        [StringLength(50)]
        public string Name { get; set; }

        public List<User> Users { get; set; }
    }
}
