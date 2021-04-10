using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoKMais.Models.ViewModels
{
    public class AlteracaoSenhaViewModel
    {
        [Required(ErrorMessage = "Número de CPF Inválido")]
        [RegularExpression(@"[0-9]{3}[\.][0-9]{3}[\.][0-9]{3}[-][0-9]{2}", ErrorMessage = "Número de CPF Inválido")]
        [Display(Name = "CPF")]
        public string Cpf { get; set; }

        public string Token { get; set; }

        [Required(ErrorMessage = "Nova senha inválido")]
        [DataType(DataType.Password)]
        [Display(Name = "Nova senha")]
        public string NovaSenha { get; set; }

        [Required(ErrorMessage = "Confirmar nova senha inválido")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha", Prompt = "*******")]
        [Compare("NovaSenha", ErrorMessage = "Senha não está igual")]
        public string ConfirmarNovaSenha { get; set; }
    }
}
