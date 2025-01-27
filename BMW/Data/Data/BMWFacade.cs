using BMW.Data.Models;
using System;
using System.Collections.Generic;

namespace BMW.Data.Data
{
    public class BMWFacade : IBMWFacade
    {
        private static BMWFacade? _instance;

        // DAOs
        private readonly UtilizadorDAO _utilizadorDAO;
        private readonly EncomendaDAO _encomendaDAO;
        private readonly VeiculoDAO _veiculoDAO;
        private readonly FuncionarioDAO _funcionarioDAO;
        private readonly ProgressoDAO _progressoDAO;
        private readonly AlertaDAO _alertaDAO;
        private readonly RelatorioDAO _relatorioDAO;
        private readonly PosicaoDAO _posicaoDAO;
        private readonly EstadoDAO _estadoDAO;
        private readonly FaseMontagemDAO _faseMontagemDAO;

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
            _posicaoDAO = PosicaoDAO.GetInstance();
            _estadoDAO = EstadoDAO.GetInstance();
            _faseMontagemDAO = FaseMontagemDAO.GetInstance();
        }

        // Singleton
        public static BMWFacade GetInstance()
        {
            return _instance ??= new BMWFacade();
        }

        // ================================
        // 1. Autenticação e Gestão de Utilizadores
        // ================================
        public void Registar(string email, string password, string nome, bool isCliente)
        {
            var utilizador = new Utilizador(0, email, nome, password, isCliente);
            _utilizadorDAO.Put(utilizador);
        }


        public Boolean AtualizarPassword(string email, string password)
        {
            var user = _utilizadorDAO.GetByEmail(email);
            if(user != null){
                _utilizadorDAO.ChangePassword(user.IdUtilizador, password);
                return true;
            }else return false;
        }

        public Utilizador? LogIn(string email, string password)
        {
            var utilizador = _utilizadorDAO.GetByEmail(email);
            if (utilizador != null && utilizador.Password == password)
            {
                return utilizador;
            }
            throw new Exception("Credenciais inválidas");
        }

        public List<Utilizador> GetClientes()
        {
            return _utilizadorDAO.ValuesClientes();
        }

        public Utilizador GetUtilizador(int id)
        {
            return _utilizadorDAO.Get(id) ?? throw new Exception("Utilizador não encontrado");
        }

        // ================================
        // 2. Gestão de Encomendas
        // ================================
        public Estado GetEstadoById(int estadoId)
        {
            try
            {
                var estado = EstadoDAO.GetInstance().GetById(estadoId);
                if (estado == null)
                {
                    throw new Exception($"Estado com ID {estadoId} não encontrado.");
                }
                return estado;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter estado com ID {estadoId}: {ex.Message}", ex);
            }
        }
        
        public Encomenda? GetEncomendaById(int id)
        {
            var encomenda = EncomendaDAO.GetInstance().Get(id);
            if (encomenda == null)
            {
                throw new Exception($"Encomenda com ID {id} não encontrada.");
            }
            return encomenda;
        }
        

        public ICollection<Encomenda> GetListaEncomendas(int clienteId)
        {
            return _encomendaDAO.GetByClienteID(clienteId);
        }

        public ICollection<Encomenda> GetAllEncomendas()
        {
            return _encomendaDAO.GetAll();
        }

        public void RegistarEncomenda(int clienteId, int veiculoId, string observacoes)
        {
            var encomenda = new Encomenda(0, DateTime.Now, observacoes, clienteId, veiculoId, 1);
            _encomendaDAO.Put(encomenda);
        }

        public List<Estado> GetEstados()
        {
            return _estadoDAO.GetAll();
        }

        // ================================
        // 3. Gestão do Catálogo de Veículos
        // ================================
        public Veiculo GetVeiculoById(int veiculoId)
        {
            var veiculo = VeiculoDAO.GetInstance().Get(veiculoId);
            if (veiculo == null)
                throw new Exception($"Veículo com ID {veiculoId} não encontrado.");
            return veiculo;
        }

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

