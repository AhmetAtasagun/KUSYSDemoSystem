namespace KUSYS.Core.Models.Response
{
    public class CourseStudentsResponse
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public IEnumerable<StudentResponse> Students { get; set; }
    }
}
