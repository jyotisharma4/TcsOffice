USE [TCSOfficeDb]
GO

/****** Object:  StoredProcedure [dbo].[UpdateCompany]    Script Date: 04-05-2018 01:05:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateCompany] 
	-- Add the parameters for the stored procedure here
	@Name nvarchar(256), 
	@Phone nvarchar(50),
	@Email nvarchar(max),
	@IsActive bit,
	@Id int,
	@Address nvarchar(max)

AS
BEGIN
	Update Companies set  Address=@Address,
			 CompanyName =@Name, 
			  Email =@Email,
			  Phone=@Phone, 
			  IsActive = @IsActive
	 where Id =@Id;
END

GO

