using LoKMais.Models;
using LoKMais.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoKMais.Controllers
{
    public class AutenticadorController : Controller
    {
        private readonly IToastNotification _toastNotification;
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly ILogger<AutenticadorController> _logger;

        public AutenticadorController(IToastNotification toastNotification,
            UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager,
            ILogger<AutenticadorController> logger)
        {
            _logger = logger;
            _signInManager = signInManager;
            _toastNotification = toastNotification;
            _userManager = userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AcessoNegado() => View();

        [HttpGet]
        public IActionResult CriarUsuario() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CriarUsuario(UsuarioViewModel model)
        {
            var cpf = new CPF(model.Cpf);
            cpf.SemFormatacao();
            if (!ModelState.IsValid || !CPF.Validar(cpf.Codigo))
            {
                return View(model);
            }
            var userEmail = await _userManager.FindByEmailAsync(model.Email);
            if (userEmail != null) return View(model);

            var usuario = new Usuario(cpf.Codigo)
            {
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(usuario, model.Senha);
            var resultRole = await _userManager.AddToRoleAsync(usuario, "LokMais");
            if (!result.Succeeded)
            {
                AddErrors(result);
                if (!resultRole.Succeeded)
                {
                    AddErrors(resultRole);
                }
                return View(model);
            }
            _logger.LogWarning($"Usuatrio criado com sucesso: Usuario{usuario.UserName}, E-mail {usuario.Email}.");
            _toastNotification.AddSuccessToastMessage("Usuário Criado");
            return RedirectToAction("Usuarios");
        }
        [HttpGet]
        public IActionResult Usuarios()
        {
            return View();
        }

        protected void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
