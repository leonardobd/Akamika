create database Akamika
go
use Akamika
go
create table Persona(
PersonaID int identity,
PersonaNombres varchar(100) not null,
PersonaApellidos varchar(100) not null,
PersonaSexo varchar(1) not null,
PersonaFoto varchar(100),
PersonaFechaRegistro datetime default getdate() not null,
PersonaEstado char(2) default 'AC' not null,
/*Auditoria*/
PersonaFechaNacimiento date not null,
UsuarioRegistroP int,
FechaModificacionP datetime,
UsuarioModificacionP int
constraint PersonaIDPK primary key(PersonaID)
)
create table TipoUsuario(
TipoUsuarioID int identity,
TipoUsuarioNombre varchar(20),
TipoUsuarioEstado char(2) default 'AC' not null,
/*Auditoria*/
FechaRegistroTU datetime default getdate(),
UsuarioRegistroTU int,
FechaModificacionTU datetime,
UsuarioModificacionTU int
constraint TipoUsuarioIDPK primary key(TipoUsuarioID)
)
go
create table Usuario(
UsuarioID int identity,
UsuarioCorreo varchar(80) not null,
UsuarioPassw varbinary(8000) not null,
UsuarioTipo int not null,
PersonaID int not null,
UsuarioEstado char(2) default 'IN' not null,
/*Auditoria*/
FechaRegistroC datetime default getdate(),
UsuarioRegistroC int,
FechaModificacionC datetime,
UsuarioModificacionC int
constraint UsuarioIDPK primary key(UsuarioID),
constraint PersonaIDFKU foreign key(PersonaID)
	references Persona(PersonaID),
constraint UsuarioTipoIDFKU foreign key(UsuarioTipo)
	references TipoUsuario(TipoUsuarioID)
)
go
create table Categoria(
CategoriaID int identity,
CategoriaNombre varchar(30) not null,
CategoriaEstado char(2) default 'AC' not null,
CategoriaImagen varchar(30) default 'noimageCat.png' not null,
CategoriaIcono varchar(30) default 'noiconCat.png' not null,
/*Auditoria*/
FechaRegistroC datetime default getdate(),
UsuarioRegistroC int,
FechaModificacionC datetime,
UsuarioModificacionC int
constraint CategoriaIDPK primary key(CategoriaID)
)
go
create table Curso(
CursoID int identity,
CursoNombre varchar(30) not null,
CursoEstado char(2) default 'AC' not null,
CategoriaID int,
CursoDescripción varchar(max),
CursoImagen varchar(30) default 'noimageCur.png' not null,
/*Auditoria*/
FechaRegistroCU datetime default getdate(),
UsuarioRegistroCU int,
FechaModificacionCU datetime,
UsuarioModificacionCU int
constraint CursoIDPK primary key(CursoID),
constraint CategoriaIDFKCU foreign key(CategoriaID)
references Categoria(CategoriaID)
)
go
create table Sesion(
SesionID int identity,
SesionTitulo varchar(30) not null,
SesionDescripción varchar(5000) default 'Sin descripción',
SesionEstado char(2) default 'AC' not null,
SesionVideo varchar(50) not null,
CursoID int,
/*Auditoria*/
FechaRegistroS datetime default getdate(),
UsuarioRegistroS int,
FechaModificacionS datetime,
UsuarioModificacionS int
constraint SesionIDPK primary key(SesionID),
constraint CursoIDFKCU foreign key(CursoID)
references Curso(CursoID)
) 
create table CursoRegistro
(
CursoRegistroID int identity,
SesionID int,
UsuarioID int,
CursoRegistroEstado char(2) default 'AS', /*AS: Asociado, TM: Terminado, PD: Pendiente */
/*Auditoria*/
FechaRegistroS datetime default getdate(),
UsuarioRegistroS int,
FechaModificacionS datetime,
UsuarioModificacionS int
constraint CursoRegistroIDPKCR primary key(CursoRegistroID),
constraint UsuarioIDFKCR foreign key (UsuarioID)
	references Usuario(UsuarioID),
constraint SesionIDFKCR foreign key(SesionID)
	references Sesion(SesionID)
)
go
create table Suscrito(
SuscritoID int identity,
SuscritoCorreo varchar(40),
/*Auditoria*/
FechaRegistroSu datetime default getdate()
constraint SuscritoIDPKSu primary key(SuscritoID)
)
go
create table Comentario(
ComentarioID int identity,
UsuarioID int,
CursoID int,
ComentarioDetalle varchar(max),
 /*Auditoria*/
FechaRegistroCM datetime default getdate(),
UsuarioRegistroCM int,
FechaModificacionCM datetime,
UsuarioModificacionCM int
constraint ComentarioIDPKCM primary key(ComentarioID),
constraint UsuarioIDFKCM foreign key(UsuarioID)
	references Usuario(UsuarioID),
constraint CursoIDFKCM foreign key(CursoID)
	references Curso(CursoID)
)