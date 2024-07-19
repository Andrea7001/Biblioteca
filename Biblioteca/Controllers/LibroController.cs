using Biblioteca.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Biblioteca.Controllers
{
    [Authorize]
    public class LibroController : Controller
    {
        private readonly ContextoDeDatos contexto;

        // inyecciòn del contexto de datos.
        public LibroController(ContextoDeDatos contexto)
        {
            this.contexto = contexto;
        }
        // Acciòn que muestra la pàgina principal para administrar departamentos.
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 10; // nùmero de resgistros por pagina
            int pageNumber = (page ?? 1); // nùmero de pàgina actual, predeterminado 1 si entra vacio.

            var libros = await contexto.Libros.OrderBy(d => d.Id).ToPagedListAsync(pageNumber, pageSize);
            return View(libros);
        }

        // acción que muestra el detalle de un registro 

        public async Task<IActionResult> Details(int id)
        {
            var libro = await contexto.Libros.SingleOrDefaultAsync(d => d.Id == id);
            return View(libro);
        }

        // acción que muestra el formulario para crear un nuevo apartamento

        public IActionResult Create()
        {
            return View();
        }
        // acción que recibe los datos del formulario para guardarlos en la base de datos

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Libro libro)
        {
            if (ModelState.IsValid)
            {
                contexto.Libros.Add(libro);
                await contexto.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
                return View(libro);
        }
        // acciòn que muestra el formulario con los datos del departamento para modificar

        public async Task<IActionResult> Edit(int id)
        {
            var libro = await contexto.Libros.SingleOrDefaultAsync(d => d.Id == id);
            return View(libro);
        }

        // acción que recibe los datos modificados y los envia a la bd

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Libro libro)
        {
            if (ModelState.IsValid)
            {
                var temp = await contexto.Libros.SingleOrDefaultAsync(d => d.Id == libro.Id);
                temp.Nombre = libro.Nombre;

                contexto.Libros.Update(temp);
                await contexto.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
                return View(libro);
        }

        // acción que muestra los datos del departamento para confirmar la eliminación
        public async Task<IActionResult> Delete(int id)
        {
            var libro = await contexto.Libros.SingleOrDefaultAsync(d => d.Id == id);
            return View(libro);
        }

        // acción que recibe la confirmación para eliminar el departamento

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete(int id, Libro libro)
        {
            var temp = await contexto.Libros.SingleOrDefaultAsync(d => d.Id == id);
            if (temp != null)
            {
                contexto.Libros.Remove(temp);
                await contexto.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
                return View(temp);

        }
    }
}
