using WebApplication2.Data;
using WebApplication2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
namespace BibliotecaEFCore.Controllers
{
    public class PrestamosController : Controller
    {
        private readonly ApplicationDbContext _contexto;
        public PrestamosController(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }
        //get: Prestamos
        public async Task<IActionResult> Index()
        {
            var prestamos = await _contexto.Prestamos
                .Include(p => p.Usuario)
                .Include(p => p.Libro)
                .OrderByDescending(p => p.FechaPrestamo)
                .ToListAsync();
            return View(prestamos);
        }
        public IActionResult Create()
        {
            ViewBag.Usuarios = new SelectList(_contexto.Usuarios, "Id", "Nombre");
            ViewBag.Libros = new SelectList(_contexto.Libros, "Id", "Titulo");
            return View();
        }
        //post: prestamos , Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Prestamo prestamo)
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
            }

            if (ModelState.IsValid)
            {

                prestamo.FechaPrestamo = prestamo.FechaPrestamo == default
                    ? DateTime.Now : prestamo.FechaPrestamo;

                _contexto.Add(prestamo);
                await _contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // reponer ViegBag si falla la validación.
            ViewBag.Usuarios = new SelectList(_contexto.Usuarios, "Id", "Nombre");
            ViewBag.Libros = new SelectList(_contexto.Libros, "Id", "Titulo");

            return View(prestamo);
        }
        //get: Préstamos/edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var prestamo = await _contexto.Prestamos.FindAsync(id);
            if (prestamo == null)
            {
                return NotFound();
            }

            ViewBag.Usuarios = new SelectList(_contexto.Usuarios, "Id", "Nombre");
            ViewBag.Libros = new SelectList(_contexto.Libros, "Id", "Titulo");
            return View(prestamo);
        }
        //post: prestamos , Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Prestamo prestamo)
        {
            if (id != prestamo.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _contexto.Update(prestamo);
                await _contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //reponer ViewBag si falla la validación
            ViewBag.Usuarios = new SelectList(_contexto.Usuarios, "Id", "Nombre");
            ViewBag.Libros = new SelectList(_contexto.Libros, "Id", "Titulo");
            return View(prestamo);
        }
        //get: prestamos/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var prestamo = await _contexto.Prestamos
                .Include(p => p.Usuario)
                .Include(p => p.Libro)
                .OrderByDescending(p => p.FechaPrestamo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prestamo == null)
            {
                return NotFound();
            }
            return View(prestamo);
        }
        //post: prestamos , Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prestamo = await _contexto.Prestamos.FindAsync(id);
            if (prestamo != null)
            {

                _contexto.Prestamos.Remove(prestamo);
                await _contexto.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
