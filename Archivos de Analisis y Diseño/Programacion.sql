
--Procedimientos alamacenados Para Localizaciones--------------------
CREATE INDEX I_Localizacion_Nombre
ON Localizacion(LOCA_Nombre,LOCA_Origen, LOCA_Interno)
INCLUDE(LOCA_NombreExtendido)
go
create procedure PA_ObtenerLocalizacionByOrigen
(
	@Origen int
)
as
	select LOCA_Interno, LOCA_Nombre, LOCA_NombreExtendido, LOCA_Origen from Localizacion
	where LOCA_Origen = @Origen

go

create procedure PA_ObtenerLocalizaciones
as
	select LOCA_Interno, LOCA_Nombre, LOCA_NombreExtendido, LOCA_Origen,LOCA_Eliminado from Localizacion
	order by LOCA_Nombre
go

--select  LOCA_Nombre,PATINDEX('%[0-9]%',LOCA_Nombre) as indice,
--LEN(LOCA_Nombre) as long,
--convert(int,SUBSTRING(LOCA_Nombre,PATINDEX('%[0-9]%',LOCA_Nombre),LEN(LOCA_Nombre)-PATINDEX('%[0-9]%',LOCA_Nombre)+1)) as n
--from Localizacion
--WHERE PATINDEX('%[0-9]%',LOCA_Nombre) <>0
--order by LOCA_Nombre,n
	
--semejante a un ON DUPLICATED KEY UPDATE conpatibilidad sql2008+
--MERGE TABLA
--USING (SELECT @ID AS ID) AS SRC ON SRC.ID = TABLA.ID
--WHEN MATCHED THEN
--UPDATE SET Campo1 = @Valor
--WHEN NOT MATCHED THEN
--INSERT (Campo1,ID) VALUES (@Valor,@ID)
create procedure PA_InsertarLocalizacion
(
	@LOCA_Nombre varchar(50),
	@LOCA_NombreExtendido varchar(200),
	@LOCA_Origen int,
	@AUDI_UsuarioCrea int	
)
as
DECLARE @LOCA_Eliminado int = 0
MERGE Localizacion

USING (SELECT @LOCA_Nombre AS LOCA_Nombre, @LOCA_Origen AS LOCA_Origen,@LOCA_Eliminado AS LOCA_Eliminado) 
AS LOCA
ON (LOCA.LOCA_Nombre=Localizacion.LOCA_Nombre AND LOCA.LOCA_Origen=Localizacion.LOCA_Origen AND 
LOCA.LOCA_Eliminado=Localizacion.LOCA_Eliminado)

WHEN NOT MATCHED THEN
	INSERT (LOCA_Nombre,LOCA_NombreExtendido,LOCA_Origen,LOCA_Eliminado,AUDI_UsuarioCrea,AUDI_FechaCrea)
					VALUES(@LOCA_Nombre,@LOCA_NombreExtendido,@LOCA_Origen,0,@AUDI_UsuarioCrea,GETDATE());
go
--create alter procedure PA_InsertarLocalizacion
--(
--	@LOCA_Nombre varchar(50),
--	@LOCA_NombreExtendido varchar(200),
--	@LOCA_Origen int,
--	@AUDI_UsuarioCrea int	
--)
--as
--	insert into Localizacion(LOCA_Nombre,LOCA_NombreExtendido,LOCA_Origen,LOCA_Eliminado,AUDI_UsuarioCrea,AUDI_FechaCrea)
--					values(@LOCA_Nombre,@LOCA_NombreExtendido,@LOCA_Origen,0,@AUDI_UsuarioCrea,GETDATE())
CREATE PROCEDURE PA_UpdateLocalizacion
(
	@LOCA_Interno int,
	@LOCA_Nombre varchar(50),
	@AUDI_UsuarioEdita int,
	@LOCA_NombreExtendido varchar(200)
)
AS
	UPDATE Localizacion SET LOCA_Nombre=@LOCA_Nombre,AUDI_UsuarioEdita=@AUDI_UsuarioEdita,
	LOCA_NombreExtendido=@LOCA_NombreExtendido,AUDI_FechaEdita=GETDATE() WHERE LOCA_Interno=@LOCA_Interno
go
CREATE PROCEDURE PA_EliminarLocalizacion
(
	@LOCA_Interno int,
	@AUDI_UsuarioEdita int
)
as
	UPDATE Localizacion SET LOCA_Eliminado=1, AUDI_UsuarioEdita=@AUDI_UsuarioEdita,
	AUDI_FechaEdita=GETDATE() WHERE LOCA_Interno=@LOCA_Interno
go

--Programacion SQL para gestion de Equipos--------

--Indice de cobertura para la paginacion. Compatibilidad sql2005+
CREATE INDEX I_Equipo_Nombre
ON Equipo(EQUI_Nombre, EQUI_Interno)
INCLUDE(EQUI_Marca, EQUI_Modelo,EQUI_Serie,EQUI_Codigo,
EQUI_AnioFabricacion,EQUI_AnioServicio,EQUI_Estado,
EQUI_Descripcion,EQUI_Eliminado,TIPO_Interno)
go
--Implementamos la navegacion basica
	--COMPATIBILIDAD SQL2005+
	--PA Para la primera y ultima pagina
--------------------
--CREATE PROCEDURE PA_ObtenerPrimeraPaginaEquipos
--	@TamanioPagina int
--as
--	SELECT TOP (@TamanioPagina) EQUI_Interno, EQUI_Nombre,
--	EQUI_Marca, EQUI_Modelo,EQUI_Serie,EQUI_Codigo,
--	EQUI_AnioFabricacion,EQUI_AnioServicio,EQUI_Estado,
--	EQUI_Descripcion,EQUI_Eliminado,TIPO_Interno
--	FROM Equipo
--	ORDER BY EQUI_Nombre,EQUI_Interno
--go
--CREATE PROCEDURE PA_ObtenerUltimaPaginaEquipos
--	@TamanioPagina int
--as
--	SELECT TOP (@TamanioPagina) EQUI_Interno, EQUI_Nombre,
--	EQUI_Marca, EQUI_Modelo,EQUI_Serie,EQUI_Codigo,
--	EQUI_AnioFabricacion,EQUI_AnioServicio,EQUI_Estado,
--	EQUI_Descripcion,EQUI_Eliminado,TIPO_Interno
--	FROM Equipo
--	ORDER BY EQUI_Nombre ASC,EQUI_Interno ASC
--	--PA para la pagina siguiente y anterior
--GO
--CREATE PROCEDURE PA_ObtenerEquiposPaginaSiguiente
--	@TamanioPagina int, 
--    @UltimoEQUI_Nombre varchar(80), 
--    @UltimoEQUI_Interno int
--as
--	SELECT TOP (@TamanioPagina) EQUI_Interno, EQUI_Nombre,
--	EQUI_Marca, EQUI_Modelo,EQUI_Serie,EQUI_Codigo,
--	EQUI_AnioFabricacion,EQUI_AnioServicio,EQUI_Estado,
--	EQUI_Descripcion,EQUI_Eliminado,TIPO_Interno
--	FROM Equipo
--	WHERE EQUI_Nombre > @UltimoEQUI_Nombre OR
--			(EQUI_Nombre = @UltimoEQUI_Nombre AND
--			EQUI_Interno > @UltimoEQUI_Interno)
--	ORDER BY EQUI_Nombre, EQUI_Interno
--GO 
--CREATE PROCEDURE PA_ObtenerEquiposPaginaAnterior
--	@TamanioPagina int, 
--    @PrimerEQUI_Nombre varchar(80), 
--    @PrimerEQUI_Interno int
--as
--	SELECT TOP (@TamanioPagina) EQUI_Interno, EQUI_Nombre,
--	EQUI_Marca, EQUI_Modelo,EQUI_Serie,EQUI_Codigo,
--	EQUI_AnioFabricacion,EQUI_AnioServicio,EQUI_Estado,
--	EQUI_Descripcion,EQUI_Eliminado,TIPO_Interno
--	FROM Equipo
--	WHERE EQUI_Nombre < @PrimerEQUI_Nombre OR
--			(EQUI_Nombre = @PrimerEQUI_Nombre AND
--			EQUI_Interno < @PrimerEQUI_Interno)
--	ORDER BY EQUI_Nombre DESC, EQUI_Interno DESC
--GO
	--PA para obtener cualkier pagina. Compatibilidad SQL2005+
