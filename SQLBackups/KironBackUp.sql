USE [KironQA]
GO
/****** Object:  Table [dbo].[Holidays]    Script Date: 2024/06/19 11:48:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Holidays](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[Date] [date] NOT NULL,
	[Notes] [nvarchar](max) NULL,
	[Bunting] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RegionHolidays]    Script Date: 2024/06/19 11:48:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegionHolidays](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RegionId] [int] NOT NULL,
	[HolidayId] [int] NOT NULL,
 CONSTRAINT [PK__RegionHo__1E0B19F44308FF6D] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Regions]    Script Date: 2024/06/19 11:48:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Regions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2024/06/19 11:48:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK__Users__3214EC2770695047] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Regions] ON 

INSERT [dbo].[Regions] ([Id], [Name]) VALUES (6, N'England and Wales')
INSERT [dbo].[Regions] ([Id], [Name]) VALUES (8, N'Northern Ireland')
INSERT [dbo].[Regions] ([Id], [Name]) VALUES (7, N'Scotland')
SET IDENTITY_INSERT [dbo].[Regions] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([ID], [Username], [Password], [IsActive], [CreatedDate]) VALUES (3, N'admin@gmail.com', N'$2a$11$vdz3gkawhetX.9KnnlWrHOibohPIL2ausC9wlFtcyJWv9f5i9FmjG', 1, CAST(N'2024-06-18T14:33:13.597' AS DateTime))
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UK_Title_Date]    Script Date: 2024/06/19 11:48:45 ******/
ALTER TABLE [dbo].[Holidays] ADD  CONSTRAINT [UK_Title_Date] UNIQUE NONCLUSTERED 
(
	[Title] ASC,
	[Date] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UC_RegionHoliday]    Script Date: 2024/06/19 11:48:45 ******/
ALTER TABLE [dbo].[RegionHolidays] ADD  CONSTRAINT [UC_RegionHoliday] UNIQUE NONCLUSTERED 
(
	[RegionId] ASC,
	[HolidayId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UK_Name]    Script Date: 2024/06/19 11:48:45 ******/
ALTER TABLE [dbo].[Regions] ADD  CONSTRAINT [UK_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__Users__IsActive__59063A47]  DEFAULT ((0)) FOR [IsActive]
GO
ALTER TABLE [dbo].[RegionHolidays]  WITH CHECK ADD  CONSTRAINT [FK__RegionHol__Holid__75A278F5] FOREIGN KEY([HolidayId])
REFERENCES [dbo].[Holidays] ([Id])
GO
ALTER TABLE [dbo].[RegionHolidays] CHECK CONSTRAINT [FK__RegionHol__Holid__75A278F5]
GO
ALTER TABLE [dbo].[RegionHolidays]  WITH CHECK ADD  CONSTRAINT [FK__RegionHol__Regio__74AE54BC] FOREIGN KEY([RegionId])
REFERENCES [dbo].[Regions] ([Id])
GO
ALTER TABLE [dbo].[RegionHolidays] CHECK CONSTRAINT [FK__RegionHol__Regio__74AE54BC]
GO
/****** Object:  StoredProcedure [dbo].[GetAllNavigationItems]    Script Date: 2024/06/19 11:48:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllNavigationItems]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT  
        [ID],
        [Text],
        [ParentID]
    FROM 
        [KironQA].[dbo].[Navigation];
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllRegions]    Script Date: 2024/06/19 11:48:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[GetAllRegions]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT  
        [Id],
        [Name]
    FROM [KironQA].[dbo].[Regions];
END;
GO
/****** Object:  StoredProcedure [dbo].[GetBankHolidaysByRegion]    Script Date: 2024/06/19 11:48:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetBankHolidaysByRegion]
    @RegionId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT h.[Id], h.[Title], h.[Date], h.[Notes], h.[Bunting]
    FROM [KironQA].[dbo].[Holidays] h
    INNER JOIN [KironQA].[dbo].[RegionHolidays] rh ON h.[Id] = rh.[HolidayId]
    WHERE rh.[RegionId] = @RegionId
    ORDER BY h.[Date];
END;
GO
/****** Object:  StoredProcedure [dbo].[GetUserDetailsByUsername]    Script Date: 2024/06/19 11:48:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetUserDetailsByUsername]
    @Username NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
	
    SELECT Username , Password
	FROM Users 
	WHERE Username = @Username
     		
END;

GO
/****** Object:  StoredProcedure [dbo].[InsertHoliday]    Script Date: 2024/06/19 11:48:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertHoliday]
    @Title NVARCHAR(100),
    @Date DATE,
    @Notes NVARCHAR(500),
    @Bunting BIT,
    @RegionName NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @RegionId INT;
    DECLARE @HolidayId INT;

    SELECT @RegionId = Id
    FROM Regions
    WHERE Name = @RegionName;

    IF @RegionId IS NOT NULL
    BEGIN
        SELECT @HolidayId = Id
        FROM Holidays
        WHERE Title = @Title
        AND Date = @Date;

        IF @HolidayId IS NULL
        BEGIN
            INSERT INTO Holidays (Title, Date, Notes, Bunting)
            VALUES (@Title, @Date, @Notes, @Bunting);

            SET @HolidayId = SCOPE_IDENTITY();
        END

        IF NOT EXISTS (
            SELECT 1
            FROM RegionHolidays
            WHERE RegionId = @RegionId AND HolidayId = @HolidayId
        )
        BEGIN
            INSERT INTO RegionHolidays (RegionId, HolidayId)
            VALUES (@RegionId, @HolidayId);
        END
    END;
END;
GO
/****** Object:  StoredProcedure [dbo].[InsertUser]    Script Date: 2024/06/19 11:48:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertUser]
    @Username NVARCHAR(50),
    @Password NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewUserCreated BIT = 0;

    IF NOT EXISTS (SELECT 1 FROM Users WHERE Username = @Username)
    BEGIN
        INSERT INTO Users (Username, Password, IsActive, CreatedDate)
        VALUES (@Username, @Password, 1, GETDATE());

        SET @NewUserCreated = 1;
    END

    SELECT @NewUserCreated AS NewUserCreated;
END;
GO
