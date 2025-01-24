namespace BMW.Data.Models
{
    public class Funcionario
    {
        public int IdFuncionario { get; set; }
        public DateTime ContractDate { get; set; }
        public int Posicao { get; set; }
        public int? Supervisor { get; set; }

        public Funcionario(int idFuncionario, DateTime contractDate, int posicao, int? supervisor) {
            IdFuncionario = idFuncionario;
            ContractDate = contractDate;
            Posicao = posicao;
            Supervisor = supervisor;
        }
    }
}
