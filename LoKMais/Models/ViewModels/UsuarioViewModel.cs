using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoKMais.Models.ViewModels
{
    public class UsuarioViewModel
    {
        [Required(ErrorMessage = "Número de CPF Inválido"), Display(Name = "CPF")]
        [RegularExpression(@"[0-9]{3}[\.][0-9]{3}[-][0-9]{2}", ErrorMessage = "Número de CPF Inválido")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Email inválido"), Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Informe um Email válido")]
        public string Email { get; set; }

        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Sua senha deve conter letra maiúscula, minúscula e 8 digitos, ex.: Senha123")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])[A-Za-z\d@$!%*#?&]{8,}$",
        ErrorMessage = "Sua senha deve conter letra maiúscula, minúscula e 8 digitos, ex.: Senha123")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Confirmar senha inválido")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha", Prompt = "********")]
        [Compare("Senha", ErrorMessage = "Senha nao confere")]
        public string ConfirmarSenha { get; set; }
    }
}
