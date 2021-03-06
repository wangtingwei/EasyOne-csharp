/*
此脚本由 Visual Studio 在 2008/3/6 上的 9:59 处创建。
请在 EasyOne SiteFactory RC版数据库 上运行此脚本，使其与 EasyOne SiteFactory 正式版数据库 相同。
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
PRINT N'正在删除 [dbo].[PE_CommonModel] 的约束'
GO
ALTER TABLE [dbo].[PE_CommonModel] DROP CONSTRAINT [PK_PE_CommonModel_GeneralID]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PE_CommonModel] 的约束'
GO
ALTER TABLE [dbo].[PE_CommonModel] DROP CONSTRAINT [DF_PE_CommonModel_Hits]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PE_CommonModel] 的约束'
GO
ALTER TABLE [dbo].[PE_CommonModel] DROP CONSTRAINT [DF_PE_CommonModel_DayHits]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PE_CommonModel] 的约束'
GO
ALTER TABLE [dbo].[PE_CommonModel] DROP CONSTRAINT [DF_PE_CommonModel_WeekHits]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PE_CommonModel] 的约束'
GO
ALTER TABLE [dbo].[PE_CommonModel] DROP CONSTRAINT [DF_PE_CommonModel_MonthHits]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PE_CommonModel] 的约束'
GO
ALTER TABLE [dbo].[PE_CommonModel] DROP CONSTRAINT [DF_PE_CommonModel_LinkType]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PE_CommonModel] 的约束'
GO
ALTER TABLE [dbo].[PE_CommonModel] DROP CONSTRAINT [DF_PE_CommonModel_CommentAudited]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PE_CommonModel] 的约束'
GO
ALTER TABLE [dbo].[PE_CommonModel] DROP CONSTRAINT [DF_PE_CommonModel_CommentUnAudited]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PE_CommonModel] 的约束'
GO
ALTER TABLE [dbo].[PE_CommonModel] DROP CONSTRAINT [DF_PE_CommonModel_SigninType]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PE_CommonProduct] 的约束'
GO
ALTER TABLE [dbo].[PE_CommonProduct] DROP CONSTRAINT [PK_PE_CommonProduct_ID]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PE_CommonProduct] 的约束'
GO
ALTER TABLE [dbo].[PE_CommonProduct] DROP CONSTRAINT [DF_PE_CommonProduct_ProductCharacter]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PE_DownloadError] 的约束'
GO
ALTER TABLE [dbo].[PE_DownloadError] DROP CONSTRAINT [PK_PE_DownloadError_ErrorID]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PE_PaymentLog] 的约束'
GO
ALTER TABLE [dbo].[PE_PaymentLog] DROP CONSTRAINT [PK_PE_PaymentLog_PaymentLogID]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PE_Present] 的约束'
GO
ALTER TABLE [dbo].[PE_Present] DROP CONSTRAINT [PK_PE_Present]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PE_CommonModel] 的索引 [IX_PE_CommonModel_ItemID]'
GO
DROP INDEX [IX_PE_CommonModel_ItemID] ON [dbo].[PE_CommonModel]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PE_CommonModel] 的索引 [IX_PE_CommonModel_EliteLevel]'
GO
DROP INDEX [IX_PE_CommonModel_EliteLevel] ON [dbo].[PE_CommonModel]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PE_CommonModel] 的索引 [IX_PE_CommonModel_Priority]'
GO
DROP INDEX [IX_PE_CommonModel_Priority] ON [dbo].[PE_CommonModel]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PE_CommonProduct] 的索引 [IX_PE_CommonProduct_PresentID]'
GO
DROP INDEX [IX_PE_CommonProduct_PresentID] ON [dbo].[PE_CommonProduct]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PE_CommonProduct] 的索引 [IX_PE_CommonProduct_AlarmNum]'
GO
DROP INDEX [IX_PE_CommonProduct_AlarmNum] ON [dbo].[PE_CommonProduct]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PE_CommonProduct] 的索引 [IX_PE_CommonProduct_LimitNum]'
GO
DROP INDEX [IX_PE_CommonProduct_LimitNum] ON [dbo].[PE_CommonProduct]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PE_CommonProduct] 的索引 [IX_PE_CommonProduct_OrderNum]'
GO
DROP INDEX [IX_PE_CommonProduct_OrderNum] ON [dbo].[PE_CommonProduct]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PE_CommonProduct] 的索引 [IX_PE_CommonProduct_Keyword]'
GO
DROP INDEX [IX_PE_CommonProduct_Keyword] ON [dbo].[PE_CommonProduct]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PE_CommonProduct] 的索引 [IX_PE_CommonProduct_ProducerName]'
GO
DROP INDEX [IX_PE_CommonProduct_ProducerName] ON [dbo].[PE_CommonProduct]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PE_PaymentLog] 的索引 [IX_PE_PaymentLog_UserName]'
GO
DROP INDEX [IX_PE_PaymentLog_UserName] ON [dbo].[PE_PaymentLog]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PE_PaymentLog] 的索引 [IX_PE_PaymentLog_OrderID]'
GO
DROP INDEX [IX_PE_PaymentLog_OrderID] ON [dbo].[PE_PaymentLog]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PE_PaymentLog] 的索引 [IX_PE_PaymentLog_PaymentNum]'
GO
DROP INDEX [IX_PE_PaymentLog_PaymentNum] ON [dbo].[PE_PaymentLog]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PE_PaymentLog] 的索引 [IX_PE_PaymentLog_PlatformID]'
GO
DROP INDEX [IX_PE_PaymentLog_PlatformID] ON [dbo].[PE_PaymentLog]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PE_PaymentLog] 的索引 [IX_PE_PaymentLog_MoneyPay]'
GO
DROP INDEX [IX_PE_PaymentLog_MoneyPay] ON [dbo].[PE_PaymentLog]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PR_Shop_Pruducer_GetById]'
GO
DROP PROCEDURE [dbo].[PR_Shop_Pruducer_GetById]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PR_Shop_Pruducer_Update]'
GO
DROP PROCEDURE [dbo].[PR_Shop_Pruducer_Update]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PR_Shop_Pruducer_SetOnTop]'
GO
DROP PROCEDURE [dbo].[PR_Shop_Pruducer_SetOnTop]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PR_Shop_Pruducer_SetElite]'
GO
DROP PROCEDURE [dbo].[PR_Shop_Pruducer_SetElite]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PR_Shop_Pruducer_SetPassed]'
GO
DROP PROCEDURE [dbo].[PR_Shop_Pruducer_SetPassed]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PR_Contents_Nodes_GetCountDownOrderId]'
GO
DROP PROCEDURE [dbo].[PR_Contents_Nodes_GetCountDownOrderId]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PR_Contents_Nodes_GetNodesListInParentPath]'
GO
DROP PROCEDURE [dbo].[PR_Contents_Nodes_GetNodesListInParentPath]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PR_Contents_Nodes_GetCountUpOrderId]'
GO
DROP PROCEDURE [dbo].[PR_Contents_Nodes_GetCountUpOrderId]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PR_Contents_Nodes_UpdatePrevId]'
GO
DROP PROCEDURE [dbo].[PR_Contents_Nodes_UpdatePrevId]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PR_Contents_Nodes_GetChildNodesList]'
GO
DROP PROCEDURE [dbo].[PR_Contents_Nodes_GetChildNodesList]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PR_Contents_Nodes_GetDownOrderListByOrderId]'
GO
DROP PROCEDURE [dbo].[PR_Contents_Nodes_GetDownOrderListByOrderId]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PR_Contents_Nodes_GetList]'
GO
DROP PROCEDURE [dbo].[PR_Contents_Nodes_GetList]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PR_Contents_Nodes_GetUpOrderId]'
GO
DROP PROCEDURE [dbo].[PR_Contents_Nodes_GetUpOrderId]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PR_Contents_Nodes_GetCountRootId]'
GO
DROP PROCEDURE [dbo].[PR_Contents_Nodes_GetCountRootId]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PR_Contents_Nodes_GetDownOrderListByRootId]'
GO
DROP PROCEDURE [dbo].[PR_Contents_Nodes_GetDownOrderListByRootId]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PR_Contents_Nodes_GetNodeNameForItems]'
GO
DROP PROCEDURE [dbo].[PR_Contents_Nodes_GetNodeNameForItems]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PR_Contents_Nodes_GetDownOrderId]'
GO
DROP PROCEDURE [dbo].[PR_Contents_Nodes_GetDownOrderId]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PR_Contents_Nodes_GetPrevIdAndNextId]'
GO
DROP PROCEDURE [dbo].[PR_Contents_Nodes_GetPrevIdAndNextId]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在删除 [dbo].[PR_Contents_Nodes_GetUpOrderListByRootId]'
GO
DROP PROCEDURE [dbo].[PR_Contents_Nodes_GetUpOrderListByRootId]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在重新生成 [dbo].[PE_PaymentLog]'
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
SET XACT_ABORT ON
GO
BEGIN TRANSACTION
CREATE TABLE [dbo].[tmp_ms_xx_PE_PaymentLog]
(
[PaymentLogID] [int] NOT NULL,
[UserName] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[OrderID] [int] NULL,
[PaymentNum] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[PlatformID] [int] NULL,
[MoneyPay] [money] NULL,
[MoneyTrue] [money] NULL,
[PayTime] [datetime] NULL,
[SuccessTime] [datetime] NULL,
[Status] [int] NULL,
[PlatformInfo] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[Remark] [nvarchar] (255) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
INSERT INTO [dbo].[tmp_ms_xx_PE_PaymentLog]([PaymentLogID], [UserName], [OrderID], [PaymentNum], [PlatformID], [MoneyPay], [MoneyTrue], [PayTime], [SuccessTime], [Status], [PlatformInfo], [Remark]) SELECT [PaymentLogID], [UserName], [OrderID], [PaymentNum], [PlatformID], [MoneyPay], [MoneyTrue], [PayTime], [SuccessTime], [Status], [PlatformInfo], [Remark] FROM [dbo].[PE_PaymentLog]
DROP TABLE [dbo].[PE_PaymentLog]
EXEC sp_rename N'[dbo].[tmp_ms_xx_PE_PaymentLog]', N'PE_PaymentLog'
COMMIT TRANSACTION
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
GO
PRINT N'正在 [dbo].[PE_PaymentLog] 上创建主键 [PK_PE_PaymentLog_PaymentLogID]'
GO
ALTER TABLE [dbo].[PE_PaymentLog] ADD CONSTRAINT [PK_PE_PaymentLog_PaymentLogID] PRIMARY KEY CLUSTERED  ([PaymentLogID]) ON [PRIMARY]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在创建 [dbo].[PE_PaymentLog] 的索引 [IX_PE_PaymentLog_UserName]'
GO
CREATE NONCLUSTERED INDEX [IX_PE_PaymentLog_UserName] ON [dbo].[PE_PaymentLog] ([UserName]) ON [PRIMARY]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在创建 [dbo].[PE_PaymentLog] 的索引 [IX_PE_PaymentLog_OrderID]'
GO
CREATE NONCLUSTERED INDEX [IX_PE_PaymentLog_OrderID] ON [dbo].[PE_PaymentLog] ([OrderID]) ON [PRIMARY]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在创建 [dbo].[PE_PaymentLog] 的索引 [IX_PE_PaymentLog_PaymentNum]'
GO
CREATE NONCLUSTERED INDEX [IX_PE_PaymentLog_PaymentNum] ON [dbo].[PE_PaymentLog] ([PaymentNum]) ON [PRIMARY]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在创建 [dbo].[PE_PaymentLog] 的索引 [IX_PE_PaymentLog_PlatformID]'
GO
CREATE NONCLUSTERED INDEX [IX_PE_PaymentLog_PlatformID] ON [dbo].[PE_PaymentLog] ([PlatformID]) ON [PRIMARY]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在创建 [dbo].[PE_PaymentLog] 的索引 [IX_PE_PaymentLog_MoneyPay]'
GO
CREATE NONCLUSTERED INDEX [IX_PE_PaymentLog_MoneyPay] ON [dbo].[PE_PaymentLog] ([MoneyPay]) ON [PRIMARY]
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
	@Remark nvarchar(255)   
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
			[Remark]   
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
		@Remark
	)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在重新生成 [dbo].[PE_CommonProduct]'
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
SET XACT_ABORT ON
GO
BEGIN TRANSACTION
CREATE TABLE [dbo].[tmp_ms_xx_PE_CommonProduct]
(
[ProductID] [int] NOT NULL,
[TableName] [nvarchar] (255) COLLATE Chinese_PRC_CI_AS NULL,
[ProductName] [nvarchar] (255) COLLATE Chinese_PRC_CI_AS NULL,
[ProductNum] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[ProductType] [int] NULL,
[Unit] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL,
[ProductThumb] [nvarchar] (255) COLLATE Chinese_PRC_CI_AS NULL,
[ProductPic] [nvarchar] (255) COLLATE Chinese_PRC_CI_AS NULL,
[ServiceTermUnit] [int] NULL,
[ServiceTerm] [int] NULL,
[Price] [money] NULL,
[Price_Market] [money] NULL,
[Price_Member] [money] NULL,
[Price_Agent] [money] NULL,
[EnableBuyWhenOutofstock] [bit] NULL,
[EnableWholesale] [bit] NULL,
[Price_Wholesale1] [money] NULL,
[Price_Wholesale2] [money] NULL,
[Price_Wholesale3] [money] NULL,
[Number_Wholesale1] [int] NULL,
[Number_Wholesale2] [int] NULL,
[Number_Wholesale3] [int] NULL,
[PresentID] [int] NULL,
[PresentNumber] [int] NULL,
[PresentPoint] [int] NULL,
[PresentExp] [int] NULL,
[PresentMoney] [money] NULL,
[StocksProject] [int] NULL,
[SalePromotionType] [int] NULL,
[AlarmNum] [int] NULL,
[BuyTimes] [int] NULL,
[MinNumber] [int] NULL,
[Discount] [float] NULL,
[IncludeTax] [int] NULL,
[TaxRate] [float] NULL,
[Properties] [nvarchar] (255) COLLATE Chinese_PRC_CI_AS NULL,
[Weight] [float] NULL,
[LimitNum] [int] NULL,
[EnableSale] [bit] NULL,
[EnableSingleSell] [bit] NULL,
[ProductKind] [int] NULL,
[DependentProducts] [ntext] COLLATE Chinese_PRC_CI_AS NULL,
[Stocks] [int] NULL,
[OrderNum] [int] NULL,
[DownloadUrl] [nvarchar] (255) COLLATE Chinese_PRC_CI_AS NULL,
[Remark] [ntext] COLLATE Chinese_PRC_CI_AS NULL,
[ProductCharacter] [int] NULL CONSTRAINT [DF_PE_CommonProduct_ProductCharacter] DEFAULT ((0)),
[Keyword] [nvarchar] (255) COLLATE Chinese_PRC_CI_AS NULL,
[ProducerName] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL,
[TrademarkName] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL,
[BarCode] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL,
[ProductIntro] [nvarchar] (255) COLLATE Chinese_PRC_CI_AS NULL,
[ProductExplain] [ntext] COLLATE Chinese_PRC_CI_AS NULL,
[IsNew] [bit] NULL,
[IsHot] [bit] NULL,
[IsBest] [bit] NULL,
[Stars] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
INSERT INTO [dbo].[tmp_ms_xx_PE_CommonProduct]([ProductID], [TableName], [ProductName], [ProductNum], [ProductType], [Unit], [ProductThumb], [ProductPic], [ServiceTermUnit], [ServiceTerm], [Price], [Price_Market], [Price_Member], [Price_Agent], [EnableBuyWhenOutofstock], [EnableWholesale], [Price_Wholesale1], [Price_Wholesale2], [Price_Wholesale3], [Number_Wholesale1], [Number_Wholesale2], [Number_Wholesale3], [PresentID], [PresentNumber], [PresentPoint], [PresentExp], [PresentMoney], [StocksProject], [SalePromotionType], [AlarmNum], [BuyTimes], [MinNumber], [Discount], [IncludeTax], [TaxRate], [Properties], [Weight], [LimitNum], [EnableSale], [EnableSingleSell], [ProductKind], [DependentProducts], [Stocks], [OrderNum], [DownloadUrl], [Remark], [ProductCharacter], [Keyword], [ProducerName], [TrademarkName], [BarCode], [ProductIntro], [ProductExplain], [IsNew], [IsHot], [IsBest], [Stars]) SELECT [ProductID], [TableName], [ProductName], [ProductNum], [ProductType], [Unit], [ProductThumb], [ProductPic], [ServiceTermUnit], [ServiceTerm], [Price], [Price_Market], [Price_Member], [Price_Agent], [EnableBuyWhenOutofstock], [EnableWholesale], [Price_Wholesale1], [Price_Wholesale2], [Price_Wholesale3], [Number_Wholesale1], [Number_Wholesale2], [Number_Wholesale3], [PresentID], [PresentNumber], [PresentPoint], [PresentExp], [PresentMoney], [StocksProject], [SalePromotionType], [AlarmNum], [BuyTimes], [MinNumber], [Discount], [IncludeTax], [TaxRate], [Properties], [Weight], [LimitNum], [EnableSale], [EnableSingleSell], [ProductKind], [DependentProducts], [Stocks], [OrderNum], [DownloadUrl], [Remark], [ProductCharacter], [Keyword], [ProducerName], [TrademarkName], [BarCode], [ProductIntro], [ProductExplain], [IsNew], [IsHot], [IsBest], [Stars] FROM [dbo].[PE_CommonProduct]
DROP TABLE [dbo].[PE_CommonProduct]
EXEC sp_rename N'[dbo].[tmp_ms_xx_PE_CommonProduct]', N'PE_CommonProduct'
COMMIT TRANSACTION
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
GO
PRINT N'正在 [dbo].[PE_CommonProduct] 上创建主键 [PK_PE_CommonProduct_ID]'
GO
ALTER TABLE [dbo].[PE_CommonProduct] ADD CONSTRAINT [PK_PE_CommonProduct_ID] PRIMARY KEY CLUSTERED  ([ProductID]) ON [PRIMARY]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在创建 [dbo].[PE_CommonProduct] 的索引 [IX_PE_CommonProduct_PresentID]'
GO
CREATE NONCLUSTERED INDEX [IX_PE_CommonProduct_PresentID] ON [dbo].[PE_CommonProduct] ([PresentID]) ON [PRIMARY]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在创建 [dbo].[PE_CommonProduct] 的索引 [IX_PE_CommonProduct_AlarmNum]'
GO
CREATE NONCLUSTERED INDEX [IX_PE_CommonProduct_AlarmNum] ON [dbo].[PE_CommonProduct] ([AlarmNum]) ON [PRIMARY]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在创建 [dbo].[PE_CommonProduct] 的索引 [IX_PE_CommonProduct_LimitNum]'
GO
CREATE NONCLUSTERED INDEX [IX_PE_CommonProduct_LimitNum] ON [dbo].[PE_CommonProduct] ([LimitNum]) ON [PRIMARY]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在创建 [dbo].[PE_CommonProduct] 的索引 [IX_PE_CommonProduct_OrderNum]'
GO
CREATE NONCLUSTERED INDEX [IX_PE_CommonProduct_OrderNum] ON [dbo].[PE_CommonProduct] ([OrderNum]) ON [PRIMARY]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在创建 [dbo].[PE_CommonProduct] 的索引 [IX_PE_CommonProduct_Keyword]'
GO
CREATE NONCLUSTERED INDEX [IX_PE_CommonProduct_Keyword] ON [dbo].[PE_CommonProduct] ([Keyword]) ON [PRIMARY]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在创建 [dbo].[PE_CommonProduct] 的索引 [IX_PE_CommonProduct_ProducerName]'
GO
CREATE NONCLUSTERED INDEX [IX_PE_CommonProduct_ProducerName] ON [dbo].[PE_CommonProduct] ([ProducerName]) ON [PRIMARY]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PE_Address]'
GO
ALTER TABLE [dbo].[PE_Address] ALTER COLUMN [HomePhone] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PE_PayPlatForm]'
GO
ALTER TABLE [dbo].[PE_PayPlatForm] ALTER COLUMN [Rate] [float] NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在重新生成 [dbo].[PE_DownloadError]'
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
SET XACT_ABORT ON
GO
BEGIN TRANSACTION
CREATE TABLE [dbo].[tmp_ms_xx_PE_DownloadError]
(
[ErrorID] [int] NOT NULL,
[InfoID] [int] NULL,
[ErrorUrl] [nvarchar] (255) COLLATE Chinese_PRC_CI_AS NULL,
[ErrorTimes] [int] NULL,
[ErrorDate] [datetime] NULL
) ON [PRIMARY]
INSERT INTO [dbo].[tmp_ms_xx_PE_DownloadError]([ErrorID], [InfoID], [ErrorTimes], [ErrorDate]) SELECT [ErrorID], [InfoID], [ErrorTimes], [ErrorDate] FROM [dbo].[PE_DownloadError]
DROP TABLE [dbo].[PE_DownloadError]
EXEC sp_rename N'[dbo].[tmp_ms_xx_PE_DownloadError]', N'PE_DownloadError'
COMMIT TRANSACTION
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
GO
PRINT N'正在 [dbo].[PE_DownloadError] 上创建主键 [PK_PE_DownloadError_ErrorID]'
GO
ALTER TABLE [dbo].[PE_DownloadError] ADD CONSTRAINT [PK_PE_DownloadError_ErrorID] PRIMARY KEY CLUSTERED  ([ErrorID]) ON [PRIMARY]
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
PRINT N'正在重新生成 [dbo].[PE_CommonModel]'
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
SET XACT_ABORT ON
GO
BEGIN TRANSACTION
CREATE TABLE [dbo].[tmp_ms_xx_PE_CommonModel]
(
[GeneralID] [int] NOT NULL,
[NodeID] [int] NOT NULL,
[ModelID] [int] NOT NULL,
[ItemID] [int] NULL,
[TableName] [nvarchar] (255) COLLATE Chinese_PRC_CI_AS NULL,
[Title] [nvarchar] (255) COLLATE Chinese_PRC_CI_AS NOT NULL,
[Inputer] [nvarchar] (255) COLLATE Chinese_PRC_CI_AS NULL,
[Hits] [int] NULL CONSTRAINT [DF_PE_CommonModel_Hits] DEFAULT ((0)),
[DayHits] [int] NULL CONSTRAINT [DF_PE_CommonModel_DayHits] DEFAULT ((0)),
[WeekHits] [int] NULL CONSTRAINT [DF_PE_CommonModel_WeekHits] DEFAULT ((0)),
[MonthHits] [int] NULL CONSTRAINT [DF_PE_CommonModel_MonthHits] DEFAULT ((0)),
[LinkType] [int] NULL CONSTRAINT [DF_PE_CommonModel_LinkType] DEFAULT ((0)),
[UpdateTime] [datetime] NULL,
[CreateTime] [datetime] NULL,
[TemplateFile] [nvarchar] (255) COLLATE Chinese_PRC_CI_AS NULL,
[Status] [int] NULL,
[EliteLevel] [int] NULL,
[Priority] [int] NULL,
[CommentAudited] [int] NULL CONSTRAINT [DF_PE_CommonModel_CommentAudited] DEFAULT ((0)),
[CommentUnAudited] [int] NULL CONSTRAINT [DF_PE_CommonModel_CommentUnAudited] DEFAULT ((0)),
[SigninType] [int] NULL CONSTRAINT [DF_PE_CommonModel_SigninType] DEFAULT ((0)),
[InputTime] [datetime] NULL,
[PassedTime] [datetime] NULL,
[Editor] [nvarchar] (255) COLLATE Chinese_PRC_CI_AS NULL,
[LastHitTime] [datetime] NULL,
[DefaultPicUrl] [nvarchar] (255) COLLATE Chinese_PRC_CI_AS NULL,
[UploadFiles] [ntext] COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
INSERT INTO [dbo].[tmp_ms_xx_PE_CommonModel]([GeneralID], [NodeID], [ModelID], [ItemID], [TableName], [Title], [Inputer], [Hits], [DayHits], [WeekHits], [MonthHits], [LinkType], [UpdateTime], [CreateTime], [TemplateFile], [Status], [EliteLevel], [Priority], [CommentAudited], [CommentUnAudited], [SigninType], [InputTime], [PassedTime], [Editor], [LastHitTime]) SELECT [GeneralID], [NodeID], [ModelID], [ItemID], [TableName], [Title], [Inputer], [Hits], [DayHits], [WeekHits], [MonthHits], [LinkType], [UpdateTime], [CreateTime], [TemplateFile], [Status], [EliteLevel], [Priority], [CommentAudited], [CommentUnAudited], [SigninType], [InputTime], [PassedTime], [Editor], [LastHitTime] FROM [dbo].[PE_CommonModel]
DROP TABLE [dbo].[PE_CommonModel]
EXEC sp_rename N'[dbo].[tmp_ms_xx_PE_CommonModel]', N'PE_CommonModel'
COMMIT TRANSACTION
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
GO
PRINT N'正在 [dbo].[PE_CommonModel] 上创建主键 [PK_PE_CommonModel_GeneralID]'
GO
ALTER TABLE [dbo].[PE_CommonModel] ADD CONSTRAINT [PK_PE_CommonModel_GeneralID] PRIMARY KEY CLUSTERED  ([GeneralID]) ON [PRIMARY]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在创建 [dbo].[PE_CommonModel] 的索引 [IX_PE_CommonModel_ItemID]'
GO
CREATE NONCLUSTERED INDEX [IX_PE_CommonModel_ItemID] ON [dbo].[PE_CommonModel] ([ItemID] DESC) ON [PRIMARY]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在创建 [dbo].[PE_CommonModel] 的索引 [IX_PE_CommonModel_EliteLevel]'
GO
CREATE NONCLUSTERED INDEX [IX_PE_CommonModel_EliteLevel] ON [dbo].[PE_CommonModel] ([EliteLevel] DESC) ON [PRIMARY]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在创建 [dbo].[PE_CommonModel] 的索引 [IX_PE_CommonModel_Priority]'
GO
CREATE NONCLUSTERED INDEX [IX_PE_CommonModel_Priority] ON [dbo].[PE_CommonModel] ([Priority] DESC) ON [PRIMARY]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PE_Model]'
GO
ALTER TABLE [dbo].[PE_Model] ADD
[SearchTemplate] [nvarchar] (255) COLLATE Chinese_PRC_CI_AS NULL,
[AdvanceSearchFormTemplate] [nvarchar] (255) COLLATE Chinese_PRC_CI_AS NULL,
[AdvanceSearchTemplate] [nvarchar] (255) COLLATE Chinese_PRC_CI_AS NULL
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
	@AdvanceSearchTemplate nvarchar(255)
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
		AdvanceSearchTemplate=@AdvanceSearchTemplate
    WHERE 
        ModelID = @ModelID
    RETURN
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PE_ShoppingCarts]'
GO
ALTER TABLE [dbo].[PE_ShoppingCarts] ADD
[InformResult] [int] NULL CONSTRAINT [DF_PE_ShoppingCarts_InformResult] DEFAULT ((0))
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
PRINT N'正在创建 [dbo].[PR_Shop_Producer_GetById]'
GO
CREATE PROCEDURE [dbo].[PR_Shop_Producer_GetById]
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
PRINT N'正在创建 [dbo].[PR_Shop_Producer_SetPassed]'
GO
CREATE PROCEDURE [dbo].[PR_Shop_Producer_SetPassed] 
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
PRINT N'正在创建 [dbo].[PR_Shop_Producer_Update]'
GO
CREATE PROCEDURE [dbo].[PR_Shop_Producer_Update] 
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
PRINT N'正在创建 [dbo].[PR_Shop_Producer_SetElite]'
GO
CREATE PROCEDURE [dbo].[PR_Shop_Producer_SetElite] 
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
	@BatchInfoFilePath	nvarchar(200),
	@Character int,
	@MaxPerUser int,
	@PrintTemplate nvarchar(255),
	@SearchTemplate nvarchar(255),
	@AdvanceSearchFormTemplate nvarchar(255),
	@AdvanceSearchTemplate nvarchar(255)
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
			[AdvanceSearchTemplate]
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
		@AdvanceSearchTemplate
	)
BEGIN
		-- 不存在创建指定数据表
		EXEC('CREATE TABLE '+ @TableName+' (ID int,CONSTRAINT [PK_'+@TableName+'_ID] PRIMARY KEY CLUSTERED
(
 [ID] ASC
))')

END
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
    @ContacterName nvarchar(50)
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
            [ContacterName] = @ContacterName
	WHERE
		OrderID = @OrderID







GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在创建 [dbo].[PR_Shop_Producer_SetOnTop]'
GO
CREATE PROCEDURE [dbo].[PR_Shop_Producer_SetOnTop] 
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
PRINT N'正在改变 [dbo].[PE_SpecialCategory]'
GO
ALTER TABLE [dbo].[PE_SpecialCategory] ADD
[SearchTemplatePath] [nvarchar] (255) COLLATE Chinese_PRC_CI_AS NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在改变 [dbo].[PE_Specials]'
GO
ALTER TABLE [dbo].[PE_Specials] ADD
[SearchTemplatePath] [nvarchar] (255) COLLATE Chinese_PRC_CI_AS NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在创建 [dbo].[PE_Question]'
GO
CREATE TABLE [dbo].[PE_Question]
(
[ID] [int] NOT NULL,
[TypeID] [int] NOT NULL,
[QuestionTitle] [nvarchar] (255) COLLATE Chinese_PRC_CI_AS NOT NULL,
[QuestionContent] [text] COLLATE Chinese_PRC_CI_AS NULL,
[QuestionCreateTime] [datetime] NOT NULL,
[QuestionCreator] [nvarchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL,
[ReplyCreator] [nvarchar] (30) COLLATE Chinese_PRC_CI_AS NULL,
[ReplyTime] [datetime] NULL,
[ProductVersion] [nvarchar] (255) COLLATE Chinese_PRC_CI_AS NULL,
[SystemType] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[ProductDBType] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[IP] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL,
[Url] [nvarchar] (255) COLLATE Chinese_PRC_CI_AS NULL,
[AntiVirus] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[FireWall] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[ErrorCode] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[ErrorText] [text] COLLATE Chinese_PRC_CI_AS NULL,
[Score] [int] NULL,
[IsPublic] [bit] NULL,
[IsReply] [bit] NULL,
[IsSolved] [bit] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在 [dbo].[PE_Question] 上创建主键 [PK_PE_Question_ID]'
GO
ALTER TABLE [dbo].[PE_Question] ADD CONSTRAINT [PK_PE_Question_ID] PRIMARY KEY CLUSTERED  ([ID]) ON [PRIMARY]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在创建 [dbo].[PE_QuestionType]'
GO
CREATE TABLE [dbo].[PE_QuestionType]
(
[TypeID] [int] NOT NULL,
[TypeName] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在 [dbo].[PE_QuestionType] 上创建主键 [PK_PE_QuestionType_TypeID]'
GO
ALTER TABLE [dbo].[PE_QuestionType] ADD CONSTRAINT [PK_PE_QuestionType_TypeID] PRIMARY KEY CLUSTERED  ([TypeID]) ON [PRIMARY]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在创建 [dbo].[PE_QuestionType_Admin]'
GO
CREATE TABLE [dbo].[PE_QuestionType_Admin]
(
[TypeID] [int] NOT NULL,
[AdminID] [int] NOT NULL
) ON [PRIMARY]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在 [dbo].[PE_QuestionType_Admin] 上创建主键 [PK_PE_QuestionType_Admin]'
GO
ALTER TABLE [dbo].[PE_QuestionType_Admin] ADD CONSTRAINT [PK_PE_QuestionType_Admin] PRIMARY KEY CLUSTERED  ([TypeID], [AdminID]) ON [PRIMARY]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在创建 [dbo].[PE_Reply]'
GO
CREATE TABLE [dbo].[PE_Reply]
(
[ID] [int] NOT NULL,
[replyCreator] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL,
[QuestionID] [int] NULL,
[replyTime] [datetime] NOT NULL,
[replyContent] [text] COLLATE Chinese_PRC_CI_AS NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在 [dbo].[PE_Reply] 上创建主键 [PK_PE_Reply]'
GO
ALTER TABLE [dbo].[PE_Reply] ADD CONSTRAINT [PK_PE_Reply] PRIMARY KEY CLUSTERED  ([ID]) ON [PRIMARY]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在创建 [dbo].[PE_Reply] 的索引 [IX_PE_Reply_QuestionID]'
GO
CREATE NONCLUSTERED INDEX [IX_PE_Reply_QuestionID] ON [dbo].[PE_Reply] ([QuestionID]) ON [PRIMARY]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在创建 [dbo].[PE_Version]'
GO
CREATE TABLE [dbo].[PE_Version]
(
[VersionID] [int] NOT NULL IDENTITY(1, 1),
[Major] [int] NULL,
[Minor] [int] NULL,
[Build] [int] NULL,
[Revision] [int] NULL,
[CreatedDate] [datetime] NULL
) ON [PRIMARY]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在 [dbo].[PE_Version] 上创建主键 [PK_PE_Version]'
GO
ALTER TABLE [dbo].[PE_Version] ADD CONSTRAINT [PK_PE_Version] PRIMARY KEY CLUSTERED  ([VersionID]) ON [PRIMARY]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在重新生成 [dbo].[PE_Present]'
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
SET XACT_ABORT ON
GO
BEGIN TRANSACTION
CREATE TABLE [dbo].[tmp_ms_xx_PE_Present]
(
[PresentID] [int] NOT NULL,
[PresentName] [nvarchar] (255) COLLATE Chinese_PRC_CI_AS NULL,
[PresentPic] [nvarchar] (255) COLLATE Chinese_PRC_CI_AS NULL,
[Unit] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL,
[PresentNum] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[ServiceTermUnit] [int] NULL,
[ServiceTerm] [int] NULL,
[Price] [money] NULL,
[Price_Market] [money] NULL,
[Weight] [float] NULL,
[Stocks] [int] NULL,
[AlarmNum] [int] NULL,
[ProductCharacter] [int] NULL,
[DownloadUrl] [nvarchar] (255) COLLATE Chinese_PRC_CI_AS NULL,
[Remark] [nvarchar] (255) COLLATE Chinese_PRC_CI_AS NULL,
[PresentThumb] [nvarchar] (255) COLLATE Chinese_PRC_CI_AS NULL,
[PresentIntro] [nvarchar] (255) COLLATE Chinese_PRC_CI_AS NULL,
[PresentExplain] [ntext] COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
INSERT INTO [dbo].[tmp_ms_xx_PE_Present]([PresentID], [PresentName], [PresentPic], [Unit], [PresentNum], [ServiceTermUnit], [ServiceTerm], [Price], [Price_Market], [Weight], [Stocks], [AlarmNum], [ProductCharacter], [DownloadUrl], [Remark]) SELECT [PresentID], [PresentName], [PresentPic], [Unit], [PresentNum], [ServiceTermUnit], [ServiceTerm], [Price], [Price_Market], [Weight], [Stocks], [AlarmNum], [ProductCharacter], [DownloadUrl], [Remark] FROM [dbo].[PE_Present]
DROP TABLE [dbo].[PE_Present]
EXEC sp_rename N'[dbo].[tmp_ms_xx_PE_Present]', N'PE_Present'
COMMIT TRANSACTION
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
GO
PRINT N'正在 [dbo].[PE_Present] 上创建主键 [PK_PE_Present]'
GO
ALTER TABLE [dbo].[PE_Present] ADD CONSTRAINT [PK_PE_Present] PRIMARY KEY CLUSTERED  ([PresentID]) ON [PRIMARY]
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
PRINT N'正在向 [dbo].[PE_Version] 添加约束 '
GO
ALTER TABLE [dbo].[PE_Version] ADD CONSTRAINT [IX_PE_Version] UNIQUE NONCLUSTERED  ([Major], [Minor], [Build], [Revision]) ON [PRIMARY]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'正在创建扩展属性'
GO
EXEC sp_addextendedproperty N'MS_Description', N'模型公共数据表', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonModel', NULL, NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'内容全局ID', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonModel', 'COLUMN', N'GeneralID'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'节点ID', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonModel', 'COLUMN', N'NodeID'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'模型ID', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonModel', 'COLUMN', N'ModelID'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'相应表的记录ID', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonModel', 'COLUMN', N'ItemID'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'模型表名', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonModel', 'COLUMN', N'TableName'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'标题', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonModel', 'COLUMN', N'Title'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'录入者', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonModel', 'COLUMN', N'Inputer'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'点击数', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonModel', 'COLUMN', N'Hits'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'日点击数', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonModel', 'COLUMN', N'DayHits'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'周点击数', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonModel', 'COLUMN', N'WeekHits'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'月点击数', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonModel', 'COLUMN', N'MonthHits'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'链接类型', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonModel', 'COLUMN', N'LinkType'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'更新时间', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonModel', 'COLUMN', N'UpdateTime'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'生成时间', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonModel', 'COLUMN', N'CreateTime'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'该文章所使用的模板路径', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonModel', 'COLUMN', N'TemplateFile'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'信息状态 -3为删除，-2为退稿，-1为草稿，0为待审核，99为终审通过，其它为自定义', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonModel', 'COLUMN', N'Status'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'推荐级别', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonModel', 'COLUMN', N'EliteLevel'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'优先级别', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonModel', 'COLUMN', N'Priority'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'通过审核的评论总数', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonModel', 'COLUMN', N'CommentAudited'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'未通过审核的评论总数', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonModel', 'COLUMN', N'CommentUnAudited'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'签收类型', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonModel', 'COLUMN', N'SigninType'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'文章录入时间', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonModel', 'COLUMN', N'InputTime'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'商品公共数据表', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', NULL, NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'商品ID', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'ProductID'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'表名', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'TableName'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'商品名称', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'ProductName'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'商品编号', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'ProductNum'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'商品状态 0--正常  3--特价　4--促销', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'ProductType'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'商品单位', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'Unit'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'缩略图', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'ProductThumb'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'商品大图', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'ProductPic'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'服务期限单位 0表示年，1表示月，2 表示日', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'ServiceTermUnit'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'服务期限', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'ServiceTerm'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'零售价', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'Price'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'市场参考价', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'Price_Market'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'会员零售价', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'Price_Member'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'代理零售价', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'Price_Agent'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'缺货时是否允许购买', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'EnableBuyWhenOutofstock'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'是否允许批发', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'EnableWholesale'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'批发价1', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'Price_Wholesale1'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'批发价2', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'Price_Wholesale2'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'批发价3', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'Price_Wholesale3'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'次性购买此商品数量1', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'Number_Wholesale1'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'次性购买此商品数量2', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'Number_Wholesale2'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'次性购买此商品数量3', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'Number_Wholesale3'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'赠品ID', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'PresentID'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'促销赠送', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'PresentNumber'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'赠送点券', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'PresentPoint'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'购物积分', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'PresentExp'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'返还的现金券', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'PresentMoney'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'库存报警方案 ', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'StocksProject'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'促销设置', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'SalePromotionType'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'库存报警下限', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'AlarmNum'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'购买次数', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'BuyTimes'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'促销购买商品最小值', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'MinNumber'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'折扣率', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'Discount'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'是否包括税率', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'IncludeTax'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'税率', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'TaxRate'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'属性', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'Properties'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'商品重量', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'Weight'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'限购数量', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'LimitNum'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'是否销售', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'EnableSale'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'是否允许单独销售', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'EnableSingleSell'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'商品类别', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'ProductKind'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'从属商品', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'DependentProducts'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'库存', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'Stocks'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'订购数量', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'OrderNum'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'下载地址', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'DownloadUrl'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'下载说明', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'Remark'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'商品性质', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'ProductCharacter'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'关键字', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'Keyword'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'厂商', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'ProducerName'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'商标', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'TrademarkName'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'条形码', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'BarCode'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'简单介绍', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'ProductIntro'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'详细介绍', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'ProductExplain'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'是否最新', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'IsNew'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'是否热卖', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'IsHot'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'是否精品', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'IsBest'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'商品推荐等级', 'SCHEMA', N'dbo', 'TABLE', N'PE_CommonProduct', 'COLUMN', N'Stars'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'下载报错表', 'SCHEMA', N'dbo', 'TABLE', N'PE_DownloadError', NULL, NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'错误ID', 'SCHEMA', N'dbo', 'TABLE', N'PE_DownloadError', 'COLUMN', N'ErrorID'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'错误信息ID', 'SCHEMA', N'dbo', 'TABLE', N'PE_DownloadError', 'COLUMN', N'InfoID'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'用$$$分割的错误下载地址序号', 'SCHEMA', N'dbo', 'TABLE', N'PE_DownloadError', 'COLUMN', N'ErrorUrl'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'报错次数', 'SCHEMA', N'dbo', 'TABLE', N'PE_DownloadError', 'COLUMN', N'ErrorTimes'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'报错日期', 'SCHEMA', N'dbo', 'TABLE', N'PE_DownloadError', 'COLUMN', N'ErrorDate'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'在线支付记录表', 'SCHEMA', N'dbo', 'TABLE', N'PE_PaymentLog', NULL, NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'支付ID', 'SCHEMA', N'dbo', 'TABLE', N'PE_PaymentLog', 'COLUMN', N'PaymentLogID'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'会员用户名', 'SCHEMA', N'dbo', 'TABLE', N'PE_PaymentLog', 'COLUMN', N'UserName'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'订单ID', 'SCHEMA', N'dbo', 'TABLE', N'PE_PaymentLog', 'COLUMN', N'OrderID'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'支付序号', 'SCHEMA', N'dbo', 'TABLE', N'PE_PaymentLog', 'COLUMN', N'PaymentNum'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'支付平台', 'SCHEMA', N'dbo', 'TABLE', N'PE_PaymentLog', 'COLUMN', N'PlatformID'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'支付金额', 'SCHEMA', N'dbo', 'TABLE', N'PE_PaymentLog', 'COLUMN', N'MoneyPay'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'实际支付金额', 'SCHEMA', N'dbo', 'TABLE', N'PE_PaymentLog', 'COLUMN', N'MoneyTrue'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'交易时间', 'SCHEMA', N'dbo', 'TABLE', N'PE_PaymentLog', 'COLUMN', N'PayTime'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'交易成功时间', 'SCHEMA', N'dbo', 'TABLE', N'PE_PaymentLog', 'COLUMN', N'SuccessTime'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'支付状态', 'SCHEMA', N'dbo', 'TABLE', N'PE_PaymentLog', 'COLUMN', N'Status'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'银行信息', 'SCHEMA', N'dbo', 'TABLE', N'PE_PaymentLog', 'COLUMN', N'PlatformInfo'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'备注', 'SCHEMA', N'dbo', 'TABLE', N'PE_PaymentLog', 'COLUMN', N'Remark'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'缩略图', 'SCHEMA', N'dbo', 'TABLE', N'PE_Present', 'COLUMN', N'PresentThumb'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'简单介绍', 'SCHEMA', N'dbo', 'TABLE', N'PE_Present', 'COLUMN', N'PresentIntro'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'详细介绍', 'SCHEMA', N'dbo', 'TABLE', N'PE_Present', 'COLUMN', N'PresentExplain'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_addextendedproperty N'MS_Description', N'短信通知结果', 'SCHEMA', N'dbo', 'TABLE', N'PE_ShoppingCarts', 'COLUMN', N'InformResult'
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
