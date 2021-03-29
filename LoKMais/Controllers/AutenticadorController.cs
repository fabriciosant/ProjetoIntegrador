using LoKMais.Models;
using LoKMais.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly UserManager<Cliente> _userManager;
        private readonly SignInManager<Cliente> _signInManager;
        private readonly ILogger<AutenticadorController> _logger;

        public AutenticadorController(IToastNotification toastNotification,
            UserManager<Cliente> userManager,
            SignInManager<Cliente> signInManager,
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
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");

            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var cpf = new CPF(model.Login);
                cpf.SemFormatacao();
                var usuario = await _userManager.FindByNameAsync(cpf.Codigo);

                if (usuario == null)
                {
                    var mensagemUsuario = "Credenciais Inválidas!";
                    _toastNotification.AddErrorToastMessage(mensagemUsuario);
                    ModelState.AddModelError(string.Empty, mensagemUsuario);
                    return View(model);
                }

                var result = await _signInManager.PasswordSignInAsync
                    (
                        userName: cpf.Codigo,
                        password: model.Senha,
                        isPersistent: model.Lembrar,
                        lockoutOnFailure: true
                    );

                if (result.Succeeded)
                {
                    _toastNotification.AddSuccessToastMessage("Bem Vindo de volta!");
                    _logger.LogWarning($"Logando Usuario{usuario.UserName}, Email: {usuario.Email}.");
                    return RedirectToAction("PaginaInicial", "Home");
                }

                if (result.IsLockedOut)
                {
                    var senhaCorreta = await _userManager.CheckPasswordAsync(usuario, model.Senha);

                    if (senhaCorreta)
                    {
                        var mensagemUsuarioBloqueado = "Conta esta bloqueada!";
                        _logger.LogWarning($"Conta bloqueada Usuario: {usuario.UserName}, Mensagem: {mensagemUsuarioBloqueado}.");
                        _toastNotification.AddErrorToastMessage(mensagemUsuarioBloqueado);
                        ModelState.AddModelError(string.Empty, mensagemUsuarioBloqueado);
                    }
                }

                var mensagemErrorUsuario = "Credenciais inválidas!";
                _toastNotification.AddErrorToastMessage(mensagemErrorUsuario);
                ModelState.AddModelError(string.Empty, mensagemErrorUsuario);
            }
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
                _logger.LogWarning($"Logout Usuario: {User.Identity.Name}.");
            }
            ViewData["ReturnUrel"] = returnUrl;
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult CriarUsuario() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CriarUsuario(UsuarioViewModel model)
        {
            var cpf = new CPF(model.Cpf);
            cpf.SemFormatacao();

            var userExist = await _userManager.FindByNameAsync(model.Cpf);
            if (userExist != null)
            {
                _toastNotification.AddErrorToastMessage("Usuário já cadastrado!");
                return View(model);
            }

            var Senha = model.Senha;
            if (Senha != model.ConfirmarSenha)
            {
                _toastNotification.AddErrorToastMessage("Senhas não conferem!");
            }
            else
            {
                var usuario = new Cliente()
                {
                    UserName = cpf.Codigo,
                    Email = model.Email,
                    PhoneNumber = model.Telefone
                };
                await _userManager.CreateAsync(usuario, model.Senha);

                _logger.LogWarning($"Usuatrio criado com sucesso: Usuario{usuario.UserName}, E-mail {usuario.Email}.");
                _toastNotification.AddSuccessToastMessage("Usuário Criado");
                return RedirectToAction("CriarEndereco", "Endereco", new { cpf = model.Cpf });
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Usuarios()
        {
            var listaUsuario = await _userManager.Users.ToListAsync();
            listaUsuario.Remove(listaUsuario.First(p => p.Email == "fabriciosan47@gmail.com"));
            return View(listaUsuario);
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
