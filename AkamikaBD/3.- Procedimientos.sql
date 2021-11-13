use Akamika
go
create procedure spLog_Sistema 
@Correo varchar(80),
@Password varchar(20)
as
set nocount on
select u.UsuarioID, u.UsuarioCorreo,u.UsuarioEstado,u.UsuarioTipo,p.PersonaApellidos,p.PersonaNombres,p.PersonaFoto,p.PersonaEstado from Usuario u,Persona p
where u.UsuarioCorreo= @Correo and dbo.fnSEG_DesCifrarClave(u.UsuarioPassw) = @Password
and UsuarioEstado != 'IN' and u.PersonaID = p.PersonaID
set nocount off
go
create procedure RegistroComentario
@IDUsuario int,
@IDCurso int,
@Comentario varchar(max)
as
Declare @h int 
begin transaction
EXEC sp_xml_preparedocument @h output
if @Comentario=null
 begin
	Rollback Transaction
	return -1
End
insert Comentario(UsuarioID,CursoID,ComentarioDetalle) values(@IDUsuario,@IDCurso,@Comentario)
EXEC sp_xml_removedocument @h
	If @@Error<>0
	Begin
		Rollback Transaction
		Return -2 /*Error no controlado*/
	End
Commit Transaction
go
create procedure RegistroUsuario 
@Nombres nvarchar(100),
@Apellidos nvarchar(100),
@Sexo nvarchar(1),
@FechaNacimiento nvarchar(8),
@Email nvarchar(80),
@Contraseña nvarchar(20),
@Foto nvarchar(100)
as
Declare @h int 
Declare @IDPersona int
begin transaction
EXEC sp_xml_preparedocument @h output
if (SELECT count(*)FROM Usuario where UsuarioCorreo = @Email)>0
	Begin
		Rollback Transaction
		return -1 /*Si retorna -1 es porque ya hay un correo agregado*/
End
if(@Foto!=null or @Foto !='')
begin
insert into Persona(PersonaNombres,PersonaApellidos,PersonaSexo,PersonaFoto,PersonaFechaNacimiento)
values(@Nombres,@Apellidos,@Sexo,@Foto,@FechaNacimiento)
set @IDPersona = @@IDENTITY    
insert into Usuario (UsuarioCorreo,UsuarioPassw,PersonaID,UsuarioTipo,UsuarioEstado)
values(@Email,dbo.fnSEG_CifrarClave(@Contraseña),@IDPersona,1,'AC')
end
EXEC sp_xml_removedocument @h
	If @@Error<>0
	Begin
		Rollback Transaction
		Return -2 /*Error no controlado*/
	End
Commit Transaction
return @IDPersona

go
create procedure spListaCategoria
as
set nocount on
select CategoriaID,CategoriaNombre,CategoriaEstado,CategoriaIcono 
from Categoria 
where CategoriaEstado!='EL'
set nocount off
go
create procedure spListaCursosPorCategoria
@CategoriaID int
as
set nocount on
select c.CursoID,c.CursoNombre,c.CursoImagen,c.CursoEstado,c.CursoDescripción,ct.CategoriaID,
ct.CategoriaImagen 
from Curso c,Categoria ct
where c.CategoriaID = @CategoriaID and c.CursoEstado!='EL' 
and c.CategoriaID = ct.CategoriaID
set nocount off
go
create procedure spListaSesion 
@CursoID int
as
set nocount on
select s.SesionID,s.SesionTitulo,s.SesionEstado,convert(date,CONVERT(varchar(10), s.FechaRegistroS, 103),103) as 'FechaRegistroS'
 from Sesion s,Curso c 
 where s.CursoID = @CursoID and s.SesionEstado != 'EL' and s.CursoID = c.CursoID 
set nocount off
go
create procedure spDetalleSesionIntranet
@SesionID int
as
select  SesionID,SesionTitulo,SesionDescripción,SesionVideo,
SesionEstado
 from Sesion  where SesionID = @SesionID
 go
