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
    public class ProductoController : Controller
    {
        private readonly ILogger<ProductoController> _logger;
        private readonly ProductoRepository _productoRepository;

        public ProductoController(ILogger<ProductoController> logger)
        {
            _logger = logger;
            _productoRepository = new ProductoRepository(@"Data Source=db\Tienda.db;Cache=Shared");
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Producto> productos = _productoRepository.GetAll();
            return View(productos);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Producto());
        }

        [HttpPost]
        public IActionResult Create(Producto producto)
        {
            if (!ModelState.IsValid)
            {
                return View(producto);
            }
            _productoRepository.Create(producto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var producto = _productoRepository.GetById(id);
            return View(producto);
        }

        // Acción POST para actualizar el producto en la base de datos
        [HttpPost]
        public IActionResult Update(int id, Producto producto)
        {
            if (ModelState.IsValid)
            {
                _productoRepository.Update(id, producto);
                return RedirectToAction("Index");
            }
            return View(producto);
        }

        public IActionResult Delete(int id)
        {
            _productoRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}