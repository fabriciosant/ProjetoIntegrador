using LoKMais.Data;
using LoKMais.Data.Extensions;
using LoKMais.Models.Entities;
using LoKMais.Models.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace LoKMais.Models.ViewModels
{
    public class VeiculoViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Insira o modelo do veículo.")]
        [Display(Name = "Modelo")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "Insira a marca do veículo.")]
        [Display(Name = "Marca")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "Insira a marca do veículo.")]
        [Display(Name = "Categoria")]
        public ECategoria Categoria { get; set; }

        [Required(ErrorMessage = "Insira a marca do veículo.")]
        [Display(Name = "Placa")]
        public string Placa { get; set; }

        [Required(ErrorMessage = "Insira o ano do veículo.")]
        [Display(Name = "Ano")]
        public EAno Ano { get; set; }

        [Required(ErrorMessage = "Insira a marca do veículo.")]
        [Display(Name = "Tipo Combustivel")]
        public ETipoCombustivel TipoCombustivel { get; set; }


        [Required(ErrorMessage = "Insira a cor do veículo.")]
        [Display(Name = "Cor")]
        public ECor Cor { get; set; }
        
        [Required(ErrorMessage = "Documento é obrigatório!")]
        [FileSize(80 * 1024 * 1024)]
        [Display(Name = "Foto veiculo")]
        public IFormFile Foto { get; set; }

        [Required(ErrorMessage = "Insira a descrição do veículo.")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Insira a data inicial")]
        [Display(Name = "DataInico")]
        public DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "Insira a data Final")]
        [Display(Name = "DataFinal")]
        public DateTime DataFinal { get; set; }

        [Required(ErrorMessage = "Insira o valor da diaria")]
        [Display(Name = "Valor")]
        public decimal ValorDiaria { get; set; }


        public Veiculo ToModel() => new Veiculo
        {
            Modelo = Modelo,
            Marca = Marca,
            Categoria = Categoria,
            Placa = Placa,
            Ano = Ano,
            TipoCombustivel = TipoCombustivel,
            Cor = Cor,
            Foto = Foto.ConvertToBytes(),
            Descricao = Descricao
        };

        public VeiculoViewModel ToViewModel(Veiculo veiculo) => new VeiculoViewModel
        {
            Modelo = veiculo.Modelo,
            Marca = veiculo.Marca,
            Categoria = veiculo.Categoria,
            Placa = veiculo.Placa,
            Ano = veiculo.Ano,
            TipoCombustivel = veiculo.TipoCombustivel,
            Cor = veiculo.Cor,
            Foto = veiculo.ConverterByteToIFormFile(),
            Descricao =veiculo.Descricao
        };
    }
}
