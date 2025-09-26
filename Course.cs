public class Course
{
    public int CourseId { get; set; }
    public string Title { get; set; }

    // Navigation property for many-to-many
    public ICollection<Student> Students { get; set; } = new List<Student>();
}