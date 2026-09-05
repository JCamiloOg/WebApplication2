using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var usuarios = await  _context.Usuarios
                .Include(u => u.TarjetaBiblioteca)
                .ToListAsync();

            return View(usuarios);
        }

        public IActionResult Create()
        {
            ViewBag.AutorId = new SelectList(_context.Autores, "Id", "Nombre");
            ViewBag.Categorias = new SelectList(_context.Categorias, "Id", "Nombre");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Usuario usuario)
        {
            if(!ModelState.IsValid) return View(usuario);

            if(usuario.TarjetaBiblioteca != null)
            {
                usuario.TarjetaBiblioteca.UsuarioId = usuario.Id;
            } 

            _context.Add(usuario);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var usuario = await _context.Usuarios
                .Include(u => u.TarjetaBiblioteca)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null) return NotFound();

            return View(usuario);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Usuario usuario)
        {
            if (id != usuario.Id) return NotFound();


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _context.Usuarios.AnyAsync(e => e.Id == id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        public async Task<IActionResult> Delete(int id) {
            var usuario = await _context.Usuarios
                .Include(u => u.TarjetaBiblioteca)
                .Include(u => u.Prestamos)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (usuario == null) return NotFound();
            return View(usuario);

        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePrestamo(int id)
        {
            var prestamo = await _context.Prestamos.FindAsync(id);
            _context.Prestamos.Remove(prestamo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
