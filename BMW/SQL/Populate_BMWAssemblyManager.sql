-- Selecionar o esquema bmwassemblymanager
USE assemblymngr;
SET IDENTITY_INSERT [Fase de montagem] ON;
-- Povoar a tabela Utilizador
INSERT INTO Utilizador (email, nome, Password, isCliente)
VALUES
('diogoadmin@email.com', 'Diogo Admin', 'admin123', 0);

-- Povoar a tabela Estado
INSERT INTO Estado (idEstado, Estado)
VALUES
(1, 'Em Processamento'),
(2, 'Concluído'),
(3, 'Cancelado');

-- Povoar a tabela Fase de montagem
INSERT INTO [Fase de montagem] (idEstado_de_montagem, Ordem, Descricao, TempoExecucaoExpectavel)
VALUES
(1, 1, 'Preparação do chassi', '01:00:00'),
(2, 2, 'Montagem do sistema de suspensão', '01:30:00'),
(3, 3, 'Instalação do sistema de travagem', '01:00:00'),
(4, 4, 'Colocação do motor e transmissão', '02:30:00'),
(5, 5, 'Montagem do sistema de escape', '01:00:00'),
(6, 6, 'Instalação do sistema elétrico', '03:00:00'),
(7, 7, 'Montagem da carroçaria', '02:00:00'),
(8, 8, 'Instalação de vidros e portas', '01:30:00'),
(9, 9, 'Montagem do interior', '02:00:00'),
(10, 10, 'Instalação do sistema de ar condicionado e aquecimento', '01:00:00'),
(11, 11, 'Pintura e acabamento externo', '04:00:00'),
(12, 12, 'Montagem das rodas e pneus', '00:45:00'),
(13, 13, 'Teste de alinhamento e calibragem', '01:30:00'),
(14, 14, 'Teste de qualidade e segurança', '02:00:00'),
(15, 15, 'Inspeção final e entrega', '01:00:00');

-- Povoar a tabela Posicao
INSERT INTO Posicao (idPosicao, Tipo)
VALUES
(1, 'Administrador'),
(2, 'Operador de Montagem');

-- Povoar a tabela Funcionario
INSERT INTO Funcionario (idFuncionario, contractDate, Posicao, supervisor)
VALUES
(1, '2020-01-01 09:00:00', 1, NULL);