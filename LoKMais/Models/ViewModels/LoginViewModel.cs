using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoKMais.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Número de CPF Inválido"), Display(Name = "CPF")]
        [RegularExpression(@"[0-9]{3}[\.][0-9]{3}[\.][0-9]{3}[-][0-9]{2}", ErrorMessage = "Número de CPF Inválido")]
        public string Login { get; set; }

        [Display(Name ="Senha")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Sua senha deve conter letra maiúscula, mínúscula e 8 digitos, ex.: Senha123")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])[A-Za-z\d@$!%*#?&]{8,}$", ErrorMessage = "Sua senha deve conter letra maiúscula, mínúscula e 8 digitos, ex.: Senha123")]
        public string Senha { get; set; }

        [Display(Name ="Mantenha-me conectado")]
        public string Salvar { get; set; }
        public bool Lembrar 
        { 
            get => Salvar == "on";
            set => Lembrar = Salvar == "on";
        }
    }
}
