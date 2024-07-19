using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Biblioteca.Controllers
{
    public class PrestamoController : Controller
    {

        private readonly ContextoDeDatos contexto;
        public PrestamoController(ContextoDeDatos context)
        {
            contexto = context;
        }
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 4; // nùmero de resgistros por pagina
            int pageNumber = (page ?? 1); // nùmero de pàgina actual, predeterminado 1 si entra vacio.

            var prestamos = await contexto.Prestamos.OrderBy(d => d.Id).
            Include(e => e.Libro).Include(e => e.Lector).
            ToPagedListAsync(pageNumber, pageSize);
            return View(prestamos);
        }
        public async Task<IActionResult> Create()
        {
            var lectores = await contexto.Lectores.ToListAsync();
            var libros = await contexto.Libros.ToListAsync();
            ViewBag.Lectores = lectores;
            ViewBag.Libros = libros;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Prestamo prestamo)
        {
            if (ModelState.IsValid)
            {
                contexto.Prestamos.Add(prestamo);
                await contexto.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
                return View(prestamo);
        }
        public async Task<IActionResult> Details(int id)
        {
            var prestamo = await contexto.Prestamos.Include(b => b.Libro).Include(b => b.Lector).
                SingleOrDefaultAsync(d => d.Id == id);
            return View(prestamo);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var prestamo = await contexto.Prestamos.SingleOrDefaultAsync(d => d.Id == id);
            var lectores = await contexto.Lectores.ToListAsync();
            var libros = await contexto.Libros.ToListAsync();
            ViewBag.Lectores = lectores;
            ViewBag.Libros = libros;
            return View(prestamo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Prestamo prestamo, int id)
        {
            if (ModelState.IsValid)
            {
                var temp = await contexto.Prestamos.SingleOrDefaultAsync(d => d.Id == id);
                temp.DiaDevolucion  = prestamo.DiaDevolucion;
                temp.LibroId = prestamo.LibroId;
                temp.LectorId = prestamo.LectorId;
                temp.DiaPrestamo = prestamo.DiaPrestamo;
                temp.Categoria = prestamo.Categoria;

                contexto.Prestamos.Update(temp);
                await contexto.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
                return View(prestamo);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var prestamo = await contexto.Prestamos.SingleOrDefaultAsync(d => d.Id == id);
            return View(prestamo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete(int id, Prestamo prestamo)
        {
            var temp = await contexto.Prestamos.SingleOrDefaultAsync(d => d.Id == id);
            if (temp != null)
            {
                contexto.Prestamos.Remove(temp);
                await contexto.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return View(temp);
            }


        }

    }
}
