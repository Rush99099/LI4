using BMW.Data.Models;
using System;
using System.Collections.Generic;

namespace BMW.Data.Data
{
    public interface IBMWFacade
    {
        // 1. Autenticação e Gestão de Utilizadores
        void Registar(string email, string password, string nome, bool isCliente);
        Utilizador? LogIn(string email, string password);
        List<Utilizador> GetClientes();
        Utilizador GetUtilizador(int id);

        // 2. Gestão de Encomendas
        Estado GetEstadoById(int estadoId);
        Encomenda? GetEncomendaById(int id);
        ICollection<Encomenda> GetListaEncomendas(int clienteId);
        ICollection<Encomenda> GetAllEncomendas();
        void RegistarEncomenda(int clienteId, int veiculoId, string observacoes);
        List<Estado> GetEstados();

        // 3. Gestão do Catálogo de Veículos
        Veiculo GetVeiculoById(int id);
        ICollection<Veiculo> GetCatalogo();
        void AddVeiculo(string modelo, decimal precoBase, DateTime dataAdicao);
        void DeleteVeiculo(int veiculoId);

        // 4. Gestão de Funcionários
        ICollection<Funcionario> GetFuncionarios();
        void AddFuncionario(string nome, string email, string password, DateTime dataContratacao, int posicaoId, int? supervisor);
        void DeleteFuncionario(int funcionarioId);
        List<Posicao> GetPosicoes();
        string GetPosicaoPorFuncionario(int funcionarioId);

        // 5. Gestão de Progresso
        FaseDeMontagem GetFaseDeMontagemById(int id);
        List<Progresso> GetProgresso(int encomendaId);
        void RegistarProgresso(int encomendaId, int faseId, DateTime dataInicio, DateTime? dataFim, int idFuncionario);

        // 6. Gestão de Alertas
        ICollection<Alerta> GetAlertas();
        void RegistarAlerta(int encomendaId, int funcionarioId, string mensagem, DateTime data);

        // 7. Gestão de Relatórios
        void CriarRelatorio(int tipo, string conteudo, int funcionarioId);
        Relatorio? DownloadRelatorio(int relatorioId);

        // 8. Gestão de Fases de Montagem
        List<FaseDeMontagem> GetFasesMontagem();
    }
}
