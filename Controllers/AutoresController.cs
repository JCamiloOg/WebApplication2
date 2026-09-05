using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class AutoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AutoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. LISTAR (GET: Productos)
        public async Task<IActionResult> Index()
        {
            return View(await _context.Autores.ToListAsync());
        }


        // 3. CREAR - VISTA (GET: Productos/Create)
        public IActionResult Create()
        {
            return View();
        }

        // 3. CREAR - GUARDAR (POST: Productos/Create)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Autor autor)
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

                return View(autor);
            }

            _context.Autores.Add(autor);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // 4. EDITAR - VISTA (GET: Productos/Edit/5)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var autor = await _context.Autores.FindAsync(id);
            if (autor == null) return NotFound();

            return View(autor);
        }

        // 4. EDITAR - GUARDAR (POST: Productos/Edit/5)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Autor autor)
        {
            if (id != autor.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(autor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Autores.Any(e => e.Id == autor.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(autor);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = await _context.Autores
                .Include(a => a.Libros)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (autor == null)
            {
                return NotFound();
            }

            return View(autor);
        }

        // 5. ELIMINAR - VISTA CONFIRMACIÓN (GET: Productos/Delete/5)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = await _context.Autores
                .Include(a => a.Libros)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (autor == null)
            {
                return NotFound();
            }

            return View(autor);
        }

        // 5. ELIMINAR - ACCIÓN (POST: Productos/Delete/5)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var autor = await _context.Autores
                    .Include(a => a.Libros)
                    .FirstOrDefaultAsync(a => a.Id == id);

            if (autor == null)
            {
                return NotFound();
            }

            if (autor.Libros.Any())
            {
                TempData["Error"] =
                    "No puedes eliminar un autor que tiene libros registrados.";

                return RedirectToAction(nameof(Index));
            }

            _context.Autores.Remove(autor);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
