using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.TotalStudents = await _context.Students.CountAsync();
        ViewBag.TotalCourses = await _context.Courses.CountAsync();
        ViewBag.TotalEnrollments = await _context.Students
                                    .SelectMany(s => s.Courses)
                                    .CountAsync();

        return View();
    }
}