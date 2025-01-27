using System;

namespace BMW.Data.Models
{
    public class Progresso
    {
        // Propriedades
        public int IdEncomenda { get; set; } // Identificador único da encomenda
        public int IdFase { get; set; } // Identificador único da fase
        public DateTime StartFase { get; set; } // Data de início da fase
        public DateTime? EndFase { get; set; } // Data do final da fase (pode ser nula)
        public int? IdFuncionario { get; set; } // Identificador do funcionário (pode ser nulo)
        public string Observacao { get; set; } = string.Empty; // Observação adicional (caso necessário)

        // Construtor completo
        public Progresso(int idEncomenda, int idFase, DateTime startFase, DateTime? endFase, string observacao, int? idFuncionario = null)
        {
            IdEncomenda = idEncomenda;
            IdFase = idFase;
            StartFase = startFase;
            EndFase = endFase;
            Observacao = observacao;
            IdFuncionario = idFuncionario;
        }

        // Construtor reduzido 
        public Progresso(int idEncomenda, int idFase, DateTime startFase, DateTime? endFase, int? idFuncionario = null)
        {
            IdEncomenda = idEncomenda;
            IdFase = idFase;
            StartFase = startFase;
            EndFase = endFase;
            IdFuncionario = idFuncionario;
        }
    }
}
