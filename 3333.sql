USE [master]
GO
/****** Object:  Database [prn231_finalproject]    Script Date: 11/7/2023 6:51:09 PM ******/
CREATE DATABASE [prn231_finalproject]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'prn231_finalproject', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\prn231_finalproject.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'prn231_finalproject_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\prn231_finalproject_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [prn231_finalproject] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [prn231_finalproject].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [prn231_finalproject] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [prn231_finalproject] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [prn231_finalproject] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [prn231_finalproject] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [prn231_finalproject] SET ARITHABORT OFF 
GO
ALTER DATABASE [prn231_finalproject] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [prn231_finalproject] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [prn231_finalproject] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [prn231_finalproject] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [prn231_finalproject] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [prn231_finalproject] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [prn231_finalproject] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [prn231_finalproject] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [prn231_finalproject] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [prn231_finalproject] SET  DISABLE_BROKER 
GO
ALTER DATABASE [prn231_finalproject] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [prn231_finalproject] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [prn231_finalproject] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [prn231_finalproject] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [prn231_finalproject] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [prn231_finalproject] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [prn231_finalproject] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [prn231_finalproject] SET RECOVERY FULL 
GO
ALTER DATABASE [prn231_finalproject] SET  MULTI_USER 
GO
ALTER DATABASE [prn231_finalproject] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [prn231_finalproject] SET DB_CHAINING OFF 
GO
ALTER DATABASE [prn231_finalproject] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [prn231_finalproject] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [prn231_finalproject] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [prn231_finalproject] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'prn231_finalproject', N'ON'
GO
ALTER DATABASE [prn231_finalproject] SET QUERY_STORE = ON
GO
ALTER DATABASE [prn231_finalproject] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [prn231_finalproject]
GO
/****** Object:  Table [dbo].[Assignments]    Script Date: 11/7/2023 6:51:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Assignments](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[wlid] [int] NULL,
	[assignment_name] [nvarchar](250) NULL,
	[assignment_filesize] [int] NULL,
	[attachment] [varbinary](max) NULL,
 CONSTRAINT [PK_Assignments] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Course]    Script Date: 11/7/2023 6:51:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Course](
	[courseId] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](max) NULL,
	[code] [nchar](10) NULL,
	[categoryId] [int] NULL,
 CONSTRAINT [PK_Course] PRIMARY KEY CLUSTERED 
(
	[courseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[course_category]    Script Date: 11/7/2023 6:51:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[course_category](
	[categoryId] [int] IDENTITY(1,1) NOT NULL,
	[category_name] [nvarchar](max) NULL,
 CONSTRAINT [PK_course_category] PRIMARY KEY CLUSTERED 
(
	[categoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Enrollment]    Script Date: 11/7/2023 6:51:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Enrollment](
	[userId] [int] NOT NULL,
	[courseId] [int] NOT NULL,
	[enrollTime] [date] NULL,
 CONSTRAINT [PK_Enrollment] PRIMARY KEY CLUSTERED 
(
	[userId] ASC,
	[courseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[submission]    Script Date: 11/7/2023 6:51:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[submission](
	[userId] [int] NULL,
	[assignId] [int] NULL,
	[uploadTime] [date] NULL,
	[lastModifiedTime] [date] NULL,
	[dueDate] [date] NULL,
	[int] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_submission] PRIMARY KEY CLUSTERED 
(
	[int] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 11/7/2023 6:51:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[userId] [int] IDENTITY(1,1) NOT NULL,
	[email] [nvarchar](50) NULL,
	[password] [nvarchar](20) NULL,
	[type] [int] NULL,
	[fullname] [nvarchar](50) NULL,
	[mssv] [nchar](10) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WeekLesson]    Script Date: 11/7/2023 6:51:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WeekLesson](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[courseId] [int] NULL,
	[endDate] [date] NULL,
	[startDate] [date] NULL,
 CONSTRAINT [PK_WeekLesson] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Assignments]  WITH CHECK ADD  CONSTRAINT [FK_Assignments_WeekLesson] FOREIGN KEY([wlid])
REFERENCES [dbo].[WeekLesson] ([id])
GO
ALTER TABLE [dbo].[Assignments] CHECK CONSTRAINT [FK_Assignments_WeekLesson]
GO
ALTER TABLE [dbo].[Course]  WITH CHECK ADD  CONSTRAINT [FK_Course_course_category] FOREIGN KEY([categoryId])
REFERENCES [dbo].[course_category] ([categoryId])
GO
ALTER TABLE [dbo].[Course] CHECK CONSTRAINT [FK_Course_course_category]
GO
ALTER TABLE [dbo].[Enrollment]  WITH CHECK ADD  CONSTRAINT [FK_Enrollment_Course] FOREIGN KEY([courseId])
REFERENCES [dbo].[Course] ([courseId])
GO
ALTER TABLE [dbo].[Enrollment] CHECK CONSTRAINT [FK_Enrollment_Course]
GO
ALTER TABLE [dbo].[Enrollment]  WITH CHECK ADD  CONSTRAINT [FK_Enrollment_User] FOREIGN KEY([userId])
REFERENCES [dbo].[User] ([userId])
GO
ALTER TABLE [dbo].[Enrollment] CHECK CONSTRAINT [FK_Enrollment_User]
GO
ALTER TABLE [dbo].[submission]  WITH CHECK ADD  CONSTRAINT [FK_submission_Assignments] FOREIGN KEY([assignId])
REFERENCES [dbo].[Assignments] ([id])
GO
ALTER TABLE [dbo].[submission] CHECK CONSTRAINT [FK_submission_Assignments]
GO
ALTER TABLE [dbo].[submission]  WITH CHECK ADD  CONSTRAINT [FK_submission_User] FOREIGN KEY([userId])
REFERENCES [dbo].[User] ([userId])
GO
ALTER TABLE [dbo].[submission] CHECK CONSTRAINT [FK_submission_User]
GO
ALTER TABLE [dbo].[WeekLesson]  WITH CHECK ADD  CONSTRAINT [FK_WeekLesson_Course] FOREIGN KEY([courseId])
REFERENCES [dbo].[Course] ([courseId])
GO
ALTER TABLE [dbo].[WeekLesson] CHECK CONSTRAINT [FK_WeekLesson_Course]
GO
USE [master]
GO
ALTER DATABASE [prn231_finalproject] SET  READ_WRITE 
GO
