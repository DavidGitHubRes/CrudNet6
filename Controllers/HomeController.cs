using CrudNet6.Datos;
using CrudNet6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CrudNet6.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepositorioClientes _repositorio;
        private readonly IRepositorioGrados _repositorioGrados;
        private readonly ApplicationDBContext ContextoDatos;
        public HomeController(IRepositorioClientes repositorio, IRepositorioGrados repositorioGrados)
        {
            _repositorio = repositorio;
            _repositorioGrados = repositorioGrados;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _repositorio.Index());
        }

        [HttpGet]
        public async Task<ActionResult> Crear()
        {
            ViewBag.lista = await _repositorioGrados.ListadoGrados();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Clientes clientes)
        {
            if (ModelState.IsValid)
            {
                await _repositorio.Crear(clientes);
                return RedirectToAction(nameof(Index));
            }
            return View(clientes);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var cliente = await _repositorio.Buscar(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Clientes clientes)
        {
            if (ModelState.IsValid)
            {
                await _repositorio.Actualizar(clientes);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpPost, ActionName("Borrar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Borrar(Clientes clientes)
        {
            if (clientes == null)
            {
                return View();
            }

            await _repositorio.Borrar(clientes);
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Borrar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var cliente = await _repositorio.Buscar(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<IActionResult> Detalle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var cliente = await _repositorio.Buscar(id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        [HttpPost]
        public IActionResult Detalle()
        {
            return RedirectToAction(nameof(Index));
        }
    }
}