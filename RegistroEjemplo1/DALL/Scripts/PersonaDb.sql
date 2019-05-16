CREATE DATABASE PersonaDb
GO
USE PersonaDb
GO
CREATE TABLE Personas
(
	Id int primary key IDENTITY,
	Nombre varchar(30),
	Telefono varchar(13),
	Cedula varchar(13),
	Direccion varchar(max)
);