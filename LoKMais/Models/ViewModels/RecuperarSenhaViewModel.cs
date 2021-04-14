using System.ComponentModel.DataAnnotations;

namespace LoKMais.Models.ViewModels
{
    public class RecuperarSenhaViewModel
    {
        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display(Name = "Cpf")]
        public string Cpf { get; set; }
    }
}
