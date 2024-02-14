CREATE DATABASE CHernandezCargarMasiva

CREATE TABLE Company (IdCompany VARCHAR(130) PRIMARY KEY,
					  Company_Name VARCHAR(24) NOT NULL)
GO

CREATE TABLE Cargo (Id VARCHAR(24) NOT NULL,
					Amount DECIMAL(22,2) NOT NULL,
					Status VARCHAR(30) NOT NULL,
					Create_At TIMESTAMP NOT NULL,
					Update_At DATE NULL,
					Company_Id VARCHAR(130) REFERENCES Company(IdCompany))
GO

ALTER PROCEDURE CargaMasivaCSV
@Id VARCHAR(24),
@Amount DECIMAL(22,2),
@Status VARCHAR(30),
@Update_At DATE,
@Company_Id VARCHAR(130)
AS
INSERT INTO Cargo (Id,
				   Amount,
				   Status,
				   Update_At,
				   Company_Id)
				   VALUES
				   (@Id,
				   @Amount,
				   @Status,
				   @Update_At,
				   @Company_Id)

TRUNCATE TABLE Cargo