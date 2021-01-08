create database PersonBD
go
use PersonBD
go
create table Gender(
genderId int primary key identity,
description nvarchar(20),
status smallint
)
go
create table Person(
personId int primary key identity,
genderId int foreign key(genderId) references Gender(genderId),
name nvarchar(100),
lastName nvarchar(100)
)
go
insert into gender(description,status)values('Masculino',1)
insert into gender(description,status)values('Femenino',1)
go