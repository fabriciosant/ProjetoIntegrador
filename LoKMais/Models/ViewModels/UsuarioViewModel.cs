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
        [RegularExpression(@"[0-9]{3}[\.][0-9]{3}[\.][0-9]{3}[-][0-9]{2}", ErrorMessage = "Número de CPF Inválido")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Email inválido"), Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Informe um Email válido")]
        public string Email { get; set; }

        [Display(Name = "Telefone")]
        [RegularExpression(@"^([1-9]{2})[0-9]{4,5}-[0-9]{4}$", ErrorMessage ="Telefone inválido")]
        [Required(ErrorMessage = "Telefone deve conter 9 digitos, ex.:(91234-1234) ")]
        public string Telefone { get; set; }

        [Display(Name = "CEP")]
        [RegularExpression(@"^[0-9]{5}[-][0-9]{3}$", ErrorMessage = "CEP inválido!")]
        [Required(ErrorMessage = "CEP inválido")]
        public string Cep { get; set; }

        [Display(Name = "Logradouro")]
        [Required(ErrorMessage = "É necessário inserir o logradouro")]
        [RegularExpression(@"[A-Za-z]")]
        public string Logradouro { get; set; }

        [Display(Name = "Número")]
        [Required(ErrorMessage = "É necessário inserir o numero.")]
        [RegularExpression(@"[0-9][-][A-Za-z]{1}")]

        public string Numero { get; set; }

        [Display(Name ="Complemento")]
        [RegularExpression(@"[A-Za-z]")]
        public string Complemento { get; set; }

        [Display(Name =("Unidade Federativa"))]
        public string Uf { get; set; }

        [Display(Name = "Bairro")]
        [RegularExpression(@"[A-Za-z]")]
        [Required(ErrorMessage = "É necessário inserir o bairro.")]
        public string Bairro { get; set; }

        [Display(Name = "Bairro")]
        [RegularExpression(@"[A-Za-z]")]
        [Required(ErrorMessage = "É necessário inserir a cidade.")]
        public string Cidade { get; set; }
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
