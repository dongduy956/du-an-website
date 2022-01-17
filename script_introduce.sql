USE [nonbaohiemviettin]
GO
/****** Object:  Table [dbo].[introduce]    Script Date: 1/17/2022 5:50:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[introduce](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](100) NULL,
	[content] [ntext] NULL,
	[image] [varchar](100) NULL,
	[status] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[introduce] ON 

INSERT [dbo].[introduce] ([id], [title], [content], [image], [status]) VALUES (1, N'Công ty nón bảo hiểm việt tin', N'Công ty cổ phần Văn hóa và Truyền thông Nhã Nam Tháng 2 năm 2005, Nhã Nam, tên đầy đủ là Công ty Cổ phần Văn hóa và Truyền thông Nhã Nam đã gia nhập thị trường sách. Tác phẩm "Balzac và cô bé thợ may Trung hoa" của Đới Tư Kiệt là cuốn sách đầu tiên được những người sáng lập Nhã Nam xuất bản cả trước khi công ty ra đời. Ngay lập tức từ cuốn sách đầu tiên, độc giả đã dành sự quan tâm và yêu mến cho một thương hiệu sách mới mẻ mang trong mình khát vọng góp phần tạo lập diện mạo mới cho xuất bản văn học tại Việt Nam. Lòng say mê của đội ngũ là viên đá đầu tiên. Trải qua mấy năm phát triển, Nhã Nam đã được xây dựng dần lên trong diện mạo một nhà xuất bản vững chãi và chuyên nghiệp. Sáu tháng sau khi thành lập công ty, Nhật ký Đặng Thùy Trâm ra đời, tạo nên một cơn sốt trong xã hội, với gần 500,000 bản sách được phát hành, phá mọi kỷ lục về xuất bản trước đó, kéo theo một loạt những hiệu ứng xã hội và dư luận có ý nghĩa', N'about1.jpg', 1)
SET IDENTITY_INSERT [dbo].[introduce] OFF
GO
