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

        [Required(ErrorMessage = "Insira o ano do veículo.")]
        [Display(Name = "Ano")]
        public string Ano { get; set; }

        [Display(Name = "Cor")]
        public string Cor { get; set; }

        [Display(Name = "Foto veiculo")]
        public byte[] Foto { get; set; }

        public virtual Cliente Cliente { get; set; }
    }
}
