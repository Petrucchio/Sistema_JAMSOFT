using System.ComponentModel.DataAnnotations;

namespace Sistema_simples.Models
{
    public class Produto
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Um nome é necessário")]
        public string Nome { get; set; }
        public string? Observacao { get; set; }
    }

}