GO
CREATE PROCEDURE PA_ObtenerCualquierPaginaEquipos
	@TamanioPagina int,
	@NumeroPagina int
AS
	SELECT EQUI_Interno, EQUI_Nombre,
	EQUI_Marca, EQUI_Modelo,EQUI_Serie,EQUI_Codigo,
	EQUI_AnioFabricacion,EQUI_AnioServicio,EQUI_Estado,
	EQUI_Descripcion,EQUI_Eliminado,TIPO_Interno
	FROM (
			SELECT EQUI_Interno, EQUI_Nombre,
			EQUI_Marca, EQUI_Modelo,EQUI_Serie,EQUI_Codigo,
			EQUI_AnioFabricacion,EQUI_AnioServicio,EQUI_Estado,
			EQUI_Descripcion,EQUI_Eliminado,TIPO_Interno,
			ROW_NUMBER() OVER(ORDER BY EQUI_Nombre, EQUI_Interno) AS NumeroFila
			FROM Equipo WHERE EQUI_Eliminado=0
	) AS Equipos
	WHERE NumeroFila BETWEEN @TamanioPagina * (@NumeroPagina-1) + 1
					AND @TamanioPagina * (@NumeroPagina)
GO
CREATE PROCEDURE PA_ObtenerCualquierPaginaEquiposFiltradoPorString
	@TamanioPagina int,
	@NumeroPagina int,
	@String nvarchar(20)
AS
	SELECT EQUI_Interno, EQUI_Nombre,
	EQUI_Marca, EQUI_Modelo,EQUI_Serie,EQUI_Codigo,
	EQUI_AnioFabricacion,EQUI_AnioServicio,EQUI_Estado,
	EQUI_Descripcion,EQUI_Eliminado,TIPO_Interno
	FROM (
			SELECT EQUI_Interno, EQUI_Nombre,
			EQUI_Marca, EQUI_Modelo,EQUI_Serie,EQUI_Codigo,
			EQUI_AnioFabricacion,EQUI_AnioServicio,EQUI_Estado,
			EQUI_Descripcion,EQUI_Eliminado,TIPO_Interno,
			ROW_NUMBER() OVER(ORDER BY EQUI_Nombre, EQUI_Interno) AS NumeroFila
			FROM Equipo WHERE EQUI_Eliminado=0 
			AND (EQUI_Descripcion LIKE @String+'%' OR
			EQUI_Descripcion LIKE '%'+@String+'%')
	) AS Equipos
	WHERE NumeroFila BETWEEN @TamanioPagina * (@NumeroPagina-1) + 1
					AND @TamanioPagina * (@NumeroPagina)
GO
--Para trabajar con tablas con muchos registros
--Se debe de tener cuidado cuando se quiere un numero exácto de registros, 
--ya que esta tabla no se actualiza en linea. También es necesario que se tenga 
--habilitada la opción de "Auto create statistics" de la base de datos.
--SELECT name
--FROM sysindexes
--WHERE (name LIKE '%_WA_Sys%')
--CREATE PROCEDURE PA_TotalRegistrosAproximadoEquipo
--AS
--SELECT rows FROM sysindexes WHERE id = OBJECT_ID('Equipo') AND indid < 2 
GO
CREATE PROCEDURE PA_TotalRegistrosEquipo
AS
SELECT COUNT(EQUI_Interno) FROM Equipo WHERE EQUI_Eliminado=0
GO
	--hasta aqui para la paginacion de Equipo
--Ahora PA para Insertar, Actualizar, eliminar datos de un equipo
--MERGE TABLA
--USING (SELECT @ID AS ID) AS SRC ON SRC.ID = TABLA.ID
--WHEN MATCHED THEN
--UPDATE SET Campo1 = @Valor
--WHEN NOT MATCHED THEN
--INSERT (Campo1,ID) VALUES (@Valor,@ID)

CREATE PROCEDURE PA_InsertarEquipo
(
	@EQUI_Interno int output,
	@EQUI_Nombre varchar(80),
	@EQUI_Marca varchar(30),
	@EQUI_Modelo varchar(30),
	@EQUI_Serie varchar(20),
	@EQUI_Codigo varchar(10),
	@EQUI_AnioFabricacion int,
	@EQUI_AnioServicio int,
	@EQUI_Estado char(1),
	@EQUI_Descripcion varchar(200),
	@TIPO_Interno int,
	@AUDI_Usuario int,
	@LOCA_Interno int,
	@HILO_Fecha datetime
)
AS
BEGIN
BEGIN TRANSACTION;
MERGE Equipo AS DESTINO
USING (SELECT @EQUI_Interno AS EQUI_Interno) AS ORIGEN 
ON (ORIGEN.EQUI_Interno = DESTINO.EQUI_Interno) 
WHEN MATCHED THEN
UPDATE SET EQUI_Nombre=@EQUI_Nombre,EQUI_Marca=@EQUI_Marca,
		EQUI_Modelo=@EQUI_Modelo,EQUI_Serie=@EQUI_Serie,
		EQUI_Codigo=@EQUI_Codigo,EQUI_AnioFabricacion=@EQUI_AnioFabricacion,
		EQUI_AnioServicio=@EQUI_AnioServicio,EQUI_Estado=@EQUI_Estado,
		EQUI_Descripcion=@EQUI_Descripcion,TIPO_Interno=@TIPO_Interno,
		AUDI_UsuarioEdita=@AUDI_Usuario,AUDI_FechaEdita=GETDATE()
WHEN NOT MATCHED  THEN
	INSERT (EQUI_Nombre,
	EQUI_Marca, EQUI_Modelo,EQUI_Serie,EQUI_Codigo,
	EQUI_AnioFabricacion,EQUI_AnioServicio,EQUI_Estado,
	EQUI_Descripcion,EQUI_Eliminado,TIPO_Interno,AUDI_UsuarioCrea,AUDI_FechaCrea) 
	VALUES( @EQUI_Nombre,
	@EQUI_Marca, @EQUI_Modelo,@EQUI_Serie,@EQUI_Codigo,
	@EQUI_AnioFabricacion,@EQUI_AnioServicio,@EQUI_Estado,
	@EQUI_Descripcion,0,@TIPO_Interno,@AUDI_Usuario,GETDATE());
	IF(@@ROWCOUNT>0)
	BEGIN
		if(SCOPE_IDENTITY() IS NULL)
			set @EQUI_Interno = @EQUI_Interno;
		else
			set @EQUI_Interno = SCOPE_IDENTITY();
		IF(@LOCA_Interno IS NOT NULL)
		BEGIN
			MERGE HistorialLocalizacion AS DEST
			USING (SELECT @EQUI_Interno AS EQUI_Interno,@LOCA_Interno AS LOCA_Interno,
			@HILO_Fecha AS HILO_Fecha) AS ORIG 
			ON (ORIG.EQUI_Interno = DEST.EQUI_Interno AND ORIG.LOCA_Interno=DEST.LOCA_Interno
			AND ORIG.HILO_Fecha=DEST.HILO_Fecha)
			WHEN NOT MATCHED  THEN
			INSERT (EQUI_Interno,LOCA_Interno,HILO_Fecha)
			VALUES(@EQUI_Interno,@LOCA_Interno,GETDATE());
		END
		COMMIT TRANSACTION;
	END
	ELSE
	BEGIN
		set @EQUI_Interno = 0;
		ROLLBACK TRANSACTION;
	END
