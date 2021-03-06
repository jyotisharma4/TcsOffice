IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetAllCompany]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetAllCompany]

/****** Object:  StoredProcedure [dbo].[uspGetAllCompany]    Script Date: 29-04-2018 12:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Karan>
-- Create date: <28 April 2018>
-- Description:	<Get all active companies>
-- =============================================
CREATE PROCEDURE [dbo].[uspGetAllCompany] 
	-- Add the parameters for the stored procedure here
AS
BEGIN
	
 SELECT Companies.Address,Companies.CompanyName,Companies.Email,Companies.Id,Companies.Phone,Companies.IsActive
FROM TCSOfficeDb.dbo.Companies where Companies.IsActive = 1;
	
END




