using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace BMW.Data.Models
{
public class Relatorio
{
    // Propriedades
    public int Id { get; set; } // Identificador único do relatório
    public int Tipo { get; set; } // Tipo do relatório (pode ser um código ou enum para categorias)
    public DateTime DataGeracao { get; set; } // Data de geração do relatório
    public string Conteudo { get; set; } // Conteúdo do relatório
    public string IdFuncionario { get; set; } 

    // Construtor completo
    public Relatorio(int id, int tipo, DateTime dataGeracao, string conteudo, string idFuncionario)
    {
        Id = id;
        Tipo = tipo;
        DataGeracao = dataGeracao;
        Conteudo = conteudo;
        IdFuncionario = idFuncionario;
    }

    // Método para exibir informações do relatório
    public override string ToString()
    {
        return $"Relatório ID: {Id}, Tipo: {Tipo}, Data: {DataGeracao}, Conteúdo: {Conteudo}";
    }
}

}
