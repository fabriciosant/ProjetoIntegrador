using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoKMais.Models.ViewModels
{
    public class AlteracaoSenhaViewModel
    {
        public string Cpf { get; set; }

        public string Token { get; set; }

        [Required(ErrorMessage = "Nova senha inválido")]
        [DataType(DataType.Password)]
        [Display(Name = "Nova senha")]
        public string NovaSenha { get; set; }

        [Required(ErrorMessage = "Confirmar nova senha inválido")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        [Compare("NovaSenha", ErrorMessage = "Senhas não conferem")]
        public string ConfirmarNovaSenha { get; set; }
    }
}
