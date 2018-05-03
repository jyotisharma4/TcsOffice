USE [TCSOfficeDb]
GO

/****** Object:  StoredProcedure [dbo].[getByID]    Script Date: 04-05-2018 01:06:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[getByID] 
	-- Add the parameters for the stored procedure here
	@TableName nvarchar(50),   
    @Id int   
AS
BEGIN

 DECLARE @query nvarchar(50)
 set @query = 'SELECT * FROM '+ @TableName +' Where Id = '+ CAST(@Id AS NVARCHAR(10))
 EXECUTE sp_executesql @query
END

GO

