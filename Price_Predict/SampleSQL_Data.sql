USE [master]
GO
/****** Object:  Database [ML]    Script Date: 27-12-2024 15:56:28 ******/
CREATE DATABASE [ML]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ML', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\ML.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ML_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\ML_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ML] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ML].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ML] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ML] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ML] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ML] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ML] SET ARITHABORT OFF 
GO
ALTER DATABASE [ML] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ML] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ML] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ML] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ML] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ML] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ML] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ML] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ML] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ML] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ML] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ML] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ML] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ML] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ML] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ML] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ML] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ML] SET RECOVERY FULL 
GO
ALTER DATABASE [ML] SET  MULTI_USER 
GO
ALTER DATABASE [ML] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ML] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ML] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ML] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ML] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ML] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'ML', N'ON'
GO
ALTER DATABASE [ML] SET QUERY_STORE = OFF
GO
USE [ML]
GO
/****** Object:  Table [dbo].[countries]    Script Date: 27-12-2024 15:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[countries](
	[country_id] [char](2) NOT NULL,
	[country_name] [varchar](40) NULL,
	[region_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[country_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[departments]    Script Date: 27-12-2024 15:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[departments](
	[department_id] [int] IDENTITY(1,1) NOT NULL,
	[department_name] [varchar](30) NOT NULL,
	[location_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[department_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[dependents]    Script Date: 27-12-2024 15:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[dependents](
	[dependent_id] [int] IDENTITY(1,1) NOT NULL,
	[first_name] [varchar](50) NOT NULL,
	[last_name] [varchar](50) NOT NULL,
	[relationship] [varchar](25) NOT NULL,
	[employee_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[dependent_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[employees]    Script Date: 27-12-2024 15:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[employees](
	[employee_id] [int] IDENTITY(1,1) NOT NULL,
	[first_name] [varchar](20) NULL,
	[last_name] [varchar](25) NOT NULL,
	[email] [varchar](100) NOT NULL,
	[phone_number] [varchar](20) NULL,
	[hire_date] [date] NOT NULL,
	[job_id] [int] NOT NULL,
	[salary] [decimal](8, 2) NOT NULL,
	[manager_id] [int] NULL,
	[department_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[employee_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeTimeSheet]    Script Date: 27-12-2024 15:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeTimeSheet](
	[EmployeeID] [int] NULL,
	[LoginDate] [date] NULL,
	[StartTime] [int] NULL,
	[EndTime] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[jobs]    Script Date: 27-12-2024 15:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[jobs](
	[job_id] [int] IDENTITY(1,1) NOT NULL,
	[job_title] [varchar](35) NOT NULL,
	[min_salary] [decimal](8, 2) NULL,
	[max_salary] [decimal](8, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[job_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[locations]    Script Date: 27-12-2024 15:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[locations](
	[location_id] [int] IDENTITY(1,1) NOT NULL,
	[street_address] [varchar](40) NULL,
	[postal_code] [varchar](12) NULL,
	[city] [varchar](30) NOT NULL,
	[state_province] [varchar](25) NULL,
	[country_id] [char](2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[location_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderShipDetails]    Script Date: 27-12-2024 15:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderShipDetails](
	[OrderID] [int] NULL,
	[Freight] [float] NULL,
	[ShipName] [varchar](1000) NULL,
	[ShipAddress] [varchar](2000) NULL,
	[ShipCity] [varchar](150) NULL,
	[ShipCountry] [varchar](150) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[regions]    Script Date: 27-12-2024 15:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[regions](
	[region_id] [int] IDENTITY(1,1) NOT NULL,
	[region_name] [varchar](25) NULL,
PRIMARY KEY CLUSTERED 
(
	[region_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentScore]    Script Date: 27-12-2024 15:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentScore](
	[StudentID] [int] NULL,
	[Score] [float] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[countries] ADD  DEFAULT (NULL) FOR [country_name]
GO
ALTER TABLE [dbo].[departments] ADD  DEFAULT (NULL) FOR [location_id]
GO
ALTER TABLE [dbo].[employees] ADD  DEFAULT (NULL) FOR [first_name]
GO
ALTER TABLE [dbo].[employees] ADD  DEFAULT (NULL) FOR [phone_number]
GO
ALTER TABLE [dbo].[employees] ADD  DEFAULT (NULL) FOR [manager_id]
GO
ALTER TABLE [dbo].[employees] ADD  DEFAULT (NULL) FOR [department_id]
GO
ALTER TABLE [dbo].[jobs] ADD  DEFAULT (NULL) FOR [min_salary]
GO
ALTER TABLE [dbo].[jobs] ADD  DEFAULT (NULL) FOR [max_salary]
GO
ALTER TABLE [dbo].[locations] ADD  DEFAULT (NULL) FOR [street_address]
GO
ALTER TABLE [dbo].[locations] ADD  DEFAULT (NULL) FOR [postal_code]
GO
ALTER TABLE [dbo].[locations] ADD  DEFAULT (NULL) FOR [state_province]
GO
ALTER TABLE [dbo].[regions] ADD  DEFAULT (NULL) FOR [region_name]
GO
ALTER TABLE [dbo].[countries]  WITH CHECK ADD FOREIGN KEY([region_id])
REFERENCES [dbo].[regions] ([region_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[departments]  WITH CHECK ADD FOREIGN KEY([location_id])
REFERENCES [dbo].[locations] ([location_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[dependents]  WITH CHECK ADD FOREIGN KEY([employee_id])
REFERENCES [dbo].[employees] ([employee_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[employees]  WITH CHECK ADD FOREIGN KEY([department_id])
REFERENCES [dbo].[departments] ([department_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[employees]  WITH CHECK ADD FOREIGN KEY([job_id])
REFERENCES [dbo].[jobs] ([job_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[employees]  WITH CHECK ADD FOREIGN KEY([manager_id])
REFERENCES [dbo].[employees] ([employee_id])
GO
ALTER TABLE [dbo].[locations]  WITH CHECK ADD FOREIGN KEY([country_id])
REFERENCES [dbo].[countries] ([country_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
USE [master]
GO
ALTER DATABASE [ML] SET  READ_WRITE 
GO
