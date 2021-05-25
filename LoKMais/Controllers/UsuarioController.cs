﻿using LoKMais.Models;
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
        public IActionResult Editar(string email)
        {
            var usuario = _userManager.FindByEmailAsync(email);
            EditarUsuarioViewModel usuarioModel = new EditarUsuarioViewModel
            {
                NomeCompleto = usuario.Result.NomeCompleto,
                Cpf = usuario.Result.UserName,
                Email = usuario.Result.Email,
                Telefone = usuario.Result.PhoneNumber,
            };
            return View(usuarioModel);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(EditarUsuarioViewModel model, IUserSecurityStampStore<Cliente> userSecurityStampStore)
        {
            var cpf = new CPF(model.Cpf);
            cpf.SemFormatacao();

            var usuario = await _userManager.FindByNameAsync(cpf.Codigo);
            var token = await _userManager.GeneratePasswordResetTokenAsync(usuario);

            if (usuario != null)
            {
                var cliente = new Cliente()
                {
                    UserName = cpf.Codigo,
                    NomeCompleto = model.NomeCompleto,
                    Email = model.Email,
                    PhoneNumber = model.Telefone
                };
                await _userManager.UpdateSecurityStampAsync
                    (cliente);

                //var resultadoAlteracao =
                ////await _userManager.ResetPasswordAsync(usuario, token);

                _logger.LogWarning($"Usuário alterado com sucesso: Usuario{usuario.UserName}, E-mail {usuario.Email}.");
                _toastNotification.AddSuccessToastMessage("Alteração Salva!");
                return RedirectToAction("ListaDeUsuarios", "Administracao", new { cpf = model.Cpf });
            }
            return View(model);
        }
        #endregion

        #region Lista de Usuarios
        [HttpGet]
        public async Task<IActionResult> ListaDeUsuarios()
        {
            var listaUsuario = await _userManager.Users.Where(x => x.Email != "Fabriciosan47@gmail.com").ToListAsync();
            return View(listaUsuario);
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
    }
}
