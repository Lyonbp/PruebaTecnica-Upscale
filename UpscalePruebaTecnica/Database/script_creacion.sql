
CREATE DATABASE UpscaleDB

USE UpscaleDB;
GO

-- Tablas Maestras (Catálogos)
CREATE TABLE TiposDocumento (
    TipoDocumentoId INT PRIMARY KEY IDENTITY(1,1),
    Abreviatura NVARCHAR(10) NOT NULL,
    Descripcion NVARCHAR(100) NOT NULL
);

CREATE TABLE TiposContratacion (
    TipoContratacionId INT PRIMARY KEY IDENTITY(1,1),
    Abreviatura NVARCHAR(10) NOT NULL,
    Descripcion NVARCHAR(100) NOT NULL
);

CREATE TABLE TiposTelefono(
	TIpoTelefonoId INT PRIMARY KEY IDENTITY(1,1),
	Descripcion Nvarchar(100) NOT NULL,
);

-- Insertar datos iniciales
INSERT INTO TiposDocumento (Abreviatura, Descripcion) VALUES ('DNI', 'Documento Nacional de Identidad'), ('CE', 'Carné de Extranjería');
INSERT INTO TiposContratacion (Abreviatura, Descripcion) VALUES ('CAS', 'Contrato Administrativo de Servicios'), ('CAP', 'Cuadro para Asignación de Personal');
INSERT INTO TiposTelefono (Descripcion) VALUES ('Fijo'), ('Móvil'), ('Trabajo');

-- Tabla Principal
CREATE TABLE Usuarios (
    UsuarioId INT PRIMARY KEY IDENTITY(1,1),
    TipoDocumentoId INT NOT NULL FOREIGN KEY REFERENCES TiposDocumento(TipoDocumentoId),
    TipoContratacionId INT NOT NULL FOREIGN KEY REFERENCES TiposContratacion(TipoContratacionId),
    NumeroDocumento NVARCHAR(20) NOT NULL UNIQUE, 
    PasswordHash NVARCHAR(255) NOT NULL,
    IntentosFallidos INT DEFAULT 0,
    FechaBloqueo DATETIME NULL,
    -- Datos 
    Nombres NVARCHAR(100) NOT NULL,
    PrimerApellido NVARCHAR(100) NOT NULL,
    SegundoApellido NVARCHAR(100) NULL,
    FechaNacimiento DATE NULL,
    Nacionalidad NVARCHAR(50) DEFAULT 'Peruana',
    Sexo NVARCHAR(20) NULL,
    CorreoPrincipal NVARCHAR(100) NOT NULL,
    CorreoSecundario NVARCHAR(100) NULL,
    TelefonoMovil NVARCHAR(20) NULL,
    TelefonoSecundario NVARCHAR(20) INT NULL FOREIGN KEY REFERENCES TiposTelefono(TipoTelefonoId),
    FechaContratacion DATE NULL,
    Cargo NVARCHAR(100) NULL,
    Institucion NVARCHAR(100) NULL
);

-- Usuario de Prueba
INSERT INTO Usuarios (TipoDocumentoId, TipoContratacionId, NumeroDocumento, PasswordHash, Nombres, PrimerApellido, SegundoApellido, CorreoPrincipal, TelefonoMovil, Cargo, Institucion)
VALUES (1, 1, '46844576', '123456', 'July Camila', 'Mendoza', 'Quispe', 'test@minsa.gob.pe', '+51 999 999 999', 'Administrador de Recursos', '011 Ministerio de Salud');

SELECT *
FROM Usuarios

--BORRAR INTENTOS FALLIDOS Y FECHA DE BLOQUEO PARA EL USUARIO 
UPDATE Usuarios 
SET IntentosFallidos = 0, 
    FechaBloqueo = NULL
WHERE NumeroDocumento = '46844576';