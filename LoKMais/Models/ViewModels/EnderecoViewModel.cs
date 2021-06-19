using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoKMais.Models.ViewModels
{
    public class EnderecoViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Número de CPF Inválido")]
        [RegularExpression(@"[0-9]{3}[\.][0-9]{3}[\.][0-9]{3}[-][0-9]{2}", ErrorMessage = "Número de CPF Inválido")]
        [Display(Name = "CPF")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Cep invalido")]
        [RegularExpression(@"/^[0-9]{8}$/")]
        [Display(Name = "Cep")]
        public string Cep { get; set; }

        [Display(Name = "Logradouro")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "Digite o número!")]
        [Display(Name = "Número")]
        public string Numero { get; set; }

        [Display(Name = "Uf")]
        public string Uf { get; set; }

        [Display(Name = "Bairro")]
        public string Bairro { get; set; }

        [Display(Name = "Cidade")]
        public string Cidade { get; set; }

        [Display(Name = "Complemento")]
        public string Complemento { get; set; }
    }
}
