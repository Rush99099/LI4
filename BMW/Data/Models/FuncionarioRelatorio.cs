using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMW.Data.Models
{
    public class FuncionarioRelatorio
    {
        [Key, Column(Order = 0)]
        public int FuncionarioId { get; set; }

        [Key, Column(Order = 1)]
        public int RelatorioId { get; set; }
    }
}
