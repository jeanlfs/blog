-- Create Database
CREATE DATABASE Blog
GO

-- Create Table
USE [Blog]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Type] [varchar](20) NOT NULL CHECK ([Type] IN('Administrator', 'Writer', 'Reader')),
	[Email] [varchar](200) NOT NULL UNIQUE,
	[Password] [varchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Post](
	[PostId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](200) NOT NULL,
	[Description] [text] NOT NULL,
	[UserId] int FOREIGN KEY REFERENCES [User]([UserId]) NOT NULL
PRIMARY KEY CLUSTERED 
(
	[PostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Comment](
	[CommentId] [int] IDENTITY(1,1) NOT NULL,
	[Message] [text] NOT NULL,
	[UserId] int FOREIGN KEY REFERENCES [User]([UserId]) NOT NULL,
	[PostId] int FOREIGN KEY REFERENCES [Post]([PostId]) NOT NULL
PRIMARY KEY CLUSTERED 
(
	[CommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Insert Data

INSERT INTO [User] (Name, Type, Email, Password)
VALUES ('admin', 'Administrator', 'admin@teste.com', 'testando123')
GO