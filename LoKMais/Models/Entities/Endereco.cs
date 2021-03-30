using LoKMais.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoKMais.Models
{
    public class Endereco
    {
        public Guid EnderecoId { get; set; }

        [Required(ErrorMessage = "O campo \"{0}\" é de preenchimento obrigatório.")]
        [RegularExpression(@"[0-9]{8}$", ErrorMessage = "O campo \"{0}\" deve ser preenchido!")]
        public string Cep { get; set; }

        [Required(ErrorMessage = "O campo \"{0}\" é de preenchimento obrigatório.")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "O campo \"{0}\" é de preenchimento obrigatório.")]
        public string Numero { get; set; }

        public string Complemento { get; set; }

        [Required(ErrorMessage = "O campo \"{0}\" é de preenchimento obrigatório.")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "O campo \"{0}\" é de preenchimento obrigatório.")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "O campo \"{0}\" é de preenchimento obrigatório.")]
        public string Uf { get; set; }

        public Guid ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}
