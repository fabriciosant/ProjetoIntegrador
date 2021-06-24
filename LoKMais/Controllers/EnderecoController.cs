using LoKMais.Data;
using LoKMais.Interfaces;
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
        #region Dependencias
        private readonly IToastNotification _toastNotification;
        private readonly UserManager<Cliente> _userManager;
        private readonly SignInManager<Cliente> _signInManager;
        private readonly IEnderecoRepository _enderecoRepository;
        public EnderecoController(IToastNotification toastNotification,
            UserManager<Cliente> userManager,
            SignInManager<Cliente> signInManager,
            IEnderecoRepository enderecoRepository)
        {
            _signInManager = signInManager;
            _toastNotification = toastNotification;
            _userManager = userManager;
            _enderecoRepository = enderecoRepository;
        }
        #endregion

        #region Criar Endereco
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
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Detalhes Endereço
        [HttpGet]
        public async Task<IActionResult> Detalhe(Guid id)
        {
            var endereco = await _enderecoRepository.BuscarEnderecoPorIdAsync(id);
            return View(endereco);
        }

        #endregion

        #region Editar Endereço
        [HttpGet]
        public async Task<IActionResult> Editar(Guid enderecoId)
        {
            var endereco = await _enderecoRepository.BuscarEnderecoPorIdAsync(enderecoId);
            EnderecoViewModel enderecoModel = new EnderecoViewModel
            {
                Id = endereco.EnderecoId,
                Cep = endereco.Cep,
                Logradouro = endereco.Logradouro,
                Numero = endereco.Numero,
                Bairro = endereco.Bairro,
                Cidade = endereco.Cidade,
                Uf = endereco.Uf,
                Complemento = endereco.Complemento
            };
            return View(enderecoModel);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(EnderecoViewModel model)
        {
            var endereco = await _enderecoRepository.BuscarEnderecoPorIdAsync(model.Id);

            if (endereco != null)
            {
                endereco.Cep = model.Cep;
                endereco.Logradouro = model.Logradouro;
                endereco.Numero = model.Numero;
                endereco.Bairro = model.Bairro;
                endereco.Cidade = model.Cidade;
                endereco.Uf = model.Uf;
                endereco.Complemento = model.Complemento;

                await _enderecoRepository.UpdateAsync(endereco);
                _toastNotification.AddSuccessToastMessage("Alterações salvas!");
                return RedirectToAction("Detalhe", new{id = endereco.EnderecoId });
            }
            _toastNotification.AddErrorToastMessage("Endereço não encontrado!");
            return View(model);
        }
        #endregion

        #region Deletar Endereço
        public async Task<IActionResult> DeletarEndereco(Guid id)
        {
            var endereco = await _enderecoRepository.BuscarEnderecoPorIdAsync(id);
            if (endereco != null)
            {
               await _enderecoRepository.RemoveAsync(endereco);
                _toastNotification.AddSuccessToastMessage("Endereço Deletado com sucesso!");
            }
            else
            {
                _toastNotification.AddErrorToastMessage("Erro ao deletar Endereço");
            }

            return RedirectToAction("ListaDeUsuarios", "Usuario");
        }
        #endregion
    }
}
