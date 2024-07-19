using System.Diagnostics.Contracts;
using Biblioteca.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Biblioteca.Controllers
{
    [Authorize]
    public class LectorController : Controller
    {
        
        private readonly ContextoDeDatos contexto;

        // inyecciòn del contexto de datos.
        public LectorController(ContextoDeDatos contexto)
        {
            this.contexto = contexto;
        }
        // Acciòn que muestra la pàgina principal para administrar  lectores.
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 4; // nùmero de resgistros por pagina
            int pageNumber = (page ?? 1); // nùmero de pàgina actual, predeterminado 1 si entra vacio.

            var lectores = await contexto.Lectores.OrderBy(d => d.Id).ToPagedListAsync(pageNumber, pageSize);
            return View(lectores);
        }

        // acción que muestra el detalle de un registro 

        public async Task<IActionResult> Details(int id)
        {
            var lector = await contexto.Lectores.SingleOrDefaultAsync(d => d.Id == id);
            return View(lector);
        }

        // acción que muestra el formulario para crear un nuevo lector

        public IActionResult Create()
        {
            return View();
        }
        // acción que recibe los datos del formulario para guardarlos en la base de datos

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Lector lector)
        {
            if (ModelState.IsValid)
            {
                contexto.Lectores.Add(lector);
                await contexto.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
                return View(lector);
        }
        // acciòn que muestra el formulario con los datos del lector para modificar

        public async Task<IActionResult> Edit(int id)
        {
            var lector = await contexto.Lectores.SingleOrDefaultAsync(d => d.Id == id);
            return View(lector);
        }

        // acción que recibe los datos modificados y los envia a la bd

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Lector lector)
        {
            if (ModelState.IsValid)
            {
                var temp = await contexto.Lectores.SingleOrDefaultAsync(d => d.Id == lector.Id);
                temp.Nombre = lector.Nombre;
                temp.Apellido = lector.Apellido;
                temp.Email = lector.Email;
                temp.Telefono = lector.Telefono;

                contexto.Lectores.Update(temp);
                await contexto.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
                return View(lector);
        }

        // acción que muestra los datos del lector para confirmar la eliminación
        public async Task<IActionResult> Delete(int id)
        {
            var lector = await contexto.Lectores.SingleOrDefaultAsync(d => d.Id == id);
            return View(lector);
        }

        // acción que recibe la confirmación para eliminar el lector

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete(int id, Lector lector)
        {
            var temp = await contexto.Lectores.SingleOrDefaultAsync(d => d.Id == id);
            if (temp != null)
            {
                contexto.Lectores.Remove(temp);
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
