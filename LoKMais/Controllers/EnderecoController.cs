using LoKMais.Data;
using LoKMais.Models;
using LoKMais.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System;
using System.Threading.Tasks;

namespace LoKMais.Controllers
{
    public class EnderecoController : Controller
    {
        private readonly IToastNotification _toastNotification;
        private readonly UserManager<Cliente> _userManager;
        private readonly SignInManager<Cliente> _signInManager;
        private Contexto _contexto;
        public EnderecoController(IToastNotification toastNotification,
            UserManager<Cliente> userManager,
            SignInManager<Cliente> signInManager,
            Contexto contexto)
        {
            _signInManager = signInManager;
            _toastNotification = toastNotification;
            _userManager = userManager;
            _contexto = contexto;
        }
        public IActionResult CriarEndereco(string cpf)
        {
            ViewBag.cpf = cpf;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CriarEndereco(EnderecoViewModel model, UsuarioViewModel usuariomodel)
        {
            var cpf = new CPF(usuariomodel.Cpf);
            cpf.SemFormatacao();

            var usuario = await _userManager.FindByNameAsync(cpf.Codigo);
            if (usuario != null)
            {
                var endereco = new Endereco()
                {
                    ClienteId = usuario.Id,
                    Cep = model.Cep,
                    Logradouro = model.Logradouro,
                    Numero = model.Numero,
                    Uf = model.Uf,
                    Bairro = model.Bairro,
                    Cidade = model.Cidade,
                    Complemento = model.Complemento
                };

                usuario.Endereco = endereco;
                var result = await _userManager.UpdateAsync(usuario);

                if (!result.Succeeded)
                {
                    _toastNotification.AddAlertToastMessage("Endereço não foi cadastrado!");
                    return View(model);
                }
            }

            _toastNotification.AddSuccessToastMessage("Endereco Cadastrado!");
            return RedirectToAction("ListaDeUsuario", "Usuario");
        }

    }
}
