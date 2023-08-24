using KUSYS.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace KUSYS.Domain.Entities
{
    public class Student : BaseEntity
    {
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }


        public DateTime BirthDate { get; set; }
        public List<Course> Courses { get; set; }
    }
}
