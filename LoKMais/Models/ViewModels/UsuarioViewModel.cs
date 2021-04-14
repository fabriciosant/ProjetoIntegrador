using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoKMais.Models.ViewModels
{
    public class UsuarioViewModel : Cliente
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
        [RegularExpression(@"^([1-9]{2})[0-9]{4,5}-[0-9]{4}$", ErrorMessage ="Telefone inválido")]
        [Display(Name = "Telefone")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Sua senha deve conter letra maiúscula, minúscula e 8 digitos, ex.: Senha123")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])[A-Za-z\d@$!%*#?&]{8,}$",
        ErrorMessage = "Sua senha deve conter letra maiúscula, minúscula e 8 digitos, ex.: Senha123")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Confirmar senha inválido")]
        [DataType(DataType.Password)]
        [Compare("Senha", ErrorMessage = "Senha nao confere")]
        [Display(Name = "Confirmar Senha", Prompt = "********")]
        public string ConfirmarSenha { get; set; }
    }
}
