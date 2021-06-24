using LoKMais.Models;
using LoKMais.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NToastNotify;
using System.Linq;
using System.Threading.Tasks;

namespace LoKMais.Controllers
{
    public class UsuarioController : Controller
    {
        #region Injeções de Dependecias
        private readonly IToastNotification _toastNotification;
        private readonly UserManager<Cliente> _userManager;
        private readonly SignInManager<Cliente> _signInManager;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(IToastNotification toastNotification,
            UserManager<Cliente> userManager,
            SignInManager<Cliente> signInManager,
            ILogger<UsuarioController> logger)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _toastNotification = toastNotification;
        }
        #endregion

        #region Criar Usuario
        [HttpGet]
        public IActionResult CriarUsuario() => View();

        [HttpPost]
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
                    NomeCompleto = model.NomeCompleto,
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
        #endregion

        #region Editar Usuario
        [HttpGet]
        public async Task<IActionResult> Editar(string email)
        {
            var usuario = await _userManager.FindByEmailAsync(email);
            EditarUsuarioViewModel usuarioModel = new EditarUsuarioViewModel
            {
                NomeCompleto = usuario.NomeCompleto,
                Cpf = usuario.UserName,
                Email = usuario.Email,
                Telefone = usuario.PhoneNumber,
            };
            return View(usuarioModel);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(EditarUsuarioViewModel model)
        {
            if (string.IsNullOrEmpty(model.Cpf))
            {
                _toastNotification.AddErrorToastMessage("CPf Invalido!");
                return View(model);
            }
            var cpf = new CPF(model.Cpf);
            cpf.SemFormatacao();

            var usuario = await _userManager.FindByNameAsync(cpf.Codigo);

            if (usuario != null)
            {
                usuario.UserName = cpf.Codigo;
                usuario.NomeCompleto = model.NomeCompleto;
                usuario.Email = model.Email;
                usuario.PhoneNumber = model.Telefone;

                await _userManager.UpdateAsync(usuario);

                _toastNotification.AddSuccessToastMessage("Alteração Salva!");
                return RedirectToAction("ListaDeUsuarios", new { cpf = model.Cpf });
            }
            _toastNotification.AddErrorToastMessage("Usuario não encotrado!");
            return View(model);
        }
        #endregion

        #region Lista de Usuarios
        [HttpGet]
        public async Task<IActionResult> ListaDeUsuarios()
        {
            var listaUsuario = await _userManager.Users.Where(x => x.Email != "Fabriciosan47@gmail.com").Include(e => e.Endereco).ToListAsync();
            return View(listaUsuario);
        }
        #endregion

        #region Detalhes do Usuario
        [HttpGet]
        public async Task<IActionResult> DetalheUsuario(string email)
        {
            var detalheUsuario = await _userManager.FindByEmailAsync(email);
            return View(detalheUsuario);
        }
        #endregion

        #region DeletarUsuario
        [HttpPost]
        public async Task<IActionResult> DeletarUsuario(string id)
        {
            var usuario = await _userManager.FindByIdAsync(id);

            if (usuario == null)
            {
                _toastNotification.AddErrorToastMessage("Usuário nao encontrado!");
                return RedirectToAction("ListaDeUsuarios");
            }
            else
            {
                var result = await _userManager.DeleteAsync(usuario);
                if (result.Succeeded)
                {
                    _toastNotification.AddSuccessToastMessage("Usuário Excluido com sucesso!");
                    return RedirectToAction("ListaDeUsuarios");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return RedirectToAction("ListaDeUsuarios");
        }
        #endregion

        #region DadosCliente
        public IActionResult MeusDados()
        {
            return View();
        }
        #endregion
    }
}
