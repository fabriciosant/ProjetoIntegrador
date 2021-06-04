using LoKMais.Models.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace LoKMais.Models.Entities
{
    public class Veiculo
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Insira o modelo do veículo.")]
        [Display(Name = "Modelo")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "Insira a marca do veículo.")]
        [Display(Name = "Marca")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "Insira a categoria do veículo.")]
        [Display(Name = "Categoria")]
        public ECategoria Categoria { get; set; }
        
        [Required(ErrorMessage = "Insira a placa do veículo.")]
        [RegularExpression(@"[A-Z]{4}[-][0-9]{4}")]
        [Display(Name = "Placa")]
        public string Placa { get; set; }

        [Required(ErrorMessage = "Insira o ano do veículo.")]
        [Display(Name = "Ano")]
        public EAno Ano { get; set; }

        [Required(ErrorMessage = "Insira o Tipo de combustivel do veículo.")]
        [Display(Name = "Tipo Combustivel")]
        public ETipoCombustivel TipoCombustivel { get; set; }

        [Required]
        [Display(Name = "Cor")]
        public ECor Cor { get; set; }

        [Required]
        [Display(Name = "Foto veiculo")]
        public byte[] Foto { get; set; }

        [Required]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
    }
}
