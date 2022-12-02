using System.ComponentModel.DataAnnotations;
using static Sistema_simples.Controllers.ValidacaoCustomizada;

namespace Sistema_simples.Models
{
    public class Emprestimo
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "É necessário, ter um cliente")]
        public int Usuario { get; set; }
        public string Cliente { get; set; }
        [Required(ErrorMessage = "É necessário, ter um Produto")]
        public int ProdutoId { get; set; }
        [Required(ErrorMessage = "É necessário, ter uma Quantidade")]
        [ValidarQuantidade(ErrorMessage = "A quantidade não pode ser menor ou igual a zero")]
        public int quantidade { get; set; }
        public int ultimaquantidade { get; set; }
        public DateTime data{ get; set; }
        [Required(ErrorMessage = "É necessário, ter uma data de devolução")]
        [ValidarDevolucao(ErrorMessage = "A data de devolução não pode ser menor que a data atual")]
        public DateTime data_devolucao{ get; set; }
        public string status { get; set; }
        public string? informacoes_adicionais { get; set; }
    }
}
