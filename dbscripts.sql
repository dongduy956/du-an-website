use master 
go
if  exists(select *from sys.databases where name='nonbaohiemviettin')
drop database nonbaohiemviettin
go
create database nonbaohiemviettin
go
use nonbaohiemviettin
go
---create table permission
create table permission
(
	id int identity not null primary key,
	name varchar(20)
)
--insert data table permission
go
insert into permission values('user')
insert into permission values('admin')
go
--create table accounts
create table accounts
(
	id int identity not null primary key,
	idpermission int default 1,
	username varchar(50),
	[password] varchar(100) not null,
	[image] varchar(500),
	fullname nvarchar(100),
	email varchar(50),
	phone varchar(11),
	[address] nvarchar(100),
	constraint fk_accounts_permission foreign key(idpermission) references permission(id)
)
go
---insert data table accounts
----------admin
insert into accounts(idpermission,username,[password]) values(2,'admin','21232f297a57a5a743894a0e4a801fc3')
----------user
insert into accounts(username,[password],fullname,email,phone,[address]) values('dongduy','202cb962ac59075b964b07152d234b70',N'Dương Đông Duy','dongduy0612@gmail.com','0376880903',N'Tiền Giang')

