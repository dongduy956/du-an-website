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
	alias varchar(100),
	[status] bit default 1
)
go
---tạo bảng danh mục
create table category
(
	id int identity not null primary key,
	name nvarchar(100),
	alias varchar(100),----url seo
	[status] bit default 1

)
---tạo bảng hãng sản xuất
create table production
(
	id int identity not null primary key,
	name nvarchar(100),
	alias varchar(100),----url seo
	[status] bit default 1
)
---tạo bảng sản phẩm
create table products
(	
	id int identity not null primary key,
	name nvarchar(100),
	alias varchar(100),
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
insert into groupproduct(name,alias) values(N'Đồ bảo hộ','do-bao-ho')---id:1
insert into groupproduct(name,alias) values(N'Nón 3 phần 4','non-3-phan-4')---id:2
insert into groupproduct(name,alias) values(N'Nón bảo hiểm lật cầm','non-bao-hiem-lat-cam')---id:3
insert into groupproduct(name,alias) values(N'Nón fullface','non-full-face')
insert into groupproduct(name,alias) values(N'Nón nữa đầu','non-nua-dau')--- 4
insert into groupproduct(name,alias) values(N'Nón nữa đầu có kính','non-nua-dau-co-kinh')--- 5
insert into groupproduct(name,alias) values(N'Nón xe đạp','non-xe-dap')--- 6
insert into groupproduct(name,alias) values(N'Phụ kiện','phu-kien')--- 7
--insert dữ liệu vào bảng danh mục
insert into category(name,alias) values(N'Nón cho nam','non-cho-nam')--id:1
insert into category(name,alias) values(N'Nón cho nữ','non-cho-nu')---id:2
insert into category(name,alias) values(N'Nón người lớn và trẻ nhỏ','non-nguoi-lon-va-tre-nho')--id:3
insert into category(name,alias) values(N'Trẻ nhỏ','tre-nho')--- 4
--insert dữ liệu vào bảng hãng sản xuất
insert into production(name,alias) values(N'AC','ac')-------id:1
insert into production(name,alias) values(N'Agu','agu')-----id:2
insert into production(name,alias) values(N'Andes','andes')---id:3
insert into production(name,alias) values(N'Asia','asia')---id:4
insert into production(name,alias) values(N'JC','jc')--- 5
insert into production(name,alias) values(N'Nón sơn','non-son')--- 6
insert into production(name,alias) values(N'ROC','roc')--- 7
insert into production(name,alias) values(N'Royal','royal')--- 8
insert into production(name,alias) values(N'Chita','chita')--- 9
--insert dữ liệu vào bảng sản phẩm	
insert into products(name,alias,price,[description],idcategory,idproduction,idgroupproduct,[image],promationprice) values(N'AC01 đen cam','ac01-den-cam',50000,N'mô tả ac01 đen cam',1,1,4,N'AC01 đen cam.jpg',33333)--1
insert into products(name,alias,price,[description],idcategory,idproduction,idgroupproduct,[image]) values(N'AC01 đen đỏ','ac01-den-do',50000,N'mô tả ac01 đen đỏ',1,1,4,N'AC01 đen đỏ.jpg')--2
insert into products(name,alias,price,[description],idcategory,idproduction,idgroupproduct,[image]) values(N'AC01 đen xanh lá','ac01-den-xanh-la',50000,N'mô tả ac01 đen xanh lá',1,1,4,N'AC01 đen xanh lá.jpg')--3
insert into products(name,alias,price,[description],idcategory,idproduction,idgroupproduct,[image]) values(N'AC01 đen xanh','ac01-den-xanh',50000,N'mô tả ac01 đen xanh',1,1,4,N'AC01 đen xanh.jpg')--4
insert into products(name,alias,price,[description],idcategory,idproduction,idgroupproduct,[image]) values(N'Agu 46 đỏ','agu-46-do',50000,N'mô tả agu 46 đỏ',1,2,4,N'agu 46 do.jpg')--5
insert into products(name,alias,price,[description],idcategory,idproduction,idgroupproduct,[image]) values(N'Andes 3S109 nhám xanh khói','andes-3s109-nham-xanh-do',370000,N'Mũ bảo hiểm nửa đầu Andes 3S109 là chiếc nón cải tiến dành cho khách hàng có size vòng đầu lớn từ 57- 59cm. Đây là dòng sản phẩm truyền thống phù hợp với khách hàng thường xuyên di chuyển trong thành phố',3,1,4,N'Andes 3S109 Nham Xanh Khoi.jpg')--6
insert into products(name,alias,price,[description],idcategory,idproduction,idgroupproduct,[image]) values(N'Andes 3S110 Tem Bong W170 Trang','andes-3s110-tem-bong-w170-trang',370000,N'',3,1,4,N'Andes 3S110 Tem Bong W170 Trang.jpg')--7
insert into products(name,alias,price,[description],idcategory,idproduction,idgroupproduct,[image]) values(N'Andes 3SHALY Bóng đỏ đô','andes-3SHALY-bong-do-do',250000,N'Mũ bảo hiểm nửa đầu Andes 3SHaly được thiết kế tối giản, đáp ứng đầy đủ các tiêu chí bền bỉ – gọn nhẹ – thoải mái. Đây là chiếc nón thích hợp cho khách hàng di chuyển trong thành phố..',3,1,4,N'Andes 3SHALY Bong Do Do.jpg')--8
insert into products(name,alias,price,[description],idcategory,idproduction,idgroupproduct,[image]) values(N'Andes 3S110 Tem Bong W170 Trang','andes-3SHALY-tem-bong-nz-rang-do',250000,N'Mũ bảo hiểm nửa đầu Andes 3SHaly được thiết kế tối giản, đáp ứng đầy đủ các tiêu chí bền bỉ – gọn nhẹ – thoải mái. Đây là chiếc nón thích hợp cho khách hàng di chuyển trong thành phố.',3,1,4,N'Andes 3SHALY Tem Bong NZ Trang Do.jpg')--9
insert into products(name,alias,price,[description],idcategory,idproduction,idgroupproduct,[image]) values(N'Andes 3SHALY Tem Bóng SAO Bạc Đen','andes-3SHALY-Tem-Bong-SAO-Bac-Den',250000,N'Mũ bảo hiểm nửa đầu Andes 3SHaly được thiết kế tối giản, đáp ứng đầy đủ các tiêu chí bền bỉ – gọn nhẹ – thoải mái. Đây là chiếc nón thích hợp cho khách hàng di chuyển trong thành phố.',3,1,4,N'Andes 3SHALY Tem Bong SAO Bac Den.jpg')--10
insert into products(name,alias,price,[description],idcategory,idproduction,idgroupproduct,[image]) values(N'Andes 3SHALY Tem Bóng W194 Trắng Đen ','andes-3SHALY-Tem-Bong-W194-Trang-Den',250000,N'Mũ bảo hiểm nửa đầu Andes 3SHaly được thiết kế tối giản, đáp ứng đầy đủ các tiêu chí bền bỉ – gọn nhẹ – thoải mái. Đây là chiếc nón thích hợp cho khách hàng di chuyển trong thành phố.',3,1,4,N'Andes 3SHALY Tem Bong W194 Trang Den.jpg')--1
insert into products(name,alias,price,[description],idcategory,idproduction,idgroupproduct,[image]) values(N'Andes 3SHALY Tem Bóng W212 Trắng Xanh','andes-3SHALY-Tem-Bong-W212-Trang-Xanh',250000,N'Mũ bảo hiểm nửa đầu Andes 3SHaly được thiết kế tối giản, đáp ứng đầy đủ các tiêu chí bền bỉ – gọn nhẹ – thoải mái. Đây là chiếc nón thích hợp cho khách hàng di chuyển trong thành phố.',3,1,4,N'Andes 3SHALY Tem Bong W212 Trang Xanh.jpg')--2
insert into products(name,alias,price,[description],idcategory,idproduction,idgroupproduct,[image]) values(N'Andes 3SHALY Tem Bong W326 Trắng Xanh','andes-3SHALY-Tem-Bong-W326-Trang-Xanh',250000,N'Mũ bảo hiểm nửa đầu Andes 3SHaly được thiết kế tối giản, đáp ứng đầy đủ các tiêu chí bền bỉ – gọn nhẹ – thoải mái. Đây là chiếc nón thích hợp cho khách hàng di chuyển trong thành phố.',3,1,4,N'Andes 3SHALY Tem Bong W326 Trang Xanh.jpg')--3
insert into products(name,alias,price,[description],idcategory,idproduction,idgroupproduct,[image]) values(N'Andes 3SHALY Tem Nham NZ Xanh Đậm Trắng','andes-3SHALY-Tem-Nham-NZ-Xanh-dam-Trang',250000,N'Mũ bảo hiểm nửa đầu Andes 3SHaly được thiết kế tối giản, đáp ứng đầy đủ các tiêu chí bền bỉ – gọn nhẹ – thoải mái. Đây là chiếc nón thích hợp cho khách hàng di chuyển trong thành phố.',3,1,4,N'Andes 3SHALY Tem Nham NZ Xanh dam Trang.jpg')--4
insert into products(name,alias,price,[description],idcategory,idproduction,idgroupproduct,[image]) values(N'Andes 3SHALY Tem Nham SAO Xanh Trắng','andes-3SHALY-Tem-Nham-SAO-Xanh-Trang',250000,N'Mũ bảo hiểm nửa đầu Andes 3SHaly được thiết kế tối giản, đáp ứng đầy đủ các tiêu chí bền bỉ – gọn nhẹ – thoải mái. Đây là chiếc nón thích hợp cho khách hàng di chuyển trong thành phố.',3,1,4,N'Andes 3SHALY Tem Nham SAO Xanh Trang.jpg')--5
insert into products(name,alias,price,[description],idcategory,idproduction,idgroupproduct,[image]) values(N'Andes 3SHALY Tem Nham W188 Kem Đen.jpg','andes-3SHALY-Tem-Nham-W188-Kem-Den',250000,N'Mũ bảo hiểm nửa đầu Andes 3SHaly được thiết kế tối giản, đáp ứng đầy đủ các tiêu chí bền bỉ – gọn nhẹ – thoải mái. Đây là chiếc nón thích hợp cho khách hàng di chuyển trong thành phố.',3,1,4,N'Andes 3SHALY Tem Nham W188 Kem Den.jpg')--6
insert into products(name,alias,price,[description],idcategory,idproduction,idgroupproduct,[image]) values(N'Andes 3SHALY Tem Nham W194 Đen Bạc','andes-3SHALY-Tem Nham-W194-Den-Bac',250000,N'Mũ bảo hiểm nửa đầu Andes 3SHaly được thiết kế tối giản, đáp ứng đầy đủ các tiêu chí bền bỉ – gọn nhẹ – thoải mái. Đây là chiếc nón thích hợp cho khách hàng di chuyển trong thành phố.',3,1,4,N'Andes 3SHALY Tem Nham W194 Den Bac.jpg')--7
insert into products(name,alias,price,[description],idcategory,idproduction,idgroupproduct,[image]) values(N'Andes 3SHALY Tem Nham W211 Đen Đồng','andes-3SHALY-Tem-Nham-W211-Den-Dong',250000,N'Mũ bảo hiểm nửa đầu Andes 3SHaly được thiết kế tối giản, đáp ứng đầy đủ các tiêu chí bền bỉ – gọn nhẹ – thoải mái. Đây là chiếc nón thích hợp cho khách hàng di chuyển trong thành phố.',3,1,4,N'Andes 3SHALY Tem Nham W211 Den Dong.jpg')--8
insert into products(name,alias,price,[description],idcategory,idproduction,idgroupproduct,[image]) values(N'Andes 3SHALY Tem Nham W326 Den Dong.jpg','andes-3SHALY-Tem-Nham-W326-Den-Dong',250000,N'Mũ bảo hiểm nửa đầu Andes 3SHaly được thiết kế tối giản, đáp ứng đầy đủ các tiêu chí bền bỉ – gọn nhẹ – thoải mái. Đây là chiếc nón thích hợp cho khách hàng di chuyển trong thành phố.',3,1,4,N'Andes 3SHALY Tem Nham W326 Den Dong.jpg')--9
insert into products(name,alias,price,[description],idcategory,idproduction,idgroupproduct,[image]) values(N'Asia MT 105 Đen đỏ bóng','asia-mt-105-den-do',280000,N'Nón bảo hiểm 1/2 đầu Asia MT-105 là sản phẩm được sử dụng phổ biến. Chiếc mũ có thiết kế trẻ trung với nhiều gam màu đẹp mắt để bạn chọn lựa,Chiếc mũ thích hợp với những người di chuyển quãng đường ngắn, xung quanh thành phố..',4,1,4,N'asia-mt-105-den-do.jpg')--20
insert into products(name,alias,price,[description],idcategory,idproduction,idgroupproduct,[image]) values(N'Asia MT 105 Đen bóng','asia-mt-117-den-mo',280000,N'Nón bảo hiểm 1/2 đầu Asia MT-105 là sản phẩm được sử dụng phổ biến. Chiếc mũ có thiết kế trẻ trung với nhiều gam màu đẹp mắt để bạn chọn lựa,Chiếc mũ thích hợp với những người di chuyển quãng đường ngắn, xung quanh thành phố..',4,1,4,N'asia-mt-117-den-mo.jpg')--1
insert into products(name,alias,price,[description],idcategory,idproduction,idgroupproduct,[image]) values(N'Asia MT 105 Đen trơn bóng','asia-mt-128-den-mo',280000,N'Nón bảo hiểm 1/2 đầu Asia MT-105 là sản phẩm được sử dụng phổ biến. Chiếc mũ có thiết kế trẻ trung với nhiều gam màu đẹp mắt để bạn chọn lựa,Chiếc mũ thích hợp với những người di chuyển quãng đường ngắn, xung quanh thành phố..',4,1,4,N'asia-mt-128-den-mo-.jpg')--2