END--FIN INSERTAR EQUIPO
GO
CREATE PROCEDURE PA_EditarEquipo
(
	@EQUI_Interno int,
	@EQUI_Nombre varchar(80),
	@EQUI_Marca varchar(30),
	@EQUI_Modelo varchar(30),
	@EQUI_Serie varchar(20),
	@EQUI_Codigo varchar(10),
	@EQUI_AnioFabricacion int,
	@EQUI_AnioServicio int,
	@EQUI_Estado char(1),
	@EQUI_Descripcion varchar(200),
	@TIPO_Interno int,
	@AUDI_UsuarioEdita int
)
AS
	UPDATE Equipo SET EQUI_Nombre=@EQUI_Nombre,EQUI_Marca=@EQUI_Marca,
		EQUI_Modelo=@EQUI_Modelo,EQUI_Serie=@EQUI_Serie,
		EQUI_Codigo=@EQUI_Codigo,EQUI_AnioFabricacion=@EQUI_AnioFabricacion,
		EQUI_AnioServicio=@EQUI_AnioServicio,EQUI_Estado=@EQUI_Estado,
		EQUI_Descripcion=@EQUI_Descripcion,TIPO_Interno=@TIPO_Interno,
		AUDI_UsuarioEdita=@AUDI_UsuarioEdita,AUDI_FechaEdita=GETDATE()
		WHERE EQUI_Interno=@EQUI_Interno
GO

CREATE  PROCEDURE PA_EliminarEquipo
	@EQUI_Interno int,
	@AUDI_UsuarioEdita int
AS
	UPDATE Equipo SET EQUI_Eliminado=1,AUDI_UsuarioEdita=@AUDI_UsuarioEdita,AUDI_FechaEdita=GETDATE()
	 WHERE EQUI_Interno=@EQUI_Interno
GO
--PARA LLENAR UN COMBO
CREATE PROCEDURE PA_ObtenerTiposEquipo
AS
	SELECT TIPO_Interno, TIPO_Nombre FROM TipoEquipo
GO
CREATE  PROCEDURE PA_ObtenerEquipoPorID
	@EQUI_Interno INT
AS
 SELECT EQUI_Interno,EQUI_Nombre,
	EQUI_Marca, EQUI_Modelo,EQUI_Serie,EQUI_Codigo,
	EQUI_AnioFabricacion,EQUI_AnioServicio,EQUI_Estado,
	EQUI_Descripcion,TIPO_Interno
	FROM Equipo
	WHERE EQUI_Interno=@EQUI_Interno AND EQUI_Eliminado=0
GO
CREATE PROCEDURE PA_ObtenerLocalizacionPorID
	@LOCA_Interno int
AS
	SELECT LOCA_Interno,LOCA_Nombre,LOCA_NombreExtendido,LOCA_Origen
	FROM Localizacion
	WHERE LOCA_Interno=@LOCA_Interno AND LOCA_Eliminado=0
GO
CREATE PROCEDURE PA_ObtenerUltimaLocalizacionEquipo
	@EQUI_Interno INT
AS
SELECT TOP 1 e.EQUI_Interno,h.LOCA_Interno,
l.LOCA_Nombre,l.LOCA_NombreExtendido,
h.HILO_Fecha
FROM Equipo e
INNER JOIN HistorialLocalizacion h ON (e.EQUI_Interno=h.EQUI_Interno)
INNER JOIN Localizacion l ON (h.LOCA_Interno=l.LOCA_Interno)
WHERE e.EQUI_Interno=@EQUI_Interno
ORDER BY h.HILO_Fecha DESC
--PARA LOS USUARIOS---
GO
CREATE INDEX I_Usuario_ApellidoNombre
ON Usuario(USUA_Apellido,USUA_Nombre, USUA_Interno)
INCLUDE(USUA_Usuario,USUA_Direccion,USUA_Correo,USUA_Activo,
GRUP_Interno,AUDI_FechaCrea)
GO
CREATE PROCEDURE PA_ObtenerCualquierPaginaUsuarios
	@TamanioPagina int,
	@NumeroPagina int
AS
	SELECT USUA_Interno,USUA_Usuario,USUA_Nombre,USUA_Apellido,
	USUA_Direccion,USUA_Correo,USUA_Activo,GRUP_Interno,
	AUDI_UsuarioCrea,AUDI_FechaCrea 
	FROM (
			SELECT USUA_Interno,USUA_Usuario,USUA_Nombre,USUA_Apellido,
			USUA_Direccion,USUA_Correo,USUA_Activo,GRUP_Interno,
			AUDI_UsuarioCrea,AUDI_FechaCrea,ROW_NUMBER()
			OVER(ORDER BY USUA_Apellido,USUA_Nombre, USUA_Interno) AS NumeroFila
			FROM Usuario WHERE USUA_Eliminado=0
	) AS Usuarios
	WHERE NumeroFila BETWEEN @TamanioPagina * (@NumeroPagina-1) + 1
					AND @TamanioPagina * (@NumeroPagina)
GO
CREATE  PROCEDURE PA_ObtenerCualquierPaginaUsuariosFiltradoPorString
	@TamanioPagina int,
	@NumeroPagina int,
	@String nvarchar(20)
AS
	SELECT USUA_Interno,USUA_Usuario,USUA_Nombre,USUA_Apellido,
	USUA_Direccion,USUA_Correo,USUA_Activo,GRUP_Interno,
	AUDI_UsuarioCrea,AUDI_FechaCrea 
	FROM (
			SELECT USUA_Interno,USUA_Usuario,USUA_Nombre,USUA_Apellido,
			USUA_Direccion,USUA_Correo,USUA_Activo,GRUP_Interno,
			AUDI_UsuarioCrea,AUDI_FechaCrea,ROW_NUMBER()
			OVER(ORDER BY USUA_Apellido,USUA_Nombre, USUA_Interno) AS NumeroFila
			FROM Usuario WHERE USUA_Eliminado=0 
			AND 
			(USUA_Nombre+' '+USUA_Apellido LIKE @String+'%') OR
			(USUA_Nombre+' '+USUA_Apellido LIKE '%'+@String+'%' )
			
	) AS Usuarios
	WHERE NumeroFila BETWEEN @TamanioPagina * (@NumeroPagina-1) + 1
					AND @TamanioPagina * (@NumeroPagina)
GO
---
--declare @String varchar(20) ='admin';
--SELECT USUA_Interno,USUA_Usuario,USUA_Nombre,USUA_Apellido,
--			USUA_Direccion,USUA_Correo,USUA_Activo,GRUP_Interno,
--			AUDI_UsuarioCrea,AUDI_FechaCrea,ROW_NUMBER()
--			OVER(ORDER BY USUA_Apellido,USUA_Nombre, USUA_Interno) AS NumeroFila
--			FROM Usuario WHERE USUA_Eliminado=0 
--			AND 
--			(USUA_Nombre+' '+USUA_Apellido LIKE @String+'%') OR
--			(USUA_Nombre+' '+USUA_Apellido LIKE '%'+@String+'%' )
---
CREATE PROCEDURE PA_ObtenerUsuarioPorID
	@USUA_Interno int
AS
	SELECT USUA_Interno,USUA_Usuario,USUA_Nombre,USUA_Apellido,
	USUA_Direccion,USUA_Correo,USUA_Activo,GRUP_Interno,
	AUDI_UsuarioCrea,AUDI_FechaCrea
	FROM Usuario WHERE USUA_Eliminado=0 AND USUA_Interno=@USUA_Interno
GO
CREATE PROCEDURE PA_InsertarUsuario
(
	@USUA_Interno int output,
	@USUA_Usuario varchar(50),
	@USUA_Nombre varchar(50),
	@USUA_Apellido varchar(50),
	@USUA_Direccion varchar(100),
	@USUA_Correo varchar(100),
	@USUA_Activo bit,
	@USUA_Contrasenia nvarchar(300),
	@GRUP_Interno int,
	@AUDI_Usuario int
)
AS
BEGIN--para el PA
IF(@USUA_Interno IS NOT NULL AND @USUA_Contrasenia IS NULL)
BEGIN
	SET @USUA_Contrasenia = (SELECT TOP 1 USUA_Contrasenia from Usuario WHERE USUA_Interno=@USUA_Interno)
