-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema ASSEMBLYMNGR
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema ASSEMBLYMNGR
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `ASSEMBLYMNGR` DEFAULT CHARACTER SET utf8 ;
USE `ASSEMBLYMNGR` ;

-- -----------------------------------------------------
-- Table `ASSEMBLYMNGR`.`Utilizador`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASSEMBLYMNGR`.`Utilizador` (
  `idUtilizador` INT NOT NULL AUTO_INCREMENT,
  `email` VARCHAR(45) NOT NULL,
  `nome` VARCHAR(45) NOT NULL,
  `Password` VARCHAR(45) NOT NULL,
  `isCliente` TINYINT NOT NULL,
  PRIMARY KEY (`idUtilizador`),
  UNIQUE INDEX `idUtilizador_UNIQUE` (`idUtilizador` ASC) VISIBLE,
  UNIQUE INDEX `email_UNIQUE` (`email` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ASSEMBLYMNGR`.`Veiculo`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASSEMBLYMNGR`.`Veiculo` (
  `idVeiculo` INT NOT NULL AUTO_INCREMENT,
  `Modelo` VARCHAR(45) NOT NULL,
  `PrecoBase` INT NOT NULL,
  `DataAdicao` DATETIME NOT NULL,
  PRIMARY KEY (`idVeiculo`),
  UNIQUE INDEX `idVeiculo_UNIQUE` (`idVeiculo` ASC) VISIBLE,
  UNIQUE INDEX `Modelo_UNIQUE` (`Modelo` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ASSEMBLYMNGR`.`Relatorio`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASSEMBLYMNGR`.`Relatorio` (
  `idRelatorio` INT NOT NULL AUTO_INCREMENT,
  `Tipo` INT NOT NULL,
  `DataGeracao` DATETIME NOT NULL,
  `Conteudo` VARCHAR(45) NOT NULL,
  `idFuncionario` INT NOT NULL,
  PRIMARY KEY (`idRelatorio`),
  UNIQUE INDEX `idRelatorio_UNIQUE` (`idRelatorio` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ASSEMBLYMNGR`.`Fase de montagem`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASSEMBLYMNGR`.`Fase de montagem` (
  `idEstado de montagem` INT NOT NULL AUTO_INCREMENT,
  `Ordem` INT NOT NULL,
  `Descricao` VARCHAR(45) NOT NULL,
  `TempoExecucaoExpectavel` DATETIME NOT NULL,
  PRIMARY KEY (`idEstado de montagem`),
  UNIQUE INDEX `idEstado de montagem_UNIQUE` (`idEstado de montagem` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ASSEMBLYMNGR`.`Estado`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASSEMBLYMNGR`.`Estado` (
  `idEstado` INT NOT NULL,
  `Estado` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idEstado`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ASSEMBLYMNGR`.`Encomenda`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASSEMBLYMNGR`.`Encomenda` (
  `idEncomenda` INT NOT NULL AUTO_INCREMENT,
  `DataRegisto` DATETIME NOT NULL,
  `Observacoes` VARCHAR(45) NULL,
  `idCliente` INT NOT NULL,
  `idVeiculo` INT NOT NULL,
  `Estado` INT NOT NULL,
  PRIMARY KEY (`idEncomenda`),
  INDEX `fk_Encomenda_Estado1_idx` (`Estado` ASC) VISIBLE,
  INDEX `fk_Encomenda_Veiculo1_idx` (`idVeiculo` ASC) VISIBLE,
  INDEX `fk_Encomenda_Utilizador2_idx` (`idCliente` ASC) VISIBLE,
  CONSTRAINT `fk_Encomenda_Estado1`
    FOREIGN KEY (`Estado`)
    REFERENCES `ASSEMBLYMNGR`.`Estado` (`idEstado`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Encomenda_Veiculo1`
    FOREIGN KEY (`idVeiculo`)
    REFERENCES `ASSEMBLYMNGR`.`Veiculo` (`idVeiculo`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Encomenda_Utilizador2`
    FOREIGN KEY (`idCliente`)
    REFERENCES `ASSEMBLYMNGR`.`Utilizador` (`idUtilizador`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ASSEMBLYMNGR`.`Alerta`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASSEMBLYMNGR`.`Alerta` (
  `idAlerta` INT NOT NULL AUTO_INCREMENT,
  `idProgresso` INT NOT NULL,
  `Mensagem` VARCHAR(45) NOT NULL,
  `Data` DATETIME NOT NULL,
  PRIMARY KEY (`idAlerta`),
  CONSTRAINT `fk_Alerta_Encomenda1`
    FOREIGN KEY (`idAlerta`)
    REFERENCES `ASSEMBLYMNGR`.`Encomenda` (`idEncomenda`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ASSEMBLYMNGR`.`Progresso`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASSEMBLYMNGR`.`Progresso` (
  `idEncomenda` INT NOT NULL,
  `idFase` INT NOT NULL,
  `StartFase` DATETIME NOT NULL,
  `EndFase` DATETIME NULL,
  `idFuncionario` INT NOT NULL,
  PRIMARY KEY (`idEncomenda`, `idFase`),
  INDEX `fk_Progresso_Fase de montagem1_idx` (`idFase` ASC) VISIBLE,
  INDEX `fk_Encomenda_Utilizador1_idx` (`idFuncionario` ASC) VISIBLE,
  INDEX `fk_Encomenda_Encomenda1_idx` (`idEncomenda` ASC) VISIBLE,
  CONSTRAINT `fk_Progresso_Fase de montagem1`
    FOREIGN KEY (`idFase`)
    REFERENCES `ASSEMBLYMNGR`.`Fase de montagem` (`idEstado de montagem`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Encomenda_Utilizador1`
    FOREIGN KEY (`idFuncionario`)
    REFERENCES `ASSEMBLYMNGR`.`Utilizador` (`idUtilizador`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Encomenda_Encomenda1`
    FOREIGN KEY (`idEncomenda`)
    REFERENCES `ASSEMBLYMNGR`.`Encomenda` (`idEncomenda`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ASSEMBLYMNGR`.`Posicao`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASSEMBLYMNGR`.`Posicao` (
  `idPosicao` INT NOT NULL,
  `Tipo` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idPosicao`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ASSEMBLYMNGR`.`Funcionario`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASSEMBLYMNGR`.`Funcionario` (
  `idFuncionario` INT NOT NULL,
  `contractDate` DATETIME NOT NULL,
  `Posicao` INT NOT NULL,
  `supervisor` INT NULL,
  PRIMARY KEY (`idFuncionario`),
  INDEX `fk_Funcionario_Funcionario1_idx` (`supervisor` ASC) VISIBLE,
  INDEX `fk_Funcionario_Posicao1_idx` (`Posicao` ASC) VISIBLE,
  CONSTRAINT `fk_Funcionario_Utilizador1`
    FOREIGN KEY (`idFuncionario`)
    REFERENCES `ASSEMBLYMNGR`.`Utilizador` (`idUtilizador`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Funcionario_Funcionario1`
    FOREIGN KEY (`supervisor`)
    REFERENCES `ASSEMBLYMNGR`.`Funcionario` (`idFuncionario`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Funcionario_Posicao1`
    FOREIGN KEY (`Posicao`)
    REFERENCES `ASSEMBLYMNGR`.`Posicao` (`idPosicao`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ASSEMBLYMNGR`.`Funcionario_Relatorio`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASSEMBLYMNGR`.`Funcionario_Relatorio` (
  `Funcionario_idFuncionario` INT NOT NULL,
  `Relatorio_idRelatorio` INT NOT NULL,
  PRIMARY KEY (`Funcionario_idFuncionario`, `Relatorio_idRelatorio`),
  INDEX `fk_Funcionario_has_Relatorio_Relatorio1_idx` (`Relatorio_idRelatorio` ASC) VISIBLE,
  CONSTRAINT `fk_Funcionario_has_Relatorio_Relatorio1`
    FOREIGN KEY (`Relatorio_idRelatorio`)
    REFERENCES `ASSEMBLYMNGR`.`Relatorio` (`idRelatorio`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Funcionario_Relatorio_Utilizador1`
    FOREIGN KEY (`Funcionario_idFuncionario`)
    REFERENCES `ASSEMBLYMNGR`.`Utilizador` (`idUtilizador`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
