--DROP DATABASE Coletados; --apaga banco inteiro
--DROP TABLE Computadores; --apaga tabela
--SELECT * FROM Computadores; -- exibe tabela

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'Coletados')
	BEGIN
		CREATE DATABASE Coletados; -- Cria o banco de dados caso não exista
	END

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Computadores')
	BEGIN
		CREATE TABLE Computadores (
			IP varchar(100),
			Processador varchar(100),
			ProcessadorFabricante varchar(100),
			ProcessadorCore varchar(100),
			ProcessadorThread varchar(100),
			ProcessadorClock varchar(100),
			Ram varchar(100),
			RamTipo varchar(100),
			RamVelocidade varchar(100),
			RamVoltagem varchar(100),
			SO varchar(100),
			MAC varchar(100),
			Usuario varchar(100),
			ArmazenamentoC varchar(100),
			ArmazenamentoCTotal varchar(100),
			ArmazenamentoCLivre varchar(100),
			ArmazenamentoD varchar(100),
			ArmazenamentoDTotal varchar(100),
			ArmazenamentoDLivre varchar(100),
			ConsumoCPU varchar(100)
		);
		SELECT * FROM Computadores;
	END