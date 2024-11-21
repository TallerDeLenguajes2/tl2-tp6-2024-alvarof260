using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TP6MVC.Models;
using TP6MVC.Repositories;
using TP6MVC.ViewModels;


namespace tl2_tp6_2024_alvarof260.Controllers
{
    public class PresupuestoController : Controller
    {
        private readonly ILogger<PresupuestoController> _logger;
        private readonly PresupuestoRepository _presupuestoRepository;
        private readonly ProductoRepository _productoRepository;
        private readonly ClienteRepository _clienteRepository;

        public PresupuestoController(ILogger<PresupuestoController> logger)
        {
            _logger = logger;
            _presupuestoRepository = new PresupuestoRepository(@"Data Source=db\Tienda.db;Cache=Shared");
            _productoRepository = new ProductoRepository(@"Data Source=db\Tienda.db;Cache=Shared");
            _clienteRepository = new ClienteRepository(@"Data Source=db\Tienda.db;Cache=Shared");
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
            var viewModel = new CreatePresupuestoViewModel();
            viewModel.Clientes = _clienteRepository.GetAll();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(CreatePresupuestoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Lógica para crear el presupuesto con el cliente seleccionado
                var cliente = _clienteRepository.GetById(viewModel.IdCliente);
                var presupuesto = new Presupuesto(0, cliente);
                _presupuestoRepository.Create(presupuesto);

                return RedirectToAction("Index");
            }

            // Volver a cargar los clientes si hay un error
            viewModel.Clientes = _clienteRepository.GetAll();
            return View(viewModel);
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
            UpdatePresupuestoViewModel viewModel = new UpdatePresupuestoViewModel();
            viewModel.Presupuesto = presupuesto;
            viewModel.Productos = productos;
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Update(UpdatePresupuestoViewModel viewModel)
        {
            var producto = _productoRepository.GetById(viewModel.ProductoSeleccionado.ProductoId);

            if (viewModel.Presupuesto == null || producto == null)
            {
                return NotFound();
            }

            // Agregar producto al presupuesto
            _presupuestoRepository.Update(viewModel.Presupuesto.IdPresupuesto, producto, viewModel.ProductoSeleccionado.Cantidad);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _presupuestoRepository.Delete(id);
            return RedirectToAction("Index");
        }

    }

}