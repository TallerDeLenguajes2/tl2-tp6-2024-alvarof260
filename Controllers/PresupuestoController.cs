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

       public PresupuestoController(ILogger<PresupuestoController> logger)
        {
            _logger = logger;
            _presupuestoRepository = new PresupuestoRepository(@"Data Source=db\Tienda.db;Cache=Shared");
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Presupuesto> presupuestos = _presupuestoRepository.GetAll();
            return View(presupuestos);
        }




    }

}