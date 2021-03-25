using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoKMais.Models.ViewModels
{
    public class EnderecoViewModel
    {
        [Required(ErrorMessage = "Cep invalido")]
        [RegularExpression(@"[0-9]{2}[\.][0-9]{3}[-][0-9]{3}")]
        [Display(Name = "Cpf")]
        public string Cep { get; set; }

        [Required(ErrorMessage = "Digite o logradouro")]
        [Display(Name = "Logradouro")]
        public string Logradouro { get; set; }

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
