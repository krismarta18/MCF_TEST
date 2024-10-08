USE [master]
GO
/****** Object:  Database [TechnicalTest_MCF]    Script Date: 8/6/2024 3:32:27 AM ******/
CREATE DATABASE [TechnicalTest_MCF]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Test_MCF', FILENAME = N'C:\Users\krist\Test_MCF.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Test_MCF_log', FILENAME = N'C:\Users\krist\Test_MCF_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [TechnicalTest_MCF] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TechnicalTest_MCF].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TechnicalTest_MCF] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TechnicalTest_MCF] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TechnicalTest_MCF] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TechnicalTest_MCF] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TechnicalTest_MCF] SET ARITHABORT OFF 
GO
ALTER DATABASE [TechnicalTest_MCF] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TechnicalTest_MCF] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TechnicalTest_MCF] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TechnicalTest_MCF] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TechnicalTest_MCF] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TechnicalTest_MCF] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TechnicalTest_MCF] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TechnicalTest_MCF] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TechnicalTest_MCF] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TechnicalTest_MCF] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TechnicalTest_MCF] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TechnicalTest_MCF] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TechnicalTest_MCF] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TechnicalTest_MCF] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TechnicalTest_MCF] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TechnicalTest_MCF] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TechnicalTest_MCF] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TechnicalTest_MCF] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TechnicalTest_MCF] SET  MULTI_USER 
GO
ALTER DATABASE [TechnicalTest_MCF] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TechnicalTest_MCF] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TechnicalTest_MCF] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TechnicalTest_MCF] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TechnicalTest_MCF] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TechnicalTest_MCF] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [TechnicalTest_MCF] SET QUERY_STORE = OFF
GO
USE [TechnicalTest_MCF]
GO
/****** Object:  Table [dbo].[ms_storage_location]    Script Date: 8/6/2024 3:32:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ms_storage_location](
	[location_id] [varchar](10) NOT NULL,
	[location_name] [varchar](100) NULL,
 CONSTRAINT [PK_ms_storage_location] PRIMARY KEY CLUSTERED 
(
	[location_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ms_user]    Script Date: 8/6/2024 3:32:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ms_user](
	[user_id] [bigint] IDENTITY(1,1) NOT NULL,
	[username] [varchar](20) NULL,
	[password] [varchar](50) NULL,
	[is_active] [bit] NULL,
 CONSTRAINT [PK_ms_user] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tr_bpkb]    Script Date: 8/6/2024 3:32:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tr_bpkb](
	[agreement_number] [varchar](100) NOT NULL,
	[bpkb_no] [varchar](100) NULL,
	[branch_id] [varchar](10) NULL,
	[bpkp_date] [datetime] NULL,
	[faktur_no] [varchar](100) NULL,
	[faktur_date] [datetime] NULL,
	[location_id] [varchar](10) NULL,
	[police_no] [varchar](20) NULL,
	[bpkb_date_in] [datetime] NULL,
	[created_by] [varchar](20) NULL,
	[created_on] [datetime] NULL,
	[last_update_by] [varchar](20) NULL,
	[last_update_on] [datetime] NULL,
 CONSTRAINT [PK_tr_bpkb] PRIMARY KEY CLUSTERED 
(
	[agreement_number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tr_bpkb]  WITH CHECK ADD  CONSTRAINT [FK_tr_bpkb_ms_storage_location] FOREIGN KEY([location_id])
REFERENCES [dbo].[ms_storage_location] ([location_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tr_bpkb] CHECK CONSTRAINT [FK_tr_bpkb_ms_storage_location]
GO
USE [master]
GO
ALTER DATABASE [TechnicalTest_MCF] SET  READ_WRITE 
GO
