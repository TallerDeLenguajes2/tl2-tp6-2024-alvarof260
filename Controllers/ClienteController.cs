using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP6MVC.Models;
using TP6MVC.Repositories;

namespace TP6MVC.Controllers
{
    public class ClienteController : Controller
    {

        private readonly ILogger<ClienteController> _logger;
        private readonly ClienteRepository _clienteRepository;

        public ClienteController(ILogger<ClienteController> logger)
        {
            _logger = logger;
            _clienteRepository = new ClienteRepository(@"Data Source=db\Tienda.db;Cache=Shared");
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Cliente> clientes = _clienteRepository.GetAll();
            return View(clientes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Cliente());
        }

        [HttpPost]
        public IActionResult Create(Cliente cliente)
        {
            _clienteRepository.Create(cliente);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _clienteRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}