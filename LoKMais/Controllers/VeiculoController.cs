 using LoKMais.Data;
using LoKMais.Interfaces;
using LoKMais.Models.Entities;
using LoKMais.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System;
using System.Threading.Tasks;

namespace LoKMais.Controllers
{
    public class VeiculoController : Controller
    {
        #region Dependencias
        private readonly IToastNotification _toastNotification;
        private LkContextDB _contexto;
        private readonly IVeiculoRepository _veiculoRepository;
        public VeiculoController(IToastNotification toastNotification,
            LkContextDB contexto,
            IVeiculoRepository veiculoRepository)
        {
            _toastNotification = toastNotification;
            _contexto = contexto;
            _veiculoRepository = veiculoRepository;
        }
        #endregion

        #region Adicionar Veiculo
        [HttpGet]
        public IActionResult AdicionarVeiculo()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarVeiculo(VeiculoViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Foto.ContentType != "image/png")
                {
                    _toastNotification.AddErrorToastMessage("Arquivos somente em formato PNG");
                    return RedirectToAction("Index", "Home");
                }
                var veiculo = model.ToModel();
                var result = await _contexto.AddAsync(veiculo);
                await result.Context.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("Veiculo cadatrado com sucesso!");
            }
            else
            {
                _toastNotification.AddErrorToastMessage("Erro ao cadastrar veiculo.");
            }
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Editar Veiculo 
        [HttpGet]
        public async Task<IActionResult> Editar(Guid id)
        {
            var veiculo = await _veiculoRepository.BuscarPorIdAsync(id);
            VeiculoViewModel veiculoModel = new VeiculoViewModel
            {
                Modelo = veiculo.Modelo,
                Marca = veiculo.Marca,
                Categoria = veiculo.Categoria,
                Placa = veiculo.Placa,
                Ano = veiculo.Ano,
                TipoCombustivel = veiculo.TipoCombustivel,
                Cor = veiculo.Cor,
                Descricao = veiculo.Descricao
            };
            return View(veiculoModel);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(VeiculoViewModel model)
        {
            var veiculo = await _veiculoRepository.BuscarPorIdAsync(model.Id);

            if (veiculo != null)
            {
                veiculo.Modelo = model.Modelo;
                veiculo.Marca = model.Marca;
                veiculo.Categoria = model.Categoria;
                veiculo.Placa = model.Placa;
                veiculo.Ano = model.Ano;
                veiculo.TipoCombustivel = model.TipoCombustivel;
                veiculo.Cor = model.Cor;
                veiculo.Descricao = model.Descricao;

                await _veiculoRepository.UpdateAsync(veiculo);
                _toastNotification.AddSuccessToastMessage("Alterações salvas!");
                return RedirectToAction("ListaDeVeiculos");
            }
            _toastNotification.AddErrorToastMessage("Veiculo não encontrado!");
            return View(model);
        }
        #endregion

        #region Lista de Veiculos
        [HttpGet]
        public async Task<IActionResult> ListaDeVeiculos()
        {
            var veiculoResult = await _veiculoRepository.BuscarTodosAsync();
            return View(veiculoResult);
        }
        #endregion

        #region Detalhes veiculos

        [HttpGet]
        public async Task<IActionResult> Detalhes(Guid id)
        {
            var veiculoDetalhe = await _veiculoRepository.BuscarPorIdAsync(id);
            return View(veiculoDetalhe);
        }
        #endregion

        #region Deletar Veiculo
        public async Task<IActionResult> Deletar(Guid id)
        {
            var veiculo = await _veiculoRepository.BuscarPorIdAsync(id);

            if (veiculo != null)
            {
                await _veiculoRepository.RemoveAsync(veiculo);
                _toastNotification.AddSuccessToastMessage("Veiculo Deletado com Sucesso!");
            }
            else
            {
                _toastNotification.AddErrorToastMessage("Erro ao deletar");
            }

            return RedirectToAction("ListaDeVeiculos");
        }
        #endregion

        #region Alugar
        [HttpPost]
        public async Task<IActionResult> Alugar(VeiculoViewModel model)
        {
            
            return View();
        }
        #endregion
    }
}
