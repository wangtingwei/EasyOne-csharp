/*
此脚本由 Visual Studio 在 2008-6-3 上的 15:35 处创建。
请在 pe-dcfd602cb72a.eShop_1.0.0.0.dbo 上运行此脚本，使其与 pe-dcfd602cb72a.eShop_1.1.0.0.dbo 相同。
请在运行此脚本之前备份目标数据库。
*/
GO
SET NUMERIC_ROUNDABORT OFF
GO
SET ANSI_PADDING ON
GO
SET ANSI_WARNINGS ON
GO
SET CONCAT_NULL_YIELDS_NULL ON
GO
SET ARITHABORT ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
IF EXISTS (SELECT * FROM tempdb..sysobjects WHERE id=OBJECT_ID('tempdb..#tmpErrors')) DROP TABLE #tmpErrors
GO
CREATE TABLE #tmpErrors (Error int)
GO
SET XACT_ABORT ON
GO
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
GO
BEGIN TRANSACTION
GO
PRINT N'正在删除 [dbo].[PE_CollectionItem] 的约束'
GO
ALTER TABLE [dbo].[PE_CollectionItem] DROP CONSTRAINT [PK_PE_CollectionItem]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PE_CollectionItem] 的约束'
GO
ALTER TABLE [dbo].[PE_CollectionItem] DROP CONSTRAINT [DF_PE_CollectionItem_MaxNum]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PE_CollectionItem] 的约束'
GO
ALTER TABLE [dbo].[PE_CollectionItem] DROP CONSTRAINT [DF_PE_CollectionItem_Detection]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PR_Shop_DeliverItem_GetExpressCompannyList]'
GO
DROP PROCEDURE [dbo].[PR_Shop_DeliverItem_GetExpressCompannyList]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_DownServer_MoveUpOrderId]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_DownServer_MoveUpOrderId] 
	(
	@OrderID int
	)
AS
	SET NOCOUNT ON
	UPDATE PE_DownServer 
	SET OrderID=OrderID+1 WHERE OrderID=@OrderID 
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在创建 [dbo].[PE_CollectionExclosion]'
GO
CREATE TABLE [dbo].[PE_CollectionExclosion]
(
[ExclosionID] [int] NOT NULL,
[ExclosionName] [nvarchar] (255) COLLATE Chinese_PRC_CI_AS NOT NULL,
[ExclosionType] [int] NOT NULL,
[ExclosionStringType] [int] NULL CONSTRAINT [DF_PE_CollectionExclosion_ExclosionStringType] DEFAULT (1),
[ExclosionString] [ntext] COLLATE Chinese_PRC_CI_AS NULL,
[IsExclosionDesignatedNumber] [bit] NULL,
[ExclosionDesignatedNumber] [int] NULL,
[IsExclosionMaxNumber] [bit] NULL,
[ExclosionMaxNumber] [int] NULL,
[IsExclosionMinNumber] [bit] NULL,
[ExclosionMinNumber] [int] NULL,
[IsExclosionDesignatedDateTime] [bit] NULL,
[ExclosionDesignatedDateTime] [datetime] NULL,
[IsExclosionMaxDateTime] [bit] NULL,
[ExclosionMaxDateTime] [datetime] NULL,
[IsExclosionMinDateTime] [bit] NULL,
[ExclosionMinDateTime] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在 [dbo].[PE_CollectionExclosion] 上创建主键 [PK_PE_CollectionExclosion]'
GO
ALTER TABLE [dbo].[PE_CollectionExclosion] ADD CONSTRAINT [PK_PE_CollectionExclosion] PRIMARY KEY CLUSTERED  ([ExclosionID]) ON [PRIMARY]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_DownServer_GetDownOrderId]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_DownServer_GetDownOrderId] 
	(
	@OrderID int
	)
AS
	SET NOCOUNT ON
	SELECT
	     ServerID,ServerName,ServerUrl,ServerLogo,OrderID,ShowType 
    FROM 
         PE_DownServer
    WHERE OrderID > @OrderID 
	ORDER BY OrderID ASC 
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_PayPlatform_GetList]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_PayPlatform_GetList]

AS 
 
	SELECT * FROM PE_PayPlatform order by OrderID asc
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_UserManage_UserGroups_Update]'
GO
ALTER PROCEDURE [dbo].[PR_UserManage_UserGroups_Update]
(
	@GroupID int,    
	@GroupName nvarchar(20),    
	@Description ntext,    
	@Settings ntext,
	@GroupType int,
	@GroupSetting ntext  
)
AS
	UPDATE 
		[PE_UserGroups] 
	SET
			[GroupName] = @GroupName,    
			[Description] = @Description,    
			[Settings] = @Settings,
			[GroupType] = @GroupType,
			[GroupSetting] = @GroupSetting
	WHERE
		[GroupID] = @GroupID
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PE_PaymentLog]'
GO
ALTER TABLE [dbo].[PE_PaymentLog] ADD
[Point] [int] NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_PaymentLog_Update]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_PaymentLog_Update] 
	-- Add the parameters for the stored procedure here
	@paymentLogId int,
	@Status int,    --支付状态
	@PlatformInfo nvarchar(200),    --银行信息
	@Remark nvarchar(255)    --备注
AS

DECLARE @foundStatus int
SELECT @foundStatus=Status FROM PE_PaymentLog WHERE PaymentLogId = @paymentLogId
IF @foundStatus IS NOT NULL
	IF(@foundStatus > 1)
		RETURN 1
	ELSE
		BEGIN
			UPDATE PE_PaymentLog 
			SET Status = @Status,
				PlatformInfo = @PlatformInfo,
				Remark = @Remark				
			WHERE PaymentLogId = @paymentLogId
			RETURN 2
		END
ELSE
	RETURN 0
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_PaymentLog_Add]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_PaymentLog_Add]
(
	@PaymentLogID int,
    @UserName nvarchar(50),   
	@OrderID int,    
	@PaymentNum nvarchar(50),    
	@PlatformID int,    
	@MoneyPay money,   
	@MoneyTrue money,   
	@PayTime datetime,    
	@SuccessTime datetime,   
	@Status int,   
	@PlatformInfo nvarchar(200),   
	@Remark nvarchar(255),
    @Point int
)
AS
	INSERT INTO 
		[PE_PaymentLog] 
		(
			[PaymentLogID],
            [UserName],    
			[OrderID],    
			[PaymentNum],   
			[PlatformID],   
			[MoneyPay],   
			[MoneyTrue],    
			[PayTime],    
			[SuccessTime],   
			[Status],   
			[PlatformInfo],   
			[Remark],
            [Point]
		)
	VALUES
	(
		@PaymentLogID,
        @UserName,
		@OrderID,
		@PaymentNum,
		@PlatformID,
		@MoneyPay,
		@MoneyTrue,
		@PayTime,
		@SuccessTime,
		@Status,
		@PlatformInfo,
		@Remark,
        @Point
	)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_DownServer_Delete]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_DownServer_Delete]
	(
	@ServerID int
	)
AS
	--SET NOCOUNT ON
	--处理 排序
	DECLARE @oldOrderId INT
	SELECT @oldOrderId=OrderId FROM PE_DownServer WHERE ServerID = @ServerID
	IF(@oldOrderId >= 0)
		BEGIN
			UPDATE PE_DownServer SET OrderID =OrderID-1 WHERE OrderID > @oldOrderId 
		END
    --处理相关的下载报错信息
        /*DECLARE @oldErrorId INT
        Delete
                PE_DownError
        Where [ErrorId] in 
                        (select D.ErrorId from PE_DownError D left join PE_Soft S on D.InfoId=S.SoftId where D.UrlId = @ServerID
                        )*/

	Delete
		PE_DownServer
	Where
		[ServerID]=@ServerID  
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PE_Nodes]'
GO
ALTER TABLE [dbo].[PE_Nodes] ADD
[NeedCreateHtml] [bit] NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在创建 [dbo].[PR_SPLT_STR_LEWI]'
GO
SET ANSI_NULLS OFF
GO
CREATE 
  FUNCTION [dbo].[PR_SPLT_STR_LEWI](@String NVARCHAR(4000), @SplitChar NVARCHAR(10))
  RETURNS @table TABLE (COL VARCHAR (100)) AS  
BEGIN  
   DECLARE  @Index  INT  
   SET  @Index  =  0  
        IF @String <> ''
        BEGIN            
            IF RIGHT(@String,1)<> @SplitChar 
            SET @String = @String + @SplitChar
            IF LEFT(@String,1)= @SplitChar 
            SET @String = STUFF(@String, 1, 1, '')
        End
       WHILE  CHARINDEX(@SplitChar,@String,@Index)  >  0    
       BEGIN  
           INSERT INTO @table(COL) 
           VALUES (SUBSTRING(@String, @Index, CHARINDEX(@SplitChar, @String,@Index) - @Index))
           SET @index = CHARINDEX(@SplitChar, @String, @Index) + 1 
       END  
RETURN  
END
GO
SET ANSI_NULLS ON
GO
PRINT N'正在创建 [dbo].[PR_RLTD_KIWD_SPLT_LEWI]'
GO
CREATE PROCEDURE [dbo].[PR_RLTD_KIWD_SPLT_LEWI]
@TableName VARCHAR(50),
@ContentID VARCHAR(50)
AS
BEGIN
DECLARE @GetKeywords NVARCHAR(100)
DECLARE @GetKeyword NVARCHAR(50)
DECLARE @String NVARCHAR(4000)
SET @GetKeywords='SELECT @Key=Keyword  FROM '+@TableName+' WHERE id='+@ContentID+' AND Keyword !=''||'''
EXEC SP_EXECUTESQL @GetKeywords,N'@Key VARCHAR(100) OUTPUT',@String OUTPUT
SELECT COL FROM dbo.PR_SPLT_STR_LEWI(@String,'|')
END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_DownServer_MoveOrderId]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_DownServer_MoveOrderId] 
	(
	@ServerID int,
    @AddNum int
	)
AS
	--SET NOCOUNT ON
	UPDATE PE_DownServer 
	SET OrderID=OrderID+@AddNum WHERE ServerID=@ServerID 
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_PaymentLog_GetList]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_PaymentLog_GetList] 
AS
BEGIN
	SELECT * FROM [PE_PaymentLog] ORDER BY [PaymentLogID] DESC
END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Contents_Model_AddTemplateForNodes]'
GO
ALTER PROCEDURE [dbo].[PR_Contents_Model_AddTemplateForNodes]
	(
    @NodeId int,
    @TemplateId int,
    @IsDefault bit
	)
AS
	--SET NOCOUNT ON 
	INSERT INTO
        PE_Nodes_Template(NodeId,TemplateId,IsDefault)VALUES(@NodeId,@TemplateId,@IsDefault)  
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Contents_Nodes_Add]'
GO
ALTER PROCEDURE [dbo].[PR_Contents_Nodes_Add] 
(
	@NodeId int,
	@NodeIdentifier nvarchar(50),
	@NodeType int,
	@ParentId int,
	@ParentPath ntext,
	@Depth int,
	@RootId int,
	@Child int,
	@ArrChildId ntext,
	@PrevId int,
	@NextId int,
	@OrderId int,
	@NodeDir nvarchar(50),
	@ParentDir ntext,
	@NodeName nvarchar(50),
	@Tips nvarchar(255),
	@Description ntext,
	@NodePicUrl nvarchar(255),
	@MetaKeywords nvarchar(255),
	@MetaDescription nvarchar(255),
	@ShowOnMenu bit,
	@ShowOnPath bit,
	@ShowOnMap bit,
	@ShowOnListIndex bit,
	@ShowOnListParent bit,
	@PurviewType int,
	@Creater nvarchar(50),
	@InheritPurviewFromParent int,
	@WorkFlowId int,
	@HitsOfHot int,
	@OpenType int,
	@ItemCount int,
	@ItemChecked int,
	@CommentCount int,
	@CustomContent ntext,
	
	@IsCreateContentPage bit,
	@IsCreateListPage bit,
	@AutoCreateHtmlType int,
	@ContentPageHtmlRule nvarchar(255),
	
	@ListPageHtmlRule nvarchar(255),
	@ListPageSavePathType int,
	@ListPagePostfix nvarchar(50),
	
	@RelateNode ntext,
	@RelateSpecial ntext,
	
	@ItemAspxFileName nvarchar(50),
	@DefaultTemplateFile nvarchar(255),
	@ContainChildTemplateFile nvarchar(255),
	@ItemOpenType int,
	@ItemListOrderType int,
	@ItemPageSize int,
	@LinkUrl nvarchar(255),
	@Settings ntext,
	@NeedCreateHtml bit
)

AS
	INSERT	INTO PE_Nodes
		(
			NodeID,
			NodeIdentifier,
			NodeType,
			ParentID,
			ParentPath,
			Depth,
			RootID,
			Child,
			arrChildID,
			PrevID,
			NextID,
			OrderID,
			NodeDir,
			ParentDir,
			NodeName,
			Tips,
			Description,
			NodePicUrl,
			Meta_Keywords,
			Meta_Description,
			ShowOnMenu,
			ShowOnPath,
			ShowOnMap,
			ShowOnList_Index,
			ShowOnList_Parent,
			PurviewType,
			Creater,
			InheritPurviewFromParent,
			WorkFlowID,
			HitsOfHot,
			OpenType,
			ItemCount,
			ItemChecked,
			CommentCount,
			Custom_Content,
			
			IsCreateContentPage,
			IsCreateListPage,
			AutoCreateHtmlType,
			ContentPageHtmlRule,
			ListPageHtmlRule,
			ListPageSavePathType,
			ListPagePostfix,
			
			RelateNode,
			RelateSpecial,
			
			ItemAspxFileName,
			DefaultTemplateFile,
			ContainChildTemplateFile,
			ItemOpenType,
			ItemListOrderType,
			ItemPageSize,
			LinkUrl,
			Settings,
			NeedCreateHtml
		)
		VALUES
		(
			@NodeId,
			@NodeIdentifier,
			@NodeType,
			@ParentId,
			@ParentPath,
			@Depth,
			@RootId,
			@Child,
			@ArrChildId,
			@PrevId,
			@NextId,
			@OrderId,
			@NodeDir,
			@ParentDir,
			@NodeName,
			@Tips,
			@Description,
			@NodePicUrl,
			@MetaKeywords,
			@MetaDescription,
			@ShowOnMenu,
			@ShowOnPath,
			@ShowOnMap,
			@ShowOnListIndex,
			@ShowOnListParent,
			@PurviewType,
			@Creater,
			@InheritPurviewFromParent,
			@WorkFlowId,
			@HitsOfHot,
			@OpenType,
			@ItemCount,
			@ItemChecked,
			@CommentCount,
			@CustomContent,
			
			@IsCreateContentPage,
			@IsCreateListPage,
			@AutoCreateHtmlType,
			@ContentPageHtmlRule,
			@ListPageHtmlRule,
			
			@ListPageSavePathType,
			@ListPagePostfix,
			
			@RelateNode,
			@RelateSpecial,

			@ItemAspxFileName,
			@DefaultTemplateFile,
			@ContainChildTemplateFile,
			@ItemOpenType,
			@ItemListOrderType,
			@ItemPageSize,
			@LinkUrl,
			@Settings,
			@NeedCreateHtml
		)
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_TransferLog_Add]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_TransferLog_Add]  
	(
	--@TransferLogID int,    --订单过户记录ID
	@OrderID int,    --订单ID
	@TransferTime datetime,    --过户时间
	@OwnerUserName nvarchar(20),    --过户人
	@PayerUserName nvarchar(20),    --付款人
	@TargetUserName nvarchar(20),    --过户给
	@Poundage money,    --过户费
	@Inputer nvarchar(20),    --经手人
	@Remark nvarchar(50)    --备注
	)
AS
Insert Into [PE_TransferLog] 
	(
	--[TransferLogID],    --订单过户记录ID
	[OrderID],    --订单ID
	[TransferTime],    --过户时间
	[OwnerUserName],    --过户人
	[PayerUserName],    --付款人
	[TargetUserName],    --过户给
	[Poundage],    --过户费
	[Inputer],    --经手人
	[Remark]    --备注
	)
Values
	(
	--@TransferLogID,
	@OrderID,
	@TransferTime,
	@OwnerUserName,
	@PayerUserName,
	@TargetUserName,
	@Poundage,
	@Inputer,
	@Remark
	)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_PayPlatform_SetOrderId]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_PayPlatform_SetOrderId]
	
(
	@PayPlatformID int,
	@OrderID int
)
AS
IF NOT EXISTS ( Select OrderID From PE_PayPlatform Where OrderID=@OrderID and [PayPlatformID]=@PayPlatformID)
	BEGIN
		UPDATE
			PE_PayPlatform
		SET
			[OrderID] = @OrderID
		WHERE
			[PayPlatformID] = @PayPlatformID
	END	
RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_DownloadError_Clear]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_DownloadError_Clear] 
AS
	--SET NOCOUNT ON
	DELETE 
		FROM PE_DownloadError 
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_PresentProject_List]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_PresentProject_List]
 (@id INT = 0)
AS
	IF(@id = 0)
		BEGIN
			SELECT 
			[ProjectID], [ProjectName], [BeginDate], [EndDate], [MinMoney], [MaxMoney],[PresentContent], [Price], [PresentID], [Cash], [PresentExp], [PresentPoint], [Disabled]
			FROM PE_PresentProject
			ORDER BY ProjectID ASC
		END
	ELSE
		BEGIN
			SELECT 
			[ProjectID], [ProjectName], [BeginDate], [EndDate], [MinMoney], [MaxMoney],[PresentContent], [Price], [PresentID], [Cash], [PresentExp], [PresentPoint], [Disabled]
			FROM PE_PresentProject
			WHERE ProjectID = @id
		END 
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Analytics_AddNum]'
GO
ALTER PROCEDURE [dbo].[PR_Analytics_AddNum]
	@TableName varchar(100),
	@CompareField varchar(100),
	@AddField varchar(100),
	@Data varchar(4000)