END--FIN DE IF
BEGIN TRANSACTION;
MERGE Usuario AS DESTINO
USING (SELECT @USUA_Interno AS USUA_Interno,@USUA_Usuario AS USUA_Usuario) AS ORIGEN 
ON (ORIGEN.USUA_Interno = DESTINO.USUA_Interno) 
WHEN MATCHED THEN
	UPDATE SET
	USUA_Nombre=@USUA_Nombre,USUA_Apellido=@USUA_Apellido,
	USUA_Direccion=@USUA_Direccion,USUA_Correo=@USUA_Correo,
	USUA_Activo=@USUA_Activo,USUA_Contrasenia=@USUA_Contrasenia,
	GRUP_Interno=@GRUP_Interno,
	AUDI_UsuarioEdita=@AUDI_Usuario,AUDI_FechaEdita=GETDATE()
WHEN NOT MATCHED  THEN
	INSERT (USUA_Usuario,USUA_Nombre,USUA_Apellido,
	USUA_Direccion,USUA_Correo,USUA_Activo,GRUP_Interno,
	USUA_Eliminado,USUA_Contrasenia,
	AUDI_UsuarioCrea,AUDI_FechaCrea) 
	VALUES(@USUA_Usuario,@USUA_Nombre,@USUA_Apellido,
	@USUA_Direccion,@USUA_Correo,@USUA_Activo,@GRUP_Interno,
	0,@USUA_Contrasenia,@AUDI_Usuario,GETDATE());
	IF(@@ROWCOUNT>0)
	BEGIN
		if(SCOPE_IDENTITY() IS NULL)
			set @USUA_Interno = @USUA_Interno;
		else
			set @USUA_Interno = SCOPE_IDENTITY();
		COMMIT TRANSACTION;
	END
	ELSE
	BEGIN
		set @USUA_Interno = 0;
		ROLLBACK TRANSACTION;
	END
END--FIN INSERTAR USUARIO
GO

CREATE PROCEDURE PA_ObtenerTotalUsuarios
AS
	SELECT COUNT(USUA_Interno)  AS total FROM Usuario
	WHERE USUA_Eliminado=0
GO
--
CREATE  PROCEDURE PA_ActivarUsuario
	@USUA_Interno int,
	@AUDI_UsuarioEdita int
AS
	UPDATE Usuario SET USUA_Activo=1,AUDI_UsuarioEdita=@AUDI_UsuarioEdita,AUDI_FechaEdita=GETDATE()
	 WHERE USUA_Interno=@USUA_Interno
GO
CREATE  PROCEDURE PA_DesactivarUsuario
	@USUA_Interno int,
	@AUDI_UsuarioEdita int
AS
	UPDATE Usuario SET USUA_Activo=0,AUDI_UsuarioEdita=@AUDI_UsuarioEdita,AUDI_FechaEdita=GETDATE()
	 WHERE USUA_Interno=@USUA_Interno
GO
CREATE PROCEDURE PA_EliminarUsuario
	@USUA_Interno int,
	@AUDI_UsuarioEdita int
AS
	UPDATE Usuario SET USUA_Eliminado=1,AUDI_UsuarioEdita=@AUDI_UsuarioEdita,
	USUA_Activo=0,AUDI_FechaEdita=GETDATE()
	 WHERE USUA_Interno=@USUA_Interno
GO
--declare @v bit = 0;
--UPDATE Usuario SET USUA_Eliminado=@v,AUDI_UsuarioEdita=1,AUDI_FechaEdita=GETDATE()
--	 WHERE USUA_Interno=2
GO
CREATE INDEX I_FechaIngresoPorUsuario
ON HistorialIngreso(HIIN_FechaIngreso, USUA_Interno)
INCLUDE(HIIN_Interno, HIIN_IPacceso)
GO
CREATE PROCEDURE PA_ObtenerUltimoIngresoUsuario
	@USUA_Interno int
AS
	SELECT TOP 1 HIIN_Interno,USUA_Interno,HIIN_FechaIngreso,HIIN_IPacceso FROM HistorialIngreso
	WHERE USUA_Interno=@USUA_Interno
	ORDER BY HIIN_FechaIngreso DESC
GO
CREATE PROCEDURE PA_ExisteNombreDeAcceso
	@USUA_Usuario VARCHAR(50)
AS
	SELECT TOP 1 USUA_Usuario FROM Usuario WHERE USUA_Usuario=@USUA_Usuario
GO
--para el login
CREATE PROCEDURE PA_Autenticar
	@USUA_Usuario varchar(50),
	@USUA_Contrasenia nvarchar(300)
AS
	SELECT TOP 1 USUA_Interno FROM Usuario
	WHERE USUA_Usuario=@USUA_Usuario AND USUA_Contrasenia=@USUA_Contrasenia
	AND USUA_Eliminado=0 AND USUA_Activo=1
GO
--SELECT TOP 1 USUA_Interno FROM Usuario
--	WHERE USUA_Usuario='admin' AND USUA_Contrasenia='7EjTeWh5ntH4nsUDDrnKTfdlBSbTwR2UeBy2Qp0/y4UH1yoasJeJO0FfSkvO2v3konOXrsmh0860a5Q5Pv3tyw=='
CREATE PROCEDURE PA_InsertarHistorialIngreso
	@USUA_Interno int,
	@HIIN_IPacceso varchar(15)
AS
	INSERT INTO HistorialIngreso(USUA_Interno,HIIN_FechaIngreso,HIIN_IPacceso)
	VALUES(@USUA_Interno,GETDATE(),@HIIN_IPacceso)
GO
--para el perfil del usuario
CREATE PROCEDURE PA_EditarPerfilUsuario
	@USUA_Interno int,
	@USUA_Nombre varchar(50),
	@USUA_Apellido varchar(50),
	@USUA_Direccion varchar(100),
	@USUA_Correo varchar(100),
	@USUA_Contrasenia nvarchar(300),
	@AUDI_Usuario int
AS
	IF(@USUA_Interno IS NOT NULL AND @USUA_Contrasenia IS NULL)
	BEGIN
		SET @USUA_Contrasenia = (SELECT TOP 1 USUA_Contrasenia from Usuario WHERE USUA_Interno=@USUA_Interno)
	END--FIN DE IF
	UPDATE Usuario SET
	USUA_Nombre=@USUA_Nombre,USUA_Apellido=@USUA_Apellido,
	USUA_Direccion=@USUA_Direccion,USUA_Correo=@USUA_Correo,
	USUA_Contrasenia=@USUA_Contrasenia,
	AUDI_UsuarioEdita=@AUDI_Usuario,AUDI_FechaEdita=GETDATE()
	WHERE USUA_Interno=@USUA_Interno
GO
--PARA LOS GRUPOS- ALGUNOS PA estan usados en lo que respecta alos usuarios

CREATE INDEX I_Grupo_Nombre
ON Grupo(GRUP_Nombre, GRUP_Interno)
INCLUDE(GRUP_Descripcion,GRUP_Activo,AUDI_UsuarioCrea,AUDI_FechaCrea)
GO
CREATE  PROCEDURE PA_ObtenerGrupoPorID
	@GRUP_Interno int
AS
	SELECT GRUP_Interno,GRUP_Nombre,GRUP_Descripcion,GRUP_Activo,AUDI_UsuarioCrea,AUDI_FechaCrea 
	FROM Grupo WHERE GRUP_Interno=@GRUP_Interno
GO
CREATE PROCEDURE PA_ObtenerTotalGrupos
AS
	SELECT COUNT(GRUP_Interno)  AS total FROM Grupo
	WHERE GRUP_Eliminado=0
GO
CREATE PROCEDURE PA_ObtenerGrupos
	@TamanioPagina int,
	@NumeroPagina int
AS
	SELECT GRUP_Interno,GRUP_Nombre,GRUP_Descripcion,GRUP_Activo,AUDI_UsuarioCrea,AUDI_FechaCrea 
	FROM (
			SELECT GRUP_Interno,GRUP_Nombre,GRUP_Descripcion,GRUP_Activo,AUDI_UsuarioCrea,
			AUDI_FechaCrea,ROW_NUMBER()
			OVER(ORDER BY GRUP_Nombre,GRUP_Interno) AS NumeroFila
			FROM Grupo WHERE GRUP_Eliminado=0 			
	) AS Grupos
	WHERE NumeroFila BETWEEN @TamanioPagina * (@NumeroPagina-1) + 1
					AND @TamanioPagina * (@NumeroPagina)	
