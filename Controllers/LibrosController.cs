using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class LibrosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LibrosController(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var libros = await _context.Libros
                .Include(l => l.Autor)
                .ToListAsync();

            return View(libros);
        }

        public IActionResult Create()
        {
            ViewData["AutorId"] = new SelectList(
                _context.Autores,
                "Id",
                "Nombre"
            );

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Libro libro)
        {
            if (!ModelState.IsValid)
            {
                foreach (var item in ModelState)
                {
                    Console.WriteLine($"Campo: {item.Key}");

                    foreach (var error in item.Value.Errors)
                    {
                        Console.WriteLine($"Error: {error.ErrorMessage}");
                    }
                }

                ViewData["AutorId"] = new SelectList(
                    _context.Autores,
                    "Id",
                    "Nombre",
                    libro.AutorId
                );

                return View(libro);
            }

            _context.Libros.Add(libro);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros.FindAsync(id);

            if (libro == null)
            {
                return NotFound();
            }

            ViewData["AutorId"] = new SelectList(
                _context.Autores,
                "Id",
                "Nombre",
                libro.AutorId
            );

            return View(libro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Libro libro)
        {
            if (id != libro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(libro);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Libros.Any(e => e.Id == id))
                    {
                        return NotFound();
                    }

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["AutorId"] = new SelectList(
                _context.Autores,
                "Id",
                "Nombre",
                libro.AutorId
            );

            return View(libro);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
                .Include(l => l.Autor)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var libro = await _context.Libros.FindAsync(id);

            if (libro == null)
            {
                return NotFound();
            }

            _context.Libros.Remove(libro);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
