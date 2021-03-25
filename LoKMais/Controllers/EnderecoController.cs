using LoKMais.Models;
using LoKMais.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoKMais.Controllers
{
    public class EnderecoController : Controller
    {
        private readonly IToastNotification _toastNotification;
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;

        public EnderecoController(IToastNotification toastNotification,
            UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager)
        {
            _signInManager = signInManager;
            _toastNotification = toastNotification;
            _userManager = userManager;
        }
        public IActionResult Endereco()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Endereco(EnderecoViewModel model)
        {
            var endereco = new Endereco()
            {
                Cep = model.Cep,
                Logradouro = model.Logradouro,
                Numero = model.Numero,
                Uf = model.Uf,
                Bairro = model.Bairro,
                Cidade = model.Cidade,
                Complemento = model.Complemento
            };

            return View();
        }
    }
}
