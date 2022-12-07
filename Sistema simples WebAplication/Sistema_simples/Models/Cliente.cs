using System.ComponentModel.DataAnnotations;
using static Sistema_simples.Controllers.ValidacaoCustomizada;

namespace Sistema_simples.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Um nome é necessário")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Numero de Telefone é necessário")]
        [Phone(ErrorMessage = "Numero de Telefone Invalido")]
        public string telefone { get; set; }
        [Required(ErrorMessage = "CPF/CNPJ requerido")]
        [ValidarCPf(ErrorMessage = "CPF/CNPJ Invalido")]
        [StringLength(18, MinimumLength = 11, ErrorMessage = "O campo Cpf/CNPJ precisa ter no minimo 11 digitos e no maximo 18 digitos.")]
        public string Cpf { get; set; }

    }
}
