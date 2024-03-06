USE [Balance]
GO
/****** Object:  Table [dbo].[PersonalAccount]    Script Date: 05.03.2024 23:47:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonalAccount](
	[Account] [nvarchar](50) COLLATE Cyrillic_General_CI_AS NOT NULL,
	[FullName] [nvarchar](200) COLLATE Cyrillic_General_CI_AS NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Registration]    Script Date: 05.03.2024 23:47:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Registration](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PersonalAccountId] [int] NULL,
	[Amount] [money] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ViewBalance]    Script Date: 05.03.2024 23:47:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewBalance] (Account, Amount)
AS
SELECT PA.Account, SUM(R.Amount) FROM Registration AS R
JOIN PersonalAccount AS PA ON R.PersonalAccountId = PA.id
GROUP BY PA.Account

GO
/****** Object:  Table [dbo].[PersonalAccountInvoice]    Script Date: 05.03.2024 23:47:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonalAccountInvoice](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SumAccount] [money] NULL,
	[Description] [nvarchar](200) COLLATE Cyrillic_General_CI_AS NULL,
	[PersonalAccountId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonalAccountPayment]    Script Date: 05.03.2024 23:47:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonalAccountPayment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PurposePayment] [nvarchar](200) COLLATE Cyrillic_General_CI_AS NULL,
	[PersonalAccountId] [int] NOT NULL,
	[SumPayment] [money] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[PersonalAccount] ON 

INSERT [dbo].[PersonalAccount] ([Account], [FullName], [id]) VALUES (N'17021', N'Иванов', 1)
INSERT [dbo].[PersonalAccount] ([Account], [FullName], [id]) VALUES (N'17022', N'Петров', 2)
INSERT [dbo].[PersonalAccount] ([Account], [FullName], [id]) VALUES (N'17023', N'Сидоров', 3)
SET IDENTITY_INSERT [dbo].[PersonalAccount] OFF
GO
SET IDENTITY_INSERT [dbo].[PersonalAccountInvoice] ON 

INSERT [dbo].[PersonalAccountInvoice] ([Id], [SumAccount], [Description], [PersonalAccountId]) VALUES (1, 35.0000, N'ьрыарму', 1)
INSERT [dbo].[PersonalAccountInvoice] ([Id], [SumAccount], [Description], [PersonalAccountId]) VALUES (2, 37.0000, N'ьрыарму', 2)
INSERT [dbo].[PersonalAccountInvoice] ([Id], [SumAccount], [Description], [PersonalAccountId]) VALUES (3, 14.0000, N'уапупу', 2)
INSERT [dbo].[PersonalAccountInvoice] ([Id], [SumAccount], [Description], [PersonalAccountId]) VALUES (4, 13.0000, N'ыаава ваа ва ива', 1)
INSERT [dbo].[PersonalAccountInvoice] ([Id], [SumAccount], [Description], [PersonalAccountId]) VALUES (5, 18.0000, N'епрууер', 2)
INSERT [dbo].[PersonalAccountInvoice] ([Id], [SumAccount], [Description], [PersonalAccountId]) VALUES (6, 27.0000, N'fe efehet et', 2)
INSERT [dbo].[PersonalAccountInvoice] ([Id], [SumAccount], [Description], [PersonalAccountId]) VALUES (7, 31.0000, N'wefwefwre', 3)
SET IDENTITY_INSERT [dbo].[PersonalAccountInvoice] OFF
GO
SET IDENTITY_INSERT [dbo].[PersonalAccountPayment] ON 

INSERT [dbo].[PersonalAccountPayment] ([Id], [PurposePayment], [PersonalAccountId], [SumPayment]) VALUES (1, N'ыпаукпук', 2, 20.0000)
INSERT [dbo].[PersonalAccountPayment] ([Id], [PurposePayment], [PersonalAccountId], [SumPayment]) VALUES (2, N'ыпаукпук', 1, 20.0000)
INSERT [dbo].[PersonalAccountPayment] ([Id], [PurposePayment], [PersonalAccountId], [SumPayment]) VALUES (3, N'ыпаукпук', 1, 10.0000)
INSERT [dbo].[PersonalAccountPayment] ([Id], [PurposePayment], [PersonalAccountId], [SumPayment]) VALUES (4, N'sdgvsgr', 3, 30.0000)
INSERT [dbo].[PersonalAccountPayment] ([Id], [PurposePayment], [PersonalAccountId], [SumPayment]) VALUES (5, N'sdcdsv', 3, 1.0000)
SET IDENTITY_INSERT [dbo].[PersonalAccountPayment] OFF
GO
SET IDENTITY_INSERT [dbo].[Registration] ON 

INSERT [dbo].[Registration] ([Id], [PersonalAccountId], [Amount]) VALUES (1, 1, -35.0000)
INSERT [dbo].[Registration] ([Id], [PersonalAccountId], [Amount]) VALUES (2, 2, -37.0000)
INSERT [dbo].[Registration] ([Id], [PersonalAccountId], [Amount]) VALUES (3, 2, -14.0000)
INSERT [dbo].[Registration] ([Id], [PersonalAccountId], [Amount]) VALUES (4, 2, 20.0000)
INSERT [dbo].[Registration] ([Id], [PersonalAccountId], [Amount]) VALUES (5, 1, 20.0000)
INSERT [dbo].[Registration] ([Id], [PersonalAccountId], [Amount]) VALUES (6, 1, 10.0000)
INSERT [dbo].[Registration] ([Id], [PersonalAccountId], [Amount]) VALUES (7, 1, -13.0000)
INSERT [dbo].[Registration] ([Id], [PersonalAccountId], [Amount]) VALUES (8, 2, -18.0000)
INSERT [dbo].[Registration] ([Id], [PersonalAccountId], [Amount]) VALUES (9, 2, -27.0000)
INSERT [dbo].[Registration] ([Id], [PersonalAccountId], [Amount]) VALUES (10, 3, -31.0000)
INSERT [dbo].[Registration] ([Id], [PersonalAccountId], [Amount]) VALUES (11, 3, 30.0000)
INSERT [dbo].[Registration] ([Id], [PersonalAccountId], [Amount]) VALUES (12, 3, 1.0000)
SET IDENTITY_INSERT [dbo].[Registration] OFF
GO
ALTER TABLE [dbo].[PersonalAccountInvoice] ADD  DEFAULT ((0)) FOR [SumAccount]
GO
ALTER TABLE [dbo].[PersonalAccountPayment] ADD  DEFAULT ((0)) FOR [SumPayment]
GO
ALTER TABLE [dbo].[Registration] ADD  DEFAULT ((0)) FOR [Amount]
GO
ALTER TABLE [dbo].[PersonalAccountInvoice]  WITH CHECK ADD  CONSTRAINT [FK_PersonalAccountId] FOREIGN KEY([PersonalAccountId])
REFERENCES [dbo].[PersonalAccount] ([id])
GO
ALTER TABLE [dbo].[PersonalAccountInvoice] CHECK CONSTRAINT [FK_PersonalAccountId]
GO
ALTER TABLE [dbo].[PersonalAccountPayment]  WITH CHECK ADD  CONSTRAINT [FK_PersonalAccountPaymentId] FOREIGN KEY([PersonalAccountId])
REFERENCES [dbo].[PersonalAccount] ([id])
GO
ALTER TABLE [dbo].[PersonalAccountPayment] CHECK CONSTRAINT [FK_PersonalAccountPaymentId]
GO
ALTER TABLE [dbo].[Registration]  WITH CHECK ADD FOREIGN KEY([PersonalAccountId])
REFERENCES [dbo].[PersonalAccount] ([id])
GO
ALTER TABLE [dbo].[Registration]  WITH CHECK ADD FOREIGN KEY([PersonalAccountId])
REFERENCES [dbo].[PersonalAccount] ([id])
GO
/****** Object:  StoredProcedure [dbo].[AddPersonalAccountMoney]    Script Date: 05.03.2024 23:47:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddPersonalAccountMoney]
  @PersonalAccountId int,
  @Amount money,
  @Description nvarchar(200),
  @Type int
AS BEGIN
 IF  @Type = 1
 BEGIN
  INSERT INTO PersonalAccountInvoice (PersonalAccountId , SumAccount, Description ) VALUES( @PersonalAccountId, @Amount, @Description);
  INSERT INTO Registration (PersonalAccountId , Amount ) VALUES( @PersonalAccountId, -@Amount )
 END ELSE
 BEGIN
  INSERT INTO PersonalAccountPayment (PersonalAccountId , SumPayment, PurposePayment ) VALUES( @PersonalAccountId, @Amount, @Description)
  INSERT INTO Registration (PersonalAccountId , Amount ) VALUES( @PersonalAccountId, @Amount)
 END
 
-- INSERT INTO Registration (PersonalAccountId , Amount, Type ) VALUES( @PersonalAccountId, @Amount, @Type) 

END


GO
