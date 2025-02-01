-- Turn off foreign key checks and unique checks

SET NOCOUNT ON;

-- -----------------------------------------------------
-- Schema dbo
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Table `dbo`.`Utilizador`
-- -----------------------------------------------------
CREATE TABLE dbo.Utilizador (
  idUtilizador INT IDENTITY(1,1) PRIMARY KEY,
  email VARCHAR(45) NOT NULL UNIQUE,
  nome VARCHAR(45) NOT NULL,
  Password VARCHAR(45) NOT NULL,
  isCliente TINYINT NOT NULL
);

-- -----------------------------------------------------
-- Table `dbo`.`Veiculo`
-- -----------------------------------------------------
CREATE TABLE dbo.Veiculo (
  idVeiculo INT IDENTITY(1,1) PRIMARY KEY,
  Modelo VARCHAR(45) NOT NULL UNIQUE,
  PrecoBase INT NOT NULL,
  DataAdicao DATETIME NOT NULL
);

-- -----------------------------------------------------
-- Table `dbo`.`Relatorio`
-- -----------------------------------------------------
CREATE TABLE dbo.Relatorio (
  idRelatorio INT IDENTITY(1,1) PRIMARY KEY,
  Tipo INT NOT NULL,
  DataGeracao DATETIME NOT NULL,
  Conteudo VARCHAR(250) NOT NULL,
  idFuncionario INT NOT NULL
);

-- -----------------------------------------------------
-- Table `dbo`.`Fase de montagem`
-- -----------------------------------------------------
CREATE TABLE dbo.[Fase de montagem] (
  idEstado_de_montagem INT IDENTITY(1,1) PRIMARY KEY,
  Ordem INT NOT NULL,
  Descricao VARCHAR(100) NOT NULL,
  TempoExecucaoExpectavel TIME NOT NULL
);

-- -----------------------------------------------------
-- Table `dbo`.`Estado`
-- -----------------------------------------------------
CREATE TABLE dbo.Estado (
  idEstado INT PRIMARY KEY,
  Estado VARCHAR(45) NOT NULL
);

-- -----------------------------------------------------
-- Table `dbo`.`Encomenda`
-- -----------------------------------------------------
CREATE TABLE dbo.Encomenda (
  idEncomenda INT IDENTITY(1,1) PRIMARY KEY,
  DataRegisto DATETIME NOT NULL,
  Observacoes VARCHAR(45) NULL,
  idCliente INT NOT NULL,
  idVeiculo INT NOT NULL,
  Estado INT NOT NULL,
  CONSTRAINT FK_Encomenda_Estado FOREIGN KEY (Estado) REFERENCES dbo.Estado(idEstado),
  CONSTRAINT FK_Encomenda_Veiculo FOREIGN KEY (idVeiculo) REFERENCES dbo.Veiculo(idVeiculo),
  CONSTRAINT FK_Encomenda_Utilizador FOREIGN KEY (idCliente) REFERENCES dbo.Utilizador(idUtilizador)
);

-- -----------------------------------------------------
-- Table `dbo`.`Alerta`
-- -----------------------------------------------------
CREATE TABLE dbo.Alerta (
  idAlerta INT IDENTITY(1,1) PRIMARY KEY,
  idProgresso INT NOT NULL,
  Mensagem VARCHAR(45) NOT NULL,
  Data DATETIME NOT NULL,
  CONSTRAINT FK_Alerta_Encomenda FOREIGN KEY (idProgresso) REFERENCES dbo.Encomenda(idEncomenda)
);

-- -----------------------------------------------------
-- Table `dbo`.`Progresso`
-- -----------------------------------------------------
CREATE TABLE dbo.Progresso (
  idEncomenda INT NOT NULL,
  idFase INT NOT NULL,
  StartFase DATETIME NOT NULL,
  EndFase DATETIME NULL,
  idFuncionario INT NOT NULL,
  PRIMARY KEY (idEncomenda, idFase),
  CONSTRAINT FK_Progresso_Fase_de_montagem FOREIGN KEY (idFase) REFERENCES dbo.[Fase de montagem](idEstado_de_montagem),
  CONSTRAINT FK_Progresso_Encomenda FOREIGN KEY (idEncomenda) REFERENCES dbo.Encomenda(idEncomenda),
  CONSTRAINT FK_Progresso_Utilizador FOREIGN KEY (idFuncionario) REFERENCES dbo.Utilizador(idUtilizador)
);

-- -----------------------------------------------------
-- Table `dbo`.`Posicao`
-- -----------------------------------------------------
CREATE TABLE dbo.Posicao (
  idPosicao INT PRIMARY KEY,
  Tipo VARCHAR(45) NOT NULL
);

-- -----------------------------------------------------
-- Table `dbo`.`Funcionario`
-- -----------------------------------------------------
CREATE TABLE dbo.Funcionario (
  idFuncionario INT PRIMARY KEY,
  contractDate DATETIME NOT NULL,
  Posicao INT NOT NULL,
  supervisor INT NULL,
  CONSTRAINT FK_Funcionario_Utilizador FOREIGN KEY (idFuncionario) REFERENCES dbo.Utilizador(idUtilizador),
  CONSTRAINT FK_Funcionario_Funcionario FOREIGN KEY (supervisor) REFERENCES dbo.Funcionario(idFuncionario),
  CONSTRAINT FK_Funcionario_Posicao FOREIGN KEY (Posicao) REFERENCES dbo.Posicao(idPosicao)
);

-- -----------------------------------------------------
-- Table `dbo`.`Funcionario_Relatorio`
-- -----------------------------------------------------
CREATE TABLE dbo.Funcionario_Relatorio (
  Funcionario_idFuncionario INT NOT NULL,
  Relatorio_idRelatorio INT NOT NULL,
  PRIMARY KEY (Funcionario_idFuncionario, Relatorio_idRelatorio),
  CONSTRAINT FK_Funcionario_Relatorio_Relatorio FOREIGN KEY (Relatorio_idRelatorio) REFERENCES dbo.Relatorio(idRelatorio),
  CONSTRAINT FK_Funcionario_Relatorio_Utilizador FOREIGN KEY (Funcionario_idFuncionario) REFERENCES dbo.Utilizador(idUtilizador)
);

SET NOCOUNT OFF;