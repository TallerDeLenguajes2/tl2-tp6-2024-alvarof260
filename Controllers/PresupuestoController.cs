using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TP6MVC.Models;
using TP6MVC.Repositories;


namespace tl2_tp6_2024_alvarof260.Controllers
{
    public class PresupuestoController : Controller
    {
        private readonly ILogger<PresupuestoController> _logger;
        private readonly PresupuestoRepository _presupuestoRepository;
        private readonly ProductoRepository _productoRepository;

        public PresupuestoController(ILogger<PresupuestoController> logger)
        {
            _logger = logger;
            _presupuestoRepository = new PresupuestoRepository(@"Data Source=db\Tienda.db;Cache=Shared");
            _productoRepository = new ProductoRepository(@"Data Source=db\Tienda.db;Cache=Shared");
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Presupuesto> presupuestos = _presupuestoRepository.GetAll();
            return View(presupuestos);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View(new Presupuesto());
        }

        [HttpPost]
        public IActionResult Create(Presupuesto presupuesto)
        {
            _presupuestoRepository.Create(presupuesto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var presupuesto = _presupuestoRepository.GetById(id);
            if (presupuesto == null)
            {
                return NotFound("No se encontro el presupuesto.");
            }
            List<Producto> productos = _productoRepository.GetAll();
            ViewData["Productos"] = productos;
            return View(presupuesto);
        }

        [HttpPost]
        public IActionResult Update(int id, int productoId, int cantidad)
        {
            var presupuesto = _presupuestoRepository.GetById(id);
            var producto = _productoRepository.GetById(productoId);

            if (presupuesto == null || producto == null)
            {
                return NotFound();
            }

            // Agregar producto al presupuesto
            _presupuestoRepository.Update(id, producto, cantidad);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _presupuestoRepository.Delete(id);
            return RedirectToAction("Index");
        }

    }

}