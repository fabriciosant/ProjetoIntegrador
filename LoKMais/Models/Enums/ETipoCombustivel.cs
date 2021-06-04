using System.ComponentModel.DataAnnotations;

namespace LoKMais.Models.Enums
{
    public enum ETipoCombustivel
    {
        [Display(Name = "Alcool")] Alcool,
        [Display(Name = "Diesel")] Diesel,
        [Display(Name = "Flex")] Flex,
        [Display(Name = "Gasolina")] Gasolina
    }
}