GO
CREATE PROCEDURE PA_ObtenerTodosGrupos
AS
	SELECT GRUP_Interno,GRUP_Nombre,GRUP_Descripcion,GRUP_Activo,AUDI_UsuarioCrea,AUDI_FechaCrea 
	FROM Grupo WHERE GRUP_Eliminado=0
GO
CREATE  PROCEDURE PA_ObtenerGruposFiltradoPorNombre
	@TamanioPagina int,
	@NumeroPagina int,
	@String varchar(45)
AS
	SELECT GRUP_Interno,GRUP_Nombre,GRUP_Descripcion,GRUP_Activo,AUDI_UsuarioCrea,AUDI_FechaCrea 
	FROM (
			SELECT GRUP_Interno,GRUP_Nombre,GRUP_Descripcion,GRUP_Activo,AUDI_UsuarioCrea,
			AUDI_FechaCrea,ROW_NUMBER()
			OVER(ORDER BY GRUP_Nombre,GRUP_Interno) AS NumeroFila
			FROM Grupo WHERE GRUP_Eliminado=0 AND
			( GRUP_Nombre LIKE @String+'%') OR
			(GRUP_Nombre LIKE '%'+@String+'%' )			
	) AS Grupos
	WHERE NumeroFila BETWEEN @TamanioPagina * (@NumeroPagina-1) + 1
					AND @TamanioPagina * (@NumeroPagina)	
GO
CREATE PROCEDURE PA_InsertarGrupo
(
	@GRUP_Interno int output,
	@GRUP_Nombre varchar(45),
	@GRUP_Descripcion varchar(300),
	@GRUP_Activo bit,
	@AUDI_Usuario int
)
AS
BEGIN--para el PA
BEGIN TRANSACTION;
MERGE Grupo AS DESTINO
USING (SELECT @GRUP_Interno AS GRUP_Interno,@GRUP_Nombre AS GRUP_Nombre) AS ORIGEN 
ON (ORIGEN.GRUP_Interno = DESTINO.GRUP_Interno) 
WHEN MATCHED THEN
	UPDATE SET
	GRUP_Nombre=@GRUP_Nombre,GRUP_Descripcion=@GRUP_Descripcion,GRUP_Activo=@GRUP_Activo,
	AUDI_UsuarioEdita=@AUDI_Usuario,AUDI_FechaEdita=GETDATE()
WHEN NOT MATCHED  THEN
	INSERT (GRUP_Nombre,GRUP_Descripcion,GRUP_Activo,
	AUDI_UsuarioCrea,AUDI_FechaCrea,GRUP_Eliminado) 
	VALUES(@GRUP_Nombre,@GRUP_Descripcion,@GRUP_Activo,
	@AUDI_Usuario,GETDATE(),0);
	IF(@@ROWCOUNT>0)
	BEGIN
		if(SCOPE_IDENTITY() IS NULL)
			set @GRUP_Interno = @GRUP_Interno;
		else
			set @GRUP_Interno = SCOPE_IDENTITY();
		COMMIT TRANSACTION;
	END
	ELSE
	BEGIN
		set @GRUP_Interno = 0;
		ROLLBACK TRANSACTION;
	END
END --FIN INSERTAR GRUPO
GO
CREATE  PROCEDURE PA_EliminarGrupo
	@GRUP_Interno int,
	@AUDI_UsuarioEdita int
AS
	UPDATE Grupo SET GRUP_Eliminado = 1, GRUP_Activo=0,AUDI_UsuarioEdita=@AUDI_UsuarioEdita 
	WHERE GRUP_Interno=@GRUP_Interno
GO
CREATE  PROCEDURE PA_ActivarGrupo
	@GRUP_Interno int,
	@AUDI_UsuarioEdita int
AS
	UPDATE Grupo SET GRUP_Activo=1,AUDI_UsuarioEdita=@AUDI_UsuarioEdita WHERE GRUP_Interno=@GRUP_Interno
GO
CREATE  PROCEDURE PA_DesactivarGrupo
	@GRUP_Interno int,
	@AUDI_UsuarioEdita int
AS
	UPDATE Grupo SET GRUP_Activo=0,AUDI_UsuarioEdita=@AUDI_UsuarioEdita WHERE GRUP_Interno=@GRUP_Interno
GO
CREATE PROCEDURE PA_ObtenerTotalGrupos
AS
	SELECT COUNT(GRUP_Interno) AS total FROM Grupo
	WHERE GRUP_Eliminado = 0
GO
--para las tareas
CREATE UNIQUE INDEX I_NombreCortoTarea
ON Tarea(TARE_NombreCorto)
CREATE UNIQUE INDEX I_NombreModulo
ON Modulo(MODU_Nombre)
GO
CREATE PROCEDURE PA_ObtenerTareasPorModulo
AS
	SELECT m.MODU_Interno,t.TARE_Interno,m.MODU_Nombre,t.TARE_Nombre,t.TARE_NombreCorto,t.TARE_Descripcion FROM Modulo m
	INNER JOIN Tarea t ON m.MODU_Interno=t.MODU_Interno
	ORDER BY m.MODU_Orden,m.MODU_Interno,t.TARE_Interno
GO
CREATE PROCEDURE PA_EliminarTareasDeGrupo
	@GRUP_Interno int
AS
	DELETE FROM TareaGrupo WHERE GRUP_Interno=@GRUP_Interno
GO
CREATE PROCEDURE PA_InsertarTareasDeGrupo
	@GRUP_Interno int,
	@TARE_Interno int
AS
	INSERT INTO TareaGrupo(TARE_Interno,GRUP_Interno)
	VALUES (@TARE_Interno,@GRUP_Interno)
GO
CREATE PROCEDURE PA_ObtenerTareasDeGrupo
	@GRUP_Interno int
AS
	SELECT TARE_Interno,GRUP_Interno FROM TareaGrupo
	WHERE GRUP_Interno=@GRUP_Interno
GO
----para la programacion de las actividades rutinarias
--obtendremos todas las actividades rutinarias con estado 'I'
--hasta una fecha limite	
GO
CREATE PROCEDURE PA_ObtenerActividadesIniciadasParaProgramar
	@TamanioPagina int,
	@NumeroPagina int,
	@FechaLimite DateTime
AS
	SELECT NOMB_Interno,NOMB_Descripcion,ACRU_Frecuencia,ACRU_UnidadFrecuencia,HIAR_Interno,HIAR_FechaEjecucionAnterior,
	HIAR_SiguienteFecha,HIAR_FechaProgramado,HIAR_FechaEjecutado,ACRU_Interno,EQUI_Interno,LOCA_Interno
	FROM (
			SELECT n.NOMB_Interno,n.NOMB_Descripcion,a.ACRU_Frecuencia,a.ACRU_UnidadFrecuencia,h.HIAR_FechaEjecucionAnterior,
			h.HIAR_Interno,h.HIAR_SiguienteFecha,h.HIAR_FechaProgramado,h.HIAR_FechaEjecutado,h.ACRU_Interno,h.EQUI_Interno,h.LOCA_Interno, ROW_NUMBER()
			OVER(ORDER BY n.NOMB_Descripcion,n.NOMB_Interno, h.HIAR_SiguienteFecha DESC) AS NumeroFila
			FROM NombreActividad n 
			INNER JOIN ActividadRutinaria a ON n.NOMB_Interno=a.NOMB_Interno
			INNER JOIN HistorialActRutinaria h ON  a. ACRU_Interno=h.ACRU_Interno
			WHERE HIAR_Estado='I'
			AND DATEDIFF(DAY,h.HIAR_SiguienteFecha,@FechaLimite)>=0
				
	) AS ActividadesRutinarias
	WHERE NumeroFila BETWEEN @TamanioPagina * (@NumeroPagina-1) + 1
					AND @TamanioPagina * (@NumeroPagina)	
GO
CREATE PROCEDURE PA_Total_HistorialAR_Iniciadas_HastaFechaLimite
	@FechaLimite DateTime
AS
SELECT count(HIAR_Interno) AS Total
			FROM HistorialActRutinaria WHERE HIAR_Estado='I'
			AND DATEDIFF(DAY,HIAR_SiguienteFecha,@FechaLimite)>=0