create procedure spDetalleSesionUsuario 
@SesionID int,
@CursoID int
as
set nocount on
select s.SesionID,s.SesionTitulo,s.SesionDescripción,s.SesionVideo,
s.SesionEstado,c.CursoID,c.CursoNombre,cr.CursoRegistroID,cr.CursoRegistroEstado,datename(dw,s.FechaRegistroS)+' '+ datename(dd,s.FechaRegistroS) + ' de ' + datename(mm,s.FechaRegistroS) + ' de ' + datename(yy,s.FechaRegistroS) as 'FechaRegistroS'
 from Sesion s,Curso c , CursoRegistro cr
 where s.SesionEstado != 'EL' and s.CursoID = @CursoID and cr.SesionID = @SesionID and s.SesionID = @SesionID
set nocount off
go
create procedure spPerfilUsuario
@IDUsuario int
as
set nocount on
select u.UsuarioID,u.UsuarioCorreo,dbo.fnSEG_DesCifrarClave(u.UsuarioPassw), u.UsuarioPassw,
p.PersonaApellidos,p.PersonaNombres,p.PersonaSexo,p.PersonaFoto,p.PersonaFechaNacimiento,p.PersonaID
from Usuario u,Persona p 
where u.UsuarioEstado !='EL'and u.PersonaID = p.PersonaID and u.UsuarioID=@IDUsuario
set nocount off
go
create procedure spAgregarSuscripcion
@SuscritoCorreo varchar(40)
as
Declare @h int 
begin transaction
EXEC sp_xml_preparedocument @h output
if (SELECT count(*)FROM Suscrito where SuscritoCorreo = @SuscritoCorreo)>0
	Begin
		Rollback Transaction
		return -1 /*Si retorna -1 es porque ya hay un correo agregado*/
End
insert into Suscrito(SuscritoCorreo)
values(@SuscritoCorreo)
EXEC sp_xml_removedocument @h
	If @@Error<>0
	Begin
		Rollback Transaction
		Return -2 /*Error no controlado*/
	End
Commit Transaction
go
create procedure spAgregarSesionRegistro 
 @SesionID int,
 @UsuarioID int,
 @CursoID int
 as
 Declare @h int
 begin transaction
EXEC sp_xml_preparedocument @h output
if (SELECT count(*)FROM CursoRegistro where SesionID = @SesionID and UsuarioID = @UsuarioID)>0
	Begin
		Rollback Transaction
		return -1 /*Si retorna -1 es porque ya esta registrado en este curso*/
End
if(select count(*) from Sesion where SesionID=@SesionID and CursoID = @CursoID)<1
	begin 
	RollBack Transaction
	return -3 /*Si retorna -3 es porque no hay sesion creada en el curso*/
end
insert into CursoRegistro(SesionID,UsuarioID)
values(@SesionID,@UsuarioID)
EXEC sp_xml_removedocument @h
	If @@Error<>0
	Begin
		Rollback Transaction
		Return -2 /*Error no controlado*/
	End
Commit Transaction 
go
create procedure spListarCursosRegistrados
@UsuarioID int
as
select DISTINCT c.CursoID,c.CursoNombre,c.CursoImagen
from CursoRegistro cr, Sesion s,Curso c 
where cr.UsuarioID = @UsuarioID and cr.SesionID = s.SesionID and s.CursoID = c.CursoID
go
create procedure sPListarSesionesRegistradas
@UsuarioID int,
@CursoID int
as
select s.SesionTitulo
from CursoRegistro cr, Sesion s
where cr.UsuarioID = @UsuarioID and s.SesionID = cr.SesionID and s.CursoID = @CursoID
go
create procedure spListarComentarioCurso
@CursoID int
as
select u.UsuarioID,p.PersonaNombres,p.PersonaApellidos,p.PersonaFoto,c.ComentarioID,c.ComentarioDetalle,c.FechaRegistroCM 
from Comentario c, Usuario u,Persona p 
where c.CursoID = @CursoID and c.UsuarioID = u.UsuarioID and u.PersonaID = p.PersonaID
order by c.ComentarioID desc
go
create procedure spActualizarPerfil
@PersonaID int,
@UsuarioID int,
@UsuarioCorreo varchar(80),
@UsuarioPassword varchar(80),
@PersonaNombres varchar(100),
@PersonaApellidos varchar(100),
@PersonaSexo varchar(1),
@PersonaFoto varchar(100),
@PersonaFechaNacimiento date
as
Declare @h int 
begin transaction
EXEC sp_xml_preparedocument @h output
update Persona set PersonaNombres=@PersonaNombres,PersonaApellidos=@PersonaApellidos,PersonaSexo=@PersonaSexo
,PersonaFoto=@PersonaFoto,PersonaFechaNacimiento =@PersonaFechaNacimiento, FechaModificacionP = getdate() 
where PersonaID = @PersonaID
update Usuario set UsuarioCorreo=@UsuarioCorreo ,UsuarioPassw=dbo.fnSEG_CifrarClave(@UsuarioPassword),FechaModificacionC = getdate() 
where UsuarioID = @UsuarioID
EXEC sp_xml_removedocument @h
	If @@Error<>0
	Begin
		Rollback Transaction
		Return -2 /*Error no controlado*/
	End
