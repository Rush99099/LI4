-- Selecionar o esquema bmwassemblymanager
USE assemblymngr;

-- Povoar a tabela Utilizador
INSERT INTO Utilizador (email, nome, Password, isCliente)
VALUES
('joao.silva@email.com', 'João Silva', 'password123', 0),
('ana.oliveira@email.com', 'Ana Oliveira', 'password123', 0),
('carlos.santos@email.com', 'Carlos Santos', 'password123', 1);

-- Povoar a tabela Veiculo
INSERT INTO Veiculo (Modelo, PrecoBase, DataAdicao)
VALUES
('Modelo A', 25000, '2025-01-01 10:00:00'),
('Modelo B', 30000, '2025-01-02 11:00:00'),
('Modelo C', 20000, '2025-01-03 12:00:00');

-- Povoar a tabela Estado
INSERT INTO Estado (idEstado, Estado)
VALUES
(1, 'Em Processamento'),
(2, 'Concluído'),
(3, 'Cancelado');

-- Povoar a tabela Encomenda
INSERT INTO Encomenda (idEncomenda, DataRegisto, Observações, idCliente, idVeiculo, Estado)
VALUES
(1, '2025-01-10 14:00:00', 'Entrega urgente', 2, 1, 1),
(2, '2025-01-12 15:00:00', NULL, 2, 2, 2),
(3, '2025-01-14 16:00:00', 'Cliente pediu alteração', 2, 3, 3);

-- Povoar a tabela Relatorio
INSERT INTO Relatorio (idRelatorio, Tipo, DataGeracao, Conteudo, idFuncionario)
VALUES
(1, 1, '2025-01-15 10:00:00', 'Relatório de Produção', 1),
(2, 2, '2025-01-16 11:00:00', 'Relatório de Vendas', 1);

-- Povoar a tabela Fase de montagem
INSERT INTO `Fase de montagem` (`idEstado de montagem`, `Ordem`, `Descricao`, `TempoExecucaoExpectavel`)
VALUES
(1, 1, 'Preparação de componentes', '2025-01-01 01:00:00'),
(2, 2, 'Montagem inicial', '2025-01-01 02:00:00'),
(3, 3, 'Montagem final', '2025-01-01 03:00:00');


-- Povoar a tabela Progresso
INSERT INTO Progresso (idEncomenda, idFase, StartFase, EndFase, idFuncionario)
VALUES
(1, 1, '2025-01-10 14:00:00', '2025-01-10 15:00:00', 1),
(1, 2, '2025-01-10 15:30:00', '2025-01-10 17:00:00', 1);

-- Povoar a tabela Posicao
INSERT INTO Posicao (idPosicao, Tipo)
VALUES
(1, 'Gestor de Produção'),
(2, 'Operador de Montagem');

-- Povoar a tabela Funcionario
INSERT INTO Funcionario (idFuncionario, contractDate, Posicao, supervisor)
VALUES
(1, '2020-01-01 09:00:00', 1, NULL),
(2, '2021-05-01 09:00:00', 2, 1);

SELECT * FROM UTILIZADOR