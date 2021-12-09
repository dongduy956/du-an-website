use master 
go
if  exists(select *from sys.databases where name='nonbaohiemviettin')
drop database nonbaohiemviettin
go
create database nonbaohiemviettin
go
use nonbaohiemviettin
go
---------------------------------------------------------Tạo bảng-------------------------------------------------------------
---tạo bảng quyền
create table [role]
(
	id int identity not null primary key,
	name varchar(20)
)

go
--tạo bảng tài khoản
create table accounts
(
	id int identity not null primary key,
	idrole int default 1,
	username varchar(50),
	[password] varchar(100) not null,
	[image] nvarchar(500),
	fullname nvarchar(100),
	email varchar(50),
	phone varchar(11),
	[address] nvarchar(100),
	[status] bit default 1,
	constraint fk_accounts_role foreign key(idrole) references [role](id)
)
go
---tạo bảng nhóm sản phẩm
create table groupproduct
(
	id int identity not null primary key,
	name nvarchar(100),
	metatitle varchar(100),
	[status] bit default 1
)
go
---tạo bảng danh mục
create table category
(
	id int identity not null primary key,
	name nvarchar(100),
	metatitle varchar(100),----url seo
	[status] bit default 1

)
---tạo bảng hãng sản xuất
create table production
(
	id int identity not null primary key,
	name nvarchar(100),
	metatitle varchar(100),----url seo
	[status] bit default 1
)
---tạo bảng sản phẩm
create table products
(	
	id int identity not null primary key,
	name nvarchar(100),
	metatitle varchar(100),
	[status] bit default 1,
	price decimal(18,0),
	promationprice decimal(18,0) default 0,----giá khuyến mãi nếu giá này lớn 0 giá cũ (price) sẽ bị gạch
	quantity int default 50,	
	[description] ntext,
	viewcount int default 0,
	createddate datetime default getdate(),
	[image] nvarchar(500),
	fastsell bit default 1,
	newproduct bit default 1,
	idcategory int,
	idproduction int,
	idgroupproduct int,
	constraint fk_products_category foreign key(idcategory) references category(id),
	constraint fk_products_groupproduct foreign key(idgroupproduct) references groupproduct(id),
	constraint fk_products_production foreign key(idproduction) references production(id)
)
-------------------------------------------------Insert dữ liệu----------------------------------------------------------
--insert dữ liệu vào bảng quyền
go
insert into [role] values('user')
insert into [role] values('admin')
go
---insert dữ liệu vào bảng tài khoản
----------admin
insert into accounts(idrole,username,[password]) values(2,'admin','21232f297a57a5a743894a0e4a801fc3')
----------user
insert into accounts(username,[password],fullname,email,phone,[address]) values('dongduy','202cb962ac59075b964b07152d234b70',N'Dương Đông Duy','dongduy0612@gmail.com','0376880903',N'Tiền Giang')
---insert dữ liệu vào bảng nhóm sản phẩm
insert into groupproduct(name,metatitle) values(N'Đồ bảo hộ','do-bao-ho')---id:1
insert into groupproduct(name,metatitle) values(N'Nón 3 phần 4','non-3-phan-4')---id:2
insert into groupproduct(name,metatitle) values(N'Nón bảo hiểm lật cầm','non-bao-hiem-lat-cam')---id:3
insert into groupproduct(name,metatitle) values(N'Nón fullface','non-full-face')
insert into groupproduct(name,metatitle) values(N'Nón nữa đầu','non-nua-dau')
insert into groupproduct(name,metatitle) values(N'Nón nữa đầu có kính','non-nua-dau-co-kinh')
insert into groupproduct(name,metatitle) values(N'Nón xe đạp','non-xe-dap')
insert into groupproduct(name,metatitle) values(N'Phụ kiện','phu-kien')
--insert dữ liệu vào bảng danh mục
insert into category(name,metatitle) values(N'Nón cho nam','non-cho-nam')--id:1
insert into category(name,metatitle) values(N'Nón cho nữ','non-cho-nu')---id:2
insert into category(name,metatitle) values(N'Nón người lớn và trẻ nhỏ','non-nguoi-lon-va-tre-nho')--id:3
insert into category(name,metatitle) values(N'Trẻ nhỏ','tre-nho')
--insert dữ liệu vào bảng hãng sản xuất
insert into production(name,metatitle) values(N'AC','ac')-------id:1
insert into production(name,metatitle) values(N'Agu','agu')-----id:2
insert into production(name,metatitle) values(N'Andes','andes')---id:3
insert into production(name,metatitle) values(N'Asia','asia')---id:4
insert into production(name,metatitle) values(N'JC','jc')
insert into production(name,metatitle) values(N'Nón sơn','non-son')
insert into production(name,metatitle) values(N'ROC','roc')
insert into production(name,metatitle) values(N'Royal','royal')
--insert dữ liệu vào bảng sản phẩm	
insert into products(name,metatitle,price,[description],idcategory,idproduction,idgroupproduct,[image],promationprice) values(N'AC01 đen cam','ac01-den-cam',50000,N'mô tả ac01 đen cam',1,1,4,N'AC01 đen cam.jpg',33333)
insert into products(name,metatitle,price,[description],idcategory,idproduction,idgroupproduct,[image]) values(N'AC01 đen đỏ','ac01-den-do',50000,N'mô tả ac01 đen đỏ',1,1,4,N'AC01 đen đỏ.jpg')
insert into products(name,metatitle,price,[description],idcategory,idproduction,idgroupproduct,[image]) values(N'AC01 đen xanh lá','ac01-den-xanh-la',50000,N'mô tả ac01 đen xanh lá',1,1,4,N'AC01 đen xanh lá.jpg')
insert into products(name,metatitle,price,[description],idcategory,idproduction,idgroupproduct,[image]) values(N'AC01 đen xanh','ac01-den-xanh',50000,N'mô tả ac01 đen xanh',1,1,4,N'AC01 đen xanh.jpg')
insert into products(name,metatitle,price,[description],idcategory,idproduction,idgroupproduct,[image]) values(N'Agu 46 đỏ','agu-46-do',50000,N'mô tả agu 46 đỏ',1,2,4,N'agu 46 do.jpg')