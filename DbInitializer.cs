using System.Linq;
using StudentManagement.Models;

namespace StudentManagement.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // If there are any courses, assume DB is seeded.
            if (context.Courses.Any()) return;

            var courses = new Course[]
            {
                new Course { Title = "Mathematics" },
                new Course { Title = "English" },
                new Course { Title = "Physics" },
                new Course { Title = "Chemistry" },
                new Course { Title = "Computer Science" }
            };

            context.Courses.AddRange(courses);
            context.SaveChanges();
        }
    }
}