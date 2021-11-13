use Akamika
go
create function fnSEG_CifrarClave(
@prmstrClave varchar(20))
returns varbinary(8000)
as
begin
declare @password varbinary(8000)
set  @password = ENCRYPTBYPASSPHRASE('oadkairmfaikca',@prmstrClave)
return @password
end
go
create function fnSEG_DesCifrarClave(
@prmstrClave varbinary(8000))
returns varchar(20)
as
begin
declare @password varchar(20)
set  @password = DECRYPTBYPASSPHRASE('oadkairmfaikca',@prmstrClave)
return @password
end
go