Commit Transaction
go
create procedure spCantidadVideos
as
select count(SesionID) as 'SesionID' from Sesion where SesionEstado !='EL'
go
create procedure spComentario
as
select u.UsuarioID,p.PersonaNombres,p.PersonaApellidos,p.PersonaFoto,c.ComentarioDetalle, datename(dw,c.FechaRegistroCM)+' '+ datename(dd,c.FechaRegistroCM)+' '+datename(yy,c.FechaRegistroCM)+' a las '+CONVERT(nvarchar(5),c.FechaRegistroCM, 108) as 'FechaRegistroCM' 
from Comentario c, Usuario u,Persona p 
where c.UsuarioID = u.UsuarioID and u.PersonaID = p.PersonaID
order by FechaRegistroCM desc
go
create procedure spAgregarCurso
@TituloCurso varchar(30),
@Descripcion varchar(max),
@Imagen varchar(30),
@CategoriaID int
as
Declare @h int
 begin transaction
EXEC sp_xml_preparedocument @h output
if (SELECT count(*)FROM Curso where CursoNombre = @TituloCurso)>0
	Begin
		Rollback Transaction
		return -1 /*Si retorna -1 es porque ya hay un nombre de curso agregado*/
End
begin
insert into Curso(CursoNombre,CategoriaID,CursoDescripción,CursoImagen)
values(@TituloCurso,@CategoriaID,@Descripcion,@Imagen)
end
EXEC sp_xml_removedocument @h
	If @@Error<>0
	Begin
		Rollback Transaction
		Return -2 /*Error no controlado*/
	End
Commit Transaction
go
create procedure spListaCursos
as
set nocount on
select c.CursoID,c.CursoNombre,c.CursoImagen,c.CursoEstado,c.CursoDescripción,ct.CategoriaID,
ct.CategoriaImagen 
from Curso c,Categoria ct
where c.CursoEstado!='EL' 
and c.CategoriaID = ct.CategoriaID
set nocount off
go
create procedure spDetalleCurso
@IDCurso int
as
select CursoID,CursoDescripción,CursoImagen,CursoNombre,CategoriaID from Curso where CursoID=@IDCurso
go
create procedure spActualizarCurso
@CursoID int,
@CursoDescripción varchar(max),
@CursoImagen varchar(30),
@CursoNombre varchar(30),
@CategoriaID int
as
Declare @h int 
begin transaction
EXEC sp_xml_preparedocument @h output
update Curso set CursoDescripción=@CursoDescripción,CursoImagen=@CursoImagen,CursoNombre=@CursoNombre, FechaModificacionCU = getdate(), CategoriaID = @CategoriaID 
where CursoID = @CursoID
EXEC sp_xml_removedocument @h
	If @@Error<>0
	Begin
		Rollback Transaction
		Return -2 /*Error no controlado*/
	End
Commit Transaction
go
create procedure spSesionMasVista
as
select s.SesionID,s.SesionVideo,s.SesionTitulo 
from CursoRegistro c ,Sesion s 
where c.SesionID = s.SesionID 
group by s.SesionID,c.SesionID,s.SesionVideo,s.SesionTitulo  
Having
    Count(*) > 1
