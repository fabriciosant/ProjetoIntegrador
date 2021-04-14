﻿using LoKMais.Data;
using LoKMais.Models.Entities;
using LoKMais.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Threading.Tasks;

namespace LoKMais.Controllers
{
    public class VeiculoController : Controller
    {
        private readonly IToastNotification _toastNotification;
        private Contexto _contexto;
        public VeiculoController(IToastNotification toastNotification, Contexto contexto)
        {
            _toastNotification = toastNotification;
            _contexto = contexto;
        }

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
    }
}