using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoKMais.Models.ViewModels
{
    public class EditarUsuarioViewModel : Cliente
    {
        [Required(ErrorMessage = "Digite seu nome completo")]
        [Display(Name = "Nome Completo")]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "Número de CPF Inválido")]
        [RegularExpression(@"[0-9]{3}[\.][0-9]{3}[\.][0-9]{3}[-][0-9]{2}", ErrorMessage = "Número de CPF Inválido")]
        [Display(Name = "CPF")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Email inválido")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Informe um Email válido")]
        [Display(Name = "E-mail")]
        public override string Email { get; set; }

        [Required(ErrorMessage = "Telefone deve conter 9 digitos, ex.:(91234-1234) ")]
        [RegularExpression(@"^([1-9]{2})[0-9]{4,5}-[0-9]{4}$", ErrorMessage = "Telefone inválido")]
        [Display(Name = "Telefone")]
        public string Telefone { get; set; }
    }
}
