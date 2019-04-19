IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ELMAH_LogError]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ELMAH_LogError]
GO

CREATE PROCEDURE [dbo].[ELMAH_LogError] (@ErrorId UNIQUEIDENTIFIER, @Application NVARCHAR(60), @Host NVARCHAR(30), @Type NVARCHAR(100), @Source NVARCHAR(60), @Message NVARCHAR(500), @User NVARCHAR(50), @AllXml NTEXT, @StatusCode INT, @TimeUtc DATETIME)
AS
BEGIN
	SET NOCOUNT ON
	INSERT INTO [ELMAH_Error]([ErrorId], [Application], [Host], [Type], [Source], [Message], [User], [AllXml], [StatusCode], [TimeUtc])
	VALUES (@ErrorId, @Application, @Host, @Type, @Source, @Message, @User, @AllXml, @StatusCode, @TimeUtc)
END