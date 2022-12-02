using System.ComponentModel.DataAnnotations;
using static Sistema_simples.Controllers.ValidacaoCustomizada;

namespace Sistema_simples.Models
{
    public class Produto
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Um nome é necessário")]
        public string Nome { get; set; }
        public string? Observacao { get; set; }
        [Required(ErrorMessage = "Uma quantidade é necessária")]
        [ValidarQuantidade(ErrorMessage = "A quantidade não pode ser menor ou igual a zero")]
        public int Quantidade { get; set; }
    }

}