AS
BEGIN
	exec('update ' + @TableName +' set ['+ @AddField +']=['+@AddField+']+1 where '+@CompareField +' = '''+@Data+'''')
	if @@ROWCOUNT = 0
	BEGIN
		exec('insert into '+@TableName+'(['+@CompareField+'],['+@AddField+']) values ('''+@Data+''',1)')
	END
END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Contents_Nodes_UpDate]'
GO
ALTER PROCEDURE [dbo].[PR_Contents_Nodes_UpDate]
(
	@NodeId int,
	@NodeIdentifier nvarchar(50),
	
	@ParentId int,
	@ParentPath ntext,
	@Depth int,
	@RootId int,
	@Child int,
	@ArrChildId ntext,
	@PrevId int,
	@NextId int,
	@OrderId int,
	@NodeDir nvarchar(50),
	@ParentDir ntext,
	
	
	@NodeName nvarchar(50),
	@Tips nvarchar(255),
	@Description ntext,
	@NodePicUrl nvarchar(255),
	@MetaKeywords nvarchar(255),
	@MetaDescription nvarchar(255),
	@ShowOnMenu bit,
	@ShowOnPath bit,
	@ShowOnMap bit,
	@ShowOnListIndex bit,
	@ShowOnListParent bit,
	@PurviewType int,
	@Creater nvarchar(50),
	@InheritPurviewFromParent int,
	@WorkFlowId int,
	@HitsOfHot int,
	@OpenType int,
	@ItemCount int,
	@ItemChecked int,
	@CommentCount int,
	@CustomContent ntext,
	
	@IsCreateContentPage bit,
	@IsCreateListPage bit,
	@AutoCreateHtmlType int,
	@ContentPageHtmlRule nvarchar(255),
	@ListPageHtmlRule nvarchar(255),
	@ListPageSavePathType int,
	@ListPagePostfix nvarchar(50),
	
	
	@RelateNode ntext,
	@RelateSpecial ntext,
	
	@ItemAspxFileName nvarchar(50),
	@DefaultTemplateFile nvarchar(255),
	@ContainChildTemplateFile nvarchar(255),
	@ItemOpenType int,
	@ItemListOrderType int,
	@ItemPageSize int,
	@LinkUrl nvarchar(255),
	@Settings ntext,
	@NeedCreateHtml bit
)

AS
	UPDATE
			PE_Nodes
	SET
			NodeIdentifier =@NodeIdentifier,
			
			ParentId=@ParentId,
			ParentPath=@ParentPath,
			Depth=@Depth,
			RootId=@RootId,
			Child=@Child,
			ArrChildId=@ArrChildId,
			PrevId=@PrevId,
			NextId=@NextId,
			OrderId=@OrderId,
			NodeDir=@NodeDir,
			ParentDir=@ParentDir,

			NodeName = @NodeName,
			Tips = @Tips,
			Description = @Description,
			NodePicUrl = @NodePicUrl,
			Meta_Keywords = @MetaKeywords,
			Meta_Description = @MetaDescription,
			ShowOnMenu = @ShowOnMenu,
			ShowOnPath = @ShowOnPath,
			ShowOnMap = @ShowOnMap,
			ShowOnList_Index = @ShowOnListIndex,
			ShowOnList_Parent = @ShowOnListParent,
			PurviewType = @PurviewType,
			Creater = @Creater,
			InheritPurviewFromParent = @InheritPurviewFromParent,
			WorkFlowID = @WorkFlowId,
			HitsOfHot = @HitsOfHot,
			OpenType = @OpenType,
			ItemCount = @ItemCount,
			ItemChecked = @ItemChecked,
			CommentCount = @CommentCount,
			Custom_Content = @CustomContent,
			IsCreateContentPage = @IsCreateContentPage,
			IsCreateListPage  = @IsCreateListPage ,
			AutoCreateHtmlType = @AutoCreateHtmlType,
			ContentPageHtmlRule = @ContentPageHtmlRule,
			ListPageHtmlRule = @ListPageHtmlRule,
			ListPageSavePathType=@ListPageSavePathType,
			ListPagePostfix=@ListPagePostfix,
			RelateNode = @RelateNode,
			RelateSpecial = @RelateSpecial,
			
			ItemAspxFileName = @ItemAspxFileName,
			DefaultTemplateFile = @DefaultTemplateFile,
			ContainChildTemplateFile = @ContainChildTemplateFile,
			ItemOpenType = @ItemOpenType,
			ItemListOrderType = @ItemListOrderType,
			ItemPageSize = @ItemPageSize,
			LinkUrl = @LinkUrl,
			Settings = @Settings,
			NeedCreateHtml=@NeedCreateHtml
	WHERE
			NodeID = @NodeID
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_PayPlatform_Add]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_PayPlatform_Add]
(
	@PayPlatformID int,
	@PayPlatformName nvarchar(100) ,
	@AccountsID nvarchar(100) ,
	@MD5 nvarchar(200) ,
	@Rate numeric(18,2) ,
	@IsDisabled bit,
    @IsDefault bit
)
 AS 

  DECLARE @TempID int

		SELECT
			@TempID = (ISNULL(MAX(OrderId),0) + 1)
		FROM
			PE_PayPlatform
 IF @IsDefault = 1
Begin
	Update PE_PayPlatform set [IsDefault]=0 where [IsDefault]=1
end

			
 SET NOCOUNT OFF 
 
	INSERT
	INTO
		PE_PayPlatform
		(
			[PayPlatformID],
			[PayPlatformName],
			[AccountsID],
			[MD5],
			[Rate],
			[OrderID],
			[IsDisabled],
			[IsDefault]
		)
	VALUES
		(
			@PayPlatformID,
			@PayPlatformName,
			@AccountsID,
			@MD5,
			@Rate,
			@TempID,
			@IsDisabled,
            @IsDefault
		)
		RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_PresentProject_Add]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_PresentProject_Add] 	
	(
    @ProjectName NVarChar(50),
    @BeginDate DateTime,
    @EndDate DateTime,
    @MinMoney Money,
    @MaxMoney Money,
    @PresentContent NVarChar(50),
    @Price Money,
    @PresentID NText,
    @Cash Int,
    @PresentExp Int,
    @PresentPoint Int,
    @Disabled bit
	)	
AS
/*	
set IDENTITY_INSERT PE_PresentProject ON*/
	INSERT INTO PE_PresentProject
	      (	[ProjectName], [BeginDate], [EndDate], [MinMoney], [MaxMoney],[PresentContent], [Price], [PresentID], [Cash], [PresentExp], [PresentPoint], [Disabled])
	VALUES (@ProjectName,@BeginDate,@EndDate,@MinMoney,@MaxMoney,@PresentContent, @Price, @PresentID,@Cash,@PresentExp,@PresentPoint, @Disabled)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Contents_Model_ModelIdExists]'
GO
ALTER PROCEDURE [dbo].[PR_Contents_Model_ModelIdExists]
(
    @NodeId int,
    @ModelId int
)
AS
SELECT COUNT(*) FROM PE_Nodes_Model_Template WHERE NodeId=@NodeId AND ModelId=@ModelId
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_DownServer_Update]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_DownServer_Update] 
	(
	@ServerID int,
    @ServerName nvarchar(50),
    @ServerUrl nvarchar(50),
    @ServerLogo nvarchar(255),
    @OrderID int,
    @ShowType int
	)
AS
	--SET NOCOUNT ON 
	UPDATE PE_DownServer 
	SET ServerName=@ServerName,ServerUrl=@ServerUrl,ServerLogo=@ServerLogo,OrderID=@OrderID,ShowType=@ShowType WHERE ServerID=@ServerID
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Analytics_StatIPInfo_Update]'
GO
ALTER PROCEDURE [dbo].[PR_Analytics_StatIPInfo_Update]
	@StartIp float,
	@EndIp float,
	@Address nvarchar(70),
	@oldStartIp float,
	@oldEndIp	float
AS
declare @id int

select @id=id from pe_statipinfo where StartIP=@oldStartIp and EndIP=@oldEndIp

IF Not Exists(
 select * from  pe_statipinfo where id<>@id and ( 
		   (StartIP>@StartIp and EndIP<@EndIp)
		or (@StartIp between StartIP and EndIP) 
		or (@EndIp between StartIP and EndIP))
)
Begin
	update 
		pe_statipinfo
	set 
		StartIp = @StartIp,
		EndIp = @EndIp,
		Address = @Address
	where 
		StartIp = @oldStartIp and 	EndIp = @oldEndIp
End
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_Choiceset_GetList]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_Choiceset_GetList] 
AS
	SET NOCOUNT ON
	SELECT
	     FieldID,Title,TableName,FieldName,FieldValue 
    FROM 
         PE_Dictionary 
	ORDER BY TableName,FieldID ASC 
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_Message_Add]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_Message_Add]
(
	@Title nvarchar(100),    --标题
	@Content ntext,    --内容
	@Sender nvarchar(20),    --发件人
	@Incept nvarchar(4000),    --收件人
	@SendTime datetime,    --发送日期
	@IsSend int,    --是否被发送，0为没发送，1为被发送
	@IsDelInbox int,    --收件箱中信息的删除标记，0为未删除，1为已删除
	@IsDelSendbox int,    --发件箱中信息的删除标记，0为未删除，1为已删除
	@IsRead int    --短消息状态，0为未读，1为已读
)
AS
	DECLARE   @Index   INT 
	DECLARE   @SplitChar nvarchar(2) --分隔符，格式：“，”

	SET   @Index   =   0
	SET   @SplitChar = ','   
    IF CHARINDEX( @SplitChar, @Incept, @Index) = 0
		BEGIN
			INSERT INTO 
				[PE_Message] 
				(
					[Title],    --标题
					[Content],    --内容
					[Sender],    --发件人
					[Incept],    --收件人
					[SendTime],    --发送日期
					[IsSend],    --是否被发送，0为没发送，1为被发送
					[IsDelInbox],    --收件箱中信息的删除标记，0为未删除，1为已删除
					[IsDelSendbox],    --发件箱中信息的删除标记，0为未删除，1为已删除
					[IsRead]    --短消息状态，0为未读，1为已读
				)
			VALUES
			(
				@Title,
				@Content,
				@Sender,
				@Incept,
				@SendTime,
				@IsSend,
				@IsDelInbox,
				@IsDelSendbox,
				@IsRead
			)
		END
	ElSE
		BEGIN
			WHILE  CHARINDEX( @SplitChar, @Incept, @Index)   >   0  
				BEGIN
					INSERT INTO 
						[PE_Message] 
						(
							[Title],    --标题
							[Content],    --内容
							[Sender],    --发件人
							[Incept],    --收件人
							[SendTime],    --发送日期
							[IsSend],    --是否被发送，0为没发送，1为被发送
							[IsDelInbox],    --收件箱中信息的删除标记，0为未删除，1为已删除
							[IsDelSendbox],    --发件箱中信息的删除标记，0为未删除，1为已删除
							[IsRead]    --短消息状态，0为未读，1为已读
						)
					VALUES
					(
						@Title,
						@Content,
						@Sender,
						SUBSTRING(@Incept, @Index, CHARINDEX(@SplitChar, @Incept, @Index) - @Index),
						@SendTime,
						@IsSend,
						@IsDelInbox,
						@IsDelSendbox,
						@IsRead
					)
					SET @Index = CHARINDEX(@SplitChar, @Incept, @Index) + 1 
				END
				INSERT INTO 
						[PE_Message] 
						(
							[Title],    --标题
							[Content],    --内容
							[Sender],    --发件人
							[Incept],    --收件人
							[SendTime],    --发送日期
							[IsSend],    --是否被发送，0为没发送，1为被发送
							[IsDelInbox],    --收件箱中信息的删除标记，0为未删除，1为已删除
							[IsDelSendbox],    --发件箱中信息的删除标记，0为未删除，1为已删除
							[IsRead]    --短消息状态，0为未读，1为已读
						)
					VALUES
					(
						@Title,
						@Content,
						@Sender,
						SUBSTRING(@Incept, @Index, LEN(@Incept) - @Index + 1),
						@SendTime,
						@IsSend,
						@IsDelInbox,
						@IsDelSendbox,
						@IsRead
					)
			END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_DownServer_GetServerUrlByServerId]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_DownServer_GetServerUrlByServerId] 
	(
	@ServerID int
	)
AS
	SET NOCOUNT ON
	SELECT
		  ServerID,ServerUrl
    FROM 
          PE_DownServer
	WHERE ServerID = @ServerID
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_Choiceset_GetChoicesetFieldValueByName]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_Choiceset_GetChoicesetFieldValueByName] 
(
    @TableName nvarchar(30),
    @FieldName nvarchar(30)
	)
AS
	SET NOCOUNT ON 
	SELECT 
	    * 
	FROM 
	    PE_Dictionary 
	WHERE 
	    TableName=@TableName AND FieldName=@FieldName
	ORDER BY FieldID ASC
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_PresentProject_Update]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_PresentProject_Update] 	
	(
	@ProjectId Int,
    @ProjectName NVarChar(50),
    @BeginDate DateTime,
    @EndDate DateTime,
    @MinMoney Money,
    @MaxMoney Money,
    @PresentContent NVarChar(50),
    @Price Money,
    @PresentID NText,
    @Cash Int,
    @PresentExp Int,
    @PresentPoint Int,
    @Disabled bit
	)	
AS
	set IDENTITY_INSERT PE_PresentProject ON
	UPDATE PE_PresentProject 
	SET ProjectName = @ProjectName,
		BeginDate = @BeginDate,
		EndDate = @EndDate,
		MinMoney = @MinMoney,
		MaxMoney = @MaxMoney,
		PresentContent = @PresentContent, 
		Price = @Price, 
		PresentID = @PresentID,
		Cash = @Cash,
		PresentExp = @PresentExp,
		PresentPoint = @PresentPoint 
	WHERE ProjectId = @ProjectId
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_PayPlatform_GetListOfdisabled]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_PayPlatform_GetListOfdisabled]
(
	@isdisabled bit
)
AS
	SELECT
		 * 
	FROM
		PE_PayPlatform
	WHERE
		IsDisabled=@isdisabled
	ORDER BY OrderID ASC
	
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Contents_Nodes_UpdateBasicNodesInfo]'
GO
ALTER PROCEDURE [dbo].[PR_Contents_Nodes_UpdateBasicNodesInfo] 

(	
	@NodeID int,
    @RootID int,
    @OrderID int,
    @ParentID int,
    @Child int,
    @ParentPath ntext,
    @Depth int,
    @PrevID int,
    @NextID int,
    @ArrChildID ntext,
    @ParentDir ntext,
    @NodeDir nvarchar(50),
    @PurviewType int
)

AS
	UPDATE
			PE_Nodes
	SET
		    RootID = @RootID,
			OrderID = @OrderID,
			ParentID = @ParentID,
			Child = @Child,
			ParentPath = @ParentPath,
			Depth = @Depth,
			PrevID = @PrevID,
			NextID = @NextID,
			arrChildID = @ArrChildID,
			ParentDir = @ParentDir,
			NodeDir = @NodeDir,
			PurviewType = @PurviewType
	WHERE
			NodeID = @NodeID
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_DownServer_GetUpOrderId]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_DownServer_GetUpOrderId] 
	(
	@OrderID int
	)
AS
	SET NOCOUNT ON
	SELECT
	     ServerID,ServerName,ServerUrl,ServerLogo,OrderID,ShowType 
    FROM 
         PE_DownServer
    WHERE OrderID < @OrderID 
	ORDER BY OrderID DESC  
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_UserManage_UserGroups_Add]'
GO
ALTER PROCEDURE [dbo].[PR_UserManage_UserGroups_Add]
(
	@GroupID int,    
	@GroupName nvarchar(20),    
	@Description ntext,    
	@Settings ntext,
	@GroupType int,
	@GroupSetting ntext
)
AS

	INSERT INTO 
		[PE_UserGroups] 
		(
			[GroupID],    
			[GroupName],    
			[Description],    
			[Settings],
			[GroupType],
			[GroupSetting]
		)
	VALUES
	(
		@GroupID,
		@GroupName,
		@Description,
		@Settings,
		@GroupType,
		@GroupSetting
	)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_DownServer_SetShowType]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_DownServer_SetShowType] 
	(
	@ServerID nvarchar(4000),--格式：“1，2，3，4，”
	@ShowType int,
	@SplitChar nvarchar(10)--格式：“，”
	)
AS
	--SET NOCOUNT ON 
	DECLARE   @TemptTable   TABLE   (ID   INT)   
    DECLARE   @Index   INT   
	SET   @Index   =   0   
    IF CHARINDEX(   @SplitChar,   @ServerID,   @Index) = 0
        UPDATE PE_DownServer 
	    SET ShowType=@ShowType 
	    WHERE ServerID=@ServerID
	ELSE
		WHILE   CHARINDEX(   @SplitChar,   @ServerID,   @Index)   >   0   --循环   
		BEGIN   
			INSERT   INTO   @TemptTable   (ID)   
			VALUES(SUBSTRING(@ServerID,   @Index,   CHARINDEX(@SplitChar,   @ServerID,   @Index)   -   @Index))     
			SET   @Index   =     CHARINDEX(@SplitChar,   @ServerID,   @Index)   +   1   
		END   

		UPDATE PE_DownServer 
		SET ShowType=@ShowType 
		WHERE 
	    (ServerID 
		IN
		  (
		  SELECT ID FROM @TemptTable
		  )
		 )  
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_PresentProject_Locked]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_PresentProject_Locked]
	(
	@ProjectId INT,
	@Disabled BIT
	)
AS

	Update
		PE_PresentProject
	SET
		[Disabled] = @Disabled
	WHERE
		[ProjectID] = @ProjectId
		
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_TransferLog_GetList]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_TransferLog_GetList]
AS
BEGIN
	SELECT 
	[TransferLogID],    --订单过户记录ID
	[OrderID],    --订单ID
	[TransferTime],    --过户时间
	[OwnerUserName],    --过户人
	[PayerUserName],    --付款人
	[TargetUserName],    --过户给
	[Poundage],    --过户费
	[Inputer],    --经手人
	[Remark]    --备注
	FROM [PE_TransferLog] 
	ORDER BY [TransferLogID] DESC

END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_PaymentLog_Update2]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_PaymentLog_Update2]
(
	@PaymentLogID int,    --支付ID
	@UserName nvarchar(50),    --会员用户名
	@OrderID int,    --订单ID
	@PaymentNum nvarchar(50),    --支付序号
	@PlatformID int,    --支付平台
	@MoneyPay money,    --支付金额
	@MoneyTrue money,    --实际支付金额
	@PayTime datetime,    --交易时间
	@SuccessTime datetime,    --交易成功时间
	@Status int,    --支付状态
	@PlatformInfo nvarchar(200),    --银行信息
	@Remark nvarchar(255),   --备注
    @Point int
)
AS
	UPDATE 
		[PE_PaymentLog] 
	SET
			[UserName] = @UserName,    --会员用户名
			[OrderID] = @OrderID,    --订单ID
			[PaymentNum] = @PaymentNum,    --支付序号
			[PlatformID] = @PlatformID,    --支付平台
			[MoneyPay] = @MoneyPay,    --支付金额
			[MoneyTrue] = @MoneyTrue,    --实际支付金额
			[PayTime] = @PayTime,    --交易时间
			[SuccessTime] = @SuccessTime,    --交易成功时间
			[Status] = @Status,    --支付状态
			[PlatformInfo] = @PlatformInfo,    --银行信息
			[Remark] = @Remark,  --备注
            [Point] = @Point
	WHERE
		[PaymentLogID] = @PaymentLogID
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_DownServer_GetMaxOrderId]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_DownServer_GetMaxOrderId]
AS
	SET NOCOUNT ON  
	SELECT ISNULL(MAX(OrderID),0) 
	FROM PE_DownServer
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_DownServer_MoveDownOrderId]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_DownServer_MoveDownOrderId] 
	(
	@OrderID int
	)
AS
	SET NOCOUNT ON
	UPDATE PE_DownServer 
	SET OrderID=OrderID-1 WHERE OrderID=@OrderID 
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_PayPlatform_Delete]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_PayPlatform_Delete]
(
	@PayPlatformID int
)
 AS 
 SET NOCOUNT OFF
	DELETE
		PE_PayPlatform
	WHERE
		[PayPlatformID] = @PayPlatformID
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Contents_Comment_Add]'
GO
ALTER PROCEDURE [dbo].[PR_Contents_Comment_Add]
(
	@CommentId int,
	@GeneralId int,
	@TopicId int,
	@NodeId int,
	@CommentTitle nvarchar(255),
	@Content ntext,
	@UpdateDateTime datetime,
	@Score int,
	@Position int,
	@Status bit,
	@Ip nvarchar(50),
	@IsPrivate bit,
	@UserName nvarchar(50),
	@Face nvarchar(255)
)
AS
INSERT INTO 
	[PE_Comment]
		(
			CommentId,
			GeneralId,
			TopicId,
			NodeId,
			CommentTitle,
			[Content],
			UpdateDateTime,
			Score,
			[Position],
			Status,
			Agree,
			Oppose,
			Neutral,
			IP,
			IsElite,
			IsPrivate,
			UserName,
			Face
		)
	Values
		(
			@CommentId,
			@GeneralId,
			@TopicId,
			@NodeId,
			@CommentTitle,
			@Content,
			@UpdateDateTime,
			@Score,
			@Position,
			@Status,
			0,
			0,
			0,
			@IP,
			0,
			@IsPrivate,
			@UserName,
			@Face
		)
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Analytics_StatIPInfo_Add]'
GO
ALTER PROCEDURE [dbo].[PR_Analytics_StatIPInfo_Add]
	@StartIp float,
	@EndIp float,
	@Address nvarchar(70)

AS

IF NOT EXISTS (
	SELECT id FROM PE_StatIpInfo Where  
		   (StartIp>@StartIP and EndIp<@EndIp)
		or (@StartIp between StartIP and EndIP) 
		or (@EndIp between StartIP and EndIP)
)
begin
	Insert into 
		Pe_statipinfo(StartIp,EndIp,Address)
	VALUES
		(@StartIp,@EndIp,@Address)
end
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_DownServer_GetCountOrderId]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_DownServer_GetCountOrderId] 
AS
	SET NOCOUNT ON  
	SELECT ISNULL(COUNT(ServerID),0) 
	FROM PE_DownServer
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_DownServer_GetList]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_DownServer_GetList]
AS
	SET NOCOUNT ON
	SELECT
	     ServerID,ServerName,ServerUrl,ServerLogo,OrderID,ShowType 
    FROM 
         PE_DownServer 
	ORDER BY OrderID ASC 
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Contents_Model_AddModelForNodes]'
GO
ALTER PROCEDURE [dbo].[PR_Contents_Model_AddModelForNodes]
	(
    @NodeId int,
    @ModelId int,
     @DefaultTemplateFile nvarchar(255)
	)
AS
	--SET NOCOUNT ON 
	INSERT INTO
        PE_Nodes_Model_Template(NodeId,ModelId,DefaultTemplateFile)VALUES(@NodeId,@ModelId,@DefaultTemplateFile)  
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_DownloadError_Delete]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_DownloadError_Delete] 
	(
	@ErrorID nvarchar(4000),--格式：“1，2，3，4，”
	@SplitChar nvarchar(10)--格式：“，”
	)
AS
	--SET NOCOUNT ON 
	DECLARE   @TemptTable   TABLE   (ID   INT)   
    DECLARE   @Index   INT   
	SET   @Index   =   0   
    IF CHARINDEX(   @SplitChar,   @ErrorID,   @Index) = 0
        DELETE 
        FROM PE_DownloadError 
	    WHERE ErrorID=@ErrorID
	ELSE
		WHILE   CHARINDEX(   @SplitChar,   @ErrorID,   @Index)   >   0   --循环   
		BEGIN   
			INSERT   INTO   @TemptTable   (ID)   
			VALUES(SUBSTRING(@ErrorID,   @Index,   CHARINDEX(@SplitChar,   @ErrorID,   @Index)   -   @Index))     
			SET   @Index   =     CHARINDEX(@SplitChar,   @ErrorID,   @Index)   +   1   
		END  
		 
		DELETE 
		FROM PE_DownloadError 
		WHERE 
	    (ErrorID 
		IN
		  (
		  SELECT ID FROM @TemptTable
		  )
		 )  
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_Message_GetById]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_Message_GetById] 
	(
	@MessageId int

	)

AS
	SET NOCOUNT OFF 
	SELECT
		*
	FROM
		PE_Message
	WHERE
		MessageID=@MessageId
		
	
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_Files_Add]'
GO
-- Stored procedure
ALTER PROCEDURE [dbo].[PR_Accessories_Files_Add]

(
	@ID int,
	@Name nvarchar(255),
	@Size int,
	@Quote int,
	@Path nvarchar(1000),
	@IsThumb bit
)

AS
INSERT	INTO 
	PE_Files
	(
		[ID],
		[Name],
		[Size],
		[Quote],
		[Path],
		[IsThumb]
	)
VALUES
	(
		@ID,
		@Name,
		@Size,
		@Quote,
		@Path,
		@IsThumb
	)
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Contents_Model_TemplateIdExists]'
GO
ALTER PROCEDURE [dbo].[PR_Contents_Model_TemplateIdExists]
(
    @NodeId int,
    @TemplateId int
)
AS
SELECT COUNT(*) FROM PE_Nodes_Template WHERE NodeId=@NodeId AND TemplateId=@TemplateId
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_PresentProject_Delete]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_PresentProject_Delete]
(
	@ProjectId INT
)
 AS 
	DELETE
		PE_PresentProject
	WHERE
		[ProjectID] = @ProjectId
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_PayPlatform_DisablePayPlatform]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_PayPlatform_DisablePayPlatform]

	(
	@PayPlatformID int,
	@IsDisabled bit
	)
AS
SET NOCOUNT OFF
UPDATE
	PE_PayPlatform
SET
	[IsDisabled]=@IsDisabled
WHERE
	[PayPlatformID] = @PayPlatformID

	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_DownServer_UpdateOrderId]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_DownServer_UpdateOrderId] 
	(
	@ServerID int,
	@OrderID int
	)
AS
	--SET NOCOUNT ON
	UPDATE PE_DownServer 
	SET OrderID=@OrderID WHERE ServerID=@ServerID
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_DownServer_Add]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_DownServer_Add]
	(
	@ServerID int,
	@ServerName nvarchar(50),
    @ServerUrl nvarchar(50),
    @ServerLogo nvarchar(255),
    @OrderID int,
    @ShowType int
	)
AS
	--SET NOCOUNT ON 
	INSERT INTO
        PE_DownServer(ServerID,ServerName,ServerUrl,ServerLogo,OrderID,ShowType)VALUES(@ServerID,@ServerName,@ServerUrl,@ServerLogo,@OrderID,@ShowType)  
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_Choiceset_SetFieldValue]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_Choiceset_SetFieldValue] 
	(
    @FieldValue ntext,
    @TableName nvarchar(30),
    @FieldName nvarchar(30)
	)
AS
	--SET NOCOUNT ON 
	UPDATE PE_Dictionary
	SET FieldValue=@FieldValue WHERE TableName=@TableName AND FieldName=@FieldName
	--SET NOCOUNT OFF
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_PayPlatform_Update]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_PayPlatform_Update]
(
	@PayPlatformID int,
	@PayPlatformName nvarchar(100),
	@AccountsID nvarchar(100),
	@MD5 nvarchar(200),
	@Rate Decimal(18,10),
	@OrderID int,
	@IsDisabled bit,
    @IsDefault bit
)
 AS 

 IF @IsDefault = 1
Begin
	Update PE_PayPlatform set [IsDefault]=0 where [IsDefault]=1
end
 
	UPDATE
		PE_PayPlatform
	SET 
		[PayPlatformName] = @PayPlatformName,
		[AccountsID] = @AccountsID,
		[MD5] = @MD5,
		[Rate] = @Rate,
		[OrderID] = @OrderID,
		[IsDisabled] = @IsDisabled,
        [IsDefault] = @IsDefault
	WHERE
		[PayPlatformID] = @PayPlatformID
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Analytics_StatIPInfo_Delete]'
GO
ALTER PROCEDURE [dbo].[PR_Analytics_StatIPInfo_Delete]
	@StartIp float,
	@EndIp float
AS

	Delete 
		Pe_StatIpInfo
	Where 
		StartIp = @StartIp and EndIp = @EndIp
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_DownloadError_Add]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_DownloadError_Add]
	(
	@ErrorID int,
	@InfoID int,
    @ErrorUrl nvarchar(255),
    @ErrorTimes int,
    @ErrorDate datetime
	)
AS
	--SET NOCOUNT ON 
	INSERT INTO
        PE_DownloadError(ErrorID,InfoID,ErrorUrl,ErrorTimes,ErrorDate)VALUES(@ErrorID,@InfoID,@ErrorUrl,@ErrorTimes,@ErrorDate)  
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_PayPlatform_SetDefault]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_PayPlatform_SetDefault]

(
	@PayPlatformId int
)

AS
	UPDATE
		PE_PayPlatForm
	SET
		[IsDefault] = 0
		
	UPDATE
		PE_PayPlatForm
	SET
		[IsDefault] = 1
	WHERE
		[PayPlatformId] = @PayPlatformId
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_ProductPrice_Update]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_ProductPrice_Update] 
    (
	@TableName nvarchar(255),
	@ProductId int,
	@GroupId int,
	@Price money,
    @PropertyValue nvarchar(255)
	)	
AS
IF Exists (Select * from PE_ProductPrice Where TableName=@TableName and GroupID=@GroupID and ProductId=@ProductId and PropertyValue=@PropertyValue)
Begin
	UPDATE 
		PE_ProductPrice 
	SET 
		Price=@Price
    WHERE 
		GroupID=@GroupID 
		and TableName=@TableName 
		and ProductId=@ProductId
        and (isnull(PropertyValue,'') = @PropertyValue)
End
Else
Begin
	INSERT INTO 
		PE_ProductPrice
		(
			TableName,
			ProductID,
			GroupID,
			Price,
            PropertyValue
		) 
		VALUES
		(
			@TableName,
			@ProductId,
			@GroupId,
			@Price,
            @PropertyValue
		)
End
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_DownServer_GetById]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_DownServer_GetById] 
	(
	@ServerID int
	)
AS
	SET NOCOUNT ON
	SELECT
		  ServerID,ServerName,ServerUrl,ServerLogo,OrderID,ShowType 
    FROM 
          PE_DownServer
	WHERE ServerID = @ServerID
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Contents_CommentPK_Add]'
GO
ALTER PROCEDURE [dbo].[PR_Contents_CommentPK_Add] 
(
	@PKId int,
	@CommentId int,
	@Title nvarchar(255),
	@Content ntext,
	@IP nvarchar(50),
	@UpdateTime datetime,
	@UserName nvarchar(50),
	@Position int
)
AS
	INSERT INTO PE_CommentPK
	(
		PKId,
		CommentId,
		Title,
		[Content],
		IP,
		UpdateTime,
		UserName,
		[Position]
	)
	VALUES
	(
		@PKId,
		@CommentId,
		@Title,
		@Content,
		@IP,
		@UpdateTime,
		@UserName,
		@Position
	)
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_PayPlatform_GetInfoById]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_PayPlatform_GetInfoById]
(
	@PayPlatformID int
)
 AS 
	SELECT 
		*
	 FROM
		PE_PayPlatform
	 WHERE 
		[PayPlatformID] = @PayPlatformID
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_Bank_Delete]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_Bank_Delete]
(
	@BankID int
)
 AS 
  SET NOCOUNT OFF
	DELETE
		PE_Bank
	WHERE
		[BankID] = @BankID
		RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_Trademark_GetById]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_Trademark_GetById]
(
	@TrademarkID Int
)
 AS 
 
	SELECT TrademarkID,TrademarkName,ProducerID,IsElite,TrademarkType,TrademarkPhoto,TrademarkIntro,Passed,OnTop,IsElite FROM PE_Trademark WHERE TrademarkID = @TrademarkID
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Analytics_GetOnlineByIP]'
GO
--获取在线IP的信息
ALTER PROCEDURE [dbo].[PR_Analytics_GetOnlineByIP]
	@IP Varchar(20)
AS
BEGIN
	declare @onlinetime int
	select 
		@onlinetime = isnull(OnlineTime,100) 
	From 
		PE_StatInfoList

	select 
		*              --count(UserIP) 
	from 
		PE_Statonline 
	where 
		LastTime > dateadd(s,-@onlinetime, getdate()) and UserIP = @IP

end
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_Source_GetSourceInfo]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_Source_GetSourceInfo] 
AS
		SELECT
				ID,
				Type,
				[Name],
				Passed,
				onTop,
				IsElite,
				Hits,
				LastUseTime,
				Photo,
				Intro,
				Address,
				Tel,
				Fax,
				Mail,
				Email,
				ZipCode,
				HomePage,
				Im,
				Contacter
				FROM PE_Source ORDER BY ID DESC 	
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_Bank_Update]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_Bank_Update]
(
	@BankID int,
	@BankShortName nvarchar(100),
	@BankName nvarchar(100),
	@Accounts nvarchar(100),
	@CardNum nvarchar(100),
	@HolderName nvarchar(100),
	@BankIntro nvarchar(510),
	@BankPic nvarchar(400),
	@IsDefault bit,
	@IsDisabled bit,
	@OrderID int
)

 AS 
	IF(@IsDisabled = 1)
		BEGIN
			UPDATE [PE_Bank] SET IsDefault = 0 WHERE IsDefault = 1
		END
	IF @orderId <> 0
		Begin
			--处理 排序
			DECLARE @oldOrderId INT
			DECLARE @max INT
			DECLARE @min INT
			DECLARE @count INT
			DECLARE @icount INT
			DECLARE @offset1 INT
			DECLARE @offset2 INT

			SET @icount = 1
			DECLARE @tmp TABLE(Row INT IDENTITY(1,1) PRIMARY KEY,id INT ,oid INT)
			SELECT @oldOrderId = OrderId FROM [PE_Bank] WHERE BankID = @BankId
			IF(@orderId <> @oldOrderId)
				BEGIN
					IF(@orderId > @oldOrderId)
						BEGIN
							SET @max = @orderId
							SET @min = @oldOrderId
							SET @offset1 = 0
							SET @offset2 = 1
						END
					ELSE
						BEGIN
							SET @max = @oldOrderId
							SET @min = @orderId
							SET @offset1 = 1
							SET @offset2 = 0
						END
					INSERT INTO @tmp(id,oid)  SELECT BankId,OrderId 
					FROM  [PE_Bank] 
					WHERE OrderId BETWEEN @min AND @max
					ORDER BY orderId
					SELECT @count = count(*)  From @tmp

					While(@icount < @count)
					BEGIN
						UPDATE [PE_Bank] SET OrderId = (select oid from @tmp where Row = @icount + @offset1)
						WHERE BankId =(Select Id From @tmp Where Row = @icount + @offset2)
						SET @icount = @icount + 1
					END 
				END

			--更新
			UPDATE
				[PE_Bank]
			SET 
				[BankShortName] = @BankShortName,
				[BankName] = @BankName,
				[Accounts] = @Accounts,
				[CardNum] = @CardNum,
				[HolderName] = @HolderName,
				[BankIntro] = @BankIntro,
				[BankPic] = @BankPic,
				[IsDefault] = @IsDefault,
				[IsDisabled] = @IsDisabled,
				[OrderID] = @OrderID
			WHERE
				[BankID] = @BankID
		End -- End of -----IF @orderId <> 0
	ELSE
		Begin
			--不排序的更新
			UPDATE
				[PE_Bank]
			SET 
				[BankShortName] = @BankShortName,
				[BankName] = @BankName,
				[Accounts] = @Accounts,
				[CardNum] = @CardNum,
				[HolderName] = @HolderName,
				[BankIntro] = @BankIntro,
				[BankPic] = @BankPic,
				[IsDefault] = @IsDefault,
				[IsDisabled] = @IsDisabled,
				[OrderID] = @OrderID
			WHERE
				[BankID] = @BankID
		End
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_UserManage_UserPointLog_Exists]'
GO
ALTER PROCEDURE [dbo].[PR_UserManage_UserPointLog_Exists]
(
    @UserName NVarChar(100)
)
as
select UserName from PE_PointLog where UserName=@UserName
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_Keywords_GetById]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_Keywords_GetById]
(
	@KeywordID Int
)
 AS 
 
	SELECT KeywordID,KeywordText,KeywordType,Priority,Hits,LastUseTime,arrGeneralId,QuoteTimes FROM PE_Keywords WHERE KeywordID = @KeywordID
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Analytics_SaveConfig]'
GO
ALTER PROCEDURE [dbo].[PR_Analytics_SaveConfig]
(
		@MasterTimeZone int,
		@Interval int,
		@IntervalNum int,
		@OnlineTime int,
		@VisitRecord int,
		@KillRefresh int,
		@RegFields_Fill nvarchar(255),
		@OldTotalNum int,
		@OldTotalView int,
		@StartDate nvarchar(10)
)
AS

IF Exists (select * from PE_statInfoList)
Begin
	
	UPDATE
			PE_StatInfoList
	SET
			[MasterTimeZone] = @MasterTimeZone,
			[Interval] = @Interval,
			[IntervalNum] = @IntervalNum,
			[OnlineTime] = @OnlineTime,
			[VisitRecord] = @VisitRecord,
			[KillRefresh] = @KillRefresh,
			[RegFields_Fill] = @RegFields_Fill,
			[OldTotalNum] = @OldTotalNum,
			[OldTotalView] = @OldTotalView,
			[StartDate] = @StartDate

End
Else
Begin
	INSERT INTO [PE_StatInfoList]
           ([StartDate]
           ,[TotalNum]
           ,[TotalView]
           ,[MonthNum]
           ,[MonthMaxNum]
           ,[OldMonth]
           ,[MonthMaxDate]
           ,[DayNum]
           ,[DayMaxNum]
           ,[OldDay]
           ,[DayMaxDate]
           ,[HourNum]
           ,[HourMaxNum]
           ,[OldHour]
           ,[HourMaxTime]
           ,[ChinaNum]
           ,[OtherNum]
           ,[MasterTimeZone]
           ,[Interval]
           ,[IntervalNum]
           ,[OnlineTime]
           ,[VisitRecord]
           ,[KillRefresh]
           ,[RegFields_Fill]
           ,[OldTotalNum]
           ,[OldTotalView])
     VALUES
           (@StartDate,	0,0,0,0,'','',0,0,convert(char(10),getdate(),120),'',0,0,'',0,0,0,
			@MasterTimeZone,@Interval,@IntervalNum,@OnlineTime,@VisitRecord,@KillRefresh,@RegFields_Fill,
			@OldTotalNum,@OldTotalView)

End
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Survey_SurveyManager_SetPassed]'
GO
ALTER PROCEDURE [dbo].[PR_Survey_SurveyManager_SetPassed]
(
    @SurveyID int,
    @TableName nvarchar(50)
)
as
	UPDATE 
		PE_Survey
    SET 
        IsOpen = 1
    WHERE 
         SurveyID = @SurveyID
         
	--	检查数据表是否存在
	IF object_ID(@TableName) is not null --EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(@TableName) AND type in (N'U'))
	BEGIN
		EXEC('drop table ' +@TableName)
	END
		-- 不存在创建指定数据表
		EXEC('CREATE TABLE '+ @TableName+' ([RecordID] [int] IDENTITY (1, 1) PRIMARY KEY,[SurveyID] [int],[UserName] [nvarchar](255),[IP] [nvarchar](255),[SubmitTime] [datetime])')
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_Producer_ProducerNameExists]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_Producer_ProducerNameExists]
(
    @ProducerName NVarChar(200)
)
as

declare @result int

select * from PE_Producer where ProducerName=@ProducerName

if @@rowcount > 0
    set @result=1
else
    set @result=0

return @result
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_Producer_Add]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_Producer_Add] 	
	(
	@ProducerID int,
	@ProducerName NVarChar(200),
	@ProducerShortName NVarChar(50),
	@Birthday DateTime,
	@Address NVarChar(255),
	@Phone NVarChar(50),
	@Fax NVarChar(50),
	@Postcode NVarChar(10),
	@Homepage NVarChar(50),
	@Email NVarChar(50),
	@ProducerIntro NText,
	@ProducerPhoto NVarChar(255),
	@ProducerType Int
	)	
AS
	INSERT INTO PE_Producer
	      (ProducerID,ProducerName, ProducerShortName, BirthDay, Address, Phone, Fax, Postcode, 
	      Homepage, Email, ProducerIntro, ProducerPhoto, ProducerType,Passed,LastUseTime)
	VALUES (@ProducerID,@ProducerName,@ProducerShortName,@Birthday,@Address,@Phone,@Fax,@Postcode,@Homepage,@Email,@ProducerIntro,@ProducerPhoto,@ProducerType,1,getDate())
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_AD_Advertisement_Update]'
GO
ALTER PROCEDURE [dbo].[PR_AD_Advertisement_Update]
(
	@ADId int,
	@strZoneId NvarChar(4000),    
	@UserId int,    
	@ADType int,    
	@ADName nvarchar(100),    
	@ImgUrl nvarchar(255),    
	@ImgWidth int,    
	@ImgHeight int,    
	@FlashWmode int,    
	@ADIntro ntext,    
	@LinkUrl nvarchar(255),    
	@LinkTarget int,    
	@LinkAlt nvarchar(255),    
	@Priority int,    
	@Setting ntext,    
	@CountView bit,    
	@Views int,    
	@CountClick bit,    
	@Clicks int,    
	@Passed bit,
	@OverdueDate DateTime 
)
AS
SET NOCOUNT OFF

	DECLARE @active bit
	DECLARE @zoneId int
	DECLARE @split varchar(1)
	
	SET @split = ','
	IF @strZoneId!=''
	BEGIN
		SET @strZoneId = @strZoneId+','
		DELETE FROM PE_Zone_Advertisement WHERE ADID=@ADID
		WHILE CHARINDEX(@split,@strZoneId)>0
		BEGIN
			SET @zoneId = CONVERT(int,LEFT(@strZoneId,CHARINDEX(@split,@strZoneId)-1))
			SET @strZoneId = STUFF(@strZoneId,1,CHARINDEX(@split,@strZoneId),'')
			INSERT INTO PE_Zone_Advertisement(ADID,ZoneID)VALUES(@ADID,@ZoneId)
		END
	END
BEGIN


	UPDATE 
		[PE_Advertisement] 
	SET
		
			[UserId] = @UserId,    
			[ADType] = @ADType,    
			[ADName] = @ADName,    
			[ImgUrl] = @ImgUrl,    
			[ImgWidth] = @ImgWidth,    
			[ImgHeight] = @ImgHeight,    
			[FlashWmode] = @FlashWmode,    
			[ADIntro] = @ADIntro,    
			[LinkUrl] = @LinkUrl,    
			[LinkTarget] = @LinkTarget,    
			[LinkAlt] = @LinkAlt,    
			[Priority] = @Priority,    
			[Setting] = @Setting,    
			[CountView] = @CountView,    
			[Views] = @Views,    
			[CountClick] = @CountClick,    
			[Clicks] = @Clicks,    
			[Passed] = @Passed,
			[OverdueDate]=@OverdueDate
	
	WHERE
		[ADId] = @ADId
END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PE_Orders]'
GO
ALTER TABLE [dbo].[PE_Orders] ADD
[DeliveryTime] [nvarchar] (128) COLLATE Chinese_PRC_CI_AS NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_Order_Confirm]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_Order_Confirm] 
	-- Add the parameters for the stored procedure here
	@OrderId int
AS

DECLARE @foundStatus int,@getMoneyReceipt money
SELECT @foundStatus=Status,@getMoneyReceipt=MoneyReceipt FROM PE_Orders WHERE OrderID = @OrderId

IF @foundStatus IS NOT NULL
	IF(@foundStatus > 1)
		RETURN 1
	ELSE
		BEGIN
			UPDATE PE_Orders SET Status = 1	WHERE OrderID = @OrderId
			RETURN 2
		END
ELSE
	RETURN 0
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_DeliverType_Delete]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_DeliverType_Delete]
	(
		@typeId INT
	)
	
AS
	declare @Oid int
	select @Oid = OrderId
	From PE_DeliverType
	where TypeId = @typeId

	DELETE PE_DeliverType 
	WHERE TypeId = @typeId

	Update PE_DeliverType
	Set OrderId = OrderId - 1
	Where OrderId > @Oid
	
	DELETE PE_DeliverCharge
	WHERE DeliverTypeId = @typeId
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_DeliverCharge_Add]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_DeliverCharge_Add] 
(
	@deliverTypeID int ,
	@areaType int = 0 ,
	@arrArea ntext ,
	@charge_Min money =0 ,
	@weight_Min float = 0 ,
	@chargePerUnit money = 0 ,
	@weightPerUnit float = 0 ,
	@charge_Max money = 0
)
 AS 
 
 SET NOCOUNT OFF
 
	INSERT INTO PE_DeliverCharge(
		[DeliverTypeID],
		[AreaType],
		[arrArea],
		[Charge_Min],
		[Weight_Min],
		[ChargePerUnit],
		[WeightPerUnit],
		[Charge_Max])
	VALUES(
		@deliverTypeID,
		@areaType,
		@arrArea,
		@charge_Min,
		@weight_Min,
		@chargePerUnit,
		@weightPerUnit,
		@charge_Max
	)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PE_CommonProduct]'
GO
ALTER TABLE [dbo].[PE_CommonProduct] ADD
[Minimum] [int] NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_Product_DeleteAll]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_Product_DeleteAll] 
(
	@TableName nvarchar(255),
	@ProductID int
)
AS
BEGIN

	if not exists(select * from PE_OrderItem where TableName=@TableName and ProductID=@ProductID)
	Begin
		delete PE_CommonProduct where TableName=@TableName and ProductID=@ProductID
		delete PE_ProductData where TableName=@TableName and ProductID=@ProductID
		delete PE_ProductPrice where TableName=@TableName and ProductID=@ProductID
		if exists(select * from PE_Cards where TableName=@TableName and ProductID=@ProductID)
		Begin
			update PE_Cards Set ProductID=0,TableName='' 
				Where TableName=@TableName and ProductID=@ProductID
		End
	End
END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_DeliverCharge_GetById]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_DeliverCharge_GetById] 
(
	@id INT
)
AS
	SELECT [Id]
      ,[DeliverTypeId]
      ,[AreaType]
      ,[arrArea]
      ,[Charge_Min]
      ,[Weight_Min]
      ,[ChargePerUnit]
      ,[WeightPerUnit]
      ,[Charge_Max]
  FROM [PE_DeliverCharge]
  WHERE [Id] = @id

	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_Trademark_TrademarkNameExists]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_Trademark_TrademarkNameExists]
(
    @TrademarkName NVarChar(200)
)
as

declare @result int

select * from PE_Trademark where TrademarkName=@TrademarkName

if @@rowcount > 0
    set @result=1
else
    set @result=0

return @result
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PE_CommonModel]'
GO
ALTER TABLE [dbo].[PE_CommonModel] ADD
[PinyinTitle] [nvarchar] (1000) COLLATE Chinese_PRC_CI_AS NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在创建 [dbo].[PE_CommonModel] 的索引 [IX_PE_CommonModel_UpdateTime]'
GO
CREATE NONCLUSTERED INDEX [IX_PE_CommonModel_UpdateTime] ON [dbo].[PE_CommonModel] ([UpdateTime] DESC) ON [PRIMARY]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Contents_ModelTemplates_Add]'
GO
ALTER PROCEDURE [dbo].[PR_Contents_ModelTemplates_Add]
(
	@TemplateId int,    
	@TemplateName nVarchar(255),    
	@TemplateDescription nText,    
	@Field ntext,
	@IsEshop bit  
)
AS
	INSERT INTO 
		[PE_ModelTemplates] 
		(
			[TemplateId],    
			[TemplateName],    
			[TemplateDescription],    
			[Field],
			[IsEshop]   
		)
	VALUES
	(
		@TemplateId,
		@TemplateName,
		@TemplateDescription,
		@Field,
		@IsEshop
	)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_Region_Add]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_Region_Add] 
	(
    @Country nvarchar(20),
    @Province nvarchar(25),
    @City nvarchar(30),
    @Area varchar(25),
    @PostCode varchar(10),
    @AreaCode nvarchar(8)
	)
AS
	SET NOCOUNT OFF
	
	INSERT INTO
	PE_Region(Country,Province,City,Area,PostCode,AreaCode) VALUES(@Country,@Province,@City,@Area,@PostCode,@AreaCode)
 
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Contents_ModelTemplates_GetInfoById]'
GO
ALTER PROCEDURE [dbo].[PR_Contents_ModelTemplates_GetInfoById]
(
	@TemplateId Int
)
 AS 
	SELECT * FROM PE_ModelTemplates WHERE TemplateId = @TemplateId
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_PaymentType_GetById]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_PaymentType_GetById]
 (
	@id INT = 0
 )
AS
	--显示TypeID = @id的付款方式
	SELECT * 
	FROM PE_PaymentType 
	WHERE TypeId = @id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PE_Users]'
GO
ALTER TABLE [dbo].[PE_Users] ADD
[PayPassword] [nvarchar] (32) COLLATE Chinese_PRC_CI_AS NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_UserManage_Users_BatchLock]'
GO
ALTER PROCEDURE [dbo].[PR_UserManage_Users_BatchLock]
	
(
	@strUserID Varchar(4000),
	@LockState int,
	@ApproveState int
)
	
AS
	/* SET NOCOUNT ON */ 
	/* SET NOCOUNT ON */ 
	DECLARE @UserId int
	DECLARE @split varchar(1)
	
	SET @split = ','
	SET @strUserId = @strUserId+','
	
	WHILE CHARINDEX(@split,@strUserId)>0
	
	BEGIN
		SET @UserId = CONVERT(int,LEFT(@strUserId,CHARINDEX(@split,@strUserId)-1))
		SET @strUserId = STUFF(@strUserId,1,CHARINDEX(@split,@strUserId),'')
	
		UPDATE PE_Users SET [Status]=@LockState,LastLockoutTime=GETDATE() WHERE UserId = @UserId AND [Status]=@ApproveState
	END
	
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Crm_Address_Update]'
GO
ALTER PROCEDURE [dbo].[PR_Crm_Address_Update] 	
	(
	@AddressID int,    --地址Id
	@UserName nvarchar(20),    --用户名
	@ConsigneeName nvarchar(255),
	@HomePhone nvarchar(30),    --固定电话
	@Mobile nvarchar(30),    --移动电话
	@Country nvarchar(50),    --国家
	@Province nvarchar(30),    --省份
	@City nvarchar(30),    --城市
	@Area nvarchar(30),
	@Address nvarchar(255),    --详细地址
	@ZipCode nvarchar(50)    --邮编
	--@IsDefault bit    --是否默认
)
AS


	UPDATE 
		[PE_Address] 
	SET
			[AddressID] = @AddressID,    --地址Id
			[UserName] = @UserName,    --用户名
			[ConsigneeName]=@ConsigneeName,
			[HomePhone] = @HomePhone,    --固定电话
			[Mobile] = @Mobile,    --移动电话
			[Country] = @Country,    --国家
			[Province] = @Province,    --省份	
			[City] = @City,    --城市
			[Area] = @Area,
			[Address] = @Address,    --详细地址
			[ZipCode] = @ZipCode    --邮编
			--[IsDefault] = @IsDefault    --是否默认
	WHERE
		[AddressID] = @AddressID
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_Region_CheckArea]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_Region_CheckArea] 

	(
	@Area varchar(25),
	@Province varchar(30),
	@City varchar(25)
	)

AS
	SET NOCOUNT ON
	SELECT COUNT(*) FROM PE_Region WHERE Area=@Area AND Province=@Province AND City=@City
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Analytics_GetOnlineCount]'
GO
--获取在线人数
ALTER PROCEDURE [dbo].[PR_Analytics_GetOnlineCount]

AS
BEGIN
	declare @onlinetime int
	select 
		@onlinetime = isnull(OnlineTime,0) 
	From 
		PE_StatInfoList

	select 
		count(UserIP) 
	from 
		PE_Statonline 
	where 
		LastTime > dateadd(s,-@onlinetime, getdate())

end
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_UserManage_UserPointLog_DeleteUser]'
GO
ALTER PROCEDURE [dbo].[PR_UserManage_UserPointLog_DeleteUser]
	(
	@UserName varchar(4000)
	)
	
AS
	 SET NOCOUNT OFF
	 BEGIN 
	 
	 DELETE From PE_PointLog WHERE UserName IN (@UserName)
	 
	END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_WordReplace_Update]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_WordReplace_Update] 
    (
    @ItemID int,
	@SourceWord nvarchar(250),    
	@TargetWord nvarchar(250),
	@ReplaceType int,
	@ScopesType int,
	@Priority int,    
	@ReplaceTimes int,    
	@OpenType bit,
	@IsEnabled bit,
	@Title nvarchar(255)
	)	
AS
	UPDATE 
		PE_WordReplaceItem
    SET 
        SourceWord = @SourceWord, TargetWord=@TargetWord,ReplaceType=@ReplaceType,
        ScopesType=@ScopesType,Priority = @Priority,ReplaceTimes=@ReplaceTimes,OpenType=@OpenType,IsEnabled = @IsEnabled,Title=@Title
    WHERE 
         ItemID = @ItemID
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Crm_ComplainItem_Update]'
GO
ALTER PROCEDURE [dbo].[PR_Crm_ComplainItem_Update]
(
	@ItemID int,    
	@ClientID int,    
	@ContacterID int,   
	@ComplainType int,   
	@ComplainMode int,   
	@Title nvarchar(50),    
	@Content ntext,    
	@FirstReceiver nvarchar(50),    
	@DateAndTime datetime,  
	@MagnitudeOfExigence int,    
	@Process ntext,    
	@Processor nvarchar(50),   
	@Result nvarchar(50), 
	@EndTime datetime, 
	@Feedback nvarchar(255),
	@ConfirmTime datetime, 
	@ConfirmCaller nvarchar(50),   
	@ConfirmScore int,   
	@ConfirmFeedback nvarchar(255),  
	@Status int,  
	@CurrentOwner nvarchar(50), 
	@Remark nvarchar(255),
    @Defendant nvarchar(50)
)
AS
	UPDATE 
		[PE_ComplainItem] 
	SET
		
			[ClientID] = @ClientID,    
			[ContacterID] = @ContacterID,   
			[ComplainType] = @ComplainType,    
			[ComplainMode] = @ComplainMode,    
			[Title] = @Title,    
			[Content] = @Content,    
			[FirstReceiver] = @FirstReceiver,    
			[DateAndTime] = @DateAndTime,   
			[MagnitudeOfExigence] = @MagnitudeOfExigence,    
			[Process] = @Process,  
			[Processor] = @Processor,    
			[Result] = @Result,   
			[EndTime] = @EndTime,    
			[Feedback] = @Feedback,    
			[ConfirmTime] = @ConfirmTime,   
			[ConfirmCaller] = @ConfirmCaller,   
			[ConfirmScore] = @ConfirmScore, 
			[ConfirmFeedback] = @ConfirmFeedback,   
			[Status] = @Status,    
			[CurrentOwner] = @CurrentOwner,    
			[Remark] = @Remark,
            [Defendant] = @Defendant 
		
	WHERE
		[ItemID] = @ItemID
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_Region_GetByID]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_Region_GetByID] 

	(
	@RegionId int
	)
AS
	/* SET NOCOUNT ON */
	SELECT
		*
	FROM
		PE_Region WHERE RegionID=@RegionId  
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_Author_Add]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_Author_Add] 
(
    @UserId int,
    @Name varchar(50),
    @Type varchar(50),
    @Passed bit,
    @onTop bit,
    @IsElite bit,
    @Hits int,
    @LastUseTime DateTime,
    @TemplateID int,
    @Photo varchar(255),
    @Intro nvarchar(255),
    @Address varchar(50),
    @Tel varchar(50),
    @Fax varchar(50),
    @Mail varchar(50),
    @Email varchar(50),
    @ZipCode int,
    @HomePage varchar(50),
    @Im varchar(50),
    @Sex smallint,
    @BirthDay DateTime,
    @Company varchar(50),
    @Department varchar(50)
)
AS

	INSERT INTO
	PE_Author(UserID,Type,[Name],Passed,onTop,IsElite,Hits,LastUseTime,TemplateID,Photo,Intro,Address,Tel,Fax,Mail,Email,ZipCode,HomePage,Im,Sex,BirthDay,Company,Department) VALUES(@UserId,@Type,@Name,@Passed,@onTop,@IsElite,@Hits,@LastUseTime,@TemplateID,@Photo,@Intro,@Address,@Tel,@Fax,@Mail,@Email,@ZipCode,@HomePage,@Im,@Sex,@BirthDay,@Company,@Department)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_UserManage_Cards_Add]'
GO
--添加充值卡
ALTER PROCEDURE [dbo].[PR_UserManage_Cards_Add]
(
		   @CardType int,
           @ProductID int,
		   @TableName nvarchar(255) ='',
           @CardNum nvarchar(30),
           @Password nvarchar(50),
           @AgentName nvarchar(100),
           @Money money,
           @ValidNum int,
           @ValidUnit int,
           @EndDate datetime,
           @UserName nvarchar(50),
           @CreateTime datetime,
           @OrderItemID int,
           @ProductName nvarchar(255)
)
AS
	IF EXISTS(select * from PE_Cards where CardNum=@CardNum and Password=@Password and ProductID=@ProductID)
		BEGIN
			RETURN 0 --存在着相同的充值卡,不添加记录,返回 0
		END
	ELSE
		Begin
			INSERT INTO [PE_Cards]
			   ([CardType],[ProductID],[TableName],[CardNum],[Password],[AgentName],[Money]
			   ,[ValidNum],[ValidUnit],[EndDate],[UserName],[CreateTime],[OrderItemID],[ProductName])
			VALUES
			   (@CardType,@ProductID,@TableName,@CardNum,@Password,@AgentName,@Money
				,@ValidNum,@ValidUnit, @EndDate, @UserName, @CreateTime,@OrderItemID,@ProductName) 
			RETURN @@ROWCOUNT
		End
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_Invoice_GetById]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_Invoice_GetById]
(
	@InvoiceId Int
)
 AS 
 
	SELECT I.*,O.OrderNum,O.MoneyTotal,O.MoneyReceipt,C.ShortedForm AS ClientName FROM PE_InvoiceItem I LEFT JOIN (PE_Orders O LEFT JOIN PE_Client C ON O.ClientID=C.ClientID) ON I.OrderID=O.OrderID WHERE InvoiceID = @InvoiceID
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Analytics_StatVisitor_Add]'
GO
ALTER PROCEDURE [dbo].[PR_Analytics_StatVisitor_Add] 
	 (
           @Ip nvarchar(15),
           @Address nvarchar(50),
           @System nvarchar(20),
           @Browser nvarchar(20),
           @Screen nvarchar(10),
           @Color nvarchar(16),
           @Referer nvarchar(100),
           @Timezone int)

AS
BEGIN
	declare @VisitCount int
	declare @VisitRecord int
	select @VisitCount=count(ID) From PE_StatVisitor
	select top 1 @VisitRecord=VisitRecord From PE_StatInfoList
	IF @VisitCount >= @VisitRecord 
	BEGIN
		declare @id int
		select top 1 @id = id from PE_StatVisitor order by VTime asc
		UPDATE [PE_StatVisitor]
		SET 
			[VTime] = getdate(),
			[Ip] = @Ip,
			[Address] = @Address,
			[System] = @System,
			[Browser] = @Browser,
			[Screen] = @Screen, 
			[Color] = @Color, 
			[Referer] = @Referer, 
			[Timezone] = @Timezone
		WHERE 
			id = @id
	END
	ELSE
	BEGIN
		INSERT INTO [PE_StatVisitor]([VTime],[Ip],[Address],[System],[Browser],[Screen],[Color],[Referer],[Timezone])
     VALUES
        (getdate(), @Ip, @Address,@System,@Browser,@Screen,@Color, @Referer,@Timezone)
	END
END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Contents_ModelTemplates_Update]'
GO
ALTER PROCEDURE [dbo].[PR_Contents_ModelTemplates_Update]
    (
	@TemplateId int,    
	@TemplateName nVarchar(255),    
	@TemplateDescription nText,
	@Field ntext ='' ,
	@IsEshop bit  
	)	
AS
	UPDATE 
		[PE_ModelTemplates] 
    SET 
	    TemplateName = @TemplateName, 
	    TemplateDescription = @TemplateDescription      
    WHERE 
        TemplateID = @TemplateId
    RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_Bank_SetDefault]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_Bank_SetDefault]

(
	@BankID int
)

AS
	UPDATE
		PE_Bank
	SET
		[IsDefault] = 0
		
	UPDATE
		PE_Bank
	SET
		[IsDefault] = 1
	WHERE
		[BankID] = @BankID
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_Trademark_Add]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_Trademark_Add] 	
	(
	@TrademarkName NVarChar(200),
    @TrademarkType Int,
    @ProducerID Int,
    @IsElite Bit,	
	@TrademarkPhoto NVarChar(255),
	@TrademarkIntro NText	
	)	
AS
	INSERT INTO PE_Trademark
	      (TrademarkName,TrademarkType,ProducerID,IsElite,TrademarkPhoto,TrademarkIntro,Passed)
	VALUES (@TrademarkName,@TrademarkType,@ProducerID,@IsElite,@TrademarkPhoto,@TrademarkIntro,1)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_UserManage_Users_Add]'
GO
ALTER PROCEDURE [dbo].[PR_UserManage_Users_Add]
(
	@UserID int,    
	@GroupID int,    
	@CompanyID int,    
	@ClientID int,     
	@UserType int,    
	@UserName nvarchar(20),    
	@UserPassword nvarchar(32),    
	@LastPassword nvarchar(32),    
	@Question nvarchar(50),    
	@Answer nvarchar(50),    
	@Email nvarchar(100),    
	@Sex int,    
	@RegTime datetime,    
	@JoinTime datetime,    
	@LoginTimes int,    
	@LastLoginTime datetime,    
	@LastPresentTime datetime,    
	@LastLoginIP nvarchar(255),    
	@LastPasswordChangedTime datetime,    
	@LastLockoutTime datetime,    
	@FailedPasswordAttemptCount int,    
	@FirstFailedPasswordAttempTime datetime,    
	@FailedPasswordAnswerAttempCount int,    
	@FirstFailedPasswordAnswerAttempTime datetime,    
	@Status int,    
	@CheckNum char(10),    
	@EnableResetPassword bit,    
	@UserFace nvarchar(255),    
	@FaceWidth int,    
	@FaceHeight int,    
	@Sign ntext,    
	@PrivacySetting int,    
	@Balance money,    
	@UserPoint int,    
	@UserExp int,    
	@ConsumeMoney int,    
	@ConsumePoint int,    
	@ConsumeExp int,    
	@PostItems int,    
	@PassedItems int,    
	@RejectItems int,    
	@DelItems int,    
	@EndTime datetime,    
	@IsInheritGroupRole bit ,   
    @UserFriendGroup  nvarchar(255),
    @TrueName nvarchar(255),
    @PayPassword nvarchar(32)
)
AS
	INSERT INTO 
		[PE_Users] 
		(
			[UserID],    
			[GroupID],    
			[CompanyID],    
			[ClientID],       
			[UserType],    
			[UserName],    
			[UserPassword],    
			[LastPassword],    
			[Question],    
			[Answer],    
			[Email],    
			[Sex],    
			[RegTime],    
			[JoinTime],    
			[LoginTimes],    
			[LastLoginTime],    
			[LastPresentTime],    
			[LastLoginIP],    
			[LastPasswordChangedTime],    
			[LastLockoutTime],    
			[FailedPasswordAttemptCount],    
			[FirstFailedPasswordAttempTime],    
			[FailedPasswordAnswerAttempCount],    
			[FirstFailedPasswordAnswerAttempTime],    
			[Status],    
			[CheckNum],    
			[EnableResetPassword],    
			[UserFace],    
			[FaceWidth],    
			[FaceHeight],    
			[Sign],    
			[PrivacySetting],    
			[Balance],    
			[UserPoint],    
			[UserExp],    
			[ConsumeMoney],    
			[ConsumePoint],    
			[ConsumeExp],    
			[PostItems],    
			[PassedItems],    
			[RejectItems],    
			[DelItems],    
			[EndTime],    
			[IsInheritGroupRole],  
            [UserFriendGroup],
            [TrueName],
            [PayPassword]
		)
	VALUES
	(
		@UserID,
		@GroupID,
		@CompanyID,
		@ClientID,
		@UserType,
		@UserName,
		@UserPassword,
		@LastPassword,
		@Question,
		@Answer,
		@Email,
		@Sex,
		@RegTime,
		@JoinTime,
		@LoginTimes,
		@LastLoginTime,
		@LastPresentTime,
		@LastLoginIP,
		@LastPasswordChangedTime,
		@LastLockoutTime,
		@FailedPasswordAttemptCount,
		@FirstFailedPasswordAttempTime,
		@FailedPasswordAnswerAttempCount,
		@FirstFailedPasswordAnswerAttempTime,
		@Status,
		@CheckNum,
		@EnableResetPassword,
		@UserFace,
		@FaceWidth,
		@FaceHeight,
		@Sign,
		@PrivacySetting,
		@Balance,
		@UserPoint,
		@UserExp,
		@ConsumeMoney,
		@ConsumePoint,
		@ConsumeExp,
		@PostItems,
		@PassedItems,
		@RejectItems,
		@DelItems,
		@EndTime,
		@IsInheritGroupRole,
        @UserFriendGroup,
        @TrueName,
        @PayPassword
	)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_DeliverType_GetList]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_DeliverType_GetList]
	
AS
	SELECT  * FROM    PE_DeliverType order by orderId
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_UserManage_UserValidLog_Exists]'
GO
ALTER PROCEDURE [dbo].[PR_UserManage_UserValidLog_Exists]
(
    @UserName NVarChar(100)
)
as
select count(*) from PE_ValidLog where UserName=@UserName
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_WordReplace_Add]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_WordReplace_Add]	
(
    @ItemID int,
	@SourceWord NVarChar(250),    
	@TargetWord NVarChar(250),
	@ReplaceType int,
	@ScopesType int,
	@Priority int,    
	@ReplaceTimes int,    
	@OpenType bit,
	@IsEnabled bit,
	@Title nvarchar(255)  
)
AS
	INSERT INTO 
		[PE_WordReplaceItem] 
		(   
			[SourceWord],    
			[TargetWord], 
			[ReplaceType],
			[ScopesType],
			[Priority],    
			[ReplaceTimes],    
			[OpenType],
			[IsEnabled],
			[Title]    
		)
	VALUES
	(
		@SourceWord,
		@TargetWord,
		@ReplaceType,
		@ScopesType,
		@Priority,
		@ReplaceTimes,
		@OpenType,
		@IsEnabled,
		@Title
	)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Crm_Client_Add]'
GO
ALTER PROCEDURE [dbo].[PR_Crm_Client_Add] 	
	(
	@ClientID Int, 
	@ParentID Int, 
	@ClientNum NVarChar(20), 
	@ClientType Int, 
	@ClientName NVarChar(100), 
	@ShortedForm NVarChar(20), 
	@Area Int, 
	@ClientField Int, 
	@ValueLevel Int, 
	@CreditLevel Int, 
	@Importance Int, 
	@ConnectionLevel Int, 
	@GroupID Int, 
	@SourceType Int, 
	@PhaseType Int, 
	@Remark NText, 
	@CreateTime datetime, 
	@UpdateTime datetime, 
	@Owner NVarChar(50)
	)	
AS
	INSERT INTO [PE_Client]
	      (	[ClientID], [ParentID], [ClientNum], [ClientType], [ClientName], [ShortedForm], [Area], [ClientField], [ValueLevel], [CreditLevel], [Importance], [ConnectionLevel], [GroupID], [SourceType], [PhaseType], [Remark], [CreateTime], [UpdateTime], [Owner] )
	VALUES (@ClientID, @ParentID, @ClientNum, @ClientType, @ClientName, @ShortedForm, @Area, @ClientField, @ValueLevel, @CreditLevel, @Importance, @ConnectionLevel, @GroupID, @SourceType, @PhaseType, @Remark, @CreateTime, @UpdateTime, @Owner)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Survey_SurveyVote_Add]'
GO
ALTER PROCEDURE [dbo].[PR_Survey_SurveyVote_Add]
(
	@SurveyId int,    
	@QuestionId int,  
	@OptionCount int 
)
AS
declare @option int
set @option = 0

While( @option < @OptionCount)
Begin
	INSERT INTO [PE_SurveyVote]
		(
			 SurveyId,
			 QuestionId,
			 OptionId
		) 
	VALUES 
		(
			 @SurveyId,
			 @QuestionId,
			 @option
		)
	set @option = @option +1
End
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_Invoice_GetByOrderId]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_Invoice_GetByOrderId]
(
	@OrderId Int
)
 AS 
 
	SELECT O.*,C.ShortedForm AS ClientName FROM PE_Orders O LEFT JOIN PE_Client C ON O.ClientID=C.ClientID WHERE OrderID = @OrderId AND NeedInvoice = 1
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_Keywords_Update]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_Keywords_Update] 
    (
    @KeywordID Int,
	@KeywordText NVarChar(200),
    @KeywordType Int,
    @Priority Int,
    @LastUseTime DateTime,
    @ArrayGeneralId ntext,
    @QuoteTimes int
	)	
AS
	UPDATE 
		PE_Keywords
    SET 
        KeywordText = @KeywordText, KeywordType=@KeywordType,Priority=@Priority,LastUseTime=@LastUseTime,arrGeneralID=@ArrayGeneralId ,QuoteTimes=@QuoteTimes
    WHERE 
         KeywordID = @KeywordID
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_Order_GetByOrderId]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_Order_GetByOrderId]
(@OrderId Int)
AS

BEGIN
	SELECT O.*,C.ShortedForm AS ClientName FROM PE_Orders O LEFT JOIN PE_Client C ON O.ClientID=C.ClientID WHERE OrderID = @OrderId 

END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_Refund_Add]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_Refund_Add] 
    (
    @OrderID Int,
	@Money money,
	@HandlingCharge money,
	@RefundType Int
	)	
AS
	DECLARE  @ClientID int, @UserName NVarChar(50)
	SELECT @ClientID = ClientID,@UserName=UserName FROM PE_Orders WHERE OrderID = @OrderID AND MoneyReceipt>=(@Money + @HandlingCharge)
	
	IF(@UserName is not Null Or @ClientID is not Null)
	BEGIN	
		UPDATE PE_Orders SET Status = 1, MoneyReceipt=MoneyReceipt-(@Money + @HandlingCharge)  WHERE OrderID = @OrderID 
		IF(@RefundType=1)
		BEGIN
			IF(@UserName is not Null Or @ClientID is not Null)
			BEGIN
				IF (@UserName <> '')
				BEGIN
					UPDATE PE_Users SET Balance=Balance+@Money WHERE UserName=@UserName
				END
				ELSE
				BEGIN
					UPDATE PE_Client SET Balance=Balance+@Money WHERE ClientID=@ClientID
				END
            END
		END 
	END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_Bank_Add]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_Bank_Add]
(
	@BankID int,
	@BankShortName nvarchar(100) ,
	@BankName nvarchar(100) ,
	@Accounts nvarchar(100) ,
	@CardNum nvarchar(100) ,
	@HolderName nvarchar(100) ,
	@BankIntro nvarchar(510) ,
	@BankPic nvarchar(400) ,
	@IsDefault bit ,
	@IsDisabled bit 
)
AS 
 SET NOCOUNT OFF 
DECLARE @TempID int

SELECT
	@TempID = (ISNULL(MAX(OrderId),0) + 1)
FROM
	PE_Bank

INSERT	INTO 
	PE_Bank
	(
		[BankID],
		[BankShortName],
		[BankName],
		[Accounts],
		[CardNum],
		[HolderName],
		[BankIntro],
		[BankPic],
		[IsDefault],
		[IsDisabled],
		[OrderID]
	)
VALUES
	(
		@BankID,
		@BankShortName,
		@BankName,
		@Accounts,
		@CardNum,
		@HolderName,
		@BankIntro,
		@BankPic,
		@IsDefault,
		@IsDisabled,
		@TempID
	)
	IF @IsDefault=1
		UPDATE PE_Bank Set IsDefault = 0 WHERE BankShortName!=@BankShortName
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PE_CollectionFieldRules]'
GO
ALTER TABLE [dbo].[PE_CollectionFieldRules] ADD
[ExclosionID] [int] NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_Producer_SetElite]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_Producer_SetElite] 
    (
    @ProducerID Int,
	@IsElite Bit
	)	
AS
	UPDATE 
		PE_Producer
    SET 
        IsElite = @IsElite
    WHERE 
         ProducerID = @ProducerID
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Analytics_StatOnline_Add]'
GO
ALTER PROCEDURE [dbo].[PR_Analytics_StatOnline_Add]
(
	@UserIP Varchar(20),
	@UserAgent varchar(255),
	@UserPage varchar(4000)
)
AS
BEGIN

	declare @onlinetime int
	declare @onNowTime DateTime
	declare @id int

	select @onlinetime = isnull(OnlineTime,0) From PE_StatInfoList
	set @onNowTime = dateadd(s,-@onlinetime, getdate())

	IF EXISTS(select * from PE_Statonline where LastTime > @onNowTime and UserIP = @UserIP)
	Begin
		update PE_StatOnline 
		set LastTime = getdate(),UserPage = @UserPage 
		where LastTime > @onNowTime and UserIP = @UserIP
	End
	Else
	Begin
		select top 1 @id=id from PE_StatOnline where LastTime< @onNowTime order by LastTime
		IF @id is not null
		Begin
			UPDATE PE_StatOnline 
			set UserIP=@UserIP,
				UserAgent=@UserAgent,
				UserPage=@UserPage,
				Ontime=getdate(),
				LastTime=getdate() 
			where id=@id
		End
		Else
		Begin
			INSERT INTO 
				PE_StatOnline(UserIP,UserAgent,UserPage,OnTime,LastTime) 
			VALUES
				(@UserIP,@UserAgent,@UserPage,getdate(),getdate())
		End
	End
end
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Crm_Service_Update]'
GO
ALTER PROCEDURE [dbo].[PR_Crm_Service_Update]
(
	@ItemId int,    --明细ID
	@ClientId int,    --客户ID
	@ContacterId int,    --联系人ID
	@OrderId int,    --订单ID
	@ServiceTime datetime,    --服务时间
	@ServiceType nvarchar(50),    --服务类型
	@ServiceMode nvarchar(50),    --服务方式
	@ServiceTitle nvarchar(50),    --服务主题
	@ServiceContent ntext,    --服务内容
	@ServiceResult int,    --服务结果，0为未完成，1为完成
	@TakeTime int,    --花费时间
	@Processor nvarchar(50),    --执行人
	@Inputer nvarchar(50),    --录入者
	@Feedback ntext,    --客户反馈
	@ConfirmTime datetime,    --回访时间
	@ConfirmCaller nvarchar(50),    --回访人
	@ConfirmScore int,    --客户评价
	@ConfirmFeedback ntext,    --客户反馈
	@Remark ntext    --备注
)
AS
	UPDATE 
		[PE_ServiceItem] 
	SET
			--[ItemId] = @ItemId,    --明细ID
			[ClientId] = @ClientId,    --客户ID
			[ContacterId] = @ContacterId,    --联系人ID
			[OrderId] = @OrderId,    --订单ID
			[ServiceTime] = @ServiceTime,    --服务时间
			[ServiceType] = @ServiceType,    --服务类型
			[ServiceMode] = @ServiceMode,    --服务方式
			[ServiceTitle] = @ServiceTitle,    --服务主题
			[ServiceContent] = @ServiceContent,    --服务内容
			[ServiceResult] = @ServiceResult,    --服务结果，0为未完成，1为完成
			[TakeTime] = @TakeTime,    --花费时间
			[Processor] = @Processor,    --执行人
			[Inputer] = @Inputer,    --录入者
			[Feedback] = @Feedback,    --客户反馈
			[ConfirmTime] = @ConfirmTime,    --回访时间
			[ConfirmCaller] = @ConfirmCaller,    --回访人
			[ConfirmScore] = @ConfirmScore,    --客户评价
			[ConfirmFeedback] = @ConfirmFeedback,    --客户反馈
			[Remark] = @Remark    --备注
		
	WHERE
		[ItemId] = @ItemId
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_UserManage_UserValidLog_DeleteUser]'
GO
ALTER PROCEDURE [dbo].[PR_UserManage_UserValidLog_DeleteUser]
	(
	@UserName varchar(4000)
	)
	
AS
	 SET NOCOUNT OFF
	 BEGIN 
	 
	 DELETE From PE_ValidLog WHERE UserName IN (@UserName)
	 
	END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_InsideLink_IsEnabled]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_InsideLink_IsEnabled]
    (
    @InsideLinkID int,
	@IsEnabled bit  
	)	
AS
	UPDATE 
		PE_InsideLink
    SET 
        IsEnabled = @IsEnabled
    WHERE 
        InsideLinkID = @InsideLinkID
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Survey_SurveyManager_Add]'
GO
ALTER PROCEDURE [dbo].[PR_Survey_SurveyManager_Add]
(
	@SurveyID int,    
	@SurveyName nvarchar(50),    
	@Description ntext,   
	@FileName nvarchar(50),   
	@IPRepeat int,    
	@CreateDate datetime,   
	@EndTime datetime,    
	@IsOpen int,    
	@NeedLogin int,    
	@PresentPoint int,   
	@LockIPType int,    
	@SetIPLock ntext,    
	@LockUrl ntext,   
	@SetPassword nvarchar(50),    
	@Template nvarchar(255)   
)
AS
	INSERT INTO 
		[PE_Survey] 
		(
			[SurveyID],    
			[SurveyName],    
			[Description],   
			[FileName],    
			[IPRepeat],    
			[CreateDate],   
			[EndTime],    
			[IsOpen],    
			[NeedLogin],    
			[PresentPoint],    
			[LockIPType],    
			[SetIPLock],    
			[LockUrl],    
			[SetPassword],    
			[Template]    
		)
	VALUES
	(
		@SurveyID,   
		@SurveyName,
		@Description,
		@FileName,
		@IPRepeat,
		@CreateDate,
		@EndTime,
		@IsOpen,
		@NeedLogin,
		@PresentPoint,
		@LockIPType,
		@SetIPLock,
		@LockUrl,
		@SetPassword,
		@Template
	)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Crm_Service_Add]'
GO
ALTER PROCEDURE [dbo].[PR_Crm_Service_Add]
(
	--@ItemId int,    --明细ID
	@ClientId int,    --客户ID
	@ContacterId int,    --联系人ID
	@OrderId int,    --订单ID
	@ServiceTime datetime,    --服务时间
	@ServiceType nvarchar(50),    --服务类型
	@ServiceMode nvarchar(50),    --服务方式
	@ServiceTitle nvarchar(50),    --服务主题
	@ServiceContent ntext,    --服务内容
	@ServiceResult int,    --服务结果，0为未完成，1为完成
	@TakeTime int,    --花费时间
	@Processor nvarchar(50),    --执行人
	@Inputer nvarchar(50),    --录入者
	@Feedback ntext,    --客户反馈
	@ConfirmTime datetime,    --回访时间
	@ConfirmCaller nvarchar(50),    --回访人
	@ConfirmScore int,    --客户评价
	@ConfirmFeedback ntext,    --客户反馈
	@Remark ntext    --备注
)
AS
	INSERT INTO 
		[PE_ServiceItem] 
		(
			--[ItemId],
			[ClientId],    --客户ID
			[ContacterId],    --联系人ID
			[OrderId],    --订单ID
			[ServiceTime],    --服务时间
			[ServiceType],    --服务类型
			[ServiceMode],    --服务方式
			[ServiceTitle],    --服务主题
			[ServiceContent],    --服务内容
			[ServiceResult],    --服务结果，0为未完成，1为完成
			[TakeTime],    --花费时间
			[Processor],    --执行人
			[Inputer],    --录入者
			[Feedback],    --客户反馈
			[ConfirmTime],    --回访时间
			[ConfirmCaller],    --回访人
			[ConfirmScore],    --客户评价
			[ConfirmFeedback],    --客户反馈
			[Remark]    --备注
		)
	VALUES
	(
		--@ItemId,
		@ClientId,
		@ContacterId,
		@OrderId,
		@ServiceTime,
		@ServiceType,
		@ServiceMode,
		@ServiceTitle,
		@ServiceContent,
		@ServiceResult,
		@TakeTime,
		@Processor,
		@Inputer,
		@Feedback,
		@ConfirmTime,
		@ConfirmCaller,
		@ConfirmScore,
		@ConfirmFeedback,
		@Remark
	)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_Trademark_SetElite]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_Trademark_SetElite] 
    (
    @TrademarkID Int,
	@IsElite Bit
	)	
AS
	UPDATE 
		PE_Trademark
    SET 
        IsElite = @IsElite
    WHERE 
         TrademarkID = @TrademarkID
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PE_Model]'
GO
ALTER TABLE [dbo].[PE_Model] ADD
[ChargeTips] [nvarchar] (255) COLLATE Chinese_PRC_CI_AS NULL,
[CommentManageTemplate] [nvarchar] (255) COLLATE Chinese_PRC_CI_AS NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Contents_Model_GetById]'
GO
ALTER PROCEDURE [dbo].[PR_Contents_Model_GetById]
(
	@ModelID Int
)
 AS 
 
	SELECT * FROM [PE_Model] WHERE ModelID = @ModelID
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_Keywords_Add]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_Keywords_Add]	
	(
	@KeywordText NVarChar(200),
    @KeywordType Int,
    @Priority Int,
    @ArrayGeneralId ntext,
    @QuoteTimes int
	)	
AS
	INSERT INTO PE_Keywords
	      (KeywordText,KeywordType,Priority,arrGeneralID,QuoteTimes)
	VALUES (@KeywordText,@KeywordType,@Priority,@ArrayGeneralId,@QuoteTimes)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_Bank_GetList]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_Bank_GetList]

 AS 
 SET NOCOUNT OFF	
	SELECT 
		[BankID],
		[BankShortName],
		[BankName],
		[Accounts],
		[CardNum],
		[HolderName],
		[BankIntro],
		[BankPic],
		[IsDefault],
		[IsDisabled],
		[OrderID]
	 FROM
		PE_Bank
	 order by IsDefault desc,OrderID
	 RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_Refund_GetByOrderId]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_Refund_GetByOrderId] 
	(
	@OrderId Int
)
 AS 
 
	select O.OrderNum,O.Email,O.UserName,C.ClientID,C.ClientName,O.MoneyTotal,O.MoneyReceipt from PE_Orders O left join PE_Client C on O.ClientID=C.ClientID where O.OrderID=@OrderId and MoneyReceipt>0
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Contents_Model_IsModelNameExists]'
GO
ALTER PROCEDURE [dbo].[PR_Contents_Model_IsModelNameExists]
(
    @ModelName NVarChar(50)
)
as
select count(*) from [PE_Model] where ModelName=@ModelName
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_Invoice_Add]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_Invoice_Add] 	
	(
	@ClientId INT,
	@UserName NVarChar(50),
	@OrderId INT,
    @InvoiceType INT,
	@InvoiceDate DateTime,
	@InvoiceNum NVarChar(50),
	@InvoiceTitle NVarChar(50),
	@InvoiceContent NTEXT,
    @Memo NTEXT,
	@TotalMoney MONEY,
	@Drawer NVarChar(50),
	@Inputer NVarChar(50)
	)	
AS
   INSERT INTO PE_InvoiceItem
	      (ClientID,UserName, OrderID,InvoiceType, InvoiceDate, InvoiceNum, InvoiceTitle, InvoiceContent,Memo, TotalMoney, 
	      Drawer, Inputer, InputTime)
	VALUES (@ClientId,@UserName,@OrderId,@InvoiceType,@InvoiceDate,@InvoiceNum,@InvoiceTitle,@InvoiceContent,@Memo,@TotalMoney,@Drawer,@Inputer,getDate())
    
    UPDATE PE_Orders SET Invoiced = 1 WHERE OrderID = @OrderId
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Crm_Address_Add]'
GO
ALTER PROCEDURE [dbo].[PR_Crm_Address_Add] 	
	(
	@AddressID int,    --地址Id
	@UserName nvarchar(20),    --用户名
	@HomePhone nvarchar(30)='',    --固定电话
	@Mobile nvarchar(30)='',    --移动电话
	@Country nvarchar(50),    --国家
	@Province nvarchar(30),    --省份
	@City nvarchar(30)='',    --城市
	@Area nvarchar(30)='',
	@Address nvarchar(255),    --详细地址
	@ZipCode nvarchar(50)='',    --邮编
	@ConsigneeName nvarchar(255)=''
	
)
AS
	DECLARE @IsDefault bit
    SET @IsDefault = 0
	IF not exists(select * from PE_Address where UserName=@UserName)
	begin
		set @IsDefault = 1
	end

	INSERT INTO 
		[PE_Address] 
		(
			[AddressID],    --地址Id
			[UserName],    --用户名
			[ConsigneeName],
			[HomePhone],    --固定电话
			[Mobile],    --移动电话
			[Country],    --国家
			[Province],    --省份
			[City],    --城市
			[Area],
			[Address],    --详细地址
			[ZipCode],    --邮编
			[IsDefault]    --是否默认
		)
	VALUES
	(
		@AddressID,
		@UserName,
		@ConsigneeName,
		@HomePhone,
		@Mobile,
		@Country,
		@Province,
		@City,
		@Area,
		@Address,
		@ZipCode,
		@IsDefault
	)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_AD_ADZone_Active]'
GO
ALTER PROCEDURE [dbo].[PR_AD_ADZone_Active] 
(
	@strZoneId  varchar(8000)
)
	
AS
	/* SET NOCOUNT ON */ 
	DECLARE @active bit
	DECLARE @zoneId int
	DECLARE @split varchar(1)
	
	SET @split = ','
	SET @strZoneId = @strZoneId+','
	
	WHILE CHARINDEX(@split,@strZoneId)>0
	
	BEGIN
		SET @zoneId = CONVERT(int,LEFT(@strZoneId,CHARINDEX(@split,@strZoneId)-1))
		SET @strZoneId = STUFF(@strZoneId,1,CHARINDEX(@split,@strZoneId),'')
	
		SELECT @active=Active FROM PE_ADZone WHERE ZoneId = @zoneId
		IF @active = 0
			SET @active = 1
		UPDATE PE_ADZone SET Active=@active WHERE ZoneId = @zoneId
	END
	
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在重新生成 [dbo].[PE_CollectionItem]'
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
SET XACT_ABORT ON
GO
BEGIN TRANSACTION
CREATE TABLE [dbo].[tmp_ms_xx_PE_CollectionItem]
(
[ItemID] [int] NOT NULL,
[ItemName] [nvarchar] (255) COLLATE Chinese_PRC_CI_AS NOT NULL,
[UrlName] [nvarchar] (255) COLLATE Chinese_PRC_CI_AS NULL,
[CodeType] [nvarchar] (255) COLLATE Chinese_PRC_CI_AS NOT NULL,
[Url] [ntext] COLLATE Chinese_PRC_CI_AS NOT NULL,
[Intro] [ntext] COLLATE Chinese_PRC_CI_AS NULL,
[NodeID] [int] NOT NULL,
[InfoNodeID] [nvarchar] (255) COLLATE Chinese_PRC_CI_AS NULL,
[ModelID] [int] NOT NULL,
[SpecialID] [nvarchar] (255) COLLATE Chinese_PRC_CI_AS NULL,
[OrderType] [int] NOT NULL,
[MaxNum] [int] NULL CONSTRAINT [DF_PE_CollectionItem_MaxNum] DEFAULT (0),
[NewsCollecDate] [datetime] NULL,
[Detection] [bit] NULL CONSTRAINT [DF_PE_CollectionItem_Detection] DEFAULT (0),
[AutoCreateHtml] [bit] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
INSERT INTO [dbo].[tmp_ms_xx_PE_CollectionItem]([ItemID], [ItemName], [UrlName], [CodeType], [Url], [Intro], [NodeID], [ModelID], [OrderType], [MaxNum], [NewsCollecDate], [Detection]) SELECT [ItemID], [ItemName], [UrlName], [CodeType], [Url], [Intro], [NodeID], [ModelID], [OrderType], [MaxNum], [NewsCollecDate], [Detection] FROM [dbo].[PE_CollectionItem]
DROP TABLE [dbo].[PE_CollectionItem]
EXEC sp_rename N'[dbo].[tmp_ms_xx_PE_CollectionItem]', N'PE_CollectionItem'
COMMIT TRANSACTION
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
GO
PRINT N'正在 [dbo].[PE_CollectionItem] 上创建主键 [PK_PE_CollectionItem]'
GO
ALTER TABLE [dbo].[PE_CollectionItem] ADD CONSTRAINT [PK_PE_CollectionItem] PRIMARY KEY CLUSTERED  ([ItemID]) ON [PRIMARY]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_AD_Advertisement_Passed]'
GO
ALTER PROCEDURE [dbo].[PR_AD_Advertisement_Passed] 
	
	(
	@strAdId varchar(8000)
	)
	
AS
	/* SET NOCOUNT ON */ 
	DECLARE @passed bit
	DECLARE @adId int
	DECLARE @split varchar(1)
	
	SET @split = ','
	SET @strAdId = @strAdId+','
	
	WHILE CHARINDEX(@split,@strAdId)>0
	BEGIN
		SET @adId = CONVERT(int,LEFT(@strAdId,CHARINDEX(@split,@strAdId)-1))
		SET @strAdId = STUFF(@strAdId,1,CHARINDEX(@split,@strAdId),'')
		
		SELECT @passed = Passed FROM PE_Advertisement WHERE AdID=@adId
		IF @passed = 0 
			SET @passed =1
		UPDATE PE_Advertisement SET passed = @passed WHERE ADId=@adId
	END
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_BankrollItem_Update]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_BankrollItem_Update]
	@ItemId int,
	@UserName nvarchar(50),
	@ClientId int,
	@DateAndTime datetime,
	@Money money,
	@MoneyType int,
	@CurrencyType int,
	@eBankId int,
	@Bank nvarchar(50),
	@OrderID int,
	@PaymentId int,
	@Remark nvarchar(255),
	@LogTime datetime,
	@Inputer nvarchar(50),
	@IP nvarchar(50),
	@Status int
AS

SET NOCOUNT OFF

UPDATE [dbo].[PE_BankrollItem] SET
	[UserName] = @UserName,
	[ClientId] = @ClientId,
	[DateAndTime] = @DateAndTime,
	[Money] = @Money,
	[MoneyType] = @MoneyType,
	[CurrencyType] = @CurrencyType,
	[eBankId] = @eBankId,
	[Bank] = @Bank,
	[OrderId] = @OrderId,
	[PaymentId] = @PaymentId,
	[Remark] = @Remark,
	[LogTime] = @LogTime,
	[Inputer] = @Inputer,
	[IP] = @IP,
	[Status] = @Status
WHERE
	[ItemId] = @ItemId
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_Order_AgentPayment]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_Order_AgentPayment] 
	(
	@OrderID Int,
	@Money money,
	@RefundType Int
	)
AS
	DECLARE  @AgentBalance money,@AgentClientID Int,@UserName NVARCHAR (4000)
	SELECT AgentName,MoneyReceipt,MoneyTotal FROM PE_Orders WHERE OrderID = @OrderID AND MoneyReceipt<MoneyTotal
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_AD_Advertisement_CancelPassed]'
GO
ALTER PROCEDURE [dbo].[PR_AD_Advertisement_CancelPassed]
	(
	@strAdId Nvarchar(4000)
	)
	
AS
	SET NOCOUNT OFF
	DECLARE @passed bit
	DECLARE @adId int
	DECLARE @split varchar(1)
	
	SET @split = ','
	SET @strAdId = @strAdId+','
	
	WHILE CHARINDEX(@split,@strAdId)>0
	BEGIN
		SET @adId = CONVERT(int,LEFT(@strAdId,CHARINDEX(@split,@strAdId)-1))
		SET @strAdId = STUFF(@strAdId,1,CHARINDEX(@split,@strAdId),'')
		
		SELECT @passed = Passed FROM PE_Advertisement WHERE AdID=@adId
		IF @passed = 1 
			SET @passed = 0
		UPDATE PE_Advertisement SET passed = @passed WHERE ADId=@adId
	END
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_AD_ADZone_Pause]'
GO
ALTER PROCEDURE [dbo].[PR_AD_ADZone_Pause] 
	
	(
	@strZoneId  varchar(8000)
	)
	
AS
	/* SET NOCOUNT ON */ 
	DECLARE @active bit
	DECLARE @zoneId int
	DECLARE @split varchar(1)
	
	SET @split = ','
	SET @strZoneId = @strZoneId+','
	
	WHILE CHARINDEX(@split,@strZoneId)>0
	
	BEGIN
		SET @zoneId = CONVERT(int,LEFT(@strZoneId,CHARINDEX(@split,@strZoneId)-1))
		SET @strZoneId = STUFF(@strZoneId,1,CHARINDEX(@split,@strZoneId),'')
	
		SELECT @active=Active FROM PE_ADZone WHERE ZoneId = @zoneId
		IF @active = 1
			SET @active = 0
		UPDATE PE_ADZone SET Active=@active WHERE ZoneId = @zoneId
	END
	
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_DeliverType_GetMaxId]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_DeliverType_GetMaxId] 
	
AS
	SELECT ISNULL(MAX(TypeId),0) 
	FROM PE_DeliverType
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_DeliverType_Add]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_DeliverType_Add]
	 (  @typeId int,
		@typeName nvarchar(50),
		@intro nvarchar(50),
		@chargeType int,
		@isDefault bit = 0,
		@isDisabled bit = 0,
		@releaseType int = 0,
		@minMoney1 money = 0,
		@releaseCharge money = 0,
		@minmoney2 money = 0,
		@maxCharge money = 0,
		@minMoney3 money = 0,
		@charge_Min money = 0,
		@charge_Max money = 0,
		@includeTax bit = 0,
		@taxRate float = 0,
		@charge_Percent smallint = 0
     )
AS
	DECLARE @orderId int
	SELECT @orderId = (ISNULL(MAX(OrderId),0) + 1) FROM PE_DeliverType
	
	IF(@typeId = 1)
		BEGIN
			
			SET @isDefault = 1
		END
	ELSE
		BEGIN
			IF(@isDefault = 1)
				BEGIN
					UPDATE PE_DeliverType SET IsDefault = 0 WHERE IsDefault = 1
				END
		END

	INSERT INTO [PE_DeliverType]
           ([TypeId]
           ,[TypeName]
           ,[Intro]
           ,[ChargeType]
           ,[IsDefault]
           ,[IsDisabled]
           ,[OrderId]
           ,[ReleaseType]
           ,[MinMoney1]
           ,[ReleaseCharge]
           ,[Minmoney2]
           ,[MaxCharge]
           ,[MinMoney3]
           ,[Charge_Min]
           ,[Charge_Max]
		   ,[IncludeTax]
		   ,[TaxRate]
           ,[Charge_Percent]
		   )
     VALUES
          ( @typeId,
			@typeName,
			@intro,
			@chargeType,
			@isDefault,
			@isDisabled,
			@orderId ,
			@releaseType ,
			@minMoney1,
			@releaseCharge,
			@minmoney2,
			@maxCharge,
			@minMoney3,
			@charge_Min,
			@charge_Max,
			@includeTax,
			@taxRate,
			@charge_Percent
            )
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_Producer_SetOnTop]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_Producer_SetOnTop] 
    (
    @ProducerID Int,
	@OnTop Bit
	)	
AS
	UPDATE 
		PE_Producer
    SET 
        OnTop = @OnTop
    WHERE 
         ProducerID = @ProducerID
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_Keywords_Exists]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_Keywords_Exists]
(
    @KeywordText NVarChar(200)
)
as

select count(*) from PE_Keywords where KeywordText=@KeywordText
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_AD_Advertisement_Add]'
GO
ALTER PROCEDURE [dbo].[PR_AD_Advertisement_Add]
(
@ADID int,
@strZoneId nvarchar(4000),
@UserID int,    
@ADType int,    
@ADName nvarchar(100),    
@ImgUrl nvarchar(255),    
@ImgWidth int,    
@ImgHeight int,    
@FlashWmode int,    
@ADIntro ntext,    
@LinkUrl nvarchar(255),    
@LinkTarget int,    
@LinkAlt nvarchar(255),    
@Priority int,    
@Setting ntext,    
@CountView bit,    
@Views int,    
@CountClick bit,    
@Clicks int,    
@Passed bit,
@OverdueDate DateTime 
) 
AS
SET NOCOUNT OFF
 
	DECLARE @active bit
	DECLARE @zoneId int
	DECLARE @split varchar(1)
	
	SET @split = ','
	IF @strZoneId!=''
	BEGIN
		SET @strZoneId = @strZoneId+','
		WHILE CHARINDEX(@split,@strZoneId)>0
		BEGIN
			SET @zoneId = CONVERT(int,LEFT(@strZoneId,CHARINDEX(@split,@strZoneId)-1))
			SET @strZoneId = STUFF(@strZoneId,1,CHARINDEX(@split,@strZoneId),'')
			INSERT INTO PE_Zone_Advertisement(ADID,ZoneID)VALUES(@ADID,@ZoneId)
		END
	END
BEGIN 
INSERT INTO [PE_Advertisement] 
([ADID],[UserID],[ADType],[ADName],[ImgUrl],[ImgWidth],[ImgHeight],[FlashWmode],[ADIntro],[LinkUrl],[LinkTarget],[LinkAlt],[Priority],[Setting],[CountView],[Views],[CountClick],[Clicks],[Passed],[OverdueDate])
VALUES
(@ADID,@UserID,@ADType,@ADName,@ImgUrl,@ImgWidth,@ImgHeight,@FlashWmode,@ADIntro,@LinkUrl,@LinkTarget,@LinkAlt,@Priority,@Setting,@CountView,@Views,@CountClick,@Clicks,@Passed,@OverdueDate)
END
RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Contents_Model_IsTableNameExists]'
GO
ALTER PROCEDURE [dbo].[PR_Contents_Model_IsTableNameExists]
(
    @TableName NVarChar(50)
)
as
BEGIN
	SET NOCOUNT OFF	
	
	select count(*) from [PE_Model] where TableName=@TableName

End
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_AD_ADZone_Update]'
GO
ALTER PROCEDURE [dbo].[PR_AD_ADZone_Update]
	(
	@ZoneId int,
    @ZoneName nvarchar(100),
    @ZoneJSName nvarchar(100),
    @ZoneIntro nvarchar(255),
    @ZoneType int,
    @DefaultSetting bit,
    @ZoneSetting nvarchar(255),
    @ZoneWidth int,
    @ZoneHeight int,
    @Active bit,
    @ShowType int,
    @UpdateTime Datetime
	)
	
AS
 SET NOCOUNT OFF
 BEGIN
	UPDATE PE_AdZone 
	SET ZoneName=@ZoneName,ZoneJSName=@ZoneJSName,ZoneIntro=@ZoneIntro,ZoneType=@ZoneType,DefaultSetting=@DefaultSetting,ZoneSetting=@ZoneSetting,ZoneWidth=@ZoneWidth,ZoneHeight=@ZoneHeight,Active=@Active,ShowType=@ShowType
	WHERE
	ZoneId=@ZoneId
	
 END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_Author_Update]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_Author_Update]
(
    @ID int,
    @UserId int,
    @Name varchar(50),
    @Type varchar(50),
    @Passed bit,
    @onTop bit,
    @IsElite bit,
    @Hits int,
    @LastUseTime DateTime,
    @TemplateID int,
    @Photo varchar(255),
    @Intro nvarchar(255),
    @Address varchar(50),
    @Tel varchar(50),
    @Fax varchar(50),
    @Mail varchar(50),
    @Email varchar(50),
    @ZipCode int,
    @HomePage varchar(50),
    @Im varchar(50),
    @Sex smallint,
    @BirthDay DateTime,
    @Company varchar(50),
    @Department varchar(50)
)
AS
	IF @ID > 0
	BEGIN

		UPDATE PE_Author SET
		UserID=@UserId,Type=@Type,[Name]=@Name,Passed=@Passed,onTop=@onTop,IsElite=@IsElite,Hits=@Hits,LastUseTime=@LastUseTime,TemplateID=@TemplateID,Photo=@Photo,Intro=@Intro,Address=@Address,Tel=@Tel,Fax=@Fax,Mail=@Mail,Email=@Email,ZipCode=@ZipCode,HomePage=@HomePage,Im=@Im,Sex=@Sex,BirthDay=@BirthDay,Company=@Company,Department=@Department
		WHERE ID=@ID
		
	END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_InsideLink_Update]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_InsideLink_Update] 
    (
    @InsideLinkID int,
	@Source NVarChar(250),    
	@ReplaceUrl NVarChar(250),    
	@Priority int,    
	@ReplaceTimes int,    
	@OpenType bit,
	@IsEnabled bit    
	)	
AS
	UPDATE 
		PE_InsideLink
    SET 
        Source = @Source, ReplaceUrl=@ReplaceUrl,Priority=@Priority,ReplaceTimes=@ReplaceTimes,OpenType=@OpenType,IsEnabled = @IsEnabled 
    WHERE 
         InsideLinkID = @InsideLinkID
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_Order_Add]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_Order_Add] 
(
	@OrderID int,    
	@OrderNum nvarchar(20),    
	@UserName nvarchar(50),    
	@AgentName nvarchar(50), 
	@Functionary nvarchar(50), 
	@ClientID int,    
	@MoneyTotal money,    
	@MoneyGoods money,   
	@NeedInvoice bit,   
	@InvoiceContent ntext,   
	@Invoiced bit,    
	@Remark ntext,    
	@MoneyReceipt money,    
	@BeginDate datetime,    
	@InputTime datetime,   
	@ContacterName nvarchar(50),   
	@Address nvarchar(255),   
	@ZipCode nvarchar(10),    
	@Mobile nvarchar(50),    
	@Phone nvarchar(50),   
	@Email nvarchar(50),  
	@PaymentType int,  
	@DeliverType int,  
	@Status int,   
	@DeliverStatus int,   
	@EnableDownload bit,   
	@PresentMoney money,   
	@PresentPoint int, 
	@PresentExp int, 
	@Discount_Payment float,  
	@Charge_Deliver money ,
    @Memo ntext,
    @OutOfStockProject int,
    @OrderType int,
    @CouponID int,
    @DeliveryTime nvarchar(128)
)
AS
	INSERT INTO 
		[PE_Orders] 
		(
			[OrderID],    
			[OrderNum],   
			[UserName],    
			[AgentName], 
			[Functionary],  
			[ClientID],    
			[MoneyTotal],    
			[MoneyGoods],    
			[NeedInvoice],    
			[InvoiceContent],   
			[Invoiced],  
			[Remark],   
			[MoneyReceipt],   
			[BeginDate],   
			[InputTime],   
			[ContacterName],  
			[Address],   
			[ZipCode],  
			[Mobile],   
			[Phone],    
			[Email],   
			[PaymentType],   
			[DeliverType],  
			[Status],   
			[DeliverStatus],  
			[EnableDownload],  
			[PresentMoney],   
			[PresentPoint],  
			[PresentExp],  
			[Discount_Payment],   
			[Charge_Deliver],
            [Memo],
            [OutOfStockProject],
            [OrderType],
            [CouponID],
            [DeliveryTime]
		)
	VALUES
	(
		@OrderID,
		@OrderNum,
		@UserName,
		@AgentName,
		@Functionary,
		@ClientID,
		@MoneyTotal,
		@MoneyGoods,
		@NeedInvoice,
		@InvoiceContent,
		@Invoiced,
		@Remark,
		@MoneyReceipt,
		@BeginDate,
		@InputTime,
		@ContacterName,
		@Address,
		@ZipCode,
		@Mobile,
		@Phone,
		@Email,
		@PaymentType,
		@DeliverType,
		@Status,
		@DeliverStatus,
		@EnableDownload,
		@PresentMoney,
		@PresentPoint,
		@PresentExp,
		@Discount_Payment,
		@Charge_Deliver,
        @Memo,
        @OutOfStockProject,
        @OrderType,
        @CouponID,
        @DeliveryTime
	)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_Remittance_Add]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_Remittance_Add] 
    (
    @OrderID Int,
	@Money money
	)	
AS
	UPDATE 
		PE_Orders
    SET 
        Status = 1, MoneyReceipt=MoneyReceipt+@Money
    WHERE 
         OrderID = @OrderID AND MoneyReceipt<MoneyTotal
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_Bank_SetOrderID]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_Bank_SetOrderID]
	
(
	@BankID int,
	@OrderID int
)

AS
IF NOT EXISTS ( Select OrderID From PE_Bank Where OrderID=@OrderID and BankID<>@BankID)
	BEGIN
		UPDATE
			PE_Bank
		SET
			[OrderID] = @OrderID
		WHERE
			[BankID] = @BankID
	END	
RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_UserManage_Users_BatchUnLock]'
GO
ALTER PROCEDURE [dbo].[PR_UserManage_Users_BatchUnLock]
	(
	@strUserID Varchar(4000),
	@LockState int,
	@ApproveState int
	)
	
AS
	/* SET NOCOUNT ON */ 
	/* SET NOCOUNT ON */ 
	DECLARE @Status bit
	DECLARE @UserId int
	DECLARE @split varchar(1)
	
	SET @split = ','
	SET @strUserId = @strUserId+','
	
	WHILE CHARINDEX(@split,@strUserId)>0
	
	BEGIN
		SET @UserId = CONVERT(int,LEFT(@strUserId,CHARINDEX(@split,@strUserId)-1))
		SET @strUserId = STUFF(@strUserId,1,CHARINDEX(@split,@strUserId),'')
	
		UPDATE PE_Users SET [Status]=@ApproveState WHERE UserId = @UserId AND [Status]=@LockState
	END
	
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Crm_Client_Update]'
GO
ALTER PROCEDURE [dbo].[PR_Crm_Client_Update] 
(
	@ClientID Int, 
	@ParentID Int, 
	@ClientNum NVarChar(20), 
	@ClientType Int, 
	@ClientName NVarChar(100), 
	@ShortedForm NVarChar(20), 
	@Area Int, 
	@ClientField Int, 
	@ValueLevel Int, 
	@CreditLevel Int, 
	@Importance Int, 
	@ConnectionLevel Int, 
	@GroupID Int, 
	@SourceType Int, 
	@PhaseType Int, 
	@Remark NText, 
	@UpdateTime datetime, 
	@Owner NVarChar(50)
)
AS
	UPDATE [PE_Client]
   SET 
		ParentID=@ParentID, 
		ClientNum=@ClientNum, 
		ClientType=@ClientType, 
		ClientName=@ClientName, 
		ShortedForm=@ShortedForm, 
		Area=@Area, 
		ClientField=@ClientField, 
		ValueLevel=@ValueLevel, 
		CreditLevel=@CreditLevel, 
		Importance=@Importance, 
		ConnectionLevel=@ConnectionLevel, 
		GroupID=@GroupID, 
		SourceType=@SourceType, 
		PhaseType=@PhaseType, 
		Remark=@Remark, 
		UpdateTime=@UpdateTime, 
		Owner=@Owner
 WHERE  ClientID = @ClientID
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_Trademark_SetPassed]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_Trademark_SetPassed] 
    (
    @TrademarkID Int,
	@Passed Bit
	)	
AS
	UPDATE 
		PE_Trademark
    SET 
        Passed = @Passed
    WHERE 
         TrademarkID = @TrademarkID
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_Log_Update]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_Log_Update]
(
	@LogID int,
	@Category int,    
	@Priority int,    
	@Title nvarchar(255),    
	@Message text,    
	@Timestamp datetime,    
	@UserName nvarchar(255),
    @UserIP nvarchar(20),
	@Source text,
	@ScriptName nvarchar(255),    
	@PostString text    
)
AS
	UPDATE [PE_Log]
   SET [Category] = @Category,
      [Priority] = @Priority,
      [Title] = @Title,
      [Message] = @Message,
      [Timestamp] = @Timestamp,
      [UserName] = @UserName,
      [UserIP] = @UserIP,
      [Source] = @Source,
      [ScriptName] = @ScriptName,
      [PostString] = @PostString
 WHERE LogID = @LogID
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_Region_GetProvinceList]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_Region_GetProvinceList]
AS
SET NOCOUNT ON
SELECT DISTINCT Province FROM PE_Region
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Contents_Model_Add]'
GO
ALTER PROCEDURE [dbo].[PR_Contents_Model_Add]	
(
    @ModelID int,
	@ModelName NVarChar(50),    
	@Description NVarChar(250)='',
	@TableName nvarchar(50),
	@ItemName nvarchar(50),
	@ItemUnit nvarchar(50), 
	@ItemIcon nvarchar(255),   
	@IsCountHits bit,
	@IsEshop bit,
	@Disabled bit, 
	@Field ntext ='',
    @DefaultTemplateFile nvarchar(255)='',
    @EnableCharge bit,
    @EnableSignin bit,
    @EnableVote bit,
    @AddInfoFilePath	nvarchar(200),
	@ManageInfoFilePath	nvarchar(200),
	@PreviewInfoFilePath nvarchar(200),
	@ChargeTips nvarchar(255),
	@BatchInfoFilePath	nvarchar(200),
	@Character int,
	@MaxPerUser int,
	@PrintTemplate nvarchar(255),
	@SearchTemplate nvarchar(255),
	@AdvanceSearchFormTemplate nvarchar(255),
	@AdvanceSearchTemplate nvarchar(255),
	@CommentManageTemplate nvarchar(255)
)
AS
	INSERT INTO 
		[PE_Model] 
		(  
			[ModelID], 
			[ModelName],    
			[Description], 
			[TableName],
			[ItemName],
			[ItemUnit],  
			[ItemIcon],  
			[IsCountHits],
			[IsEshop],    
			[Disabled],
			[Field],
			[DefaultTemplateFile],
			[EnableCharge],
			[EnableSignin],
			[EnableVote],
			[AddInfoFilePath],
			[ManageInfoFilePath],
		    [PreviewInfoFilePath],
	        [BatchInfoFilePath],
			[Character],
			[MaxPerUser],
			[PrintTemplate],
			[SearchTemplate],
			[AdvanceSearchFormTemplate],
			[AdvanceSearchTemplate],
			[ChargeTips],
			[CommentManageTemplate]
		)
	VALUES
	(
		@ModelID,
		@ModelName,
		@Description,
		@TableName,
		@ItemName,
		@ItemUnit,
		@ItemIcon,
		@IsCountHits,
		@IsEshop,
		@Disabled,
		@Field,
		@DefaultTemplateFile,
		@EnableCharge,
		@EnableSignin,
		@EnableVote,
		@AddInfoFilePath,
		@ManageInfoFilePath,
		@PreviewInfoFilePath,
		@BatchInfoFilePath,
		@Character,
		@MaxPerUser,
		@PrintTemplate,
		@SearchTemplate,
	    @AdvanceSearchFormTemplate,
		@AdvanceSearchTemplate,
		@ChargeTips,
		@CommentManageTemplate
	)
BEGIN
		-- 不存在创建指定数据表
		EXEC('CREATE TABLE [dbo].'+ @TableName+' (ID int,CONSTRAINT [PK_'+@TableName+'_ID] PRIMARY KEY CLUSTERED
(
 [ID] ASC
))')

END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_Producer_GetById]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_Producer_GetById]
(
	@ProducerID Int
)
 AS 
 
	SELECT ProducerID,ProducerName,ProducerShortName,BirthDay,Address,Phone,Fax,Postcode,Homepage,Email,ProducerIntro,ProducerPhoto,ProducerType,Passed,onTop,IsElite FROM PE_Producer WHERE ProducerID = @ProducerID
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_UserManage_UserPointLog_UpdateTimes]'
GO
ALTER PROCEDURE [dbo].[PR_UserManage_UserPointLog_UpdateTimes]
(
   @Id int,
   @Ip nvarchar(100)
)
AS
SET NOCOUNT OFF

    BEGIN
		Update PE_PointLog set Times=Times+1,IP=@Ip where LogID=@Id 
	END

	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_DeliverCharge_GetListByAreaType]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_DeliverCharge_GetListByAreaType] 
(
	@typeId INT,
	@areaType INT
)
AS
	SELECT [Id]
      ,[DeliverTypeId]
      ,[AreaType]
      ,[arrArea]
      ,[Charge_Min]
      ,[Weight_Min]
      ,[ChargePerUnit]
      ,[WeightPerUnit]
      ,[Charge_Max]
  FROM [PE_DeliverCharge]
  WHERE [DeliverTypeId] = @typeId AND [AreaType] =  @areaType

	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_DeliverCharge_Update]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_DeliverCharge_Update] 
(
	@id int ,
	@areaType int = 0 ,
	@arrArea ntext ,
	@charge_Min money =0 ,
	@weight_Min float = 0 ,
	@chargePerUnit money = 0 ,
	@weightPerUnit float = 0 ,
	@charge_Max money = 0
)
AS
	UPDATE [PE_DeliverCharge]
   SET 
      [AreaType] = @areaType ,
      [arrArea] = @arrArea ,
      [Charge_Min] = @charge_Min ,
      [Weight_Min] = @weight_Min ,
      [ChargePerUnit] = @chargePerUnit ,
      [WeightPerUnit] = @weightPerUnit ,
      [Charge_Max] = @charge_Max
 WHERE  [Id] = @id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Survey_SurveyManager_Update]'
GO
ALTER PROCEDURE [dbo].[PR_Survey_SurveyManager_Update]
(
	@SurveyID int,    
	@SurveyName nvarchar(50),    
	@Description ntext,    
	@FileName nvarchar(50),    
	@IPRepeat int,   
	@CreateDate datetime,    
	@EndTime datetime,   
	@IsOpen int,    
	@NeedLogin int,    
	@PresentPoint int,    
	@LockIPType int,    
	@SetIPLock ntext,    
	@LockUrl ntext,    
	@SetPassword nvarchar(50),   
	@Template nvarchar(255)  
)
AS
	UPDATE 
		[PE_Survey] 
	SET
			[SurveyName] = @SurveyName,    
			[Description] = @Description,  
			[FileName] = @FileName,  
			[IPRepeat] = @IPRepeat,   
			[CreateDate] = @CreateDate,   
			[EndTime] = @EndTime, 
			[IsOpen] = @IsOpen,    
			[NeedLogin] = @NeedLogin,   
			[PresentPoint] = @PresentPoint,    
			[LockIPType] = @LockIPType,   
			[SetIPLock] = @SetIPLock,  
			[LockUrl] = @LockUrl,  
			[SetPassword] = @SetPassword, 
			[Template] = @Template   
	WHERE
		[SurveyID] = @SurveyID
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_DeliverCharge_GetByTypeId]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_DeliverCharge_GetByTypeId] 
(
	@typeId INT
)
AS
	SELECT [Id]
      ,[DeliverTypeId]
      ,[AreaType]
      ,[arrArea]
      ,[Charge_Min]
      ,[Weight_Min]
      ,[ChargePerUnit]
      ,[WeightPerUnit]
      ,[Charge_Max]
  FROM [PE_DeliverCharge]
  WHERE [DeliverTypeId] = @typeId

	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_Log_Add]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_Log_Add]
(
	@Category int,    
	@Priority int,    
	@Title nvarchar(255),    
	@Message text,    
	@Timestamp datetime,    
	@UserName nvarchar(255),
    @UserIP nvarchar(20),
	@Source text,
	@ScriptName nvarchar(255),    
	@PostString text    
)
AS
	INSERT INTO 
		[PE_Log] 
		(
			Category,    
			Priority,    
			Title,    
			[Message],    
			[Timestamp],    
			UserName,
			UserIP,
			Source,
			ScriptName,    
			PostString    
		)
	VALUES
	(
	@Category ,    
	@Priority ,    
	@Title ,    
	@Message ,    
	@Timestamp ,    
	@UserName ,
    @UserIP ,
	@Source ,
	@ScriptName ,    
	@PostString     
	)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_UserManage_UserPointLog_Add]'
GO
ALTER PROCEDURE [dbo].[PR_UserManage_UserPointLog_Add](
	@LogID int,
	@UserName nvarchar(100),
	@ModuleType int,
	@InfoID int,
	@Point real,
	@LogTime DateTime,
	@Times int,
	@IncomePayOut int,
	@Remark ntext,
	@IP nvarchar(100),
	@Inputer nvarchar(100),
    @Memo ntext
)   
AS
	/* SET NOCOUNT ON */ 
	INSERT INTO
        PE_PointLog(UserName,ModuleType,InfoID,Point,LogTime,Times,IncomePayOut,Remark,IP,Inputer,Memo)VALUES(@UserName,@ModuleType,@InfoID,@Point,@LogTime,@Times,@IncomePayOut,@Remark,@IP,@Inputer,@Memo)  
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_UserManage_User_Exists]'
GO
ALTER PROCEDURE [dbo].[PR_UserManage_User_Exists]
(
    @UserName NVarChar(250)
)
as

select count(*) from PE_Users  where UserName=@UserName
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_Source_Delete]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_Source_Delete] 
(
    @ID nvarchar(100)
)
AS
	IF @ID IS NOT NULL 
	BEGIN
		DELETE FROM PE_Source WHERE ID in(@ID)
	END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_BankrollItem_Add]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_BankrollItem_Add]
	@UserName nvarchar(50) = null,
	@ClientID int = 0,
	@DateAndTime datetime=0,
	@Money money = 0,
	@MoneyType int = 0,
	@CurrencyType int = 0,
	@eBankID int = 0,
	@Bank nvarchar(50) = null,
	@OrderID int = 0,
	@PaymentID int = 0,
	@Remark nvarchar(255) = null,
	@LogTime datetime = 0,
	@Inputer nvarchar(50) = null,
	@IP nvarchar(50) =null,
	@Status int,
    @Memo NTEXT
AS

SET NOCOUNT OFF

INSERT INTO [dbo].[PE_BankrollItem] (
	[UserName],
	[ClientID],
	[DateAndTime],
	[Money],
	[MoneyType],
	[CurrencyType],
	[eBankID],
	[Bank],
	[OrderID],
	[PaymentID],
	[Remark],
	[LogTime],
	[Inputer],
	[IP],
	[Status],
    [Memo]
) VALUES (
	@UserName,
	@ClientID,
	@DateAndTime,
	@Money,
	@MoneyType,
	@CurrencyType,
	@eBankID,
	@Bank,
	@OrderID,
	@PaymentID,
	@Remark,
	@LogTime,
	@Inputer,
	@IP,
	@Status,
    @Memo
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_AD_ADZone_Add]'
GO
ALTER PROCEDURE [dbo].[PR_AD_ADZone_Add]
	(	
	@ZoneId int,
    @ZoneName nvarchar(100),
    @ZoneJSName nvarchar(100),
    @ZoneIntro nvarchar(255),
    @ZoneType int,
    @DefaultSetting bit,
    @ZoneSetting nvarchar(255),
    @ZoneWidth int,
    @ZoneHeight int,
    @Active bit,
    @ShowType int,
    @UpdateTime Datetime
	)

AS
SET NOCOUNT OFF 
BEGIN
INSERT INTO
PE_AdZone(ZoneId,ZoneName,ZoneJSName,ZoneIntro,ZoneType,DefaultSetting,ZoneSetting,ZoneWidth,ZoneHeight,Active,ShowType,UpdateTime)
Values(@ZoneId,@ZoneName,@ZoneJSName,@ZoneIntro,@ZoneType,@DefaultSetting,@ZoneSetting,@ZoneWidth,@ZoneHeight,@Active,@ShowType,@UpdateTime)
END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_WordReplace_GetById]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_WordReplace_GetById]
(
	@ItemID Int
)
 AS 
	SELECT * FROM PE_WordReplaceItem WHERE ItemID = @ItemID
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_Producer_Update]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_Producer_Update] 
    (
    @ProducerID Int,
	@ProducerName NVarChar(200),
	@ProducerShortName NVarChar(50),
	@Birthday DateTime,
	@Address NVarChar(255),
	@Phone NVarChar(50),
	@Fax NVarChar(50),
	@Postcode NVarChar(10),
	@Homepage NVarChar(50),
	@Email NVarChar(50),
	@ProducerIntro NText,
	@ProducerPhoto NVarChar(255),
	@ProducerType Int
	)	
AS
	UPDATE 
		PE_Producer
    SET 
        ProducerName = @ProducerName, ProducerShortName=@ProducerShortName, BirthDay=@Birthday, Address=@Address, Phone=@Phone, Fax=@Fax, Postcode=@Postcode, Homepage=@Homepage, Email=@Email, ProducerIntro=@ProducerIntro, ProducerPhoto=@ProducerPhoto, ProducerType=@ProducerType
    WHERE 
         ProducerID = @ProducerID
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_OrderItem_Update]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_OrderItem_Update] 
	(
	@ItemID int,    --ID
	@OrderID int,    --订单ID
	@ProductID int,    --产品ID
	@TableName nvarchar(255),    
	@Property nvarchar(255),    
	@SaleType int,    --销售类型  1--正常销售  2--换购  3--赠送  4--批发
	@Price_Market money,    --原价
	@Price money,    --系统计算出的销售价
	@TruePrice money,    --实际销售价
	@Amount int,    --订购数量
	@SubTotal money,    --订购金额
	@BeginDate datetime,    --开始计算服务期限日期
	@ServiceTerm int,    --服务期限
	@ServiceTermUnit int,    --服务年限单位　0－年，1－月，2－日
	@Remark nvarchar(255),    --备注
	@PresentMoney money,    --返还的现金券
	@PresentPoint int,    --赠送点券
	@PresentExp int,   --赠送的积分
    @ProductCharacter int,
    @ProductName nvarchar(255),
    @Unit nvarchar(20),
    @Weight float
)
AS
	UPDATE 
		[PE_OrderItem] 
	SET
		
			[ItemID] = @ItemID,    --ID
			[OrderID] = @OrderID,    --订单ID
			[ProductID] = @ProductID,    --产品ID
			[TableName] = @TableName,    
			[Property] = @Property,    
			[SaleType] = @SaleType,    --销售类型  1--正常销售  2--换购  3--赠送  4--批发
			[Price_Market] = @Price_Market,    --原价
			[Price] = @Price,    --系统计算出的销售价
			[TruePrice] = @TruePrice,    --实际销售价
			[Amount] = @Amount,    --订购数量
			[SubTotal] = @SubTotal,    --订购金额
			[BeginDate] = @BeginDate,    --开始计算服务期限日期
			[ServiceTerm] = @ServiceTerm,    --服务期限
			[ServiceTermUnit] = @ServiceTermUnit,    --服务年限单位　0－年，1－月，2－日
			[Remark] = @Remark,    --备注
			[PresentMoney] = @PresentMoney,    --返还的现金券
			[PresentPoint] = @PresentPoint,    --赠送点券
			[PresentExp] = @PresentExp,    --赠送的积分
			[ProductCharacter] = @ProductCharacter ,
            [ProductName] = @ProductName,
            [Unit] = @Unit,
            [Weight] = @Weight
	WHERE
		[ItemID] = @ItemID
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_ShoppingCarts_Add]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_ShoppingCarts_Add] 
	(
	@UserName nvarchar(50),
	@CartID nvarchar(50),
	@ProductID int,
	@Quantity int,
	@IsPresent bit,
	@UpdateTime datetime,
	@TableName nvarchar(200),
	@Property nvarchar(50)
	)
AS
	/* SET NOCOUNT ON */ 
INSERT INTO 
	PE_ShoppingCarts
	( 
	UserName,
	CartID,
	ProductID,
	Quantity,
	IsPresent,
	UpdateTime,
	TableName,
	Property	
	 ) 
 VALUES
	(
	@UserName,
	@CartID,
	@ProductID,
	@Quantity,
	@IsPresent,
	@UpdateTime,
	@TableName,
	@Property	
	)
	
RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_UserManage_UserValidLog_Delete]'
GO
ALTER PROCEDURE [dbo].[PR_UserManage_UserValidLog_Delete]
	(
	@Date DateTime
	)
	
AS
	 SET NOCOUNT OFF
	 BEGIN 
	 
	 DELETE From PE_ValidLog WHERE LogTime < @Date
	 
	END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_Bank_SetDisabled]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_Bank_SetDisabled]
(
	@BankID int,		-----银行ID
	@IsDisabled bit		-----True为禁用，False为启用
)

AS
 SET NOCOUNT OFF
	UPDATE
		PE_Bank
	SET
		[IsDisabled] = @IsDisabled
	WHERE
		[BankID] = @BankID
	
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Crm_ComplainItem_Add]'
GO
ALTER PROCEDURE [dbo].[PR_Crm_ComplainItem_Add]
(
	@ItemID int,    
	@ClientID int,    
	@ContacterID int,   
	@ComplainType int,    
	@ComplainMode int,   
	@Title nvarchar(50),   
	@Content ntext,   
	@FirstReceiver nvarchar(50),    
	@DateAndTime datetime,   
	@MagnitudeOfExigence int,   
	@Process ntext,    
	@Processor nvarchar(50),   
	@Result nvarchar(50),    
	@EndTime datetime,    
	@Feedback nvarchar(255),    
	@ConfirmTime datetime,    
	@ConfirmCaller nvarchar(50),    
	@ConfirmScore int,   
	@ConfirmFeedback nvarchar(255),    
	@Status int,    
	@CurrentOwner nvarchar(50),    
	@Remark nvarchar(255),
    @Defendant nvarchar(50) 
)
AS
	INSERT INTO 
		[PE_ComplainItem] 
		(
			[ItemID],
			[ClientID],   
			[ContacterID],  
			[ComplainType],  
			[ComplainMode],  
			[Title], 
			[Content], 
			[FirstReceiver], 
			[DateAndTime],  
			[MagnitudeOfExigence], 
			[Process],  
			[Processor], 
			[Result],  
			[EndTime],   
			[Feedback],    
			[ConfirmTime],  
			[ConfirmCaller],  
			[ConfirmScore],   
			[ConfirmFeedback],   
			[Status],    --记录状态，0为未处理，1为处理中，2为已处理，3为已回访
			[CurrentOwner],    
			[Remark],
            [Defendant]
		)
	VALUES
	(
		@ItemID,
		@ClientID,
		@ContacterID,
		@ComplainType,
		@ComplainMode,
		@Title,
		@Content,
		@FirstReceiver,
		@DateAndTime,
		@MagnitudeOfExigence,
		@Process,
		@Processor,
		@Result,
		@EndTime,
		@Feedback,
		@ConfirmTime,
		@ConfirmCaller,
		@ConfirmScore,
		@ConfirmFeedback,
		@Status,
		@CurrentOwner,
		@Remark,
        @Defendant
	)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_UserManage_Users_GetById]'
GO
ALTER PROCEDURE [dbo].[PR_UserManage_Users_GetById]
 (
    @UserID int
 )
AS
	SELECT * FROM PE_Users Where UserID=@UserID
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_UserManage_UserPointLog_Delete]'
GO
ALTER PROCEDURE [dbo].[PR_UserManage_UserPointLog_Delete]
	(
	@Date DateTime
	)
	
AS
	 SET NOCOUNT OFF
	 BEGIN  
		DELETE PE_PointLog WHERE LogTime < @Date
	 END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Contents_Model_Update]'
GO
-- Stored procedure
ALTER PROCEDURE [dbo].[PR_Contents_Model_Update] 
    (
    @ModelID int,
	@ModelName NVarChar(50),    
	@Description NVarChar(250)='',
	@TableName nvarchar(50),
	@ItemName nvarchar(50),
	@ItemUnit nvarchar(50),   
	@ItemIcon nvarchar(255), 
	@IsCountHits bit,
	@IsEshop bit,
	@Disabled bit,    
	@DefaultTemplateFile nvarchar(255)='',
	@EnableCharge bit,
    @EnableSignin bit,
    @EnableVote bit,
    @AddInfoFilePath	nvarchar(200),
	@ManageInfoFilePath	nvarchar(200),
	@PreviewInfoFilePath nvarchar(200),
	@BatchInfoFilePath nvarchar(200),
	@MaxPerUser int,
	@PrintTemplate nvarchar(255),
	@SearchTemplate nvarchar(255),
	@AdvanceSearchFormTemplate nvarchar(255),
	@AdvanceSearchTemplate nvarchar(255),
	@ChargeTips nvarchar(255),
	@CommentManageTemplate nvarchar(255)
	)	
AS
	UPDATE 
		[PE_Model] 
    SET 
        ModelID = @ModelID, 
        ModelName=@ModelName,
        Description=@Description,
        TableName=@TableName,
        ItemName = @ItemName,
        ItemUnit=@ItemUnit,
        ItemIcon=@ItemIcon,
        IsCountHits=@IsCountHits,
        IsEshop=@IsEshop,
        Disabled = @Disabled,
        DefaultTemplateFile= @DefaultTemplateFile,
        EnableCharge = @EnableCharge,
        EnableSignin = @EnableSignin,
        AddInfoFilePath= @AddInfoFilePath,
		ManageInfoFilePath= @ManageInfoFilePath,		
		PreviewInfoFilePath= @PreviewInfoFilePath,
		BatchInfoFilePath = @BatchInfoFilePath,
		MaxPerUser = @MaxPerUser,
		PrintTemplate = @PrintTemplate,
		EnableVote=@EnableVote,
		SearchTemplate=@SearchTemplate,
		AdvanceSearchFormTemplate=@AdvanceSearchFormTemplate,
		AdvanceSearchTemplate=@AdvanceSearchTemplate,
		ChargeTips=@ChargeTips,
		CommentManageTemplate=@CommentManageTemplate
    WHERE 
        ModelID = @ModelID
    RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_PaymentType_Add]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_PaymentType_Add] 
	(
		@typeName NVARCHAR(50),
		@intro NVARCHAR(50) = NULL,
		@discount FLOAT = 100.0,
		@isDefault BIT,
		@isDisabled BIT,
		@category int = 0
	)
	
AS

	
	DECLARE @typeId INT
	DECLARE @orderId INT
	
	SELECT @typeId = ISNULL(MAX(TypeId),0) + 1, @orderId = ISNULL(MAX(OrderId),0) + 1 FROM PE_PaymentType
	
	IF(@typeID = 1)
		BEGIN
			SET @isDefault = 1
		END
	ELSE
		BEGIN
			IF(@isDefault = 1)
				BEGIN
					UPDATE PE_PaymentType SET IsDefault = 0 WHERE IsDefault = 1
				END
		END
	
	INSERT INTO PE_PaymentType(TypeId,TypeName,Intro,Discount,OrderId,IsDefault,IsDisabled,Category)
	VALUES(@typeId,@typeName,@intro,@discount,@orderId,@isDefault,@isDisabled,@category)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_Source_Update]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_Source_Update] 
(
	@ID int,
    @Type varchar(50),
    @Name varchar(50),
    @Passed bit,
    @onTop bit,
    @IsElite bit,
    @Hits int,
    @LastUseTime DateTime,
    @Photo varchar(50),
    @Intro nvarchar(255),
    @Address varchar(50),
    @Tel varchar(50),
    @Fax varchar(50),
    @Mail varchar(50),
    @Email varchar(50),
    @ZipCode int,
    @HomePage varchar(50),
    @Im varchar(50),
    @ContacterName varchar(50)
)
AS
	IF @ID > 0
	BEGIN
		UPDATE PE_Source SET
		Type=@Type,[Name]=@Name,Passed=@Passed,onTop=@onTop,IsElite=@IsElite,Hits=@Hits,LastUseTime=@LastUseTime,Photo=@Photo,Intro=@Intro,Address=@Address,Tel=@Tel,Fax=@Fax,Mail=@Mail,Email=@Email,ZipCode=@ZipCode,HomePage=@HomePage,Im=@Im,Contacter=@ContacterName
		WHERE ID=@ID
		
	END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Contents_Model_Disabled]'
GO
ALTER PROCEDURE [dbo].[PR_Contents_Model_Disabled]
(
    @ModelID int,
    @Disabled bit
 )	
AS	
UPDATE [PE_Model] SET Disabled=@Disabled WHERE ModelID=@ModelID
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_PaymentType_GetList]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_PaymentType_GetList]
 
AS

	--显示全部付款方式
	SELECT *
	FROM PE_PaymentType  order by orderId
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_Source_Insert]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_Source_Insert] 
(
    @Type varchar(50),
    @Name varchar(50),
    @Passed bit,
    @onTop bit,
    @IsElite bit,
    @Hits int,
    @LastUseTime DateTime,
    @Photo varchar(50),
    @Intro nvarchar(255),
    @Address varchar(50),
    @Tel varchar(50),
    @Fax varchar(50),
    @Mail varchar(50),
    @Email varchar(50),
    @ZipCode int,
    @HomePage varchar(50),
    @Im varchar(50),
    @ContacterName varchar(50)
)
AS
	INSERT INTO
	PE_Source(Type,Name,Passed,onTop,IsElite,Hits,LastUseTime,Photo,Intro,Address,Tel,Fax,Mail,Email,ZipCode,HomePage,Im,Contacter) VALUES(@Type,@Name,@Passed,@onTop,@IsElite,@Hits,@LastUseTime,@Photo,@Intro,@Address,@Tel,@Fax,@Mail,@Email,@ZipCode,@HomePage,@Im,@ContacterName)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_Region_Update]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_Region_Update] 
	(
	@RegionID int,
    @Country nvarchar(20),
    @Province nvarchar(25),
    @City nvarchar(30),
    @Area varchar(25),
    @PostCode varchar(10),
    @AreaCode nvarchar(8)
	)
AS
	SET NOCOUNT OFF
	
	UPDATE PE_Region 
	SET Country=@Country,Province=@Province,City=@City,Area=@Area,PostCode=@PostCode,AreaCode=@AreaCode WHERE RegionID=@RegionID
 
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_UserManage_UserNameIsRepeat]'
GO
ALTER PROCEDURE [dbo].[PR_UserManage_UserNameIsRepeat]
	(
	@UserName Nvarchar(4000)
	
	)
	
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @Total int 
	
	SELECT @Total=Count(*) FROM PE_Users WHERE UserName = @UserName
	
	RETURN @Total
	END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_Remittance_GetByOrderId]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_Remittance_GetByOrderId] 
	(
	@OrderId Int
)
 AS 
 
	select O.ClientID,O.OrderID, O.OrderNum,O.UserName,O.Email,C.ShortedForm as ClientName,O.MoneyTotal,O.MoneyReceipt from PE_Orders O left join PE_Client C on O.ClientID=C.ClientID where O.OrderID=@OrderId and MoneyTotal>MoneyReceipt
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_Trademark_SetOnTop]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_Trademark_SetOnTop] 
    (
    @TrademarkID Int,
	@OnTop Bit
	)	
AS
	UPDATE 
		PE_Trademark
    SET 
        OnTop = @OnTop
    WHERE 
         TrademarkID = @TrademarkID
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_Author_Delete]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_Author_Delete] 
(
    @ID nvarchar(100)
)
AS	
	IF @ID IS NOT NULL 
	BEGIN
		DELETE FROM PE_Author WHERE ID in(@ID) 
	END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_DeliverType_Update]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_DeliverType_Update] 
	(  
		@typeId int,
		@typeName nvarchar(50),
		@intro nvarchar(50),
		@chargeType int,
		@isDefault bit ,
		@isDisabled bit ,
		@orderId int,
		@releaseType int = 0,
		@minMoney1 money = 0,
		@releaseCharge money = 0,
		@minmoney2 money = 0,
		@maxCharge money = 0,
		@minMoney3 money = 0,
		@charge_Min money = 0,
		@charge_Max money = 0,
		@includeTax bit = 0,
		@taxRate float = 0,
		@charge_Percent smallint = 0
	)
AS

--处理 默认值
	IF(@isDefault = 1)
		BEGIN
			UPDATE [PE_DeliverType] SET IsDefault = 0 WHERE IsDefault = 1
		END
	
	--处理 排序
	DECLARE @oldOrderId INT
	DECLARE @max INT
	DECLARE @min INT
	DECLARE @count INT
	DECLARE @icount INT
	DECLARE @offset1 INT
	DECLARE @offset2 INT

	SET @icount = 1
	DECLARE @tmp TABLE(Row INT IDENTITY(1,1) PRIMARY KEY,id INT ,oid INT)
	SELECT @oldOrderId = OrderId FROM [PE_DeliverType] WHERE TypeID = @typeId
	IF(@orderId <> @oldOrderId)
	BEGIN
		IF(@orderId > @oldOrderId)
		BEGIN
			SET @max = @orderId
			SET @min = @oldOrderId
			SET @offset1 = 0
			SET @offset2 = 1
		END
		ELSE
		BEGIN
			SET @max = @oldOrderId
			SET @min = @orderId
			SET @offset1 = 1
			SET @offset2 = 0
		END
		INSERT INTO @tmp(id,oid)  SELECT TypeId,OrderId 
		FROM  [PE_DeliverType] 
		WHERE OrderId BETWEEN @min AND @max
		ORDER BY orderId
		SELECT @count = count(*)  From @tmp

		While(@icount < @count)
		BEGIN
			UPDATE [PE_DeliverType] SET OrderId = (select oid from @tmp where Row = @icount + @offset1)
			WHERE TypeId =(Select Id From @tmp Where Row = @icount + @offset2)
			SET @icount = @icount + 1
		END 
	END
	
	UPDATE [PE_DeliverType]
   SET 
       [TypeName] = @typeName
      ,[Intro] = @intro
      ,[ChargeType] = @chargeType
      ,[IsDefault] = @isDefault
      ,[IsDisabled] = @isDisabled
      ,[OrderId] = @orderId
      ,[ReleaseType] = @releaseType
      ,[MinMoney1] = @minMoney1
      ,[ReleaseCharge] = @releaseCharge
      ,[Minmoney2] =@minmoney2
      ,[MaxCharge] = @maxCharge
      ,[MinMoney3] = @minMoney3
      ,[Charge_Min] = @charge_Min
      ,[Charge_Max] = @charge_Max
	  ,[IncludeTax] = @includeTax
	  ,[TaxRate] = @taxRate
      ,[Charge_Percent] = @charge_Percent
 WHERE TypeId = @typeId
 if not (@@error > 0)
	begin
		return 1
	end
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_Order_Update]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_Order_Update] 
	(
	@OrderID int ,
	@UserName nvarchar(50) ,
	@AgentName nvarchar(50) ,
	@Functionary nvarchar(50) ,
	@MoneyTotal money ,
	@MoneyGoods money ,
	@NeedInvoice bit ,
	@InvoiceContent ntext ,
	@Remark ntext ,
	@BeginDate datetime ,
	@Address nvarchar(255) ,
	@ZipCode nvarchar(10) ,
	@Mobile nvarchar(50) ,
	@Phone nvarchar(50) ,
	@Email nvarchar(50) ,
	@PaymentType int ,
	@DeliverType int ,
    @Status int ,
	@Discount_Payment float ,
	@MoneyReceipt money, 
	@EnableDownload bit, 
	@DeliverStatus int,   
	@Charge_Deliver money ,
    @Memo ntext,
	@PresentMoney money,   
	@PresentPoint int, 
	@PresentExp int,
    @OutOfStockProject int,
    @OrderType int,
    @CouponID int,
    @InputTime DateTime,
    @ContacterName nvarchar(50),
    @DeliveryTime nvarchar(128)
	)
AS
	UPDATE 
		[PE_Orders] 
	SET
			[UserName] = @UserName,
			[AgentName] = @AgentName,
			[Functionary] = @Functionary,
			[MoneyTotal] = @MoneyTotal,
			[MoneyGoods] = @MoneyGoods,
			[NeedInvoice] = @NeedInvoice,
			[InvoiceContent] = @InvoiceContent,
			[Remark] = @Remark,
			[BeginDate] = @BeginDate,
			[Address] = @Address,
			[ZipCode] = @ZipCode,
			[Mobile] = @Mobile,
			[Phone] = @Phone,
			[Email] = @Email,
			[PaymentType] = @PaymentType,
			[DeliverType] = @DeliverType,
            [Status] = @Status,
			[Discount_Payment] = @Discount_Payment,
			[Charge_Deliver] = @Charge_Deliver,
			[MoneyReceipt]=@MoneyReceipt,
			[EnableDownload]=@EnableDownload,
			[DeliverStatus]=@DeliverStatus,
            [Memo] = @Memo,
            [PresentMoney] = @PresentMoney,
            [PresentPoint] = @PresentPoint,
			[PresentExp] = @PresentExp,
            [OutOfStockProject] = @OutOfStockProject,
            [OrderType] = @OrderType,
            [CouponID] = @CouponID,
            [InputTime] = @InputTime,
            [ContacterName] = @ContacterName,
            [DeliveryTime] = @DeliveryTime
	WHERE
		OrderID = @OrderID
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Crm_Contacter_Update]'
GO
ALTER PROCEDURE [dbo].[PR_Crm_Contacter_Update]
(
	@ContacterID int,    
	@ClientID int,
	@ParentID int,
    @UserName nvarchar(20), 
	@UserType int,    
	@TrueName nvarchar(20),    
	@Sex int,    
	@Title nvarchar(50),    
	@Company nvarchar(100),    
	@Department nvarchar(50),    
	@Position nvarchar(50),    
	@Operation nvarchar(50),    
	@CompanyAddress nvarchar(200),    
	@Email nvarchar(100),    
	@Homepage nvarchar(255),    
	@QQ nvarchar(20),    
	@ICQ nvarchar(25),    
	@MSN nvarchar(100),    
	@Yahoo nvarchar(100),    
	@UC nvarchar(100),    
	@Aim nvarchar(100),    
	@OfficePhone nvarchar(30),    
	@HomePhone nvarchar(30),    
	@PHS nvarchar(30),    
	@Fax nvarchar(30),    
	@Mobile nvarchar(30),    
	@Country nvarchar(50),    
	@Province nvarchar(30),    
	@City nvarchar(30),    
	@Address nvarchar(255),    
	@ZipCode nvarchar(10),    
	@NativePlace nvarchar(50),    
	@Nation nvarchar(50),    
	@Birthday datetime,    
	@IDCard nvarchar(20),    
	@Marriage int,
    @Family ntext,      
	@Income int,    
	@Education int,    
	@GraduateFrom nvarchar(255),    
	@InterestsOfLife nvarchar(255),    
	@InterestsOfCulture nvarchar(255),    
	@InterestsOfAmusement nvarchar(255),    
	@InterestsOfSport nvarchar(255),    
	@InterestsOfOther nvarchar(255),    
	@UpdateTime datetime
)
AS
	UPDATE 
		[PE_Contacter] 
	SET
			[ClientID] = @ClientID,    
			[ParentID] = @ParentID,    
			[ContacterID] = @ContacterID,  
            [UserName] = @UserName,
			[UserType] = @UserType,    
			[TrueName] = @TrueName,    
			[Sex] = @Sex,    
			[Title] = @Title,    
			[Company] = @Company,    
			[Department] = @Department,    
			[Position] = @Position,    
			[Operation] = @Operation,    
			[CompanyAddress] = @CompanyAddress,    
			[Email] = @Email,    
			[Homepage] = @Homepage,    
			[QQ] = @QQ,    
			[ICQ] = @ICQ,    
			[MSN] = @MSN,    
			[Yahoo] = @Yahoo,    
			[UC] = @UC,    
			[Aim] = @Aim,    
			[OfficePhone] = @OfficePhone,    
			[HomePhone] = @HomePhone,    
			[PHS] = @PHS,    
			[Fax] = @Fax,    
			[Mobile] = @Mobile,    
			[Country] = @Country,    
			[Province] = @Province,    
			[City] = @City,    
			[Address] = @Address,    
			[ZipCode] = @ZipCode,    
			[NativePlace] = @NativePlace,    
			[Nation] = @Nation,    
			[Birthday] = @Birthday,    
			[IDCard] = @IDCard,    
			[Marriage] = @Marriage,
			[Family] = @Family,      
			[Income] = @Income,    
			[Education] = @Education,    
			[GraduateFrom] = @GraduateFrom,    
			[InterestsOfLife] = @InterestsOfLife,    
			[InterestsOfCulture] = @InterestsOfCulture,    
			[InterestsOfAmusement] = @InterestsOfAmusement,    
			[InterestsOfSport] = @InterestsOfSport,    
			[InterestsOfOther] = @InterestsOfOther,    
			[UpdateTime] = @UpdateTime
	WHERE
		[ContacterID] = @ContacterID
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_InsideLink_Add]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_InsideLink_Add]	
(
    @InsideLinkID int,
	@Source NVarChar(250),    
	@ReplaceUrl NVarChar(250),    
	@Priority int,    
	@ReplaceTimes int,    
	@OpenType bit,
	@IsEnabled bit  
)
AS
	INSERT INTO 
		[PE_InsideLink] 
		(   
			[Source],    
			[ReplaceUrl],    
			[Priority],    
			[ReplaceTimes],    
			[OpenType],
			[IsEnabled]    
		)
	VALUES
	(
		@Source,
		@ReplaceUrl,
		@Priority,
		@ReplaceTimes,
		@OpenType,
		@IsEnabled
	)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_UserManage_Cards_Update]'
GO
ALTER PROCEDURE [dbo].[PR_UserManage_Cards_Update]
(
	@CardID int,    --充值卡ID
	@CardType int,    --充值卡类型
	@ProductID int,    --充值卡所属商品
	@TableName nvarchar(255),    
	@CardNum nvarchar(30),    --卡号
	@Password nvarchar(50),    --密码
	@AgentName nvarchar(100),    --代理商
	@Money money,    --面值
	@ValidNum int,    --点数
	@ValidUnit int,    --点数单位
	@EndDate datetime,    --截止日期
	@UserName nvarchar(50),    --使用者
	@UseTime datetime,    --充值时间
	@CreateTime datetime,    --生成时间
	@OrderItemID int    --售出
)
AS
	UPDATE 
		[PE_Cards] 
	SET
			[CardType] = @CardType,    --充值卡类型
			[ProductID] = @ProductID,    --充值卡所属商品
			[TableName] = @TableName,    
			[CardNum] = @CardNum,    --卡号
			[Password] = @Password,    --密码
			[AgentName] = @AgentName,    --代理商
			[Money] = @Money,    --面值
			[ValidNum] = @ValidNum,    --点数
			[ValidUnit] = @ValidUnit,    --点数单位
			[EndDate] = @EndDate,    --截止日期
			[UserName] = @UserName,    --使用者
			[UseTime] = @UseTime,    --充值时间
			[CreateTime] = @CreateTime,    --生成时间
			[OrderItemID] = @OrderItemID    --售出
	WHERE
		[CardID] = @CardID
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_OrderItem_Add]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_OrderItem_Add] 
	(
           @ItemID int,
           @OrderID int,
           @ProductID int,
           @TableName nvarchar(255),
		   @Property nvarchar(255),
           @SaleType int,
           @Price_Market money,
           @Price money,
           @TruePrice money,
           @Amount int,
           @SubTotal money,
           @BeginDate datetime,
           @ServiceTerm int,
		   @ServiceTermUnit int,
           @Remark nvarchar(255),
           @PresentMoney money,
           @PresentPoint int,
           @PresentExp int,
		   @ProductCharacter int,
           @ProductName nvarchar(255),
           @Unit nvarchar(20),
           @Weight float
	)
AS
	INSERT INTO PE_OrderItem
           ([ItemID]
           ,[OrderID]
           ,[ProductID]
           ,[TableName]
		   ,[Property]
           ,[SaleType]
           ,[Price_Market]
           ,[Price]
           ,[TruePrice]
           ,[Amount]
           ,[SubTotal]
           ,[BeginDate]
           ,[ServiceTerm]
           ,[ServiceTermUnit]
           ,[Remark]
           ,[PresentMoney]
           ,[PresentPoint]
           ,[PresentExp]
		   ,[ProductCharacter]
           ,[ProductName]
           ,[Unit]
           ,[Weight])
     VALUES
           ( @ItemID ,
           @OrderID ,
           @ProductID ,
           @TableName ,
		   @Property ,
           @SaleType ,
           @Price_Market ,
           @Price ,
           @TruePrice ,
           @Amount ,
           @SubTotal ,
           @BeginDate ,
           @ServiceTerm ,
           @ServiceTermUnit,
           @Remark ,
           @PresentMoney ,
           @PresentPoint ,
           @PresentExp ,
           @ProductCharacter,
           @ProductName,
           @Unit,
           @Weight)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_PaymentType_Delete]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_PaymentType_Delete]
	(@typeId INT)
AS
declare @Oid int
	Select 
		@Oid = OrderId 
	from 
		PE_PaymentType
	where
		[TypeId] = @typeId
	
	DELETE 
		PE_PaymentType 
	WHERE 
		TypeId = @typeId

	Update 
		PE_PaymentType
	Set
		OrderId = OrderId - 1
	Where
		OrderId > @Oid
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在创建 [dbo].[PR_SERCH_KNTNT_LEWI]'
GO
CREATE PROCEDURE [dbo].[PR_SERCH_KNTNT_LEWI]
@Keyword VARCHAR(50),
@HighLight VARCHAR(50),
@pagesize VARCHAR(50),
@startrow VARCHAR(50)
AS
BEGIN
    DECLARE @TabName VARCHAR(20)
    DECLARE @SerchStr VARCHAR(300)
    CREATE TABLE #(CID INT,TTL VARCHAR(50),UPTAM DATETIME,KNTNT NTEXT)
    DECLARE TAB CURSOR FOR SELECT DISTINCT C.TableName FROM PE_CommonModel C WHERE C.Status=99 AND C.Title LIKE '%'+@Keyword+'%'
    OPEN TAB
    FETCH NEXT FROM TAB INTO @TabName
    WHILE @@FETCH_STATUS = 0
    BEGIN
        SET @SerchStr = 'INSERT INTO # SELECT A.GeneralID,A.Title,A.UpdateTime,B.Content FROM PE_CommonModel A INNER JOIN '
            + @TabName +' B ON A.GeneralID=B.ID WHERE A.Status=99 AND A.Title like '+'''%'+@Keyword+'%''' 
        EXECUTE (@SerchStr)
        FETCH NEXT FROM TAB INTO @TabName
    END
    CLOSE TAB
    DEALLOCATE TAB
    EXEC 
    (' 
    SELECT TOP '+@pagesize+' REPLACE(C.TTL, '''+@keyword+''', '' <font style="'+@HighLight+'">'+@Keyword+' </font>'') AS TTL,C.CID,C.KNTNT,C.UPTAM  
    FROM # C 
    WHERE C.CID NOT IN(SELECT TOP '+@startrow+' IC.CID FROM # IC ORDER BY IC.UPTAM) 
    ')
    DROP TABLE #
END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_UserManage_UserPointLog_UserPointSum]'
GO
-- Stored procedure
ALTER PROCEDURE [dbo].[PR_UserManage_UserPointLog_UserPointSum]
(
    @SumType int,
    @UserName NVarChar(100)
)
As

SET NOCOUNT OFF
If (@SumType = 1)
    BEGIN
		select sum(Point) from PE_PointLog where IncomePayout=1 and UserName = @UserName
	END
Else
	BEGIN
		select sum(Point) from PE_PointLog where IncomePayout=2 and UserName = @UserName
	END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_PaymentType_Update]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_PaymentType_Update]
		@typeId INT,
		@typeName NVARCHAR(50),
		@intro NVARCHAR(50) = NULL,
		@discount FLOAT = 100.0,
		@orderId INT = 0,
		@category INT = 0,
		@isDefault BIT,
		@isDisabled BIT
AS

	
	--处理 默认值
	IF(@isDefault = 1)
		BEGIN
			UPDATE PE_PaymentType SET IsDefault = 0 WHERE IsDefault = 1
		END
	IF @orderId <> 0
	Begin
		--处理 排序
		--处理 排序
		DECLARE @oldOrderId INT
		DECLARE @max INT
		DECLARE @min INT
		DECLARE @count INT
		DECLARE @icount INT
		DECLARE @offset1 INT
		DECLARE @offset2 INT

		SET @icount = 1
		DECLARE @tmp TABLE(Row INT IDENTITY(1,1) PRIMARY KEY,id INT ,oid INT)
		SELECT @oldOrderId = OrderId FROM PE_PaymentType WHERE TypeID = @typeId
		IF(@orderId <> @oldOrderId)
			BEGIN
				IF(@orderId > @oldOrderId)
				BEGIN
					SET @max = @orderId
					SET @min = @oldOrderId
					SET @offset1 = 0
					SET @offset2 = 1
				END
				ELSE
				BEGIN
					SET @max = @oldOrderId
					SET @min = @orderId
					SET @offset1 = 1
					SET @offset2 = 0
				END
				INSERT INTO @tmp(id,oid)  SELECT TypeId,OrderId 
				FROM  PE_PaymentType 
				WHERE OrderId BETWEEN @min AND @max
				ORDER BY orderId
				SELECT @count = count(*)  From @tmp

				While(@icount < @count)
				BEGIN
					UPDATE PE_PaymentType SET OrderId = (select oid from @tmp where Row = @icount + @offset1)
					WHERE TypeId =(Select Id From @tmp Where Row = @icount + @offset2)
					SET @icount = @icount + 1
				END 
			END
			--更新
			UPDATE PE_PaymentType 
			SET TypeName = @typeName,
				Intro = @intro,
				Discount = @discount,
				OrderId = @orderId,
				IsDefault = @isDefault,
				IsDisabled = @isDisabled,
				Category = @category
			WHERE TypeId = @typeId
	End -- End of -----IF @orderId <> 0
ELSE
	Begin
		--不排序的更新
			UPDATE PE_PaymentType 
			SET TypeName = @typeName,
				Intro = @intro,
				Discount = @discount,
				IsDefault = @isDefault,
				IsDisabled = @isDisabled,
				Category = @category
			WHERE TypeId = @typeId
	End
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_Author_GetAuthorInfoByID]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_Author_GetAuthorInfoByID]
(
	@ID int
)
AS
    IF @ID > 0
    BEGIN
        SELECT
		ID,
		UserID,
		Type,
		[Name],
		Passed,
		onTop,
		IsElite,
		Hits,
		LastUseTime,
		TemplateID,
		Photo,
		Intro,
		Address,
		Tel,
		Fax,
		Mail,
		Email,
		ZipCode,
		HomePage,
		Im,
		Sex,
		BirthDay,
		Company,
		Department
		FROM PE_Author WHERE ID=@ID
	END
	
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_Region_Delete]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_Region_Delete] 
	(
	@RegionID varchar(10) 
	)
AS
	SET NOCOUNT OFF 
	DELETE
		PE_Region
	WHERE
		[RegionID]=@RegionID
	
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_Producer_SetPassed]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_Producer_SetPassed] 
    (
    @ProducerID Int,
	@Passed Bit
	)	
AS
	UPDATE 
		PE_Producer
    SET 
        Passed = @Passed
    WHERE 
         ProducerID = @ProducerID
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_DeliverType_GetById]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_DeliverType_GetById]
	(
	@typeId INT
	)
AS
	SELECT *  FROM [PE_DeliverType]
  WHERE  TypeId = @typeId
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_Keywords_SearchKeyword]'
GO
ALTER procedure [dbo].[PR_Accessories_Keywords_SearchKeyword] 	@KeywordText NVarChar(200)as	if exists (select * from PE_Keywords where KeywordText=@KeywordText)		update PE_Keywords set Hits=Hits+1,LastUseTime=getdate() where KeywordText=@KeywordText	else		insert PE_Keywords (KeywordText,Hits,LastUseTime) values (@KeywordText ,1,getdate())
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_UserManage_UserPointLog_PointSum]'
GO
ALTER PROCEDURE [dbo].[PR_UserManage_UserPointLog_PointSum]
(
    @SumType int
)
As

SET NOCOUNT OFF
If (@SumType = 1)
    BEGIN
		select sum(Point) from PE_PointLog where IncomePayout=1
	END
Else
	BEGIN
		select sum(Point) from PE_PointLog where IncomePayout=2
	END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_Trademark_Update]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_Trademark_Update] 
    (
    @TrademarkID Int,
	@TrademarkName NVarChar(200),
    @TrademarkType Int,
    @ProducerId Int,
    @IsElite Bit,	
	@TrademarkPhoto NVarChar(255),
	@TrademarkIntro NText	
	)	
AS
	UPDATE 
		PE_Trademark
    SET 
        TrademarkName = @TrademarkName, TrademarkType=@TrademarkType,ProducerID=@ProducerId,IsElite=@IsElite, TrademarkPhoto=@TrademarkPhoto,TrademarkIntro=@TrademarkIntro
    WHERE 
         TrademarkID = @TrademarkID
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_Bank_GetBankInfoByID]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_Bank_GetBankInfoByID]
(
	@BankID int
)
 AS 
  SET NOCOUNT OFF
	SELECT 
		[BankID],
		[BankShortName],
		[BankName],
		[Accounts],
		[CardNum],
		[HolderName],
		[BankIntro],
		[BankPic],
		[IsDefault],
		[IsDisabled],
		[OrderID]
	 FROM
		PE_Bank
	 WHERE
		[BankID] = @BankID
		RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_Source_GetSourceInfoByID]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_Source_GetSourceInfoByID]
(
	@ID int
)
AS
    IF @ID > 0
    BEGIN
		SELECT
		ID,
		Type,
		[Name],
		Passed,
		onTop,
		IsElite,
		Hits,
		LastUseTime,
		Photo,
		Intro,
		Address,
		Tel,
		Fax,
		Mail,
		Email,
		ZipCode,
		HomePage,
		Im,
		Contacter
		FROM PE_Source WHERE ID=@ID
	END
	
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_Source_GetSourceInfoByType]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_Source_GetSourceInfoByType] 
	(
	@Type varchar(1000)
	)
AS
		SELECT
				ID,
				Type,
				[Name],
				Passed,
				onTop,
				IsElite,
				Hits,
				LastUseTime,
				Photo,
				Intro,
				Address,
				Tel,
				Fax,
				Mail,
				Email,
				ZipCode,
				HomePage,
				Im,
				Contacter
				FROM PE_Source WHERE Type=@Type
				ORDER BY ID DESC 	
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Analytics_StatVisit_Update]'
GO
ALTER PROCEDURE [dbo].[PR_Analytics_StatVisit_Update]
	@visit int
	
AS
BEGIN
	IF NOT EXISTS(Select * from PE_StatVisit)
	Begin
		Insert Into PE_StatVisit values(0,0,0,0,0,0,0,0,0,0)
	End

	IF @visit > 0 and @visit <= 10
	Begin
		declare @num char(2)
		set @num = convert(char(2),@visit)
		exec('update PE_StatVisit set ['+ @num+'] = ['+@num+'] + 1')
	End

	IF @visit > 10
	Begin
		update PE_StatVisit
		set [10] = [10] + 1
	End
END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Crm_Contacter_Add]'
GO
ALTER PROCEDURE [dbo].[PR_Crm_Contacter_Add]
(
	@ContacterID int,    
	@ClientID int,    
	@ParentID int,
    @UserName nvarchar(20),   
	@UserType int,    
	@TrueName nvarchar(20),    
	@Sex int,    
	@Title nvarchar(50),    
	@Company nvarchar(100),    
	@Department nvarchar(50),    
	@Position nvarchar(50),    
	@Operation nvarchar(50),    
	@CompanyAddress nvarchar(200),    
	@Email nvarchar(100),    
	@Homepage nvarchar(255),    
	@QQ nvarchar(20),    
	@ICQ nvarchar(25),    
	@MSN nvarchar(100),    
	@Yahoo nvarchar(100),    
	@UC nvarchar(100),    
	@Aim nvarchar(100),    
	@OfficePhone nvarchar(30),    
	@HomePhone nvarchar(30),    
	@PHS nvarchar(30),    
	@Fax nvarchar(30),    
	@Mobile nvarchar(30),    
	@Country nvarchar(50),    
	@Province nvarchar(30),    
	@City nvarchar(30),    
	@Address nvarchar(255),    
	@ZipCode nvarchar(10),    
	@NativePlace nvarchar(50),    
	@Nation nvarchar(50),    
	@Birthday datetime,    
	@IDCard nvarchar(20),    
	@Marriage int,    
	@Family ntext,    
	@Income int,    
	@Education int,    
	@GraduateFrom nvarchar(255),    
	@InterestsOfLife nvarchar(255),
	@InterestsOfCulture nvarchar(255),    
	@InterestsOfAmusement nvarchar(255),    
	@InterestsOfSport nvarchar(255),    
	@InterestsOfOther nvarchar(255),    
	@CreateTime datetime,    
	@UpdateTime datetime,    
	@Owner nvarchar(50) = ''  
)
AS
	INSERT INTO 
		[PE_Contacter] 
		(
			[ContacterID],    
			[ClientID],    
			[ParentID],
            [UserName],
			[UserType],    
			[TrueName],    
			[Sex],    
			[Title],    
			[Company],    
			[Department],    
			[Position],    
			[Operation],    
			[CompanyAddress],    
			[Email],    
			[Homepage],    
			[QQ],    
			[ICQ],    
			[MSN],    
			[Yahoo],    
			[UC],    
			[Aim],    
			[OfficePhone],    
			[HomePhone],    
			[PHS],    
			[Fax],    
			[Mobile],    
			[Country],    
			[Province],    
			[City],    
			[Address],    
			[ZipCode],    
			[NativePlace],    
			[Nation],    
			[Birthday],    
			[IDCard],    
			[Marriage],    
			[Family],    
			[Income],    
			[Education],    
			[GraduateFrom],    
			[InterestsOfLife],    
			[InterestsOfCulture],    
			[InterestsOfAmusement],    
			[InterestsOfSport],    
			[InterestsOfOther],    
			[CreateTime],    
			[UpdateTime],    
			[Owner]    
		)
	VALUES
	(
		@ContacterID,
		@ClientID,
		@ParentID,
        @UserName,
		@UserType,
		@TrueName,
		@Sex,
		@Title,
		@Company,
		@Department,
		@Position,
		@Operation,
		@CompanyAddress,
		@Email,
		@Homepage,
		@QQ,
		@ICQ,
		@MSN,
		@Yahoo,
		@UC,
		@Aim,
		@OfficePhone,
		@HomePhone,
		@PHS,
		@Fax,
		@Mobile,
		@Country,
		@Province,
		@City,
		@Address,
		@ZipCode,
		@NativePlace,
		@Nation,
		@Birthday,
		@IDCard,
		@Marriage,
		@Family,
		@Income,
		@Education,
		@GraduateFrom,
		@InterestsOfLife,
		@InterestsOfCulture,
		@InterestsOfAmusement,
		@InterestsOfSport,
		@InterestsOfOther,
		@CreateTime,
		@UpdateTime,
		@Owner
	)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_WordReplace_Exists]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_WordReplace_Exists]
(
    @SourceWord NVarChar(250)
)
as
select count(*) from PE_WordReplaceItem where SourceWord=@SourceWord
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_DeliverCharge_Delete]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_DeliverCharge_Delete] 
(
	@id INT
)
AS
BEGIN

	DELETE PE_DeliverCharge
	WHERE ID = @id
	
END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_UserManage_Users_Update]'
GO
ALTER PROCEDURE [dbo].[PR_UserManage_Users_Update]
(             
    @UserID int,    
	@GroupID int,    
	@CompanyID int,    
	@ClientID int,     
	@UserType int,    
	@UserName nvarchar(20),    
	@UserPassword nvarchar(32),    
	@LastPassword nvarchar(32),    
	@Question nvarchar(50),    
	@Answer nvarchar(50),    
	@Email nvarchar(100),    
	@Sex int,    
	@RegTime datetime,    
	@JoinTime datetime,    
	@LoginTimes int,    
	@LastLoginTime datetime,    
	@LastPresentTime datetime,    
	@LastLoginIP nvarchar(20),    
	@LastPasswordChangedTime datetime,    
	@LastLockoutTime datetime,    
	@FailedPasswordAttemptCount int,    
	@FirstFailedPasswordAttempTime datetime,    
	@FailedPasswordAnswerAttempCount int,    
	@FirstFailedPasswordAnswerAttempTime datetime,    
	@Status int,    
	@CheckNum char(10),    
	@EnableResetPassword bit,    
	@UserFace nvarchar(255),    
	@FaceWidth int,    
	@FaceHeight int,    
	@Sign ntext,    
	@PrivacySetting int,    
	@Balance money,    
	@UserPoint int,    
	@UserExp int,    
	@ConsumeMoney int,    
	@ConsumePoint int,    
	@ConsumeExp int,    
	@PostItems int,    
	@PassedItems int,    
	@RejectItems int,    
	@DelItems int,    
	@EndTime datetime,    
	@IsInheritGroupRole bit,    
	@TrueName nvarchar(255),
	@PayPassword nvarchar(32)
)
AS
	UPDATE 
		[PE_Users] 
	SET
	
			[UserID] = @UserID,    
			[GroupID] = @GroupID,    
			[CompanyID] = @CompanyID,    
			[ClientID] = @ClientID,     
			[UserType] = @UserType,    
			[UserName] = @UserName,    
			[UserPassword] = @UserPassword,    
			[LastPassword] = @LastPassword,    
			[Question] = @Question,    
			[Answer] = @Answer,    
			[Email] = @Email,    
			[Sex] = @Sex,    
			[RegTime] = @RegTime,    
			[JoinTime] = @JoinTime,    
			[LoginTimes] = @LoginTimes,    
			[LastLoginTime] = @LastLoginTime,    
			[LastPresentTime] = @LastPresentTime,    
			[LastLoginIP] = @LastLoginIP,    
			[LastPasswordChangedTime] = @LastPasswordChangedTime,    
			[LastLockoutTime] = @LastLockoutTime,    
			[FailedPasswordAttemptCount] = @FailedPasswordAttemptCount,    
			[FirstFailedPasswordAttempTime] = @FirstFailedPasswordAttempTime,    
			[FailedPasswordAnswerAttempCount] = @FailedPasswordAnswerAttempCount,    
			[FirstFailedPasswordAnswerAttempTime] = @FirstFailedPasswordAnswerAttempTime,    
			[Status] = @Status,    
			[CheckNum] = @CheckNum,    
			[EnableResetPassword] = @EnableResetPassword,    
			[UserFace] = @UserFace,    
			[FaceWidth] = @FaceWidth,    
			[FaceHeight] = @FaceHeight,    
			[Sign] = @Sign,    
			[PrivacySetting] = @PrivacySetting,    
			[Balance] = @Balance,    
			[UserPoint] = @UserPoint,    
			[UserExp] = @UserExp,    
			[ConsumeMoney] = @ConsumeMoney,    
			[ConsumePoint] = @ConsumePoint,    
			[ConsumeExp] = @ConsumeExp,    
			[PostItems] = @PostItems,    
			[PassedItems] = @PassedItems,    
			[RejectItems] = @RejectItems,    
			[DelItems] = @DelItems,    
			[EndTime] = @EndTime,    
			[IsInheritGroupRole] = @IsInheritGroupRole,
			[TrueName] = @TrueName,
			[PayPassword] =@PayPassword  
		
	WHERE
		[UserID] = @UserID
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_UserManage_UserValidLog_Add]'
GO
ALTER PROCEDURE [dbo].[PR_UserManage_UserValidLog_Add](

	@LogID int,
	@UserName nvarchar(100),
	@ValidNum int,
	@IncomePayOut int,
	@LogTime DateTime,
	@Remark ntext,
	@IP nvarchar(100),
	@Inputer nvarchar(100),
    @Memo ntext
)   
AS
	/* SET NOCOUNT ON */ 
	INSERT INTO
        PE_ValidLog(UserName,ValidNum,IncomePayout,LogTime,Remark,IP,Inputer,Memo)VALUES(@UserName,@ValidNum,@IncomePayout,@LogTime,@Remark,@IP,@Inputer,@Memo)  
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_DeliverCharge_GetChargeParmOfWeight]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_DeliverCharge_GetChargeParmOfWeight] 
	@AreaType int,
	@DeliverType int
	
AS
	SET NOCOUNT OFF
	SELECT 
		[Charge_Min],
		[Weight_Min],
		[ChargePerUnit],
		[WeightPerUnit],
		[Charge_Max],
		[arrArea]
	 FROM
		PE_DeliverCharge
	 WHERE
		[AreaType] = @AreaType AND [DeliverTypeID]=@DeliverType
		RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_InsideLink_GetById]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_InsideLink_GetById]
(
	@InsideLinkID Int
)
 AS 
	SELECT InsideLinkID,Source,ReplaceUrl,Priority,ReplaceTimes,OpenType,IsEnabled FROM PE_InsideLink WHERE InsideLinkID = @InsideLinkID
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_UserManage_Users_BatchInquire]'
GO
ALTER PROCEDURE [dbo].[PR_UserManage_Users_BatchInquire]
	(
	@UserType int,
	@Id NVarChar(4000),
	@EndTime DateTime
	)
AS

BEGIN
	SET NOCOUNT OFF
				
	If (@UserType = 0)
        BEGIN
			--SELECT UserId,UserName,EndTime FROM PE_Users Order by UserID
			UPDATE PE_Users SET EndTime=@EndTime 
        END
    Else If(@UserType = 1)
        BEGIN
			--SELECT UserId,UserName,EndTime FROM PE_Users Where GroupID in (@SelectGroupID) Order by UserID	
			UPDATE PE_Users SET EndTime=@EndTime WHERE GroupId IN(@Id)
        END
	Else If(@UserType = 2)
        BEGIN
			--SELECT UserId,UserName,EndTime FROM PE_Users Where UserID in (@SelectUserID) Order by UserID
			UPDATE PE_Users SET EndTime = @EndTime WHERE UserId IN(@Id)
        END
END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Crm_Client_GetById]'
GO
ALTER PROCEDURE [dbo].[PR_Crm_Client_GetById]
(
	@ClientID Int
)
 AS 
 
	SELECT * FROM PE_Client WHERE ClientID = @ClientID
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Contents_ModelTemplates_GetField]'
GO
ALTER PROCEDURE [dbo].[PR_Contents_ModelTemplates_GetField]
(
	@TemplateId int     
)
AS
	SELECT Field FROM PE_ModelTemplates WHERE TemplateId = @TemplateId
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_Product_DeleteModel]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_Product_DeleteModel] 
(
	@TableName nvarchar(255)
)
AS
BEGIN

	if not exists(select * from PE_OrderItem where TableName=@TableName)
	Begin
		delete PE_CommonProduct where TableName=@TableName
		delete PE_ProductData where TableName=@TableName
		delete PE_ProductPrice where TableName=@TableName
		if exists(select * from PE_Cards where TableName=@TableName)
		Begin
			update PE_Cards Set ProductID=0,TableName='' 
				Where TableName=@TableName
		End
	End
END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Analytics_DoInit]'
GO
ALTER PROCEDURE [dbo].[PR_Analytics_DoInit]

AS
delete from PE_StatAddress
delete from PE_StatBrowser
delete from PE_StatColor
delete from PE_StatIp
delete from PE_StatMozilla
delete from PE_StatRefer
delete from PE_StatScreen
delete from PE_StatSystem
delete from PE_StatTimezone
delete from PE_StatVisit
delete from PE_StatWeburl
delete from PE_StatDay
delete from PE_StatMonth
delete from PE_StatWeek
delete from PE_StatYear
delete from PE_StatVisitor
Declare @date char(10)
set @date = convert(char(10),getdate(),120)
update PE_StatInfoList 
	set StartDate= @date,
		OldDay= @date,
		TotalNum=0,
		TotalView=0,
		MonthNum=0,
		MonthMaxNum=0,
		OldMonth='',
		MonthMaxDate='',
		DayNum=0,
		DayMaxNum=0,
		DayMaxDate='',
		HourNum=0,
		HourMaxNum=0,
		OldHour='',
		HourMaxTime='',
		ChinaNum=0,
		OtherNum=0

if not (@@error > 0)
begin
	return 1
end
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Analytics_StatUpdate]'
GO
ALTER PROCEDURE [dbo].[PR_Analytics_StatUpdate]
(
		@_visit int,
		@_IP nvarchar(15)='',
		@_address nvarchar(50) = '',
		@_system nvarchar(20)='',
		@_browser nvarchar(20)='',
		@_screen nvarchar(10)='',
		@_color nvarchar(16)='',
		@_referer nvarchar(100)='',
		@_weburl nvarchar(50)='',
		@_timezone nvarchar(50)='',
		@_visitTimezone int,
		@_keyword nvarchar(16)='',
		@_mozilla nvarchar(255)=''
)
AS
BEGIN
	declare @RegFields_Fill nvarchar(4000)
	declare @_scope nvarchar(20)
	declare @Province nvarchar(4000)
	declare @dayLong nvarchar(10)
	declare @monthLong nvarchar(7)
	declare @hour nvarchar(2)
	declare @year nvarchar(4)
	declare @month nvarchar(2)
	declare @day nvarchar(2)
	declare @week nvarchar(1)

	set @monthLong = convert(nvarchar(7),getdate(),120)
	set @dayLong = convert(nvarchar(10),getdate(),120)
	set @year = datename(yyyy,getdate())
	set @month = convert(nvarchar(2),datepart(mm,getdate()))
	set @day = datename(dd,getdate())
	set @hour = datename(hh,getdate())
	set @week =convert(nvarchar(1),datepart(dw,getdate()))
	set @Province ='北京天津上海重庆黑龙江吉林辽宁江苏浙江安徽河南河北湖南湖北山东山西内蒙古陕西甘肃宁夏青海新疆西藏云南贵州四川广东广西福建江西海南香港澳门台湾内部网未知'

--	--分析地址
--	IF @_IP = '127.0.0.1'
--		BEGIN
--			set @_address ='本机内部环回地址'
--			set @_scope = 'ChinaNum'
--		END
--	ELSE
--		BEGIN
			IF charindex(@_address,@Province,0) > 0
				set @_scope = 'ChinaNum'
			ELSE
				set @_scope = 'OtherNum'
--		END
	

	select @RegFields_Fill=RegFields_Fill from pe_statInfolist

	--更新PE_StatVisit表
	IF charindex('FVisit',@RegFields_Fill,0) > 0
		EXEC [PR_Analytics_StatVisit_Update] @visit = @_visit

	--更新PE_StatVisitor表
	EXEC [PR_Analytics_StatVisitor_Add]
		  @Ip = @_IP,
		  @Address = @_address,
		  @System = @_system,
		  @Browser = @_browser,
		  @Screen = @_screen,
		  @Color = @_color,
		  @Referer = @_referer,
		  @Timezone = @_visitTimezone

	--更新PE_StatInfoList表
	IF NOT EXISTS(Select Top 1 * From PE_StatInfoList)
	Begin
		INSERT INTO PE_StatInfoList([StartDate],[TotalNum],[TotalView],[MonthNum],[MonthMaxNum],[OldMonth],[MonthMaxDate],[DayNum],[DayMaxNum],[OldDay],[DayMaxDate],[HourNum],[HourMaxNum],[OldHour],[HourMaxTime],[ChinaNum],[OtherNum],[MasterTimeZone],[Interval],[IntervalNum],[OnlineTime],[VisitRecord],[KillRefresh],[RegFields_Fill],[OldTotalNum],[OldTotalView])
		VALUES(@dayLong,0,0,0,0,0,'',0,0,@dayLong,'',0,0,'','',0,0,0,60,10,120,0,0,'',0,0)
	End
	Exec ('Update PE_StatInfoList set TotalNum = TotalNum +1,TotalView = TotalView +1,'+@_scope+'='+@_scope+'+1')
	declare @StartDate nvarchar(10)
	declare @OldDay nvarchar(10)
	select top 1 @Startdate = startDate ,@OldDay = OldDay From PE_StatInfoList
	IF @StartDate is null
		Update PE_StatInfoList set startDate = @dayLong
	IF @OldDay is null
	begin
		Update PE_StatInfoList set OldDay = @dayLong
		set @OldDay = @dayLong
	end
	Exec [PR_Analytics_ModiMaxNum]

	--更新其它表
	IF @_keyword <> '' and charindex('FKeyword',@RegFields_Fill,0) > 0
	   Exec [PR_Analytics_AddNum] @TableName='PE_StatKeyword',@CompareField='Tkeyword',@AddField = 'TkeywordNum',@Data = @_keyword

	IF charindex('FSystem',@RegFields_Fill,0) > 0
	   Exec [PR_Analytics_AddNum] @TableName='PE_StatSystem',@CompareField='TSystem',@AddField = 'TSysNum',@Data = @_system

	IF charindex('FBrowser',@RegFields_Fill,0) > 0
	   Exec [PR_Analytics_AddNum] @TableName='PE_StatBrowser',@CompareField='TBrowser',@AddField = 'TBrwNum',@Data = @_browser

	IF charindex('FMozilla',@RegFields_Fill,0) > 0
	   Exec [PR_Analytics_AddNum] @TableName='PE_StatMozilla',@CompareField='TMozilla',@AddField = 'TMozNum',@Data = @_mozilla

	IF charindex('FScreen',@RegFields_Fill,0) > 0
	   Exec [PR_Analytics_AddNum] @TableName='PE_StatScreen',@CompareField='TScreen',@AddField = 'TScrNum',@Data = @_screen

	IF charindex('FColor',@RegFields_Fill,0) > 0
	   Exec [PR_Analytics_AddNum] @TableName='PE_StatColor',@CompareField='TColor',@AddField = 'TColNum',@Data = @_color

	IF charindex('FTimezone',@RegFields_Fill,0) > 0
	   Exec [PR_Analytics_AddNum] @TableName='PE_StatTimezone',@CompareField='TTimezone',@AddField = 'TTimNum',@Data = @_timezone

	IF charindex('FRefer',@RegFields_Fill,0) > 0
	   Exec [PR_Analytics_AddNum] @TableName='PE_StatRefer',@CompareField='TRefer',@AddField = 'TRefNum',@Data = @_referer

	IF charindex('FWeburl',@RegFields_Fill,0) > 0
	   Exec [PR_Analytics_AddNum] @TableName='PE_StatWeburl',@CompareField='TWeburl',@AddField = 'TWebNum',@Data = @_weburl

	IF charindex('FAddress',@RegFields_Fill,0) > 0
	   Exec [PR_Analytics_AddNum] @TableName='PE_StatAddress',@CompareField='TAddress',@AddField = 'TAddNum',@Data = @_address

	IF charindex('FIP',@RegFields_Fill,0) > 0
	   Exec [PR_Analytics_AddNum] @TableName='PE_StatIp',@CompareField='TIp',@AddField = 'TIpNum',@Data = @_IP

	Exec [PR_Analytics_AddNum] @TableName='PE_StatDay',@CompareField='TDay',@AddField = @hour,@Data = @dayLong
	Exec [PR_Analytics_AddNum] @TableName='PE_StatDay',@CompareField='TDay',@AddField = @hour,@Data = 'Total'
	Exec [PR_Analytics_AddNum] @TableName='PE_StatYear',@CompareField='TYear',@AddField = @month,@Data = @year
	Exec [PR_Analytics_AddNum] @TableName='PE_StatYear',@CompareField='TYear',@AddField = @month,@Data = 'Total'
	Exec [PR_Analytics_AddNum] @TableName='PE_StatMonth',@CompareField='TMonth',@AddField = @day,@Data = @monthLong
	Exec [PR_Analytics_AddNum] @TableName='PE_StatMonth',@CompareField='TMonth',@AddField = @day,@Data = 'Total'
	Exec [PR_Analytics_AddNum] @TableName='PE_StatWeek',@CompareField='TWeek',@AddField = @week,@Data = 'Total'
	IF DateDiff(week,@OldDay,getdate()) > 0 
		Delete From PE_StatWeek Where TWeek = 'Current'
	Exec [PR_Analytics_AddNum] @TableName='PE_StatWeek',@CompareField='TWeek',@AddField = @week,@Data = 'Current'

END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[SplitID]'
GO
SET ANSI_NULLS OFF
GO
ALTER  FUNCTION  [dbo].[SplitID] 
(  
     @String  nvarchar(4000),  --格式：“1，2，3，4，”
     @SplitChar  nvarchar(10)  --格式：“，”
)  
RETURNS    @table  Table(ID  varchar(100))  
AS  
BEGIN  
   DECLARE  @Index  INT  
   SET  @Index  =  0  

		IF @String <> ''
		Begin
			IF Right(@String,1)<> @SplitChar 
				set @String = @String + @SplitChar
		End
 
       WHILE  CHARINDEX(@SplitChar,@String,@Index)  >  0    
       BEGIN  
           INSERT  INTO  @table(ID)  
           VALUES  
           (  
               SUBSTRING(@String,@Index,CHARINDEX(@SplitChar,@String,@Index)-@Index)  
           )  
               SET  @index  =  CHARINDEX(@SplitChar,@String,@Index)+1        
   END  
RETURN  
END
GO
SET ANSI_NULLS ON
GO
PRINT N'正在改变 [dbo].[PE_Question]'
GO
ALTER TABLE [dbo].[PE_Question] ADD
[LastUpdateTime] [datetime] NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PE_SpecialCategory]'
GO
ALTER TABLE [dbo].[PE_SpecialCategory] ADD
[NeedCreateHtml] [bit] NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PE_Specials]'
GO
ALTER TABLE [dbo].[PE_Specials] ADD
[NeedCreateHtml] [bit] NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在创建 [dbo].[PE_Courier]'
GO
CREATE TABLE [dbo].[PE_Courier]
(
[CourierId] [int] NOT NULL IDENTITY(1, 1),
[ShortName] [nvarchar] (64) COLLATE Chinese_PRC_CI_AS NOT NULL,
[FullName] [nvarchar] (256) COLLATE Chinese_PRC_CI_AS NULL,
[Address] [nvarchar] (512) COLLATE Chinese_PRC_CI_AS NULL,
[Telephone] [nvarchar] (64) COLLATE Chinese_PRC_CI_AS NULL,
[Contacter] [nvarchar] (64) COLLATE Chinese_PRC_CI_AS NULL,
[SearchUrl] [nvarchar] (256) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_BankroolItem_GetBill]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_BankroolItem_GetBill]
(
@StartRows int,
@PageSize int,
@SortColumn varchar(1000),
@StrColumn varchar(1000),
@Sorts varchar(100),
@Filter varchar(1000),
@TableName varchar(1000),
@Total int OUTPUT
)
AS

SET NOCOUNT ON

Begin
IF @StartRows<=0
	SET @StartRows = 0

DECLARE @equalOperator char(2)
IF @StartRows=0
	BEGIN
	  SET @equalOperator = '='
	  SET @StartRows = 1
	END
ELSE
  SET @equalOperator = ''

/*Set sorting variables.*/
DECLARE @operator char(2)
DECLARE @strSortColumn varchar(10)
	
IF CHARINDEX('DESC',@Sorts)>0
	BEGIN
		SET @strSortColumn = REPLACE(@Sorts, 'DESC', '')
		SET @operator = '<' + @equalOperator
	END
ELSE
	BEGIN
		IF CHARINDEX('ASC', @Sorts) = 0
			SET @strSortColumn = REPLACE(@Sorts, 'ASC', '')
		SET @operator = '>' + @equalOperator
	END

DECLARE @strFilter varchar(1000)
DECLARE @strSimpleFilter varchar(1000)

IF @Filter IS NOT NULL AND @Filter!=''
	BEGIN
		SET @strFilter = ' WHERE ' + @Filter + ' '
		SET @strSimpleFilter = ' AND ' + @Filter + ' '
	END
ELSE
	BEGIN	
		SET @strFilter = ''
		SET @strSimpleFilter = ''
	END

DECLARE @Sql nVarchar(4000)
SET @Sql=N'SELECT @Total=Count(Distinct B.OrderID) FROM ' + @TableName + @strFilter
Exec sp_executesql @Sql, N'@Total Int Out',@Total Out 

IF @PageSize<=0
SET @PageSize=@Total

EXEC(
'DECLARE @SortId int ' +
'DECLARE @SortCol DateTime '+
'SET ROWCOUNT '+ @StartRows + 
' SELECT @SortId = '+ @SortColumn + ',@SortCol=Max(B.DateAndTime) FROM '+ @TableName + @strFilter +' Group By B.OrderID Order by Max(B.DateAndTime) ' +@Sorts+ ', B.OrderID ' +@Sorts + 
' SET ROWCOUNT '+ @PageSize +
' SELECT '+ @StrColumn +' FROM '+ @TableName +  
' WHERE (B.DateAndTime '+@operator+' @SortCol OR (B.DateAndTime = @SortCol AND B.OrderID '+@operator+' @SortId )) '+ @strSimpleFilter + ' Group By B.OrderID ORDER BY Max(B.DateAndTime) ' +@Sorts+ ',B.OrderID '+ @Sorts + ''
)


Return @Total
END

SET ROWCOUNT 0

SET NOCOUNT OFF
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_InsideLink_Delete]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_InsideLink_Delete]
(
	@InsideLinkID nvarchar(4000)
)
 AS 
		DECLARE @sql nvarchar(4000)
		set @sql = 'DELETE PE_InsideLink WHERE InsideLinkID IN (' + @InsideLinkID + ')'
		exec(@sql)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_Keywords_Delete]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_Keywords_Delete]
(
	@KeywordId nvarchar(4000)
)
 AS 
		DECLARE @sql nvarchar(4000)
		set @sql = 'DELETE PE_Keywords WHERE KeywordId IN (' + @KeywordId + ')'
		exec(@sql)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_Message_Delete]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_Message_Delete]
	(
	@DeleteType int,
	@DeleteValue nvarchar(100)
	)
AS
	SET NOCOUNT OFF
	BEGIN
		DECLARE @Sql nVarchar(4000)
		
		IF @DeleteType=0
			BEGIN
				SET @Sql=N'DELETE PE_Message WHERE MessageID in (' + @DeleteValue + ')'
			END
		ELSE IF @DeleteType=1
			BEGIN
				SET @Sql=N'DELETE PE_Message WHERE Sender in (' + @DeleteValue + ')'
			END
		ELSE
			BEGIN
				IF @DeleteValue=0
					SET @Sql=N'DELETE PE_Message'
				ELSE
					SET @Sql=N'DELETE PE_Message WHERE  datediff(day,SendTime,getdate()) >=' + @DeleteValue
			END

		Exec sp_executesql @Sql
	END
	RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_PaymentLog_Delete]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_PaymentLog_Delete]
(
	@paymentLogId NVARCHAR (4000)
)
 AS 
	DEClARE @sql NVARCHAR (4000)
	SET @sql = 'DELETE [PE_PaymentLog] WHERE [Status]=1 AND [PaymentLogID] IN(' +@paymentLogId +')'
	EXEC(@sql)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_WordReplace_Delete]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_WordReplace_Delete]
(
	@ItemID nvarchar(4000)
)
 AS 
		DECLARE @sql nvarchar(4000)
		set @sql = 'DELETE PE_WordReplaceItem WHERE ItemID IN (' + @ItemID + ')'
		exec(@sql)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Accessories_WordReplace_IsEnabled]'
GO
ALTER PROCEDURE [dbo].[PR_Accessories_WordReplace_IsEnabled]
    (
    @ItemID nvarchar(4000),
	@IsEnabled nvarchar(10) 
	)	
AS
	
	DECLARE @sql nvarchar(4000)
	set @sql = 'UPDATE PE_WordReplaceItem SET IsEnabled = ' + @IsEnabled + ' WHERE ItemID IN (' + @ItemID + ')'
	exec(@sql)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_AD_ADZone_GetExportList]'
GO
ALTER PROCEDURE [dbo].[PR_AD_ADZone_GetExportList]
(
	@ZoneID NVarChar(250)
)

AS

  --获取要导出的广告版位列表    
    EXEC(' SELECT ZoneID,ZoneName,ZoneJSName,ZoneIntro,ZoneType,DefaultSetting,ZoneSetting,ZoneWidth,ZoneHeight,ShowType,Active,UpdateTime FROM PE_AdZone WHERE ZoneID in ('+@ZoneID+')  ORDER BY ZoneID ASC')
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Common_GetList]'
GO
ALTER PROCEDURE [dbo].[PR_Common_GetList]
(
@StartRows int,
@PageSize int,
@SortColumn varchar(1000),
@StrColumn varchar(1000),
@Sorts varchar(100),
@Filter varchar(4000),
@TableName varchar(1000),
@Total int OUTPUT
)
AS

SET NOCOUNT ON

Begin
IF @StartRows<=0
	SET @StartRows = 0

DECLARE @equalOperator char(2)
IF @StartRows=0
	BEGIN
	  SET @equalOperator = '='
	  SET @StartRows = 1
	END
ELSE
  SET @equalOperator = ''

/*Set sorting variables.*/
DECLARE @operator char(2)
DECLARE @strSortColumn varchar(10)
	
IF CHARINDEX('DESC',@Sorts)>0
	BEGIN
		SET @strSortColumn = REPLACE(@Sorts, 'DESC', '')
		SET @operator = '<' + @equalOperator
	END
ELSE
	BEGIN
		IF CHARINDEX('ASC', @Sorts) = 0
			SET @strSortColumn = REPLACE(@Sorts, 'ASC', '')
		SET @operator = '>' + @equalOperator
	END

DECLARE @strFilter varchar(4000)
DECLARE @strSimpleFilter varchar(4000)

IF @Filter IS NOT NULL AND @Filter!=''
	BEGIN
		SET @strFilter = ' WHERE ' + @Filter + ' '
		SET @strSimpleFilter = ' AND ' + @Filter + ' '
	END
ELSE
	BEGIN	
		SET @strFilter = ''
		SET @strSimpleFilter = ''
	END

DECLARE @Sql nVarchar(4000)
SET @Sql=N'SELECT @Total=Count(*) FROM ' + @TableName + @strFilter
Exec sp_executesql @Sql, N'@Total Int Out',@Total Out 

IF @PageSize<=0
SET @PageSize=@Total

EXEC(
'DECLARE @SortId int ' +
'SET ROWCOUNT '+ @StartRows + '
 SELECT @SortId = '+ @SortColumn + ' FROM '+ @TableName + @strFilter +' ORDER BY '+ @SortColumn + ' ' +@Sorts +'
 SET ROWCOUNT '+ @PageSize  + '
 SELECT '+ @StrColumn +' FROM '+ @TableName + ' 
 WHERE '+ @SortColumn  + @operator +' @SortId '+ @strSimpleFilter + ' ORDER BY '+ @SortColumn + ' '+ @Sorts + ''
)


Return @Total
END

SET ROWCOUNT 0

SET NOCOUNT OFF
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Common_GetList2]'
GO
ALTER PROCEDURE [dbo].[PR_Common_GetList2]
(
@StartRows int,
@PageSize int,
@SortColumnDbType  varchar(100), --排序字段的数值类型 如 Int ,DateTime等
@SortColumn varchar(1000),
@StrColumn varchar(1000),
@Sorts varchar(100),
@Filter varchar(4000),
@TableName varchar(1000),
@Total int OUTPUT
)
AS

SET NOCOUNT ON

Begin
IF @StartRows<=0 
	SET @StartRows = 0
SET @StartRows = @StartRows +1
IF @PageSize < 1
	SET @PageSize = 10

/*Set sorting variables.*/
DECLARE @operator char(2)
DECLARE @strSortColumn varchar(10)
	
IF CHARINDEX('DESC',@Sorts)>0
	BEGIN
		SET @strSortColumn = REPLACE(@Sorts, 'DESC', '')
		SET @operator = '<='
	END
ELSE
	BEGIN
		IF CHARINDEX('ASC', @Sorts) = 0
			SET @strSortColumn = REPLACE(@Sorts, 'ASC', '')
		SET @operator = '>='
	END

DECLARE @strFilter varchar(4000)
DECLARE @strSimpleFilter varchar(4000)

IF @Filter IS NOT NULL AND @Filter!=''
	BEGIN
		SET @strFilter = ' WHERE ' + @Filter + ' '
		SET @strSimpleFilter = ' AND ' + @Filter + ' '
	END
ELSE
	BEGIN	
		SET @strFilter = ''
		SET @strSimpleFilter = ''
	END


Exec(
'DECLARE @SortId '+ @SortColumnDbType +
' SET ROWCOUNT '+ @StartRows + '
 SELECT @SortId = '+ @SortColumn + ' FROM '+ @TableName + @strFilter +' ORDER BY '+ @SortColumn + ' ' +@Sorts +'
 SET ROWCOUNT '+ @PageSize  + '
 SELECT '+ @StrColumn +' FROM '+ @TableName + ' 
 WHERE '+ @SortColumn  + @operator +' @SortId '+ @strSimpleFilter + ' ORDER BY '+ @SortColumn +' '+ @Sorts + ''
)

DECLARE @Sql nVarchar(4000)

SET @Sql=N'SELECT @Total=Count(*) FROM ' + @TableName
Exec sp_executesql @Sql, N'@Total Int Out',@Total Out 
RETURN @Total
END

SET ROWCOUNT 0

SET NOCOUNT OFF
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Common_GetListBySortColumn]'
GO
ALTER PROCEDURE [dbo].[PR_Common_GetListBySortColumn]
(
@StartRows int,
@PageSize int,
@PrimaryColumn varchar(1000),
@SortColumnDbType  varchar(100), --排序字段的数值类型 如 Int ,DateTime等
@SortColumn varchar(1000),
@StrColumn varchar(1000),
@Sorts varchar(100),
@Filter varchar(4000),
@TableName varchar(1000),
@Total int OUTPUT
)
AS

SET NOCOUNT ON

Begin
IF @StartRows<=0
	SET @StartRows = 0

DECLARE @equalOperator char(2)
IF @StartRows=0
	BEGIN
	  SET @equalOperator = '='
	  SET @StartRows = 1
	END
ELSE
  SET @equalOperator = ''

/*Set sorting variables.*/
DECLARE @operator char(2)
	
IF CHARINDEX('DESC',@Sorts)>0
	BEGIN
		SET @operator = '<' + @equalOperator
	END
ELSE
	BEGIN
		SET @operator = '>' + @equalOperator
	END
DECLARE @strFilter varchar(4000)
DECLARE @strSimpleFilter varchar(4000)

IF @Filter IS NOT NULL AND @Filter!=''
	BEGIN
		SET @strFilter = ' WHERE ' + @Filter + ' '
		SET @strSimpleFilter = ' AND ' + @Filter + ' '
	END
ELSE
	BEGIN	
		SET @strFilter = ''
		SET @strSimpleFilter = ''
	END

DECLARE @Sql nVarchar(4000)
SET @Sql=N'SELECT @Total=Count(*) FROM ' + @TableName + @strFilter
Exec sp_executesql @Sql, N'@Total Int Out',@Total Out 

IF @PageSize<=0
SET @PageSize=@Total

IF @PrimaryColumn!=@SortColumn
BEGIN
EXEC(
'DECLARE @SortId int ' +
'DECLARE @SortCol '+ @SortColumnDbType + ' '+
'SET ROWCOUNT '+ @StartRows + '
 SELECT @SortId = '+ @PrimaryColumn + ',@SortCol='+ @SortColumn +' FROM '+ @TableName + @strFilter +' ORDER BY '+ @SortColumn +' ' +@Sorts + ',' +  @PrimaryColumn + ' ' +@Sorts +'
 SET ROWCOUNT '+ @PageSize  + '
 SELECT '+ @StrColumn +' FROM '+ @TableName + ' 
 WHERE ('+ @SortColumn  + @operator+' @SortCol OR ('+ @SortColumn  + '= @SortCol AND '+ @PrimaryColumn  + @operator +' @SortId ))' + @strSimpleFilter + ' ORDER BY '+ @SortColumn + ' ' +@Sorts + ',' + @PrimaryColumn + ' '+ @Sorts + ''
)
END
ELSE
BEGIN
EXEC(
'DECLARE @SortId int ' +
'SET ROWCOUNT '+ @StartRows + '
 SELECT @SortId = '+ @SortColumn + ' FROM '+ @TableName + @strFilter +' ORDER BY '+ @SortColumn + ' ' +@Sorts +'
 SET ROWCOUNT '+ @PageSize  + '
 SELECT '+ @StrColumn +' FROM '+ @TableName + ' 
 WHERE '+ @SortColumn  + @operator +' @SortId '+ @strSimpleFilter + ' ORDER BY '+ @SortColumn +' '+ @Sorts + ''
)
END


Return @Total
END

SET ROWCOUNT 0

SET NOCOUNT OFF
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Contents_ModelTemplates_GetExportList]'
GO
ALTER PROCEDURE [dbo].[PR_Contents_ModelTemplates_GetExportList]
(
	@TemplateID NVarChar(250)
)
AS
 
    EXEC(' SELECT * FROM PE_ModelTemplates WHERE TemplateID in ('+@TemplateID+')  ORDER BY TemplateID ASC')
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_Producer_Delete]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_Producer_Delete]
(
	@ProducerId nvarchar(4000)
)
 AS 
		DECLARE @sql nvarchar(4000)
		set @sql = 'DELETE PE_Producer WHERE ProducerID IN (' + @ProducerId + ')'
		exec(@sql)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_Product_Update]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_Product_Update] 
	(
	@Unit nvarchar(20),
	@Stocks int,
	@Weight float,
	@SalePromotionType int,
	@MinNumber int,
	@PresentNumber int,
	@PresentId int,
	@PresentExp int,
	@PresentMoney decimal,
	@PresentPoint int,
	@EliteLevel int,
	@UpdateTime Datetime,
	@TemplateFile nvarchar(255)='',
	@TaxRate float,
	@GeneralIdList nvarchar(1000),
	@NodeIdList nvarchar(1000),
	@ModelIdList nvarchar(1000)
	)
AS
BEGIN
	DECLARE @sql nvarchar(1000)
	DECLARE @sqlChild nvarchar(1000)
	DECLARE @FieldStr nvarchar(20)
	
	SET @sql='Select P.ID From PE_CommonModel M inner join PE_CommonProduct P on M.ItemID=P.ProductID and M.TableName=P.TableName'
	IF @GeneralIdList != ''
		BEGIN
			SET @sql = @sql + ' where M.GeneralID IN(' + @GeneralIdList + ')'
			SET @sqlChild =' (' + @GeneralIdList + ')'
			SET @FieldStr = 'GeneralID'
		END
	ELSE
		BEGIN
			SET @FieldStr = 'NodeID'
			SET @sql = @sql + ' Where M.NodeID IN(' + @NodeIdList + ')'
			SET @sqlChild =' (' + @NodeIdList + ')'
			IF @ModelIdList != ''
				BEGIN
					SET @sql= @sql + ' And ModelID IN(' + @ModelIdList + ')'
					SET @sqlChild = @sqlChild + ' And ModelID IN(' + @ModelIdList + ')'
				END
			ELSE
				BEGIN
					SET @sql= @sql + ' And ModelID IN(Select ModelID From PE_ContentModel Where IsEShop = 1)'
					SET @sqlChild = @sqlChild + ' And ModelID IN(Select ModelID From PE_ContentModel Where IsEShop = 1)'
				END				
		END
		--declare @t table(id int)
		--insert into @t exec(@sql)
		DECLARE @UpdateSql nvarchar(1000)
		SET @UpdateSql = 'Update PE_CommonProduct Set ID = ID'
		
		IF @Unit!=''
			SET @UpdateSql =@UpdateSql+',Unit=''' + @Unit + ''''
		IF @Stocks!=0
			SET @UpdateSql =@UpdateSql+',Stocks=' + Convert(char(10), @Stocks)
		IF @Weight!=0
			SET @UpdateSql =@UpdateSql+',Weight=' + Convert(char(10), @Weight)
		IF @SalePromotionType!=0
			SET @UpdateSql =@UpdateSql+',SalePromotionType=' + Convert(char(10), @SalePromotionType)
		IF @MinNumber!=0
			SET @UpdateSql =@UpdateSql+',MinNumber=' + Convert(char(10), @MinNumber)
		IF @PresentId!=0
			SET @UpdateSql =@UpdateSql+',PresentId=' + Convert(char(10), @PresentId)
		IF @PresentExp!=0
			SET @UpdateSql =@UpdateSql+',PresentExp=' + Convert(char(10), @PresentExp)
		IF @PresentMoney!=0
			SET @UpdateSql =@UpdateSql+',PresentMoney=' + Convert(char(10), @PresentMoney)
		IF @PresentPoint!=0
			SET @UpdateSql =@UpdateSql+',PresentPoint=' + Convert(char(10), @PresentPoint)
		IF @TaxRate!=0
			SET @UpdateSql =@UpdateSql+',TaxRate=' + Convert(char(10), @TaxRate)

		SET @UpdateSql =@UpdateSql+'Where ID IN (' + @sql  + ')'
		
		EXEC(@UpdateSql)
		
		DECLARE @UpdateSql1 nvarchar(1000)
		SET @UpdateSql1 = 'Update PE_CommonModel Set GeneralID = GeneralID'
		IF @EliteLevel!=0
			SET @UpdateSql1 =@UpdateSql1+',EliteLevel=' + Convert(char(10), @EliteLevel)
		IF @UpdateTime!=''
			SET @UpdateSql1 =@UpdateSql1+',UpdateTime=''' + Convert(char(10), @UpdateTime) +''''
		IF @TemplateFile!=''
			SET @UpdateSql1 =@UpdateSql1+',TemplateFile=''' + @TemplateFile +''''
			
		EXEC(@UpdateSql1+' WHERE	'+@FieldStr+' IN'+@sqlChild)
END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_SaleCount_GetList]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_SaleCount_GetList]
(
@StartRows int,
@PageSize int,
@InfoType varchar(100),
@SortColumn varchar(1000),
@StrColumn varchar(1000),
@Sorts varchar(100),
@Filter varchar(1000),
@TableName varchar(1000),
@Total int OUTPUT
)
AS

SET NOCOUNT ON

Begin
IF @StartRows<=0 
	SET @StartRows = 0
SET @StartRows = @StartRows +1
IF @PageSize < 1
	SET @PageSize = 10

/*Set sorting variables.*/
DECLARE @operator char(2)
DECLARE @strSortColumn varchar(10)
	
IF CHARINDEX('DESC',@Sorts)>0
	BEGIN
		SET @strSortColumn = REPLACE(@Sorts, 'DESC', '')
		SET @operator = '<='
	END
ELSE
	BEGIN
		IF CHARINDEX('ASC', @Sorts) = 0
			SET @strSortColumn = REPLACE(@Sorts, 'ASC', '')
		SET @operator = '>='
	END

DECLARE @strFilter varchar(1000)
DECLARE @strSimpleFilter varchar(1000)

IF @InfoType= 'NoDeliver'
	SET @Filter = ' I.OrderID in (select OrderID from PE_Orders where (MoneyReceipt>=MoneyTotal or MoneyReceipt>0) And DeliverStatus<=1) ' + @Filter
ELSE
	SET @Filter = ' I.OrderID in (select OrderID from PE_Orders where MoneyReceipt>=MoneyTotal) ' + @Filter

IF @Filter IS NOT NULL AND @Filter!=''
	BEGIN
		SET @strFilter = ' WHERE ' + @Filter + ' '
		SET @strSimpleFilter = ' AND ' + @Filter + ' '
	END
ELSE
	BEGIN	
		SET @strFilter = ''
		SET @strSimpleFilter = ''
	END


EXEC(
'DECLARE @SortId int ' +
'SET ROWCOUNT '+ @StartRows + '
 SELECT @SortId = '+ @SortColumn + ' FROM '+ @TableName + @strFilter +' GROUP BY I.ProductID,I.TableName ORDER BY '+ @SortColumn + ' ' +@Sorts +'
 SET ROWCOUNT '+ @PageSize  + '
 SELECT '+ @StrColumn +' FROM '+ @TableName + @strFilter + ' 
  GROUP BY I.ProductID,I.TableName Having ' + @SortColumn  + @operator +' @SortId  ORDER BY '+ @SortColumn +' '+ @Sorts + ''
)


DECLARE @Sql nVarchar(4000)

SET @Sql=N'SELECT @Total=Count(DISTINCT I.ProductID,I.TableName) FROM ' + @TableName + @strFilter
Exec sp_executesql @Sql, N'@Total Int Out',@Total Out 
Return @Total
END

SET ROWCOUNT 0

SET NOCOUNT OFF
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_ShoppingCart_GetList]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_ShoppingCart_GetList] 
(
@StartRows int,
@PageSize int,
@SortColumnDbType  varchar(100), --排序字段的数值类型 如 Int ,DateTime等
@SortColumn varchar(1000),
@StrColumn varchar(1000),
@Sorts varchar(100),
@Filter varchar(1000),
@TableName varchar(1000),
@Total int OUTPUT
)
AS

SET NOCOUNT ON

Begin
IF @StartRows<=0 
	SET @StartRows = 0

IF @PageSize < 1
	SET @PageSize = 10

DECLARE @equalOperator char(2)
IF @StartRows=0
	BEGIN
	  SET @equalOperator = '='
	  SET @StartRows = 1
	END
ELSE
  SET @equalOperator = ''

/*Set sorting variables.*/
DECLARE @operator char(2)
DECLARE @strSortColumn varchar(10)
	
IF CHARINDEX('DESC',@Sorts)>0
	BEGIN
		SET @strSortColumn = REPLACE(@Sorts, 'DESC', '')
		SET @operator = '<' + @equalOperator
	END
ELSE
	BEGIN
		IF CHARINDEX('ASC', @Sorts) = 0
			SET @strSortColumn = REPLACE(@Sorts, 'ASC', '')
		SET @operator = '>' + @equalOperator
	END

DECLARE @strFilter varchar(1000)
DECLARE @strSimpleFilter varchar(1000)

IF @Filter IS NOT NULL AND @Filter!=''
	BEGIN
		SET @strFilter = ' WHERE ' + @Filter + ' '
		SET @strSimpleFilter = ' AND ' + @Filter + ' '
	END
ELSE
	BEGIN	
		SET @strFilter = ''
		SET @strSimpleFilter = ''
	END

DECLARE @Sql nVarchar(4000)
SET @Sql=N'SELECT @Total=Count(DISTINCT CartID) FROM ' + @TableName + @strFilter
Exec sp_executesql @Sql, N'@Total Int Out',@Total Out 


--DECLARE @Str nVARCHAR(4000)
--IF @StartRows = 0 
--	BEGIN
--		SET @Str='SELECT TOP '+CAST(@PageSize AS VARCHAR(20))+' ' + @StrColumn + ' FROM 
--		' + @TableName + @strFilter + ' GROUP BY CartID ORDER BY min(CartItemID) ' + @Sorts
--	END
--ELSE
--	BEGIN
--		SET @Str='SELECT TOP '+CAST(@PageSize AS VARCHAR(20))+' ' + @StrColumn + ' FROM 
--		'+@TableName+'  WHERE CartID NOT IN (SELECT TOP '+CAST((@StartRows) 
--		AS VARCHAR(20))+' CartID FROM '+ @TableName+ @strFilter  + ' GROUP BY CartID ORDER BY min(CartItemID) ' + @Sorts+') '+ @strSimpleFilter + ' GROUP BY CartID ORDER BY min(CartItemID) ' + @Sorts
--	END
--EXEC sp_ExecuteSql @Str

Exec(
'DECLARE @SortId '+ @SortColumnDbType +
'DECLARE @StartId int '+
'SET ROWCOUNT '+ @StartRows + '
 SELECT @SortId = '+ @SortColumn + ' FROM '+ @TableName + @strFilter +' GROUP BY CartID ORDER BY MIN(CartItemID) ' +@Sorts +'
 SELECT @StartId = CartItemID FROM '+ @TableName + ' WHERE CartID = @SortId
 SET ROWCOUNT '+ @PageSize  + '
 SELECT '+ @StrColumn +' FROM '+ @TableName + ' 
 WHERE CartItemID '+ @operator +' @StartId '+ @strSimpleFilter + ' GROUP BY CartID ORDER BY MIN(CartItemID) '+ @Sorts + ''
)

RETURN @Total
END

--SET ROWCOUNT 0

--SET NOCOUNT OFF
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_Statistics_GetList]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_Statistics_GetList] 
(
@StartRows int,
@PageSize int,
@SortColumn varchar(1000),
@StrColumn varchar(1000),
@Sorts varchar(100),
@Filter varchar(1000),
@TableName varchar(1000),
@ID VARCHAR(255), --需要排序的不重复的ID号
@Group VARCHAR(255),
@Total int OUTPUT
)
AS

If @PageSize = 0
	BEGIN
		SET @PageSize = 20
    END




DECLARE @strFilter varchar(1000)
DECLARE @strSimpleFilter varchar(1000)

IF @Filter IS NOT NULL AND @Filter!=''
	BEGIN
		SET @strFilter = ' WHERE ' + @Filter + ' '
		SET @strSimpleFilter = ' AND ' + @Filter + ' '
	END
ELSE
	BEGIN	
		SET @strFilter = ''
		SET @strSimpleFilter = ''
	END


DECLARE @Sql nVarchar(4000)

SET @Sql=N'SELECT @Total=(Count(DISTINCT ' +@ID+')) FROM ' + @TableName + @strFilter 
Exec sp_executesql @Sql, N'@Total Int Out',@Total Out 

DECLARE @Str nVARCHAR(4000)
IF @StartRows = 0 
	BEGIN
		SET @Str='SELECT TOP '+CAST(@PageSize AS VARCHAR(20))+' ' + @StrColumn + ' FROM 
		'+@TableName+ @strFilter + ' ' + @Group +' ORDER BY '+@SortColumn + ' ' + @Sorts
	END
ELSE
	BEGIN
		SET @Str='SELECT TOP '+CAST(@PageSize AS VARCHAR(20))+' ' + @StrColumn + ' FROM 
		'+@TableName+'  WHERE '+@ID+' NOT IN (SELECT TOP '+CAST((@StartRows) 
		AS VARCHAR(20))+' '+@ID+' FROM '+ @TableName+ @strFilter  + ' ' + @Group + ' ORDER BY '+@SortColumn + ' ' + @Sorts+') '+ @strSimpleFilter + ' ' + @Group +' ORDER BY '+@SortColumn + ' ' + @Sorts
	END
EXEC sp_ExecuteSql @Str

Return @Total
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_Shop_Trademark_Delete]'
GO
ALTER PROCEDURE [dbo].[PR_Shop_Trademark_Delete]
(
	@TrademarkId nvarchar(4000)
)
 AS 
		DECLARE @sql nvarchar(4000)
		set @sql = 'DELETE PE_Trademark WHERE TrademarkID IN (' + @TrademarkId + ')'
		exec(@sql)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_UserManage_Users_BatchMove]'
GO
ALTER PROCEDURE [dbo].[PR_UserManage_Users_BatchMove]
(	
	@UserType int = 1,
	@GroupId NVarChar(500) ='',
	@UserId NVarChar(4000) = '',
	@UserName NVarChar(255) = '',
	@StartUserId int = 0,
	@EndUserId int = 0,
	@BatchUserGroupId NVarChar(500) =''
)
AS
BEGIN
	SET NOCOUNT OFF
	If (@UserType = 1)
        BEGIN
			EXEC('Update PE_Users set GroupID= ' + @GroupId +' Where UserID  in (' + @UserId + ')')
        END
    Else If(@UserType = 2)
        BEGIN
			EXEC('Update PE_Users set GroupID= ' + @GroupId +' Where UserName  in (''' + @UserName + ''')')	
        END
	Else If(@UserType = 3)
        BEGIN
			EXEC('Update PE_Users set GroupID= ' + @GroupId +' Where UserId between ' + @StartUserId + ' and ' + @EndUserId)	
        END
    Else If(@UserType = 4)
        BEGIN
			EXEC('Update PE_Users set GroupID= ' + @GroupId +' Where GroupID  in (' + @BatchUserGroupId + ')')	
        END
END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PR_UserManage_Users_Delete]'
GO
ALTER PROCEDURE [dbo].[PR_UserManage_Users_Delete]
(
	@UserId nvarchar(4000)
)
 AS 
		DECLARE @sql nvarchar(4000)
		set @sql = 'DELETE PE_Users WHERE UserID IN (' + @UserId + ')'
		exec(@sql)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在创建 [dbo].[PR_RLTD_KIWD_LNKR_LEWI]'
GO
CREATE PROCEDURE [dbo].[PR_RLTD_KIWD_LNKR_LEWI]
@ShowNum VARCHAR(50),
@TableName VARCHAR(50),
@Condition VARCHAR(50),
@Sorting VARCHAR(50),
@ContentID VARCHAR(50)
AS
BEGIN
EXEC
('
    DECLARE @GetKeyword NVARCHAR(50)
    DECLARE @String NVARCHAR(4000)
    SELECT @String=Keyword FROM '+@TableName+' WHERE id='''+@ContentID+''' AND Keyword != ''||''
    SELECT TOP 1 @GetKeyword=COL FROM dbo.LEWI_SPLT_STR(@String,''|'')
    SET @String = NULL
    SELECT @String = cast(arrGeneralID AS VARCHAR(4000)) FROM PE_Keywords WHERE Keywordtext=@GetKeyword
    SELECT TOP '+@ShowNum+'  C.Title,C.GeneralID,C.UpdateTime,C.Hits FROM dbo.PE_CommonModel C INNER JOIN dbo.LEWI_SPLT_STR(@String,'','') D ON C.GeneralID =D.COL WHERE C.GeneralID!='''+@ContentID+''' '+ @Condition +'ORDER BY '+ @Sorting 
)
END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变扩展属性'
GO
EXEC sp_updateextendedproperty N'MS_Description', N'送货方式名称', 'USER', N'dbo', 'TABLE', N'PE_DeliverType', 'COLUMN', N'TypeName'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_updateextendedproperty N'MS_Description', N'投票的标题', 'USER', N'dbo', 'TABLE', N'PE_Vote', 'COLUMN', N'VoteTitle'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_updateextendedproperty N'MS_Description', N'是否启用投票', 'USER', N'dbo', 'TABLE', N'PE_Vote', 'COLUMN', N'IsAlive'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在创建扩展属性'
GO
EXEC sp_addextendedproperty N'MS_Description', N'排除ID', 'USER', N'dbo', 'TABLE', N'PE_CollectionExclosion', 'COLUMN', N'ExclosionID'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'排除名称', 'USER', N'dbo', 'TABLE', N'PE_CollectionExclosion', 'COLUMN', N'ExclosionName'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'排除类型', 'USER', N'dbo', 'TABLE', N'PE_CollectionExclosion', 'COLUMN', N'ExclosionType'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'排除字符类别', 'USER', N'dbo', 'TABLE', N'PE_CollectionExclosion', 'COLUMN', N'ExclosionStringType'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'排除字符', 'USER', N'dbo', 'TABLE', N'PE_CollectionExclosion', 'COLUMN', N'ExclosionString'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'是否排除指定数字', 'USER', N'dbo', 'TABLE', N'PE_CollectionExclosion', 'COLUMN', N'IsExclosionDesignatedNumber'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'排除指定数字', 'USER', N'dbo', 'TABLE', N'PE_CollectionExclosion', 'COLUMN', N'ExclosionDesignatedNumber'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'是否排除大于数字', 'USER', N'dbo', 'TABLE', N'PE_CollectionExclosion', 'COLUMN', N'IsExclosionMaxNumber'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'排除大于数字', 'USER', N'dbo', 'TABLE', N'PE_CollectionExclosion', 'COLUMN', N'ExclosionMaxNumber'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'是否排除小于数字', 'USER', N'dbo', 'TABLE', N'PE_CollectionExclosion', 'COLUMN', N'IsExclosionMinNumber'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'排除小于数字', 'USER', N'dbo', 'TABLE', N'PE_CollectionExclosion', 'COLUMN', N'ExclosionMinNumber'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'是否排除指定时间', 'USER', N'dbo', 'TABLE', N'PE_CollectionExclosion', 'COLUMN', N'IsExclosionDesignatedDateTime'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'排除指定时间', 'USER', N'dbo', 'TABLE', N'PE_CollectionExclosion', 'COLUMN', N'ExclosionDesignatedDateTime'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'是否排除大于时间', 'USER', N'dbo', 'TABLE', N'PE_CollectionExclosion', 'COLUMN', N'IsExclosionMaxDateTime'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'排除大于时间', 'USER', N'dbo', 'TABLE', N'PE_CollectionExclosion', 'COLUMN', N'ExclosionMaxDateTime'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'是否排除小于时间', 'USER', N'dbo', 'TABLE', N'PE_CollectionExclosion', 'COLUMN', N'IsExclosionMinDateTime'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'排除小于时间', 'USER', N'dbo', 'TABLE', N'PE_CollectionExclosion', 'COLUMN', N'ExclosionMinDateTime'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'字段类型', 'USER', N'dbo', 'TABLE', N'PE_CollectionFieldRules', 'COLUMN', N'FieldType'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'排除关联ID', 'USER', N'dbo', 'TABLE', N'PE_CollectionFieldRules', 'COLUMN', N'ExclosionID'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'采集历史记录ID', 'USER', N'dbo', 'TABLE', N'PE_CollectionHistory', 'COLUMN', N'HistoryID'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'项目ID', 'USER', N'dbo', 'TABLE', N'PE_CollectionHistory', 'COLUMN', N'ItemID'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'模型ID', 'USER', N'dbo', 'TABLE', N'PE_CollectionHistory', 'COLUMN', N'ModelID'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'栏目ID', 'USER', N'dbo', 'TABLE', N'PE_CollectionHistory', 'COLUMN', N'NodeID'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'文章ID', 'USER', N'dbo', 'TABLE', N'PE_CollectionHistory', 'COLUMN', N'GeneralID'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'信息标题', 'USER', N'dbo', 'TABLE', N'PE_CollectionHistory', 'COLUMN', N'Title'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'采集时间', 'USER', N'dbo', 'TABLE', N'PE_CollectionHistory', 'COLUMN', N'CollectionTime'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'采集状态（成功，失败）', 'USER', N'dbo', 'TABLE', N'PE_CollectionHistory', 'COLUMN', N'Result'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'采集URL', 'USER', N'dbo', 'TABLE', N'PE_CollectionHistory', 'COLUMN', N'NewsUrl'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'项目名称', 'USER', N'dbo', 'TABLE', N'PE_CollectionItem', 'COLUMN', N'ItemName'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'采集网站名称', 'USER', N'dbo', 'TABLE', N'PE_CollectionItem', 'COLUMN', N'UrlName'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'编码类型', 'USER', N'dbo', 'TABLE', N'PE_CollectionItem', 'COLUMN', N'CodeType'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'采集地址', 'USER', N'dbo', 'TABLE', N'PE_CollectionItem', 'COLUMN', N'Url'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'采集简介', 'USER', N'dbo', 'TABLE', N'PE_CollectionItem', 'COLUMN', N'Intro'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'栏目ID', 'USER', N'dbo', 'TABLE', N'PE_CollectionItem', 'COLUMN', N'NodeID'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'虚栏目ID', 'USER', N'dbo', 'TABLE', N'PE_CollectionItem', 'COLUMN', N'InfoNodeID'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'模型ID', 'USER', N'dbo', 'TABLE', N'PE_CollectionItem', 'COLUMN', N'ModelID'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'专题ID', 'USER', N'dbo', 'TABLE', N'PE_CollectionItem', 'COLUMN', N'SpecialID'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'采集顺序　0　正续　1　倒续', 'USER', N'dbo', 'TABLE', N'PE_CollectionItem', 'COLUMN', N'OrderType'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'指定采集数量　不指定为全部　', 'USER', N'dbo', 'TABLE', N'PE_CollectionItem', 'COLUMN', N'MaxNum'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'最后一次采集时间', 'USER', N'dbo', 'TABLE', N'PE_CollectionItem', 'COLUMN', N'NewsCollecDate'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'是否测试通过', 'USER', N'dbo', 'TABLE', N'PE_CollectionItem', 'COLUMN', N'Detection'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'是否自动生成HTML', 'USER', N'dbo', 'TABLE', N'PE_CollectionItem', 'COLUMN', N'AutoCreateHtml'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'采集分页ID', 'USER', N'dbo', 'TABLE', N'PE_CollectionPagingRules', 'COLUMN', N'PagingRuleID'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'采集类别ID 列表 内容', 'USER', N'dbo', 'TABLE', N'PE_CollectionPagingRules', 'COLUMN', N'CorrelationRuleId'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'评论标题', 'USER', N'dbo', 'TABLE', N'PE_Comment', 'COLUMN', N'CommentTitle'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'电子邮件', 'USER', N'dbo', 'TABLE', N'PE_Comment', 'COLUMN', N'Email'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'回复用户名称', 'USER', N'dbo', 'TABLE', N'PE_Comment', 'COLUMN', N'ReplyUserName'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N' 评论标题', 'USER', N'dbo', 'TABLE', N'PE_CommentPK', 'COLUMN', N'Title'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'审核通过时间', 'USER', N'dbo', 'TABLE', N'PE_CommonModel', 'COLUMN', N'PassedTime'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'编辑', 'USER', N'dbo', 'TABLE', N'PE_CommonModel', 'COLUMN', N'Editor'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'上次点击时间', 'USER', N'dbo', 'TABLE', N'PE_CommonModel', 'COLUMN', N'LastHitTime'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'默认首页图片', 'USER', N'dbo', 'TABLE', N'PE_CommonModel', 'COLUMN', N'DefaultPicUrl'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'上传文件', 'USER', N'dbo', 'TABLE', N'PE_CommonModel', 'COLUMN', N'UploadFiles'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'拼音标题', 'USER', N'dbo', 'TABLE', N'PE_CommonModel', 'COLUMN', N'PinyinTitle'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'最低购买数量', 'USER', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'Minimum'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'用户名', 'USER', N'dbo', 'TABLE', N'PE_Contacter', 'COLUMN', N'UserName'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'优惠券ID', 'USER', N'dbo', 'TABLE', N'PE_Coupon', 'COLUMN', N'CouponID'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'使用次数', 'USER', N'dbo', 'TABLE', N'PE_CouponItem', 'COLUMN', N'Usetimes'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'ID', 'USER', N'dbo', 'TABLE', N'PE_DeliverCharge', 'COLUMN', N'ID'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'送货方式ID', 'USER', N'dbo', 'TABLE', N'PE_DeliverType', 'COLUMN', N'TypeID'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'模型图标', 'USER', N'dbo', 'TABLE', N'PE_Model', 'COLUMN', N'ItemIcon'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'搜索页模板', 'USER', N'dbo', 'TABLE', N'PE_Model', 'COLUMN', N'SearchTemplate'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'高级搜索表单模板', 'USER', N'dbo', 'TABLE', N'PE_Model', 'COLUMN', N'AdvanceSearchFormTemplate'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'高级搜索结果模板', 'USER', N'dbo', 'TABLE', N'PE_Model', 'COLUMN', N'AdvanceSearchTemplate'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'生成静态页时模型收费提示', 'USER', N'dbo', 'TABLE', N'PE_Model', 'COLUMN', N'ChargeTips'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'评论页模板', 'USER', N'dbo', 'TABLE', N'PE_Model', 'COLUMN', N'CommentManageTemplate'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'节点下内容每页分页数', 'USER', N'dbo', 'TABLE', N'PE_Nodes', 'COLUMN', N'ItemPageSize'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'节点下是否有需要生成HTML的内容', 'USER', N'dbo', 'TABLE', N'PE_Nodes', 'COLUMN', N'NeedCreateHtml'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'ID', 'USER', N'dbo', 'TABLE', N'PE_OrderFeedback', 'COLUMN', N'ID'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'商品名称', 'USER', N'dbo', 'TABLE', N'PE_OrderItem', 'COLUMN', N'ProductName'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'单位', 'USER', N'dbo', 'TABLE', N'PE_OrderItem', 'COLUMN', N'Unit'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'重量', 'USER', N'dbo', 'TABLE', N'PE_OrderItem', 'COLUMN', N'Weight'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'跟单员', 'USER', N'dbo', 'TABLE', N'PE_Orders', 'COLUMN', N'Functionary'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'对配送时间的要求', 'USER', N'dbo', 'TABLE', N'PE_Orders', 'COLUMN', N'DeliveryTime'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'购买点券数', 'USER', N'dbo', 'TABLE', N'PE_PaymentLog', 'COLUMN', N'Point'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'礼品编号', 'USER', N'dbo', 'TABLE', N'PE_Present', 'COLUMN', N'PresentID'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'礼品名称', 'USER', N'dbo', 'TABLE', N'PE_Present', 'COLUMN', N'PresentName'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'礼品图片', 'USER', N'dbo', 'TABLE', N'PE_Present', 'COLUMN', N'PresentPic'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'礼品单位', 'USER', N'dbo', 'TABLE', N'PE_Present', 'COLUMN', N'Unit'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'礼品数量', 'USER', N'dbo', 'TABLE', N'PE_Present', 'COLUMN', N'PresentNum'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'单位', 'USER', N'dbo', 'TABLE', N'PE_Present', 'COLUMN', N'ServiceTermUnit'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'服务期限', 'USER', N'dbo', 'TABLE', N'PE_Present', 'COLUMN', N'ServiceTerm'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'价格', 'USER', N'dbo', 'TABLE', N'PE_Present', 'COLUMN', N'Price'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'市场价', 'USER', N'dbo', 'TABLE', N'PE_Present', 'COLUMN', N'Price_Market'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'重量', 'USER', N'dbo', 'TABLE', N'PE_Present', 'COLUMN', N'Weight'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'库存', 'USER', N'dbo', 'TABLE', N'PE_Present', 'COLUMN', N'Stocks'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'警报数量', 'USER', N'dbo', 'TABLE', N'PE_Present', 'COLUMN', N'AlarmNum'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'礼品类型', 'USER', N'dbo', 'TABLE', N'PE_Present', 'COLUMN', N'ProductCharacter'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'下载地址', 'USER', N'dbo', 'TABLE', N'PE_Present', 'COLUMN', N'DownloadUrl'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'下载说明', 'USER', N'dbo', 'TABLE', N'PE_Present', 'COLUMN', N'Remark'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'问题表', 'USER', N'dbo', 'TABLE', N'PE_Question', NULL, NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'问题ID', 'USER', N'dbo', 'TABLE', N'PE_Question', 'COLUMN', N'ID'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'问题类型ID', 'USER', N'dbo', 'TABLE', N'PE_Question', 'COLUMN', N'TypeID'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'问题标题', 'USER', N'dbo', 'TABLE', N'PE_Question', 'COLUMN', N'QuestionTitle'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'问题内容', 'USER', N'dbo', 'TABLE', N'PE_Question', 'COLUMN', N'QuestionContent'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'问题创建时间', 'USER', N'dbo', 'TABLE', N'PE_Question', 'COLUMN', N'QuestionCreateTime'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'问题创建者', 'USER', N'dbo', 'TABLE', N'PE_Question', 'COLUMN', N'QuestionCreator'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'最后回复者', 'USER', N'dbo', 'TABLE', N'PE_Question', 'COLUMN', N'ReplyCreator'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'最后回复时间', 'USER', N'dbo', 'TABLE', N'PE_Question', 'COLUMN', N'ReplyTime'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'产品版本', 'USER', N'dbo', 'TABLE', N'PE_Question', 'COLUMN', N'ProductVersion'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'系统类型', 'USER', N'dbo', 'TABLE', N'PE_Question', 'COLUMN', N'SystemType'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'产品运行的数据库', 'USER', N'dbo', 'TABLE', N'PE_Question', 'COLUMN', N'ProductDBType'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'提问者IP', 'USER', N'dbo', 'TABLE', N'PE_Question', 'COLUMN', N'IP'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'网站地址', 'USER', N'dbo', 'TABLE', N'PE_Question', 'COLUMN', N'Url'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'服务器上的杀毒软件', 'USER', N'dbo', 'TABLE', N'PE_Question', 'COLUMN', N'AntiVirus'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'服务器上的防火墙', 'USER', N'dbo', 'TABLE', N'PE_Question', 'COLUMN', N'FireWall'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'错误号', 'USER', N'dbo', 'TABLE', N'PE_Question', 'COLUMN', N'ErrorCode'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'错误提示信息', 'USER', N'dbo', 'TABLE', N'PE_Question', 'COLUMN', N'ErrorText'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'问题难度', 'USER', N'dbo', 'TABLE', N'PE_Question', 'COLUMN', N'Score'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'是否公开', 'USER', N'dbo', 'TABLE', N'PE_Question', 'COLUMN', N'IsPublic'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'是否已回复', 'USER', N'dbo', 'TABLE', N'PE_Question', 'COLUMN', N'IsReply'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'是否已解决', 'USER', N'dbo', 'TABLE', N'PE_Question', 'COLUMN', N'IsSolved'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'最后更新时间', 'USER', N'dbo', 'TABLE', N'PE_Question', 'COLUMN', N'LastUpdateTime'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'问题类型表', 'USER', N'dbo', 'TABLE', N'PE_QuestionType', NULL, NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'问题类型ID', 'USER', N'dbo', 'TABLE', N'PE_QuestionType', 'COLUMN', N'TypeID'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'问题类型名称', 'USER', N'dbo', 'TABLE', N'PE_QuestionType', 'COLUMN', N'TypeName'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'管理员问题类型表', 'USER', N'dbo', 'TABLE', N'PE_QuestionType_Admin', NULL, NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'问题类型ID', 'USER', N'dbo', 'TABLE', N'PE_QuestionType_Admin', 'COLUMN', N'TypeID'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'管理员ID', 'USER', N'dbo', 'TABLE', N'PE_QuestionType_Admin', 'COLUMN', N'AdminID'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'角色ID', 'USER', N'dbo', 'TABLE', N'PE_Roles_Permissions', 'COLUMN', N'RoleID'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'权限操作码', 'USER', N'dbo', 'TABLE', N'PE_Roles_Permissions', 'COLUMN', N'OperateCode'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'专题搜索页模板路径', 'USER', N'dbo', 'TABLE', N'PE_SpecialCategory', 'COLUMN', N'SearchTemplatePath'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'是否有需要生成HTML的专题内容', 'USER', N'dbo', 'TABLE', N'PE_SpecialCategory', 'COLUMN', N'NeedCreateHtml'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'专题搜索页模板路径', 'USER', N'dbo', 'TABLE', N'PE_Specials', 'COLUMN', N'SearchTemplatePath'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'是否有需要生成HTML的专题内容', 'USER', N'dbo', 'TABLE', N'PE_Specials', 'COLUMN', N'NeedCreateHtml'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'0点', 'USER', N'dbo', 'TABLE', N'PE_StatDay', 'COLUMN', N'0'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'1点', 'USER', N'dbo', 'TABLE', N'PE_StatDay', 'COLUMN', N'1'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'2点', 'USER', N'dbo', 'TABLE', N'PE_StatDay', 'COLUMN', N'2'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'3点', 'USER', N'dbo', 'TABLE', N'PE_StatDay', 'COLUMN', N'3'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'4点', 'USER', N'dbo', 'TABLE', N'PE_StatDay', 'COLUMN', N'4'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'5点', 'USER', N'dbo', 'TABLE', N'PE_StatDay', 'COLUMN', N'5'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'6点', 'USER', N'dbo', 'TABLE', N'PE_StatDay', 'COLUMN', N'6'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'7点', 'USER', N'dbo', 'TABLE', N'PE_StatDay', 'COLUMN', N'7'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'8点', 'USER', N'dbo', 'TABLE', N'PE_StatDay', 'COLUMN', N'8'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'9点', 'USER', N'dbo', 'TABLE', N'PE_StatDay', 'COLUMN', N'9'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'10点', 'USER', N'dbo', 'TABLE', N'PE_StatDay', 'COLUMN', N'10'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'11点', 'USER', N'dbo', 'TABLE', N'PE_StatDay', 'COLUMN', N'11'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'12点', 'USER', N'dbo', 'TABLE', N'PE_StatDay', 'COLUMN', N'12'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'13点', 'USER', N'dbo', 'TABLE', N'PE_StatDay', 'COLUMN', N'13'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'14点', 'USER', N'dbo', 'TABLE', N'PE_StatDay', 'COLUMN', N'14'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'15点', 'USER', N'dbo', 'TABLE', N'PE_StatDay', 'COLUMN', N'15'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'16点', 'USER', N'dbo', 'TABLE', N'PE_StatDay', 'COLUMN', N'16'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'17点', 'USER', N'dbo', 'TABLE', N'PE_StatDay', 'COLUMN', N'17'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'18点', 'USER', N'dbo', 'TABLE', N'PE_StatDay', 'COLUMN', N'18'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'19点', 'USER', N'dbo', 'TABLE', N'PE_StatDay', 'COLUMN', N'19'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'20点', 'USER', N'dbo', 'TABLE', N'PE_StatDay', 'COLUMN', N'20'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'21点', 'USER', N'dbo', 'TABLE', N'PE_StatDay', 'COLUMN', N'21'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'22点', 'USER', N'dbo', 'TABLE', N'PE_StatDay', 'COLUMN', N'22'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'23点', 'USER', N'dbo', 'TABLE', N'PE_StatDay', 'COLUMN', N'23'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'日期', 'USER', N'dbo', 'TABLE', N'PE_StatDay', 'COLUMN', N'TDay'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'第1天', 'USER', N'dbo', 'TABLE', N'PE_StatMonth', 'COLUMN', N'1'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'第2天', 'USER', N'dbo', 'TABLE', N'PE_StatMonth', 'COLUMN', N'2'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'第3天', 'USER', N'dbo', 'TABLE', N'PE_StatMonth', 'COLUMN', N'3'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'第4天', 'USER', N'dbo', 'TABLE', N'PE_StatMonth', 'COLUMN', N'4'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'第5天', 'USER', N'dbo', 'TABLE', N'PE_StatMonth', 'COLUMN', N'5'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'第6天', 'USER', N'dbo', 'TABLE', N'PE_StatMonth', 'COLUMN', N'6'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'第7天', 'USER', N'dbo', 'TABLE', N'PE_StatMonth', 'COLUMN', N'7'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'第8天', 'USER', N'dbo', 'TABLE', N'PE_StatMonth', 'COLUMN', N'8'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'第9天', 'USER', N'dbo', 'TABLE', N'PE_StatMonth', 'COLUMN', N'9'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'第10天', 'USER', N'dbo', 'TABLE', N'PE_StatMonth', 'COLUMN', N'10'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'第11天', 'USER', N'dbo', 'TABLE', N'PE_StatMonth', 'COLUMN', N'11'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'第12天', 'USER', N'dbo', 'TABLE', N'PE_StatMonth', 'COLUMN', N'12'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'第13天', 'USER', N'dbo', 'TABLE', N'PE_StatMonth', 'COLUMN', N'13'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'第14天', 'USER', N'dbo', 'TABLE', N'PE_StatMonth', 'COLUMN', N'14'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'第15天', 'USER', N'dbo', 'TABLE', N'PE_StatMonth', 'COLUMN', N'15'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'第16天', 'USER', N'dbo', 'TABLE', N'PE_StatMonth', 'COLUMN', N'16'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'第17天', 'USER', N'dbo', 'TABLE', N'PE_StatMonth', 'COLUMN', N'17'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'第18天', 'USER', N'dbo', 'TABLE', N'PE_StatMonth', 'COLUMN', N'18'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'第19天', 'USER', N'dbo', 'TABLE', N'PE_StatMonth', 'COLUMN', N'19'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'第20天', 'USER', N'dbo', 'TABLE', N'PE_StatMonth', 'COLUMN', N'20'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'第21天', 'USER', N'dbo', 'TABLE', N'PE_StatMonth', 'COLUMN', N'21'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'第22天', 'USER', N'dbo', 'TABLE', N'PE_StatMonth', 'COLUMN', N'22'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'第23天', 'USER', N'dbo', 'TABLE', N'PE_StatMonth', 'COLUMN', N'23'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'第24天', 'USER', N'dbo', 'TABLE', N'PE_StatMonth', 'COLUMN', N'24'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'第25天', 'USER', N'dbo', 'TABLE', N'PE_StatMonth', 'COLUMN', N'25'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'第26天', 'USER', N'dbo', 'TABLE', N'PE_StatMonth', 'COLUMN', N'26'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'第27天', 'USER', N'dbo', 'TABLE', N'PE_StatMonth', 'COLUMN', N'27'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'第28天', 'USER', N'dbo', 'TABLE', N'PE_StatMonth', 'COLUMN', N'28'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'第29天', 'USER', N'dbo', 'TABLE', N'PE_StatMonth', 'COLUMN', N'29'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'第30天', 'USER', N'dbo', 'TABLE', N'PE_StatMonth', 'COLUMN', N'30'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'第31天', 'USER', N'dbo', 'TABLE', N'PE_StatMonth', 'COLUMN', N'31'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'月份', 'USER', N'dbo', 'TABLE', N'PE_StatMonth', 'COLUMN', N'TMonth'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'状态码ID', 'USER', N'dbo', 'TABLE', N'PE_Status', 'COLUMN', N'StatusID'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'状态码', 'USER', N'dbo', 'TABLE', N'PE_Status', 'COLUMN', N'StatusCode'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'状态码名', 'USER', N'dbo', 'TABLE', N'PE_Status', 'COLUMN', N'StatusName'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'系统状态码', 'USER', N'dbo', 'TABLE', N'PE_Status', 'COLUMN', N'StatusType'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'周一', 'USER', N'dbo', 'TABLE', N'PE_StatWeek', 'COLUMN', N'1'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'周二', 'USER', N'dbo', 'TABLE', N'PE_StatWeek', 'COLUMN', N'2'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'周三', 'USER', N'dbo', 'TABLE', N'PE_StatWeek', 'COLUMN', N'3'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'周四', 'USER', N'dbo', 'TABLE', N'PE_StatWeek', 'COLUMN', N'4'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'周五', 'USER', N'dbo', 'TABLE', N'PE_StatWeek', 'COLUMN', N'5'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'周六', 'USER', N'dbo', 'TABLE', N'PE_StatWeek', 'COLUMN', N'6'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'周日', 'USER', N'dbo', 'TABLE', N'PE_StatWeek', 'COLUMN', N'7'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'一月', 'USER', N'dbo', 'TABLE', N'PE_StatYear', 'COLUMN', N'1'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'二月', 'USER', N'dbo', 'TABLE', N'PE_StatYear', 'COLUMN', N'2'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'三月', 'USER', N'dbo', 'TABLE', N'PE_StatYear', 'COLUMN', N'3'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'四月', 'USER', N'dbo', 'TABLE', N'PE_StatYear', 'COLUMN', N'4'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'五月', 'USER', N'dbo', 'TABLE', N'PE_StatYear', 'COLUMN', N'5'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'六月', 'USER', N'dbo', 'TABLE', N'PE_StatYear', 'COLUMN', N'6'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'七月', 'USER', N'dbo', 'TABLE', N'PE_StatYear', 'COLUMN', N'7'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'八月', 'USER', N'dbo', 'TABLE', N'PE_StatYear', 'COLUMN', N'8'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'九月', 'USER', N'dbo', 'TABLE', N'PE_StatYear', 'COLUMN', N'9'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'十月', 'USER', N'dbo', 'TABLE', N'PE_StatYear', 'COLUMN', N'10'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'十一月', 'USER', N'dbo', 'TABLE', N'PE_StatYear', 'COLUMN', N'11'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'十二月', 'USER', N'dbo', 'TABLE', N'PE_StatYear', 'COLUMN', N'12'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'支付密码', 'USER', N'dbo', 'TABLE', N'PE_Users', 'COLUMN', N'PayPassword'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'数据库版本控制表', 'USER', N'dbo', 'TABLE', N'PE_Version', NULL, NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'版本ID', 'USER', N'dbo', 'TABLE', N'PE_Version', 'COLUMN', N'VersionID'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'主版本号', 'USER', N'dbo', 'TABLE', N'PE_Version', 'COLUMN', N'Major'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'次版本号', 'USER', N'dbo', 'TABLE', N'PE_Version', 'COLUMN', N'Minor'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'内部版本号', 'USER', N'dbo', 'TABLE', N'PE_Version', 'COLUMN', N'Build'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'修订版本号', 'USER', N'dbo', 'TABLE', N'PE_Version', 'COLUMN', N'Revision'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'创建日期', 'USER', N'dbo', 'TABLE', N'PE_Version', 'COLUMN', N'CreatedDate'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'投票开始时间', 'USER', N'dbo', 'TABLE', N'PE_Vote', 'COLUMN', N'StartTime'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'投票结束时间', 'USER', N'dbo', 'TABLE', N'PE_Vote', 'COLUMN', N'EndTime'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'字符替换后元素的Title属性', 'USER', N'dbo', 'TABLE', N'PE_WordReplaceItem', 'COLUMN', N'Title'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
IF EXISTS (SELECT * FROM #tmpErrors) ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT>0 BEGIN
PRINT '数据库更新成功。'
COMMIT TRANSACTION
END
ELSE PRINT '数据库更新失败。'
GO
DROP TABLE #tmpErrors
GO
