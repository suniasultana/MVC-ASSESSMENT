using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class CoursesController : Controller
{
    private readonly ApplicationDbContext _context;

    public CoursesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Courses
    public async Task<IActionResult> Index()
    {
        return View(await _context.Courses.ToListAsync());
    }

    // GET: Courses/Create
    public IActionResult Create() => View();

    // POST: Courses/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Course course)
    {
        if (ModelState.IsValid)
        {
            _context.Add(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(course);
    }

    // GET: Courses/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var course = await _context.Courses.FindAsync(id);
        if (course == null) return NotFound();

        return View(course);
    }

    // POST: Courses/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Course course)
    {
        if (id != course.CourseId) return NotFound();

        if (ModelState.IsValid)
        {
            _context.Update(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(course);
    }

    // GET: Courses/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var course = await _context.Courses.FirstOrDefaultAsync(m => m.CourseId == id);
        if (course == null) return NotFound();

        return View(course);
    }

    // POST: Courses/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}