        // ================================
        // 4. Gestão de Funcionários
        // ================================
        public ICollection<Funcionario> GetFuncionarios()
        {
            return _funcionarioDAO.GetAll();
        }

        public void AddFuncionario(string nome, string email, string password, DateTime dataContratacao, int posicaoId, int? supervisor)
        {
            if (!FuncionarioDAO.PosicaoExiste(posicaoId))
            {
                throw new Exception($"A posição com ID {posicaoId} não existe.");
            }

            var utilizador = new Utilizador(email, nome, password, false);
            _utilizadorDAO.Put(utilizador);

            var utilizadorCriado = _utilizadorDAO.GetByEmail(email)
                                ?? throw new Exception("Erro ao criar utilizador associado ao funcionário.");

            var funcionario = new Funcionario(utilizadorCriado.IdUtilizador, dataContratacao, posicaoId, supervisor);
            _funcionarioDAO.Put(funcionario);
        }

        public void DeleteFuncionario(int funcionarioId)
        {
            _funcionarioDAO.Remove(funcionarioId);
            _utilizadorDAO.Remove(funcionarioId);
        }

        public List<Posicao> GetPosicoes()
        {
            return _posicaoDAO.GetAll();
        }

        public string GetPosicaoPorFuncionario(int funcionarioId)
        {
            try
            {
                // Obter funcionário pelo ID
                var funcionario = _funcionarioDAO.Get(funcionarioId);
                if (funcionario == null)
                {
                    Console.WriteLine($"Funcionário com ID {funcionarioId} não encontrado.");
                    return "Desconhecido";
                }

                // Obter a posição associada ao funcionário
                var posicao = _posicaoDAO.Get(funcionario.Posicao);
                if (posicao == null)
                {
                    Console.WriteLine($"Posição com ID {funcionario.Posicao} não encontrada.");
                    return "Desconhecido";
                }

                return posicao.Tipo;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter posição para o funcionário {funcionarioId}: {ex.Message}");
                return "Erro ao obter posição";
            }
        }


        // ================================
        // 5. Gestão de Progresso
        // ================================
        public FaseDeMontagem GetFaseDeMontagemById(int faseId)
        {
            var fase = FaseMontagemDAO.GetInstance().GetById(faseId);
            if (fase == null)
                throw new Exception($"Fase de montagem com ID {faseId} não encontrada.");
            return fase;
        }

        public List<Progresso> GetProgresso(int encomendaId)
        {
            return _progressoDAO.GetByEncomendaId(encomendaId);
        }

        public void RegistarProgresso(int encomendaId, int faseId, DateTime dataInicio, DateTime? dataFim, int idFuncionario)
        {
            var progresso = new Progresso(encomendaId, faseId, dataInicio, dataFim, "Progresso registado", idFuncionario);
            _progressoDAO.Put(progresso);
        }

        public List<FaseDeMontagem> GetFasesMontagem()
        {
            return _faseMontagemDAO.GetAll();
        }

        // ================================
        // 6. Gestão de Alertas
        // ================================
        public ICollection<Alerta> GetAlertas()
        {
            return _alertaDAO.GetAll();
        }

        public void RegistarAlerta(int encomendaId, int funcionarioId, string mensagem, DateTime data)
        {
            var alerta = new Alerta(0, encomendaId, mensagem, data);
            _alertaDAO.Put(alerta);
        }

        // ================================
        // 7. Gestão de Relatórios
        // ================================
        public void CriarRelatorio(int tipo, string conteudo, int funcionarioId)
        {
            var relatorio = new Relatorio(0, tipo, DateTime.Now, conteudo, funcionarioId);
            _relatorioDAO.Put(relatorio);
        }

        public List<Relatorio> GetRelatorios()
        {
            try
            {
                return RelatorioDAO.GetInstance().GetAll().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar relatórios: {ex.Message}", ex);
            }
        }


        public Relatorio? DownloadRelatorio(int relatorioId)
        {
            return _relatorioDAO.Get(relatorioId);
        }
    }
}
