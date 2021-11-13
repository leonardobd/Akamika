use Akamika
go
insert into 
Persona(
PersonaNombres,PersonaApellidos,PersonaSexo,PersonaFoto,
PersonaFechaNacimiento)
values('Leonardo','Burgos','M','imagen.jpg','27/03/1996')
go
insert into 
Persona(
PersonaNombres,PersonaApellidos,PersonaSexo,PersonaFoto,
PersonaFechaNacimiento)
values('Marco Joshep','Monalvo Carranza','M','imagen.jpg','02/06/1990')
go
insert into 
Persona(
PersonaNombres,PersonaApellidos,PersonaSexo,PersonaFoto,
PersonaFechaNacimiento)
values('Carlos Mario','Prado Ávila','M','imagen.jpg','13/03/1992')
go
insert into TipoUsuario(TipoUsuarioNombre)
values('Cliente')
go
insert into TipoUsuario(TipoUsuarioNombre)
values('Administrador')
go
insert into Usuario(UsuarioCorreo,UsuarioPassw,PersonaID,UsuarioTipo,UsuarioEstado)
values('lbd_2014@hotmail.com',dbo.fnSEG_CifrarClave('1234567890'),1,1,'AC')
go
insert into Usuario(UsuarioCorreo,UsuarioPassw,PersonaID,UsuarioTipo,UsuarioEstado)
values('mar_montalvo@gmail.com',dbo.fnSEG_CifrarClave('1234567890'),2,2,'AC')
go
insert into Usuario(UsuarioCorreo,UsuarioPassw,PersonaID,UsuarioTipo,UsuarioEstado)
values('carlos_prado@hotmail.com',dbo.fnSEG_CifrarClave('1234567890'),3,1,'AC')
go
insert into Categoria(CategoriaNombre,UsuarioRegistroC)
values('Tecnología',1)
go
insert into Categoria(CategoriaNombre,UsuarioRegistroC)
values('Diseño',1)
go
insert into Curso(CursoNombre,CategoriaID,CursoDescripción,CursoImagen)
values('ASP.NET con Razor y C#',1,'En este curso aprenderemos de las tecnologías Microsoft ASP.NET, de estructurado y programación 
en 4 capas.','ASP.NET.png')
go
insert into Curso(CursoNombre,CategoriaID,CursoDescripción,CursoImagen)
values('PHP MVC - Laravel',1,'En este curso aprenderemos un poco sobre software libre. Php últimamente ha aumentado su demanda
por lo que es importante estar informado sobre esta tecnología, que junto con el framework laravel son una potencia para las webs.',
'PHP.png')
go
insert into Sesion(SesionTitulo,SesionVideo,CursoID)
values('Estandarizando el proyecto','https://www.youtube.com/embed/i1Kb0yS3rSE',1)
go
insert into CursoRegistro(SesionID,UsuarioID)
values(1,1)
insert into Comentario(UsuarioID,CursoID,ComentarioDetalle) values(1,1,'¡Me encanta el curso! Deberían hacer mas cursos hablando sobre tecnologías Microsoft.'),
(3,2,'Muy recomendado para todos'),(3,2,'Pongan más cursos que tenga que ver con software libre.')
go
insert into Sesion(SesionTitulo,SesionDescripción,SesionVideo,CursoID) values('Estandarizando proyecto','No hay descripción','https://www.youtube.com/embed/i1Kb0yS3rSE',2)