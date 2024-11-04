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

        public IActionResult Index()
        {
            List<Producto> productos = _productoRepository.GetAll();
            return View(productos);
        }
    }
}