create database Serprofing
use Serprofing

create table Oficina(
idOficina int primary key identity,
descripcion varchar(100),
telefono int,
direccion varchar(100)
)

create table empleado(
idEmpleado int primary key identity,
nombre varchar(100),
dui int,
direccion varchar(100),
telefono int,
fechaIngreso date,
activo int,
idOficina int,
foreign key (idOficina) references Oficina(idOficina)
)
 
insert into oficina values('Oficinas administrativas',22162216,'Colonia las almendras, poligono b2, local 53.')
insert into empleado values('Roberto Sanchez',051781256,'Colonia San Jose, Pasaje #3, Casa#21, San Salvador',22552255,'12/10/2018',1,1)

select empleado.idEmpleado, empleado.nombre, empleado.dui, empleado.direccion, empleado.telefono, empleado.fechaIngreso, empleado.activo, empleado.idOficina, Oficina.descripcion
from empleado, Oficina
where empleado.idOficina=Oficina.idOficina