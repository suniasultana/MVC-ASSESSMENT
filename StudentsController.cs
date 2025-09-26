using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class StudentsController : Controller
{
    private readonly ApplicationDbContext _context;

    public StudentsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Students
    public async Task<IActionResult> Index()
    {
        return View(await _context.Students.Include(s => s.Courses).ToListAsync());
    }

    // GET: Students/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var student = await _context.Students
            .Include(s => s.Courses)
            .FirstOrDefaultAsync(m => m.StudentId == id);

        if (student == null) return NotFound();

        return View(student);
    }

    // GET: Students/Create
    public IActionResult Create()
    {
        ViewBag.Courses = _context.Courses.ToList();
        return View();
    }

    // POST: Students/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Student student, int[] selectedCourses)
    {
        if (ModelState.IsValid)
        {
            foreach (var courseId in selectedCourses)
            {
                var course = await _context.Courses.FindAsync(courseId);
                if (course != null)
                    student.Courses.Add(course);
            }

            _context.Add(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(student);
    }

    // GET: Students/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var student = await _context.Students
            .Include(s => s.Courses)
            .FirstOrDefaultAsync(s => s.StudentId == id);

        if (student == null) return NotFound();

        ViewBag.Courses = _context.Courses.ToList();
        return View(student);
    }

    // POST: Students/Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Student student, int[] selectedCourses)
    {
        if (id != student.StudentId) return NotFound();

        if (ModelState.IsValid)
        {
            var existingStudent = await _context.Students
                .Include(s => s.Courses)
                .FirstOrDefaultAsync(s => s.StudentId == id);

            if (existingStudent == null) return NotFound();

            existingStudent.Name = student.Name;
            existingStudent.Courses.Clear();

            foreach (var courseId in selectedCourses)
            {
                var course = await _context.Courses.FindAsync(courseId);
                if (course != null)
                    existingStudent.Courses.Add(course);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(student);
    }

    // GET: Students/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var student = await _context.Students.FirstOrDefaultAsync(m => m.StudentId == id);
        if (student == null) return NotFound();

        return View(student);
    }

    // POST: Students/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var student = await _context.Students.FindAsync(id);
        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}