go
create procedure spEliminarCurso
@CursoID int
as
Declare @h int 
begin transaction
EXEC sp_xml_preparedocument @h output
update Curso set CursoEstado = 'EL' where CursoID = @CursoID
EXEC sp_xml_removedocument @h
	If @@Error<>0
	Begin
		Rollback Transaction
		Return -2 /*Error no controlado*/
	End
Commit Transaction
go
create procedure spEliminarSesion
@SesionID int
as
Declare @h int 
begin transaction
EXEC sp_xml_preparedocument @h output
update Sesion set SesionEstado = 'EL' where SesionID = @SesionID
EXEC sp_xml_removedocument @h
	If @@Error<>0
	Begin
		Rollback Transaction
		Return -2 /*Error no controlado*/
	End
Commit Transaction
go

create procedure spAgregarSesion
@Titulo varchar(30),
@Descripcion varchar(max),
@Video varchar(30),
@IDCurso int
as
Declare @h int
 begin transaction
EXEC sp_xml_preparedocument @h output
if (SELECT count(*)FROM Sesion where SesionTitulo = @Titulo)>0
	Begin
		Rollback Transaction
		return -1 /*Si retorna -1 es porque ya hay un nombre de sesion agregado*/
End
begin
insert into Sesion(SesionTitulo,SesionDescripción,SesionVideo,CursoID)
values(@Titulo,@Descripcion,@Video,@IDCurso)
end
EXEC sp_xml_removedocument @h
	If @@Error<>0
	Begin
		Rollback Transaction
		Return -2 /*Error no controlado*/
	End
Commit Transaction
go
create procedure spActualizarSesion
@SesionID int,
@Descripción varchar(max),
@Titulo varchar(30),
@Video varchar(30)
as
Declare @h int 
begin transaction
EXEC sp_xml_preparedocument @h output
update Sesion 
set SesionDescripción=@Descripción,SesionTitulo=@Titulo,SesionVideo=@Video, FechaModificacionS = getdate() 
where SesionID = @SesionID
EXEC sp_xml_removedocument @h
	If @@Error<>0
	Begin
		Rollback Transaction
		Return -2 /*Error no controlado*/
	End
Commit Transaction
go
create procedure spAgregarCategoria
@Nombre varchar(30),
@Imagen varchar(30)
as
Declare @h int
 begin transaction
EXEC sp_xml_preparedocument @h output
if (SELECT count(*)FROM Categoria where CategoriaNombre = @Nombre)>0
	Begin
		Rollback Transaction
		return -1 /*Si retorna -1 es porque ya hay un nombre de sesion agregado*/
End
begin
insert into Categoria(CategoriaNombre,CategoriaImagen)
values(@Nombre,@Imagen)
end
EXEC sp_xml_removedocument @h
	If @@Error<>0
	Begin
		Rollback Transaction
		Return -2 /*Error no controlado*/
	End
Commit Transaction
go
create procedure spEliminaCategoria
@CategoriaID char(2)
as
Declare @h int 
begin transaction
EXEC sp_xml_preparedocument @h output
update Categoria set CategoriaEstado='EL' where CategoriaID =@CategoriaID
EXEC sp_xml_removedocument @h
	If @@Error<>0
	Begin
		Rollback Transaction
		Return -2 /*Error no controlado*/
	End
Commit Transaction
go
create procedure spDetalleCategoria
@IDCategoria int
as
select CategoriaID,CategoriaNombre,CategoriaEstado,CategoriaIcono,FechaRegistroC 
from Categoria 
where CategoriaID=@IDCategoria
go
create procedure spActualizarCategoria
@CategoriaID int,
@CategoriaNombre varchar(30),
@CategoriaIcono varchar(30)
as
Declare @h int
begin transaction
EXEC sp_xml_preparedocument @h output
update Categoria set CategoriaNombre = @CategoriaNombre, CategoriaIcono = @CategoriaIcono where CategoriaID =@CategoriaID
EXEC sp_xml_removedocument @h
	If @@Error<>0
	Begin
		Rollback Transaction
		Return -2 /*Error no controlado*/
	End
Commit Transaction