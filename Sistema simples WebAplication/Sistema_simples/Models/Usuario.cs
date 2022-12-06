using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Sistema_simples.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "É necessário informar um Usuário")]
        public string User { get; set; }
        [Required(ErrorMessage = "Um nome é necessário")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "É necessário informar uma Senha")]
        [StringLength(18, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$", ErrorMessage = "É necessário que tenha um número, letra maiúscula e letra minúscula")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