GO

--SELECT HIAR_SiguienteFecha,'2013-09-22',DATEDIFF(day,HIAR_SiguienteFecha,'2013-10-22')
--			FROM HistorialActRutinaria order by HIAR_SiguienteFecha desc
--GO
 
--Para el periodo de programacion de actividade
CREATE  PROCEDURE PA_ObtenerPeriodoProgramacion
AS
SELECT TOP 1 PPRO_Interno,PPRO_Periodo,PPRO_DiaSemana,PPRO_DiaMes FROM PeriodoProgramacion
WHERE PPRO_Activo=1
GO
CREATE  PROCEDURE PA_EditarPeriodoProgramacion
	@PPRO_Periodo varchar(7),
	@PPRO_DiaSemana int,
	@PPRO_DiaMes int,
	@AUDI_UsuarioEdita int
AS
UPDATE PeriodoProgramacion SET PPRO_Activo = 0
IF (@PPRO_Periodo = 'Diario')
BEGIN
	UPDATE PeriodoProgramacion 
	SET PPRO_Activo = 1,AUDI_UsuarioEdita=@AUDI_UsuarioEdita WHERE PPRO_Periodo='Diario'
END
ELSE IF (@PPRO_Periodo = 'Semanal')
BEGIN
	UPDATE PeriodoProgramacion 
	SET PPRO_DiaSemana=@PPRO_DiaSemana,PPRO_Activo = 1,AUDI_UsuarioEdita=@AUDI_UsuarioEdita WHERE PPRO_Periodo='Semanal'
END
ELSE IF (@PPRO_Periodo = 'Mensual')
BEGIN
	UPDATE PeriodoProgramacion 
	SET PPRO_DiaMes=@PPRO_DiaMes,PPRO_Activo = 1,AUDI_UsuarioEdita=@AUDI_UsuarioEdita WHERE PPRO_Periodo='Mensual'
END
GO
CREATE PROCEDURE PA_ObtenerNombreActividadRutinaria
	@NOMB_Interno int
AS
SELECT TOP 1 NOMB_Interno,NOMB_Descripcion
FROM NombreActividad
WHERE NOMB_Interno=@NOMB_Interno
GO

--CREATE  PROCEDURE PA_EditarProximaFechaActividadRutinaria
--	@HIAR_SiguienteFecha DATETIME,
--	@HIAR_Interno INT,
--	@AUDI_UsuarioEdita int
--AS
--UPDATE HistorialActRutinaria SET HIAR_SiguienteFecha = @HIAR_SiguienteFecha, 
--AUDI_UsuarioEdita=@AUDI_UsuarioEdita,AUDI_FechaEdita=GETDATE()
--WHERE HIAR_Interno = @HIAR_Interno
GO
CREATE PROCEDURE PA_EditarFechaProgramadaActividadRutinaria
	@HIAR_FechaProgramado DATETIME,
	@HIAR_Interno INT,
	@AUDI_UsuarioEdita int
AS
UPDATE HistorialActRutinaria SET HIAR_FechaProgramado = @HIAR_FechaProgramado, 
AUDI_UsuarioEdita=@AUDI_UsuarioEdita,AUDI_FechaEdita=GETDATE()
WHERE HIAR_Interno = @HIAR_Interno
GO
CREATE PROCEDURE PA_ProgramarActividadRutinaria
	@HIAR_Interno INT,
	@AUDI_UsuarioEdita int,
	@PERI_Interno int
AS
DECLARE @HIAR_FechaProgramado datetime
--obtenemos la fecha de programacion
SET @HIAR_FechaProgramado = (SELECT TOP 1 HIAR_FechaProgramado from HistorialActRutinaria WHERE HIAR_Interno=@HIAR_Interno)
IF(@HIAR_FechaProgramado IS NULL)
BEGIN
SET @HIAR_FechaProgramado = (SELECT TOP 1 HIAR_SiguienteFecha from HistorialActRutinaria WHERE HIAR_Interno=@HIAR_Interno)
END
UPDATE HistorialActRutinaria SET HIAR_Estado = 'P', 
HIAR_FechaProgramado=@HIAR_FechaProgramado,PERI_Interno=@PERI_Interno,
AUDI_UsuarioEdita=@AUDI_UsuarioEdita,AUDI_FechaEdita=GETDATE()
WHERE HIAR_Interno = @HIAR_Interno
GO

CREATE PROCEDURE PA_ObtenerHistorialActividadRutinariaPorID
	@HIAR_Interno INT
AS
SELECT TOP 1 HIAR_Interno,HIAR_FechaProgramado,HIAR_Ejecutor,HIAR_FechaEjecutado,HIAR_SiguienteFecha,
HIAR_Observacion,HIAR_Estado,ACRU_Interno,LOCA_Interno,EQUI_Interno FROM HistorialActRutinaria
WHERE HIAR_Interno=@HIAR_Interno
--para el filtro de actividades rutinaraias por localizacion
GO
CREATE PROCEDURE PA_ObtenerActividadesRutinariasIniciadasPorLocalizacionConFechaLimite
	@TamanioPagina int,
	@NumeroPagina int,
	@LOCA_Interno int,
	@FechaLimite DateTime
AS
	SELECT NOMB_Interno,NOMB_Descripcion,ACRU_Frecuencia,ACRU_UnidadFrecuencia,HIAR_Interno,HIAR_FechaEjecucionAnterior,
	HIAR_SiguienteFecha,HIAR_FechaProgramado,HIAR_FechaEjecutado,ACRU_Interno,EQUI_Interno,LOCA_Interno
	FROM (
			SELECT  n.NOMB_Interno,n.NOMB_Descripcion,a.ACRU_Frecuencia,a.ACRU_UnidadFrecuencia,h.HIAR_FechaEjecucionAnterior,
			h.HIAR_Interno,h.HIAR_SiguienteFecha,h.HIAR_FechaProgramado,h.HIAR_FechaEjecutado,h.ACRU_Interno,hl.EQUI_Interno,h.LOCA_Interno,
			MAX(hl.HILO_Fecha) AS HILO_Fecha,
			ROW_NUMBER()OVER(ORDER BY n.NOMB_Descripcion,n.NOMB_Interno, h.HIAR_SiguienteFecha DESC) AS NumeroFila 
			FROM NombreActividad n
			INNER JOIN ActividadRutinaria a ON n.NOMB_Interno = a. NOMB_Interno
			INNER JOIN HistorialActRutinaria h ON a.ACRU_Interno=h.ACRU_Interno
			LEFT OUTER JOIN Equipo e ON h.EQUI_Interno = e.EQUI_Interno
			LEFT OUTER JOIN HistorialLocalizacion hl ON e.EQUI_Interno = hl.EQUI_Interno
			WHERE h.HIAR_Estado='I' AND ((hl.LOCA_Interno=@LOCA_Interno AND e.EQUI_Eliminado = 0) OR h.LOCA_Interno=@LOCA_Interno)
			AND DATEDIFF(DAY,h.HIAR_SiguienteFecha,@FechaLimite)>=0
			GROUP BY hl.EQUI_Interno,h.LOCA_Interno,h.ACRU_Interno,h.HIAR_Interno,h.HIAR_SiguienteFecha,
			h.HIAR_FechaProgramado,h.HIAR_FechaEjecutado,h.HIAR_FechaEjecucionAnterior,a.ACRU_Frecuencia,a.ACRU_UnidadFrecuencia,
			n.NOMB_Interno,n.NOMB_Descripcion			
	) AS ActividadesRutinarias
	WHERE NumeroFila BETWEEN @TamanioPagina * (@NumeroPagina-1) + 1
					AND @TamanioPagina * (@NumeroPagina)	
GO
GO
CREATE  PROCEDURE PA_TotalActividadesRutinariasIniciadasPorLocalizacionConFechaLimite
	@FechaLimite DateTime,
	@LOCA_Interno int
