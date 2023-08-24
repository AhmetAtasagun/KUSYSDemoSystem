using KUSYS.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace KUSYS.Domain.Entities
{
    public class Course : BaseEntity
    {
        [StringLength(80)]
        public string Name { get; set; }

        public List<Student> Students { get; set; }
    }
}
