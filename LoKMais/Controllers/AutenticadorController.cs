using LoKMais.Models;
using LoKMais.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NToastNotify;
using System.Threading.Tasks;

namespace LoKMais.Controllers
{
    public class AutenticadorController : Controller
    {
        #region Injeções de Dependecias
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
        #endregion

        #region AcessoNegado
        [HttpGet]
        public IActionResult AcessoNegado() => View();
        #endregion

        #region Login
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
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var login = CPF.TirarFormatacao(model.Login);

                if (CPF.Validar(login) == false)
                {
                    _toastNotification.AddErrorToastMessage("Cpf inválido!");
                    return View(model);
                }
                
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
                    return RedirectToAction("Index", "Home");
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
        #endregion

        #region Logout
        [HttpPost]
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
        #endregion

        #region AlterarSenha
        [HttpGet]
        public async Task<IActionResult> AlterarSenha(string cpf)
        {
            var formatoCpf = new CPF(cpf);
            cpf = formatoCpf.Codigo;
            formatoCpf.SemFormatacao();

            var usuario = await _userManager.FindByNameAsync(cpf);
            var token = await _userManager.GeneratePasswordResetTokenAsync(usuario);
            var alterarSenha = new AlteracaoSenhaViewModel
            {
                Cpf = usuario.UserName,
                Token = token
            };
            return View(alterarSenha);
        }

        [HttpPost]
        public async Task<IActionResult> AlterarSenha(AlteracaoSenhaViewModel model, string cpf)
        {
            var formatoCpf = new CPF(cpf);
            cpf = formatoCpf.Codigo;
            var usuario = await _userManager.FindByNameAsync(cpf);

            if (usuario == null)
            {
                _toastNotification.AddErrorToastMessage("CPF invalido, verifique se o CPF informado está correto");
                return RedirectToAction("AlterarSenha");
            }

            if (model.NovaSenha != model.ConfirmarNovaSenha)
            {
                _toastNotification.AddErrorToastMessage("Senhas não conferem");
                return View();
            }

            var resultadoAlteracao =
                await _userManager.ResetPasswordAsync(usuario, model.Token, model.NovaSenha);

            if (resultadoAlteracao.Succeeded)
            {
                _toastNotification.AddSuccessToastMessage("Senha alterada com sucesso!");
                _logger.LogWarning($"Senha alterada com sucesso: Usuario {usuario.UserName}, E-mail {usuario.Email}.");
                if (User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Usuarios");
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }

            _toastNotification.AddErrorToastMessage("Erro na alteração da senha!");
            AddErrors(resultadoAlteracao);
            return View(model);
        }
        #endregion

        #region RecuperarSenha
        [HttpGet]
        public IActionResult RecuperarSenha() => View();

        [HttpPost]
        public async Task<IActionResult> RecuperarSenha(RecuperarSenhaViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _userManager.FindByNameAsync(model.Cpf);
            }
            return RedirectToAction("AlterarSenha", new { cpf = model.Cpf });
        }
        #endregion

        #region  AddErros
        protected void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        #endregion
    }
}
