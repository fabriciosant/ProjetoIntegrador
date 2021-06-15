﻿using LoKMais.Data;
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

         
        [HttpGet]
        public IActionResult Editar(Guid id)
        {
            return View();
        }
        
        [HttpGet]
        public async Task<IActionResult> Detalhes()
        {
            var veiculoResult = await _veiculoRepository.BuscarTodosAsync();
            return View(veiculoResult);
        }

        public async Task<IActionResult> Deletar(Guid id)
        {
            var veiculo = await _veiculoRepository.BuscarPorIdAsync(id);

            if (veiculo != null)
            {
                _toastNotification.AddErrorToastMessage("Erro ao deletar");
            }
            else
            {
                await _veiculoRepository.RemoveAsync(veiculo);
                _toastNotification.AddSuccessToastMessage("Veiculo Deletado com Sucesso!");
            }

            return View();
        }
    }
}
