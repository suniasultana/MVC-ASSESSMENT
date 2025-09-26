using StudentManagement.Models;

public class Student
{
    public int StudentId { get; set; }
    public string Name { get; set; }

    // Navigation property for many-to-many
    public ICollection<Course> Courses { get; set; } = new List<Course>();
}