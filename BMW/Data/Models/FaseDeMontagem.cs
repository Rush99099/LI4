using System;
using System.ComponentModel.DataAnnotations;

namespace BMW.Data.Models
{
    public class FaseDeMontagem
    {
        [Key]
        [Display(Name = "idEstado de montagem")] // Para garantir consistência com o banco
        public int Id { get; set; } 

        [Required]
        public int Ordem { get; set; }

        [Required, MaxLength(100)]
        public string Descricao { get; set; } = string.Empty;

        [Required]
        public TimeSpan TempoExecucaoExpectavel { get; set; }

        // Construtor padrão
        public FaseDeMontagem() { }

        // Construtor com parâmetros
        public FaseDeMontagem(int id, int ordem, string descricao, TimeSpan tempoExecucaoExpectavel)
        {
            Id = id;
            Ordem = ordem;
            Descricao = descricao;
            TempoExecucaoExpectavel = tempoExecucaoExpectavel;
        }
    }
}
