using LoKMais.Data;
using LoKMais.Data.Extensions;
using LoKMais.Models.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace LoKMais.Models.ViewModels
{
    public class VeiculoViewModel
    {
        [Required(ErrorMessage = "Insira o modelo do veículo.")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "Insira a marca do veículo.")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "Insira o ano do veículo.")]
        public string Ano { get; set; }

        public string Cor { get; set; }

        [Required(ErrorMessage = "Documento é obrigatório!")]
        [FileSize(80 * 1024 * 1024)]
        public IFormFile Foto { get; set; }

        public Veiculo ToModel() => new Veiculo
        {
            Modelo = Modelo,
            Marca = Marca,
            Ano = Ano,
            Cor = Cor,
            Foto = Foto.ConvertToBytes()
        };
    }
}
