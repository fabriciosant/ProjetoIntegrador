using LoKMais.Data;
using LoKMais.Interfaces;
using LoKMais.Models;
using LoKMais.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NToastNotify;
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
        private readonly IVeiculoRepository _veiculoRepository;
        private readonly IToastNotification _toastNotification;

        public HomeController(ILogger<HomeController> logger,
            LkContextDB contexto,
            IVeiculoRepository veiculoRepository,
            IToastNotification toastNotification)
        {
            _logger = logger;
            _contexto = contexto;
            _veiculoRepository = veiculoRepository;
            _toastNotification = toastNotification;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var veiculo = await _veiculoRepository.BuscarTodosAsync();
            if (veiculo == null)
            {
                _toastNotification.AddErrorToastMessage("Nenhum veiculo foi encontrado!");
                return RedirectToAction("PaginaInicial", "Home");
            }
            return View(veiculo);
        }
        
        public IActionResult PaginaInicial()
        {
            return View();
        }

        public async Task<IActionResult> AbrirArquivo(Guid veiculoId)
        {
            var veiculo = await _veiculoRepository.BuscarPorIdAsync(veiculoId);

            return File(veiculo.Foto, "image/png");
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
