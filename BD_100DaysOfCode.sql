create database BD_100DaysOfCode
GO
use BD_100DaysOfCode

create table productos (
	id int not null primary key identity,
	codigo varchar(13) not null,
	nombre varchar(50) not null,
	cantidad int not null,
	precio decimal(8,2) not null,
	rutaImagen varchar(200) not null
)
GO

create procedure AGREGAR_PRODUCTO
	@nombre varchar(50),
	@cantidad int,
	@precio decimal(8,2),
	@rutaImagen varchar(200)
AS
BEGIN
declare @codigo varchar(14) = CONCAT(FORMAT(GETDATE(), 'ddMMyyyy'), DATEDIFF(SECOND, CONVERT(DATE, GETDATE()),GETDATE())) + 
+ left(CAST((select ISNULL((SELECT MAX(id) FROM productos), 0) +1) AS VARCHAR(10)),1)
select @codigo

INSERT INTO productos (codigo, nombre, cantidad, precio, rutaImagen) 
values (@codigo, @nombre, @cantidad, @precio, @rutaImagen)
END

GO

create procedure MODIFICAR_PRODUCTO
	@id int,
	@nombre varchar(50),
	@cantidad int,
	@precio decimal(8,2),
	@rutaImagen varchar(200)
AS
BEGIN
	UPDATE productos SET nombre = @nombre, cantidad = @cantidad, precio = @precio, rutaImagen = @rutaImagen 
	WHERE id = @id
END

GO

create procedure ELIMINAR_PRODUCTO
	@id int
AS
BEGIN
	DELETE FROM productos WHERE id = @id
END

GO

create procedure BUSCAR_PRODUCTOS
	@codigo varchar(14),
	@nombre varchar(50),
	@cantidad varchar(10),
	@precio varchar(10)
AS
BEGIN
	SELECT * 
	FROM productos
	WHERE codigo like '%' + @codigo + '%' AND
		nombre like '%' + @nombre + '%' AND
		cantidad like '%' + @cantidad + '%' AND
		precio like '%' + @precio + '%'
END

--EXEC AGREGAR_PRODUCTO 'Producto', 3, 5.4, 'NoDisponible'
--EXEC MODIFICAR_PRODUCTO 1, 'Producto Modificado', 7, 20.5, 'SigueNoDisponible'
--SELECT * FROM productos
--EXEC ELIMINAR_PRODUCTO 1


--EXEC AGREGAR_PRODUCTO 'Nuevo Producto', 8, 22.50, 'NoDisponible'
--EXEC AGREGAR_PRODUCTO 'Otro Producto', 8, 21.50, 'NoDisponible'
--EXEC AGREGAR_PRODUCTO 'No es un Producto', 4, 3.50, 'NoDisponible'
--EXEC AGREGAR_PRODUCTO 'Viejo Producto', 4, 22.50, 'NoDisponible'



--EXEC BUSCAR_PRODUCTOS '12012021290741', '', '', ''
--EXEC BUSCAR_PRODUCTOS '1201202129074', 'Nuevo', '', ''
--EXEC BUSCAR_PRODUCTOS '1201202129074', '', '8', ''
--EXEC BUSCAR_PRODUCTOS '1201202129074', '', '', '22.50'

