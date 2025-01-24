namespace BMW.Data.Models
{
public class FaseMontagem
{
    // Propriedades da Fase de Montagem
    public int Id { get; set; } // Identificador único da fase de montagem
    public string Descricao { get; set; } // Descrição da fase (ex.: "Pintura")
    public int Ordem { get; set; } // Ordem da fase no processo de montagem
    public int TempoDeExecucaoExpectativa { get; set; } // Tempo esperado de execução (em minutos)

    // Construtor padrão
    public FaseMontagem(int id, int ordem, string descricao, int tempoDeExecucaoExpectativa)
    {
        if (string.IsNullOrWhiteSpace(descricao))
        {
            throw new ArgumentException("A descrição não pode ser vazia ou nula.");
        }

        if (ordem <= 0)
        {
            throw new ArgumentException("A ordem deve ser um número positivo.");
        }

        if (tempoDeExecucaoExpectativa <= 0)
        {
            throw new ArgumentException("O tempo de execução esperado deve ser maior que zero.");
        }

        Id = id;
        Descricao = descricao;
        Ordem = ordem;
        TempoDeExecucaoExpectativa = tempoDeExecucaoExpectativa;
    }

    // Método auxiliar para exibir informações da fase
    public override string ToString()
    {
        return $"FaseMontagem ID: {Id}, Descrição: {Descricao}, Ordem: {Ordem}, Tempo Esperado: {TempoDeExecucaoExpectativa} minutos";
    }
}

}
