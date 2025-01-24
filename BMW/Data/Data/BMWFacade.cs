using BMW.Data.Models;

namespace BMW.Data.Data
{
    public class BMWFacade
    {
        private static BMWFacade? _instance;

        private readonly UtilizadorDAO _utilizadorDAO;
        private readonly EncomendaDAO _encomendaDAO;
        private readonly VeiculoDAO _veiculoDAO;
        private readonly FuncionarioDAO _funcionarioDAO;
        private readonly ProgressoDAO _progressoDAO;
        private readonly AlertaDAO _alertaDAO;
        private readonly RelatorioDAO _relatorioDAO;

        // Construtor privado para Singleton
        private BMWFacade()
        {
            _utilizadorDAO = UtilizadorDAO.GetInstance();
            _encomendaDAO = EncomendaDAO.GetInstance();
            _veiculoDAO = VeiculoDAO.GetInstance();
            _funcionarioDAO = FuncionarioDAO.GetInstance();
            _progressoDAO = ProgressoDAO.GetInstance();
            _alertaDAO = AlertaDAO.GetInstance();
            _relatorioDAO = RelatorioDAO.GetInstance();
        }

        // Singleton para garantir uma única instância
        public static BMWFacade GetInstance()
        {
            if (_instance == null)
            {
                _instance = new BMWFacade();
            }
            return _instance;
        }
        /*
        // 1. Autenticação e Gestão de Utilizadores
        public void Registar(string email, string password, string nome, bool isCliente)
        {
            var utilizador = new Utilizador(0, email, nome, password, isCliente);
            _utilizadorDAO.put(utilizador);
        }
        *
        public Utilizador? LogIn(string email, string password)
        {
            var utilizador = _utilizadorDAO.GetByEmail(email);
            if (utilizador != null && utilizador.Password == password)
            {
                return utilizador;
            }
            throw new Exception("Credenciais inválidas");
        }

        // 2. Gestão de Encomendas
        public ICollection<Encomenda> GetListaEncomendas(int clienteId)
        {
            return _encomendaDAO.GetByClienteId(clienteId);
        }

        public void RegistarEncomenda(int clienteId, int veiculoId, string observacoes)
        {
            var encomenda = new Encomenda(0, DateTime.Now, observacoes, clienteId, veiculoId, 1); // Estado inicial
            _encomendaDAO.Put(encomenda);
        }

        // 3. Gestão do Catálogo de Veículos
        public ICollection<Veiculo> GetCatalogo()
        {
            return _veiculoDAO.GetAll();
        }

        public void AddVeiculo(string modelo, decimal precoBase, DateTime dataAdicao)
        {
            var veiculo = new Veiculo(0, modelo, (int)precoBase, dataAdicao);
            _veiculoDAO.Put(veiculo);
        }

        public void DeleteVeiculo(int veiculoId)
        {
            _veiculoDAO.Remove(veiculoId);
        }

        // 4. Gestão de Funcionários
        public ICollection<Funcionario> GetFuncionarios()
        {
            return _funcionarioDAO.GetAll();
        }

        public void AddFuncionario(string nome, string email, DateTime dataContratacao, int posicaoId)
        {
            var funcionario = new Funcionario(0, nome, email, dataContratacao, posicaoId);
            _funcionarioDAO.Put(funcionario);
        }

        public void DeleteFuncionario(int funcionarioId)
        {
            _funcionarioDAO.Remove(funcionarioId);
        }

        // 5. Gestão de Progresso
        public void RegistarProgresso(int encomendaId, int faseId, DateTime dataInicio, DateTime? dataFim)
        {
            var progresso = new Progresso(0, dataInicio, (dataFim.HasValue ? (int)(dataFim.Value - dataInicio).TotalMinutes : 0), "Progresso registado", faseId);
            _progressoDAO.Put(progresso);
        }

        // 6. Gestão de Alertas
        public ICollection<Alerta> GetAlertas()
        {
            return _alertaDAO.GetAll();
        }

        public void RegistarAlerta(int encomendaId, int funcionarioId, string mensagem, DateTime data)
        {
            var alerta = new Alerta(0, encomendaId, mensagem, data);
            _alertaDAO.Put(alerta);
        }

        // 7. Gestão de Relatórios
        public void CriarRelatorio(int tipo, string conteudo, int funcionarioId)
        {
            var relatorio = new Relatorio(0, tipo, DateTime.Now, conteudo, funcionarioId.ToString());
            _relatorioDAO.Put(relatorio);
        }

        public Relatorio? DownloadRelatorio(int relatorioId)
        {
            return _relatorioDAO.Get(relatorioId);
        } */
    } 
}
