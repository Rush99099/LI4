using System;
using System.Collections.Generic;
using BMW.Data.Models;

namespace BMW.Data.Data
{
    public interface IBMWFacade
    {
        // 1. Autenticação e Gestão de Utilizadores
        void Registar(string email, string password, string nome, bool isCliente);
        Utilizador? LogIn(string email, string password);

        // 2. Gestão de Encomendas
        ICollection<Encomenda> GetListaEncomendas(int clienteId);
        void RegistarEncomenda(int clienteId, int veiculoId, string observacoes);

        // 3. Gestão do Catálogo de Veículos
        ICollection<Veiculo> GetCatalogo();
        void AddVeiculo(string modelo, decimal precoBase, DateTime dataAdicao);
        void DeleteVeiculo(int veiculoId);

        // 4. Gestão de Funcionários
        ICollection<Funcionario> GetFuncionarios();
        void AddFuncionario(string nome, string email, DateTime dataContratacao, int posicaoId);
        void DeleteFuncionario(int funcionarioId);

        // 5. Gestão de Progresso
        void RegistarProgresso(int encomendaId, int faseId, DateTime dataInicio, DateTime? dataFim);

        // 6. Gestão de Alertas
        ICollection<Alerta> GetAlertas();
        void RegistarAlerta(int encomendaId, int funcionarioId, string mensagem, DateTime data);

        // 7. Gestão de Relatórios
        void CriarRelatorio(int tipo, string conteudo, int funcionarioId);
        Relatorio? DownloadRelatorio(int relatorioId);
    }
}
