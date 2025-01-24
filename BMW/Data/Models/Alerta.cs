namespace BMW.Data.Models
{
    public class Alerta
    {  
        public int IdAlerta { get; set; } // ID do alerta
        public int IdProgresso { get; set; }
        public string Mensagem { get; set; }
        public DateTime Data { get; set; }
        // Construtor completo
        public Alerta(int idAlerta, int idProgresso, string mensagem, DateTime data)
        {
            IdAlerta = idAlerta;
            IdProgresso = idProgresso;
            Mensagem = mensagem;
            Data = data;
        }
    }
}
