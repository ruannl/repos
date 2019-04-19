IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ELMAH_GetErrorXml]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ELMAH_GetErrorXml]
GO

CREATE PROCEDURE [dbo].[ELMAH_GetErrorXml](@Application NVARCHAR(60), @ErrorId UNIQUEIDENTIFIER)
AS
BEGIN
	SET NOCOUNT ON
	SELECT [AllXml] FROM  [ELMAH_Error] WHERE [ErrorId] = @ErrorId AND [Application] = @Application
END