AS
	SELECT  count(h.ACRU_Interno) AS cantidad 
	FROM NombreActividad n
			INNER JOIN ActividadRutinaria a ON n.NOMB_Interno = a. NOMB_Interno
			INNER JOIN HistorialActRutinaria h ON a.ACRU_Interno=h.ACRU_Interno
			LEFT OUTER JOIN Equipo e ON h.EQUI_Interno = e.EQUI_Interno
			LEFT OUTER JOIN HistorialLocalizacion hl ON e.EQUI_Interno = hl.EQUI_Interno
	WHERE h.HIAR_Estado='I' AND ((hl.LOCA_Interno=@LOCA_Interno AND e.EQUI_Eliminado = 0) OR h.LOCA_Interno=@LOCA_Interno)
	AND DATEDIFF(DAY,h.HIAR_SiguienteFecha,@FechaLimite)>=0
	GROUP BY hl.EQUI_Interno,h.ACRU_Interno,h.HIAR_Interno,h.HIAR_SiguienteFecha,
	a.ACRU_Frecuencia,a.ACRU_UnidadFrecuencia,
	n.NOMB_Interno,n.NOMB_Descripcion				
GO
--para el numero de semana
CREATE INDEX I_NumeroSemana_Anio
ON Programa(PERI_NumSemana, PERI_Anio)
GO
CREATE PROCEDURE PA_ObtenerIDProgramaSemanal
	@PERI_NumSemana int,
	@PERI_Anio int
AS
	SELECT TOP 1 PERI_Interno FROM Programa WHERE PERI_NumSemana=@PERI_NumSemana AND PERI_Anio=@PERI_Anio
GO
CREATE PROCEDURE PA_InsertarProgramaSemanal
	@PERI_Interno int output,
	@PERI_NumSemana int,
	@PERI_Anio int
AS
BEGIN--para el PA
MERGE Programa AS DESTINO
USING (SELECT @PERI_NumSemana AS PERI_NumSemana,@PERI_Anio AS PERI_Anio) AS ORIGEN 
ON (ORIGEN.PERI_NumSemana = DESTINO.PERI_NumSemana AND 
ORIGEN.PERI_Anio = DESTINO.PERI_Anio)
WHEN NOT MATCHED  THEN
	INSERT (PERI_NumSemana,PERI_Anio) 
	VALUES(@PERI_NumSemana,@PERI_Anio);
	
	if(SCOPE_IDENTITY() IS NULL)
		set @PERI_Interno = 0;
	else
		set @PERI_Interno = SCOPE_IDENTITY();
END --FIN PA_InsertarProgramaSemanal
----PARA la ejecucion de las actividades rutinarias
CREATE PROCEDURE PA_ObtenerActividadesRProgramadas
	@TamanioPagina int,
	@NumeroPagina int,
	@FechaLimite DateTime
AS
	SELECT NOMB_Interno,NOMB_Descripcion,ACRU_Frecuencia,ACRU_UnidadFrecuencia,HIAR_Interno,HIAR_FechaEjecucionAnterior,
	HIAR_SiguienteFecha,HIAR_FechaProgramado,HIAR_FechaEjecutado,ACRU_Interno,EQUI_Interno,LOCA_Interno
	FROM (
			SELECT n.NOMB_Interno,n.NOMB_Descripcion,a.ACRU_Frecuencia,a.ACRU_UnidadFrecuencia,h.HIAR_FechaEjecucionAnterior,
			h.HIAR_Interno,h.HIAR_SiguienteFecha,h.HIAR_FechaProgramado,h.HIAR_FechaEjecutado,h.ACRU_Interno,h.EQUI_Interno,h.LOCA_Interno, ROW_NUMBER()
			OVER(ORDER BY n.NOMB_Descripcion,n.NOMB_Interno, h.HIAR_SiguienteFecha DESC) AS NumeroFila
			FROM NombreActividad n 
			INNER JOIN ActividadRutinaria a ON n.NOMB_Interno=a.NOMB_Interno
			INNER JOIN HistorialActRutinaria h ON  a. ACRU_Interno=h.ACRU_Interno
			WHERE HIAR_Estado='P'
			AND DATEDIFF(DAY,h.HIAR_FechaProgramado,@FechaLimite)>=0
				
	) AS ActividadesRutinarias
	WHERE NumeroFila BETWEEN @TamanioPagina * (@NumeroPagina-1) + 1
					AND @TamanioPagina * (@NumeroPagina)	
GO
CREATE PROCEDURE PA_Total_ActividadesR_Programadas
	@FechaLimite DateTime
AS
SELECT count(HIAR_Interno) AS Total
			FROM HistorialActRutinaria WHERE HIAR_Estado='P'
			AND DATEDIFF(DAY,HIAR_FechaProgramado,@FechaLimite)>=0
GO
CREATE PROCEDURE PA_ObtenerActividadesRutinariasProgramadasPorLocalizacionConFechaLimite
	@TamanioPagina int,
	@NumeroPagina int,
	@LOCA_Interno int,
	@FechaLimite DateTime
AS
	SELECT NOMB_Interno,NOMB_Descripcion,ACRU_Frecuencia,ACRU_UnidadFrecuencia,HIAR_Interno,HIAR_FechaEjecucionAnterior,
	HIAR_SiguienteFecha,HIAR_FechaProgramado,HIAR_FechaEjecutado,ACRU_Interno,EQUI_Interno,LOCA_Interno
	FROM (
			SELECT  n.NOMB_Interno,n.NOMB_Descripcion,a.ACRU_Frecuencia,a.ACRU_UnidadFrecuencia,h.HIAR_FechaEjecucionAnterior,
			h.HIAR_Interno,h.HIAR_SiguienteFecha,h.HIAR_FechaProgramado,h.HIAR_FechaEjecutado,h.ACRU_Interno,hl.EQUI_Interno,h.LOCA_Interno,
			MAX(hl.HILO_Fecha) AS HILO_Fecha,
			ROW_NUMBER()OVER(ORDER BY n.NOMB_Descripcion,n.NOMB_Interno, h.HIAR_SiguienteFecha DESC) AS NumeroFila 
			FROM NombreActividad n
			INNER JOIN ActividadRutinaria a ON n.NOMB_Interno = a. NOMB_Interno
			INNER JOIN HistorialActRutinaria h ON a.ACRU_Interno=h.ACRU_Interno
			LEFT OUTER JOIN Equipo e ON h.EQUI_Interno = e.EQUI_Interno
			LEFT OUTER JOIN HistorialLocalizacion hl ON e.EQUI_Interno = hl.EQUI_Interno
			WHERE h.HIAR_Estado='P' AND ((hl.LOCA_Interno=@LOCA_Interno AND e.EQUI_Eliminado = 0) OR h.LOCA_Interno=@LOCA_Interno)
			AND DATEDIFF(DAY,h.HIAR_FechaProgramado,@FechaLimite)>=0
			GROUP BY hl.EQUI_Interno,h.LOCA_Interno,h.ACRU_Interno,h.HIAR_Interno,h.HIAR_SiguienteFecha,
			h.HIAR_FechaProgramado,h.HIAR_FechaEjecucionAnterior,h.HIAR_FechaEjecutado,a.ACRU_Frecuencia,a.ACRU_UnidadFrecuencia,
			n.NOMB_Interno,n.NOMB_Descripcion			
	) AS ActividadesRutinarias
	WHERE NumeroFila BETWEEN @TamanioPagina * (@NumeroPagina-1) + 1
					AND @TamanioPagina * (@NumeroPagina)	
GO
GO
CREATE PROCEDURE PA_TotalActividadesRutinariasProgramadasPorLocalizacionConFechaLimite
	@FechaLimite DateTime,
	@LOCA_Interno int
AS
	SELECT  count(h.ACRU_Interno) AS cantidad 
	FROM NombreActividad n
			INNER JOIN ActividadRutinaria a ON n.NOMB_Interno = a. NOMB_Interno
			INNER JOIN HistorialActRutinaria h ON a.ACRU_Interno=h.ACRU_Interno
			LEFT OUTER JOIN Equipo e ON h.EQUI_Interno = e.EQUI_Interno
			LEFT OUTER JOIN HistorialLocalizacion hl ON e.EQUI_Interno = hl.EQUI_Interno
	WHERE h.HIAR_Estado='P' AND ((hl.LOCA_Interno=@LOCA_Interno AND e.EQUI_Eliminado = 0) OR h.LOCA_Interno=@LOCA_Interno)
	AND DATEDIFF(DAY,h.HIAR_FechaProgramado,@FechaLimite)>=0
	GROUP BY hl.EQUI_Interno,h.ACRU_Interno,h.HIAR_Interno,h.HIAR_SiguienteFecha,
	a.ACRU_Frecuencia,a.ACRU_UnidadFrecuencia,
	n.NOMB_Interno,n.NOMB_Descripcion				
