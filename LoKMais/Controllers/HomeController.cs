using LoKMais.Data;
using LoKMais.Models;
using LoKMais.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LoKMais.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LkContextDB _contexto;
        public HomeController(ILogger<HomeController> logger,
            LkContextDB contexto)
        {
            _logger = logger;
            _contexto = contexto;
        }

        [HttpGet]
        public async Task<IActionResult> Index(Guid id)
        {

            return View();
        }
        
        public IActionResult PaginaInicial()
        {
            return View();
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
    }
}
