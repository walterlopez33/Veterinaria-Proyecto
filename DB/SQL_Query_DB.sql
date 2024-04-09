create database veterinaria
use veterinaria

CREATE TABLE Usuarios
(
    Login_Usuario VARCHAR(50) PRIMARY KEY,
    Clave_Usuario VARCHAR(50),
    Nombre_Usuario VARCHAR(100)
)



create procedure Enviar_Usuario
@User_Registro VARCHAR(50),
@Password_Registro VARCHAR(50),
@Name_Registro VARCHAR(100)
	AS
	BEGIN
	INSERT INTO Usuarios (Login_Usuario,Clave_Usuario,Nombre_Usuario) values (@User_Registro, @Password_Registro, @Name_Registro)
	END




INSERT INTO Usuarios VALUES ('walter','123','walter lopez')

SELECT Login_Usuario, Clave_Usuario FROM Usuarios where Login_Usuario = 'walter' and Clave_Usuario = '123' 

SELECT * FROM Usuarios



create procedure Validate_Login
@Login_Usuario VARCHAR(50),
@Clave_Usuario VARCHAR(50)
as
begin
SELECT Login_Usuario, Clave_Usuario FROM Usuarios where Login_Usuario = @Login_Usuario and Clave_Usuario = @Clave_Usuario
end

exec Validate_Login 'walter', '13' 






ALTER PROCEDURE Validate_Login
@Login_Usuario VARCHAR(50),
@Clave_Usuario VARCHAR(50)
AS

	BEGIN
	IF EXISTS (SELECT Login_Usuario, Clave_Usuario FROM Usuarios where Login_Usuario = @Login_Usuario and Clave_Usuario = @Clave_Usuario)

BEGIN
	PRINT 'Bienvenido '  +  @Login_Usuario
	END
	ELSE
	BEGIN
	PRINT 'Usuario o contraseña no existen'
	END
END

exec Validate_Login 'walter' , '123'
exec Validate_Login 'walter', '13' 



update Usuario set "nombre de la columna" = 'valor_agregar' where clave_primaria = 'el valor de la clave primaria' 





CREATE TABLE Mascotas (
    ID_Mascota int identity(1,1) PRIMARY KEY,
    Nombre_Mascota VARCHAR(100),
    Tipo_Mascota VARCHAR(50),
    Comida_Favorita VARCHAR(100)
)


INSERT INTO Mascotas VALUES ('Macha','Perro','Sardina')

SELECT * FROM Mascotas 



create procedure Enviar
@Nombre_Mascota VARCHAR(50),
@Tipo_Mascota VARCHAR(50),
@Comida_Favorita VARCHAR(100)
	AS
	BEGIN
	INSERT INTO Mascotas (Nombre_Mascota,Tipo_Mascota,Comida_Favorita) values (@Nombre_Mascota, @Tipo_Mascota, @Comida_Favorita)
	END










CREATE TABLE Citas 
(
    ID_Cita int identity(1,1),
    ID_Mascota INT,
    Proxima_fecha DATE,
    Medico_Asignado VARCHAR(100),
    FOREIGN KEY (ID_Mascota) REFERENCES Mascotas(ID_Mascota)
)



INSERT INTO Citas VALUES ('05/20/204','DR.Jose Perez')

SELECT * FROM Citas 