GO
CREATE PROCEDURE PA_EjecutarActividadRutinaria
	@HIAR_Interno INT,
	@AUDI_UsuarioEdita int,
	@HIAR_FechaEjecutado DateTime
AS
UPDATE HistorialActRutinaria SET HIAR_Estado = 'E', 
HIAR_FechaEjecutado=@HIAR_FechaEjecutado,
AUDI_UsuarioEdita=@AUDI_UsuarioEdita,AUDI_FechaEdita=GETDATE()
WHERE HIAR_Interno = @HIAR_Interno
GO
CREATE PROCEDURE PA_AnularEjecutarActividadRutinaria
	@HIAR_Interno INT,
	@AUDI_UsuarioEdita int
AS
UPDATE HistorialActRutinaria SET HIAR_Estado = 'P', 
HIAR_FechaEjecutado=NULL,
AUDI_UsuarioEdita=@AUDI_UsuarioEdita,AUDI_FechaEdita=GETDATE()
WHERE HIAR_Interno = @HIAR_Interno
GO
---para extraer el historial de actividadaes ejecutadas-----------------------*****
CREATE PROCEDURE PA_ObtenerActividadesREjecutadas
	@TamanioPagina int,
	@NumeroPagina int,
	@FechaInicio DateTime,
	@FechaFin DateTime
AS
	SELECT NOMB_Interno,NOMB_Descripcion,ACRU_Frecuencia,ACRU_UnidadFrecuencia,HIAR_Interno,HIAR_FechaEjecucionAnterior,
	HIAR_SiguienteFecha,HIAR_FechaProgramado,HIAR_FechaEjecutado,ACRU_Interno,EQUI_Interno,LOCA_Interno
	FROM (
			SELECT n.NOMB_Interno,n.NOMB_Descripcion,a.ACRU_Frecuencia,a.ACRU_UnidadFrecuencia,h.HIAR_FechaEjecucionAnterior,
			h.HIAR_Interno,h.HIAR_SiguienteFecha,h.HIAR_FechaProgramado,h.HIAR_FechaEjecutado,h.ACRU_Interno,h.EQUI_Interno,h.LOCA_Interno, ROW_NUMBER()
			OVER(ORDER BY n.NOMB_Descripcion,n.NOMB_Interno, h.HIAR_SiguienteFecha DESC) AS NumeroFila
			FROM NombreActividad n 
			INNER JOIN ActividadRutinaria a ON n.NOMB_Interno=a.NOMB_Interno
			INNER JOIN HistorialActRutinaria h ON  a. ACRU_Interno=h.ACRU_Interno
			WHERE HIAR_Estado='E'
			AND HIAR_FechaEjecutado >= @FechaInicio AND HIAR_FechaEjecutado <= @FechaFin
				
	) AS ActividadesRutinarias
	WHERE NumeroFila BETWEEN @TamanioPagina * (@NumeroPagina-1) + 1
					AND @TamanioPagina * (@NumeroPagina)	
GO
CREATE PROCEDURE PA_Total_ActividadesR_EjecutadasEntreFechas
	@FechaInicio DateTime,
	@FechaFin DateTime
AS
SELECT count(HIAR_Interno) AS Total
			FROM HistorialActRutinaria WHERE HIAR_Estado='E'
			AND HIAR_FechaEjecutado >= @FechaInicio AND HIAR_FechaEjecutado <= @FechaFin
GO
CREATE  PROCEDURE PA_ObtenerActividadesRutinariasEjecutadasPorLocalizacion
	@TamanioPagina int,
	@NumeroPagina int,
	@LOCA_Interno int,
	@FechaInicio DateTime,
	@FechaFin DateTime
AS
	SELECT NOMB_Interno,NOMB_Descripcion,ACRU_Frecuencia,ACRU_UnidadFrecuencia,HIAR_Interno,HIAR_FechaEjecucionAnterior,
	HIAR_SiguienteFecha,HIAR_FechaProgramado,HIAR_FechaEjecutado,ACRU_Interno,EQUI_Interno,LOCA_Interno
	FROM (
			SELECT  n.NOMB_Interno,n.NOMB_Descripcion,a.ACRU_Frecuencia,a.ACRU_UnidadFrecuencia,h.HIAR_FechaEjecucionAnterior,
			h.HIAR_Interno,h.HIAR_SiguienteFecha,h.HIAR_FechaProgramado,h.HIAR_FechaEjecutado,h.ACRU_Interno,hl.EQUI_Interno,h.LOCA_Interno,
			MAX(hl.HILO_Fecha) AS HILO_Fecha,
			ROW_NUMBER()OVER(ORDER BY n.NOMB_Descripcion,n.NOMB_Interno, h.HIAR_SiguienteFecha DESC) AS NumeroFila 
			FROM NombreActividad n
			INNER JOIN ActividadRutinaria a ON n.NOMB_Interno = a. NOMB_Interno
			INNER JOIN HistorialActRutinaria h ON a.ACRU_Interno=h.ACRU_Interno
			LEFT OUTER JOIN Equipo e ON h.EQUI_Interno = e.EQUI_Interno
			LEFT OUTER JOIN HistorialLocalizacion hl ON e.EQUI_Interno = hl.EQUI_Interno
			WHERE h.HIAR_Estado='E' AND ((hl.LOCA_Interno=@LOCA_Interno AND e.EQUI_Eliminado = 0) OR h.LOCA_Interno=@LOCA_Interno)
			AND HIAR_FechaEjecutado >= @FechaInicio AND HIAR_FechaEjecutado <= @FechaFin
			GROUP BY hl.EQUI_Interno,h.LOCA_Interno,h.ACRU_Interno,h.HIAR_Interno,h.HIAR_SiguienteFecha,
			h.HIAR_FechaProgramado,h.HIAR_FechaEjecucionAnterior,h.HIAR_FechaEjecutado,a.ACRU_Frecuencia,a.ACRU_UnidadFrecuencia,
			n.NOMB_Interno,n.NOMB_Descripcion			
	) AS ActividadesRutinarias
	WHERE NumeroFila BETWEEN @TamanioPagina * (@NumeroPagina-1) + 1
					AND @TamanioPagina * (@NumeroPagina)	
GO
GO
CREATE  PROCEDURE PA_TotalActividadesR_EjecutadasPorLocalizacion_EntreFechas
	@FechaInicio DateTime,
	@FechaFin DateTime,
	@LOCA_Interno int
AS
	SELECT  count(h.ACRU_Interno) AS cantidad 
	FROM NombreActividad n
			INNER JOIN ActividadRutinaria a ON n.NOMB_Interno = a. NOMB_Interno
			INNER JOIN HistorialActRutinaria h ON a.ACRU_Interno=h.ACRU_Interno
			LEFT OUTER JOIN Equipo e ON h.EQUI_Interno = e.EQUI_Interno
			LEFT OUTER JOIN HistorialLocalizacion hl ON e.EQUI_Interno = hl.EQUI_Interno
	WHERE h.HIAR_Estado='E' AND ((hl.LOCA_Interno=@LOCA_Interno AND e.EQUI_Eliminado = 0) OR h.LOCA_Interno=@LOCA_Interno)
	AND HIAR_FechaEjecutado >= @FechaInicio AND HIAR_FechaEjecutado <= @FechaFin
	GROUP BY hl.EQUI_Interno,h.ACRU_Interno,h.HIAR_Interno,h.HIAR_SiguienteFecha,
	a.ACRU_Frecuencia,a.ACRU_UnidadFrecuencia,
	n.NOMB_Interno,n.NOMB_Descripcion				
GO



