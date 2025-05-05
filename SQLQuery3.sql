
create database ControlEscolar;
go
use ControlEscolar;
go

create table Alumnos(

idAlumno INT IDENTITY(1,1) PRIMARY KEY,
nombre VARCHAR(50) NOT NULL,
apellidoPaterno VARCHAR(50) NOT NULL,
apellidoMaterno VARCHAR(50),
usuario VARCHAR(20) NOT NULL,
contrasenia VARCHAR(100) NOT NULL,
);

create table Materias(

idMateria INT IDENTITY(1,1) PRIMARY KEY,
nombre VARCHAR(50) NOT NULL,
costo DECIMAL(10, 2) NOT NULL,
);

create table AlumnosMaterias (
    idAlumno INT,
    idMateria INT,
    PRIMARY KEY (idAlumno, idMateria),
    FOREIGN KEY (idAlumno) REFERENCES Alumnos(idAlumno),
    FOREIGN KEY (idMateria) REFERENCES Materias(idMateria)
);


---------------------------------------------------------------------
CREATE PROCEDURE sp_InsertarAlumno
    @nombre NVARCHAR(50),
    @apellidoPaterno NVARCHAR(50),
    @apellidoMaterno NVARCHAR(50),
    @usuario NVARCHAR(20),
    @contrasenia NVARCHAR(100)
AS
BEGIN
    INSERT INTO Alumnos(nombre, apellidoPaterno, apellidoMaterno, usuario, contrasenia)
    VALUES (@nombre, @apellidoPaterno, @apellidoMaterno, @usuario, @contrasenia)
END;
GO

-- Stored Procedure: Insertar Materia
CREATE PROCEDURE sp_InsertarMateria
    @nombre NVARCHAR(50),
    @costo DECIMAL(10, 2)
AS
BEGIN
    INSERT INTO Materias(nombre, costo)
    VALUES (@nombre, @costo)
END;
GO

-- Stored Procedure: Agregar Materia a Alumno
CREATE PROCEDURE sp_AgregarMateriaAlumno
    @idAlumno INT,
    @idMateria INT
AS
BEGIN
    INSERT INTO AlumnosMaterias(idAlumno, idMateria)
    VALUES (@idAlumno, @idMateria)
END;
GO

-- Stored Procedure: Obtener Costo Total de un Alumno
CREATE PROCEDURE sp_CostoTotalAlumno
    @idAlumno INT
AS
BEGIN
    SELECT SUM(M.costo) AS Total
    FROM Materias M
    INNER JOIN AlumnosMaterias AM ON M.idMateria = AM.idMateria
    WHERE AM.idAlumno = @idAlumno
END;
GO




USE ControlEscolar;

IF OBJECT_ID('alumnos', 'U') IS NULL
BEGIN
  CREATE TABLE alumnos (
      idAlumno INT IDENTITY(1,1) PRIMARY KEY,
      nombre VARCHAR(50) NOT NULL,
      apellidoPaterno VARCHAR(50) NOT NULL,
      apellidoMaterno VARCHAR(50),
      usuario VARCHAR(20) NOT NULL UNIQUE,
      contrasenia VARCHAR(100) NOT NULL
  );
END
GO

IF OBJECT_ID('sp_InsertarAlumno', 'P') IS NULL
BEGIN
  EXEC('
    CREATE PROCEDURE sp_InsertarAlumno
      @nombre VARCHAR(50),
      @apellidoPaterno VARCHAR(50),
      @apellidoMaterno VARCHAR(50),
      @usuario VARCHAR(20),
      @contrasenia VARCHAR(100)
    AS
    BEGIN
      INSERT INTO alumnos (nombre, apellidoPaterno, apellidoMaterno, usuario, contrasenia)
      VALUES (@nombre, @apellidoPaterno, @apellidoMaterno, @usuario, @contrasenia)
    END
  ')
END

USE ControlEscolar;

SELECT * FROM Alumnos;
SELECT * FROM Materias;


CREATE PROCEDURE sp_BuscarAlumNombre
    @nombre NVARCHAR(50)
AS
BEGIN
    SELECT idAlumno, nombre, apellidoPaterno, apellidoMaterno, usuario
    FROM Alumnos
    WHERE nombre LIKE '%' + @nombre + '%'
END;


