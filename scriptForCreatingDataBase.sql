USE [master]
GO
/****** Object:  Database [FITNESS_CLUB]    Script Date: 23.12.2024 14:06:47 ******/
CREATE DATABASE [FITNESS_CLUB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FITNESS_CLUB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\FITNESS_CLUB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'FITNESS_CLUB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\FITNESS_CLUB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [FITNESS_CLUB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FITNESS_CLUB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FITNESS_CLUB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FITNESS_CLUB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FITNESS_CLUB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FITNESS_CLUB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FITNESS_CLUB] SET ARITHABORT OFF 
GO
ALTER DATABASE [FITNESS_CLUB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [FITNESS_CLUB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FITNESS_CLUB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FITNESS_CLUB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FITNESS_CLUB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FITNESS_CLUB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FITNESS_CLUB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FITNESS_CLUB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FITNESS_CLUB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FITNESS_CLUB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [FITNESS_CLUB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FITNESS_CLUB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FITNESS_CLUB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FITNESS_CLUB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FITNESS_CLUB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FITNESS_CLUB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FITNESS_CLUB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FITNESS_CLUB] SET RECOVERY FULL 
GO
ALTER DATABASE [FITNESS_CLUB] SET  MULTI_USER 
GO
ALTER DATABASE [FITNESS_CLUB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FITNESS_CLUB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FITNESS_CLUB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FITNESS_CLUB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [FITNESS_CLUB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [FITNESS_CLUB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'FITNESS_CLUB', N'ON'
GO
ALTER DATABASE [FITNESS_CLUB] SET QUERY_STORE = ON
GO
ALTER DATABASE [FITNESS_CLUB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [FITNESS_CLUB]
GO
/****** Object:  UserDefinedFunction [dbo].[ValidateUsers]    Script Date: 23.12.2024 14:06:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[ValidateUsers]
(
    @Login NVARCHAR(150), 
    @Password NVARCHAR(255)
)
RETURNS BIT
AS
BEGIN
    DECLARE @Table NVARCHAR(50);
    DECLARE @Sql NVARCHAR(MAX);
    DECLARE @Result BIT;

    -- Определяем, в какую таблицу обращаться
    SET @Table = CASE 
                    WHEN LEFT(@Login, 1) = '/' THEN 'Employees'
                    ELSE 'Users'
                 END;

    -- Формируем динамический запрос для проверки логина и пароля
    SET @Sql = N'SELECT @Result = CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END FROM ' + QUOTENAME(@Table) + ' WHERE login = @Login AND password = @Password';

    -- Выполняем динамический запрос
    EXEC sp_executesql @Sql, 
                       N'@Login NVARCHAR(150), @Password NVARCHAR(255), @Result BIT OUTPUT', 
                       @Login, 
                       @Password, 
                       @Result OUTPUT;

    -- Возвращаем результат
    RETURN @Result;
END;
GO
/****** Object:  Table [dbo].[CurrentServices]    Script Date: 23.12.2024 14:06:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CurrentServices](
	[ID] [int] NOT NULL,
	[ID User] [int] NOT NULL,
	[ID Service] [int] NOT NULL,
	[ID Couch] [int] NULL,
	[DataTimeStart] [datetime] NULL,
	[DateTimeFinish] [datetime] NULL,
 CONSTRAINT [PK_CurrentServices] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 23.12.2024 14:06:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[ID] [int] NOT NULL,
	[FIO] [nvarchar](255) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](50) NOT NULL,
	[DataBirth] [date] NOT NULL,
	[login] [nvarchar](20) NOT NULL,
	[password] [nvarchar](255) NOT NULL,
	[role] [int] NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 23.12.2024 14:06:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[ID] [int] NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Services]    Script Date: 23.12.2024 14:06:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Services](
	[ID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[DaysLast] [int] NOT NULL,
	[Price] [money] NULL,
	[Type] [int] NULL,
 CONSTRAINT [PK_Services] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceType]    Script Date: 23.12.2024 14:06:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceType](
	[ID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ServiceType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 23.12.2024 14:06:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] NOT NULL,
	[FIO] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](50) NOT NULL,
	[DataBirth] [date] NOT NULL,
	[login] [nvarchar](20) NOT NULL,
	[password] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_persons_data] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VisitorsLog]    Script Date: 23.12.2024 14:06:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VisitorsLog](
	[ID] [int] NOT NULL,
	[ID Client] [int] NULL,
	[ID Couch] [int] NULL,
	[Data] [datetime] NOT NULL,
	[VisitsCount] [int] NULL,
 CONSTRAINT [PK_VisitorsLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CurrentServices]  WITH CHECK ADD  CONSTRAINT [FK_CurrentServices_Employees] FOREIGN KEY([ID Couch])
REFERENCES [dbo].[Employees] ([ID])
ON UPDATE SET NULL
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[CurrentServices] CHECK CONSTRAINT [FK_CurrentServices_Employees]
GO
ALTER TABLE [dbo].[CurrentServices]  WITH CHECK ADD  CONSTRAINT [FK_CurrentServices_Services] FOREIGN KEY([ID Service])
REFERENCES [dbo].[Services] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CurrentServices] CHECK CONSTRAINT [FK_CurrentServices_Services]
GO
ALTER TABLE [dbo].[CurrentServices]  WITH CHECK ADD  CONSTRAINT [FK_CurrentServices_Users] FOREIGN KEY([ID User])
REFERENCES [dbo].[Users] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CurrentServices] CHECK CONSTRAINT [FK_CurrentServices_Users]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Roles] FOREIGN KEY([role])
REFERENCES [dbo].[Roles] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Roles]
GO
ALTER TABLE [dbo].[Services]  WITH CHECK ADD  CONSTRAINT [FK_Services_ServiceType] FOREIGN KEY([Type])
REFERENCES [dbo].[ServiceType] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Services] CHECK CONSTRAINT [FK_Services_ServiceType]
GO
ALTER TABLE [dbo].[VisitorsLog]  WITH CHECK ADD  CONSTRAINT [FK_VisitorsLog_Employees] FOREIGN KEY([ID Couch])
REFERENCES [dbo].[Employees] ([ID])
GO
ALTER TABLE [dbo].[VisitorsLog] CHECK CONSTRAINT [FK_VisitorsLog_Employees]
GO
ALTER TABLE [dbo].[VisitorsLog]  WITH CHECK ADD  CONSTRAINT [FK_VisitorsLog_Users] FOREIGN KEY([ID Client])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[VisitorsLog] CHECK CONSTRAINT [FK_VisitorsLog_Users]
GO
/****** Object:  StoredProcedure [dbo].[GetCouchesServiceStatistics]    Script Date: 23.12.2024 14:06:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetCouchesServiceStatistics]
	@Month INT,
    @Year INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        LEFT(e.FIO, CHARINDEX(' ', e.FIO) - 1) + ' ' +
        LEFT(SUBSTRING(e.FIO, CHARINDEX(' ', e.FIO) + 1, LEN(e.FIO)), 1) + '.' +
        LEFT(SUBSTRING(e.FIO, CHARINDEX(' ', e.FIO, CHARINDEX(' ', e.FIO) + 1) + 1, LEN(e.FIO)), 1) + '.' AS Тренер,
        COUNT(CS.ID) AS [Кол-во услуг],
        cast(@Month as varchar(20))+'.'+cast(@Year as varchar(4)) AS [Период (месяц)]
    FROM 
        Employees e
    LEFT JOIN 
        CurrentServices CS ON e.ID = CS.[ID Couch]
    WHERE
        (MONTH(CS.DataTimeStart) = @Month AND YEAR(CS.DataTimeStart) = @Year) OR
        (MONTH(CS.DateTimeFinish) = @Month AND YEAR(CS.DateTimeFinish) = @Year) OR
		(CS.DataTimeStart <= EOMONTH(DATEFROMPARTS(@Year, @Month, 1)) AND CS.DateTimeFinish >= DATEADD(MONTH, DATEDIFF(MONTH, 0, DATEFROMPARTS(@Year, @Month, 1)), 0))    GROUP BY 
        e.FIO
END
GO
/****** Object:  StoredProcedure [dbo].[GetFIOByLogin]    Script Date: 23.12.2024 14:06:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetFIOByLogin]
(
    @Login NVARCHAR(20)
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @FIO NVARCHAR(150);
    IF LEFT(@Login, 1) = '/'
    BEGIN
        SELECT @FIO = FIO
        FROM Employees
        WHERE login = @Login;
    END
    ELSE
    BEGIN
        SELECT @FIO = FIO
        FROM Users
        WHERE login = @Login;
    END
    SELECT @FIO AS FIO;
END
GO
/****** Object:  StoredProcedure [dbo].[GetRoleNameByLogin]    Script Date: 23.12.2024 14:06:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetRoleNameByLogin]
    @Login NVARCHAR(20),
    @RoleName NVARCHAR(20) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

	IF LEFT(@Login, 1) = '/'
    BEGIN
        SELECT @RoleName = r.Name
		FROM Employees e
		JOIN Roles r ON e.role = r.ID
		WHERE e.Login = @Login;
    END
    ELSE
    BEGIN
        SELECT @RoleName = NULL;
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[GetUserServiceDetails]    Script Date: 23.12.2024 14:06:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetUserServiceDetails]
(
    @Login NVARCHAR(150)
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @UserId INT;
    DECLARE @CouchId INT;
    DECLARE @UserType NVARCHAR(50);
	DECLARE @CurrentDate DATE = GETDATE();
    IF LEFT(@Login, 1) = '/'
    BEGIN
        SELECT @CouchId = ID
        FROM Employees
        WHERE login = @Login;
        SET @UserType = 'Employees';
    END
    ELSE
    BEGIN
        SELECT @UserId = ID
        FROM Users
        WHERE login = @Login;
        SET @UserType = 'Users';
    END
    IF @UserType = 'Employees'
    BEGIN
        SELECT 
            s.Name AS Наименование,
            CAST(@CurrentDate AS DATE) AS Дата,
            CASE WHEN cs.[ID Couch] IS NOT NULL 
                 THEN CONCAT('Начало: ', FORMAT(cs.DateTimeFinish, 'HH:mm'), char(13), 'Клиент: ', (SELECT LEFT(u.FIO, CHARINDEX(' ', u.FIO) - 1) + ' ' + LEFT(SUBSTRING(u.FIO, CHARINDEX(' ', u.FIO) + 1, LEN(u.FIO)), 1) + '.' + LEFT(SUBSTRING(u.FIO, CHARINDEX(' ', u.FIO, CHARINDEX(' ', u.FIO) + 1) + 1, LEN(u.FIO)), 1)))
                 ELSE '' END AS Комментарии
        FROM CurrentServices cs
        JOIN Services s ON cs.[ID Service] = s.ID
        LEFT JOIN Users u ON cs.[ID User] = u.ID
        WHERE cs.[ID Couch] = @CouchId
			AND @CurrentDate BETWEEN cs.DataTimeStart AND cs.DateTimeFinish;
    END
    ELSE
    BEGIN
        SELECT 
            s.Name AS Наименование,
            CAST(cs.DateTimeFinish AS DATE) AS ДействуетДо,
            CASE WHEN cs.[ID Couch] IS NOT NULL 
                 THEN CONCAT('Начало: ', FORMAT(cs.DateTimeFinish, 'HH:mm'), char(13), 'Тренер: ', (SELECT LEFT(e.FIO, CHARINDEX(' ', e.FIO) - 1) + ' ' + LEFT(SUBSTRING(e.FIO, CHARINDEX(' ', e.FIO) + 1, LEN(e.FIO)), 1) + '.' + LEFT(SUBSTRING(e.FIO, CHARINDEX(' ', e.FIO, CHARINDEX(' ', e.FIO) + 1) + 1, LEN(e.FIO)), 1)))
                 ELSE '' END AS Комментарии
        FROM CurrentServices cs
        JOIN Services s ON cs.[ID Service] = s.ID
        LEFT JOIN Employees e ON cs.[ID Couch] = e.ID
        WHERE cs.[ID User] = @UserId
			AND @CurrentDate BETWEEN cs.DataTimeStart AND cs.DateTimeFinish;
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[GetUserServiceStatistics]    Script Date: 23.12.2024 14:06:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetUserServiceStatistics]
    @Month INT,
    @Year INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        LEFT(U.FIO, CHARINDEX(' ', U.FIO) - 1) + ' ' +
        LEFT(SUBSTRING(U.FIO, CHARINDEX(' ', U.FIO) + 1, LEN(U.FIO)), 1) + '.' +
        LEFT(SUBSTRING(U.FIO, CHARINDEX(' ', U.FIO, CHARINDEX(' ', U.FIO) + 1) + 1, LEN(U.FIO)), 1) + '.' AS Клиент,
        COUNT(CS.ID) AS [Кол-во услуг],
        CAST(@Month AS VARCHAR(20)) + '.' + CAST(@Year AS VARCHAR(4)) AS [Период (месяц)]
    FROM 
        Users U
    LEFT JOIN 
        CurrentServices CS ON U.ID = CS.[ID User]
    WHERE 
        (MONTH(CS.DataTimeStart) = @Month AND YEAR(CS.DataTimeStart) = @Year) OR
        (MONTH(CS.DateTimeFinish) = @Month AND YEAR(CS.DateTimeFinish) = @Year) OR
        (CS.DataTimeStart <= EOMONTH(DATEFROMPARTS(@Year, @Month, 1)) AND CS.DateTimeFinish >= DATEADD(MONTH, DATEDIFF(MONTH, 0, DATEFROMPARTS(@Year, @Month, 1)), 0))
    GROUP BY 
        U.FIO
END
GO
/****** Object:  StoredProcedure [dbo].[ValidateUserLogin]    Script Date: 23.12.2024 14:06:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ValidateUserLogin]
(
    @Login NVARCHAR(150), 
    @Password NVARCHAR(255), 
    @IsValid BIT OUTPUT
)
AS
BEGIN
    DECLARE @Table NVARCHAR(50);
    DECLARE @Sql NVARCHAR(MAX);

    -- Определяем, в какую таблицу обращаться
    SET @Table = CASE 
                    WHEN LEFT(@Login, 1) = '/' THEN 'Employees'
                    ELSE 'Users'
                 END;

    -- Формируем динамический запрос для проверки логина и пароля
    SET @Sql = N'SELECT @IsValid = CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END 
                 FROM ' + QUOTENAME(@Table) + ' 
                 WHERE login = @Login AND password = @Password';

    -- Выполняем динамический запрос
    EXEC sp_executesql @Sql, 
                       N'@Login NVARCHAR(150), @Password NVARCHAR(255), @IsValid BIT OUTPUT', 
                       @Login, 
                       @Password, 
                       @IsValid OUTPUT;
END;
GO
USE [master]
GO
ALTER DATABASE [FITNESS_CLUB] SET  READ_WRITE 
GO
