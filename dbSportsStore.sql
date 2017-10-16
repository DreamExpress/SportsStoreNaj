USE [master]
GO

/****** Object:  Database [SportsStore]    Script Date: 2017-10-12 21:03:36 ******/
DROP DATABASE [SportsStore]
GO

/****** Object:  Database [SportsStore]    Script Date: 2017-10-12 21:03:36 ******/
CREATE DATABASE [SportsStore]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SportsStore', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\SportsStore.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SportsStore_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\SportsStore_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO

ALTER DATABASE [SportsStore] SET COMPATIBILITY_LEVEL = 130
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SportsStore].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [SportsStore] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [SportsStore] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [SportsStore] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [SportsStore] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [SportsStore] SET ARITHABORT OFF 
GO

ALTER DATABASE [SportsStore] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [SportsStore] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [SportsStore] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [SportsStore] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [SportsStore] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [SportsStore] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [SportsStore] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [SportsStore] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [SportsStore] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [SportsStore] SET  DISABLE_BROKER 
GO

ALTER DATABASE [SportsStore] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [SportsStore] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [SportsStore] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [SportsStore] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [SportsStore] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [SportsStore] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [SportsStore] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [SportsStore] SET RECOVERY FULL 
GO

ALTER DATABASE [SportsStore] SET  MULTI_USER 
GO

ALTER DATABASE [SportsStore] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [SportsStore] SET DB_CHAINING OFF 
GO

ALTER DATABASE [SportsStore] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [SportsStore] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [SportsStore] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [SportsStore] SET QUERY_STORE = OFF
GO

USE [SportsStore]
GO

ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO

ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO

ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO

ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO

ALTER DATABASE [SportsStore] SET  READ_WRITE 
GO
