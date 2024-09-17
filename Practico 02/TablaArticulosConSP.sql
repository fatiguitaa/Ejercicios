create table Articulos(
idArticulo identity(1, 1),
nombre varchar(30) not null,
precioUnitario decimal not null,
constraint pkArticulos primary key (idArticulo))

create proc ObtenerArticulos
as
begin
	select * from Articulos
end

create proc ObtenerArticuloPorId @idArticulo int
as
begin
	select * from Articulos where idArticulo = @idArticulo
end

create proc CrearArticulo @nombre varchar(30), @precioUnitario decimal
as
begin
	insert into Articulos(nombre, precioUnitario) values (@nombre, @precioUnitario)
end

create proc ModificarArticulo @idArticulo int, @nombre varchar(30), @precioUnitario decimal
as
begin
	update Articulos set nombre = @nombre, precioUnitario = @precioUnitario where idArticulo = @idArticulo
end

create proc EliminarArticulo @idArticulo int
as
begin
	delete from Articulos where idArticulo = @idArticulo
end