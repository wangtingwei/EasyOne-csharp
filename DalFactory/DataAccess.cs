namespace EasyOne.DalFactory
{
    using EasyOne.IDal.AccessManage;
    using EasyOne.IDal.Accessories;
    using EasyOne.IDal.AD;
    using EasyOne.IDal.Analytics;
    using EasyOne.IDal.Collection;
    using EasyOne.IDal.CommonModel;
    using EasyOne.IDal.Contents;
    using EasyOne.IDal.Crm;
    using EasyOne.IDal.Shop;
    using EasyOne.IDal.Survey;
    using EasyOne.IDal.TemplateProc;
    using EasyOne.IDal.Templates;
    using EasyOne.IDal.UserManage;
    using EasyOne.IDal.WorkFlow;
    using EasyOne.SqlServerDal.AccessManage;
    using EasyOne.SqlServerDal.Accessories;
    using EasyOne.SqlServerDal.AD;
    using EasyOne.SqlServerDal.Analytics;
    using EasyOne.SqlServerDal.Analytics.Report;
    using EasyOne.SqlServerDal.Collection;
    using EasyOne.SqlServerDal.CommonModel;
    using EasyOne.SqlServerDal.Contents;
    using EasyOne.SqlServerDal.Crm;
    using EasyOne.SqlServerDal.Shop;
    using EasyOne.SqlServerDal.Survey;
    using EasyOne.SqlServerDal.TemplateProc;
    using EasyOne.SqlServerDal.Templates;
    using EasyOne.SqlServerDal.UserManage;
    using EasyOne.SqlServerDal.WorkFlows;
    using System;
    using System.Reflection;

    public sealed class DataAccess
    {
        private static readonly string path = System.Web.Configuration.WebConfigurationManager.AppSettings["WebDAL"];

        private DataAccess()
        {
        }

        public static IAddress CreateAddress()
        {
            return new Address();
        }

        public static IAdministrator CreateAdmin()
        {
            return new Administrators();
        }

        public static IAdminProfile CreateAdminProfile()
        {
            return new AdminProfile();
        }

        public static IAdvertisement CreateAdvertisement()
        {
            return new Advertisement();
        }

        public static IADZone CreateAdZone()
        {
            return new ADZone();
        }

        public static IAgent CreateAgentPayment()
        {
            return new Agent();
        }

        public static IAuthor CreateAuthorInfo()
        {
            return new Author();
        }

        public static IBank CreateBank()
        {
            return new Bank();
        }

        public static IBankrollItem CreateBankrollItem()
        {
            return new BankrollItem();
        }

        public static ICards CreateCards()
        {
            return new Cards();
        }

        public static IChoiceset CreateChoiceset()
        {
            return new Choiceset();
        }

        public static IClient CreateClient()
        {
            return new Client();
        }

        public static IClientDelete CreateClientDelete()
        {
            return new ClientDelete();
        }

        public static ICollectionExclosion CreateCollectionExclosion()
        {
            return new CollectionExclosion();
        }

        public static ICollectionFieldRules CreateCollectionFieldRules()
        {
            return new CollectionFieldRules();
        }

        public static ICollectionFilterRules CreateCollectionFilterRules()
        {
            return new CollectionFilterRules();
        }

        public static ICollectionHistory CreateCollectionHistory()
        {
            return new CollectionHistory();
        }

        public static ICollectionItem CreateCollectionItem()
        {
            return new CollectionItem();
        }

        public static ICollectionListRules CreateCollectionListRules()
        {
            return new CollectionListRules();
        }

        public static ICollectionPagingRules CreateCollectionPagingRules()
        {
            return new CollectionPagingRules();
        }

        public static IComment CreateComment()
        {
            return new Comment();
        }

        public static ICommentPKZone CreateCommentPKZone()
        {
            return new CommentPKZone();
        }

        public static ICompany CreateCompany()
        {
            return new Company();
        }

        public static IComplain CreateComplain()
        {
            return new Complain();
        }

        public static IContacter CreateContacter()
        {
            return new Contacter();
        }

        public static IContentCharge CreateContentCharge()
        {
            return new ContentCharge();
        }

        public static IContentManage CreateContentManage()
        {
            return new ContentManage();
        }

        public static IPermissionContent CreateContentPermission()
        {
            return new PermissionContent();
        }

        public static IModel CreateCotentModel()
        {
            return new ModelDal();
        }

        public static IModelTemplate CreateCotentModelTemplate()
        {
            return new ModelTemplate();
        }

        public static ICounter CreateCounter()
        {
            return new Counter();
        }

        public static ICoupon CreateCoupon()
        {
            return new Coupon();
        }

        public static ICouponItem CreateCouponItem()
        {
            return new CouponItem();
        }

        public static ICourier CreateCourier()
        {
            return new Courier();
        }

        public static IDataBaseHandle CreateDataBaseHandle()
        {
            return new DataBaseHandle();
        }

        public static IDeliverCharge CreateDeliverCharge()
        {
            return new DeliverCharge();
        }

        public static IDeliverItem CreateDeliverItem()
        {
            return new DeliverItem();
        }

        public static IDeliverType CreateDeliverType()
        {
            return new DeliverType();
        }

        public static IDownloadError CreateDownloadError()
        {
            return new DownloadError();
        }

        public static IDownServer CreateDownServer()
        {
            return new DownServer();
        }

        public static IFavorite CreateFavorite()
        {
            return new Favorite();
        }

        public static IFiles CreateFiles()
        {
            return new Files();
        }

        public static IFlowProcess CreateFlowProcess()
        {
            return new FlowProcess();
        }

        public static IIncludeFile CreateIncludeFile()
        {
            return new IncludeFile();
        }

        public static IInvoice CreateInvoice()
        {
            return new Invoice();
        }

        public static IIPStorage CreateIPStorage()
        {
            return new IPStorage();
        }

        public static IKeywords CreateKeywords()
        {
            return new Keywords();
        }

        public static ILabelManage CreateLabelManage()
        {
            return new LabelManage();
        }

        public static IMessage CreateMessage()
        {
            return new Message();
        }

        public static INodes CreateNodes()
        {
            return new Nodes();
        }

        public static IOrder CreateOrder()
        {
            return new Order();
        }

        public static IOrderFeedback CreateOrderFeedback()
        {
            return new OrderFeedback();
        }

        public static IOrderItem CreateOrderItem()
        {
            return new OrderItem();
        }

        public static IOtherReport CreateOtherReport()
        {
            return new OtherReport();
        }

        public static IPackage CreatePackage()
        {
            return new Package();
        }

        public static IPaymentLog CreatePaymentLog()
        {
            return new PaymentLog();
        }

        public static IPaymentType CreatePaymentType()
        {
            return new PaymentType();
        }

        public static IPayPlatform CreatePayPlatform()
        {
            return new PayPlatform();
        }

        public static IUserPermissions CreatePermissionAccess()
        {
            return new UserPermissions();
        }

        public static IPresent CreatePresent()
        {
            return new Present();
        }

        public static IPresentProject CreatePresentProject()
        {
            return new PresentProject();
        }

        public static IProducer CreateProducer()
        {
            return new Producer();
        }

        public static IProduct CreateProduct()
        {
            return new Product();
        }

        public static IProductCommon CreateProductCommon()
        {
            return new ProductCommon();
        }

        public static IProductData CreateProductData()
        {
            return new ProductData();
        }

        public static IProductPrice CreateProductPrice()
        {
            return new ProductPrice();
        }

        public static IQuestion CreateQuestion()
        {
            return new Question();
        }

        public static IQuestionAllot CreateQuestionAllot()
        {
            return new QuestionAllot();
        }

        public static IQuestionType CreateQuestionType()
        {
            return new QuestionType();
        }

        public static IRefund CreateRefund()
        {
            return new Refund();
        }

        public static IRegion CreateRegion()
        {
            return new Region();
        }

        public static IRemittance CreateRemittance()
        {
            return new Remittance();
        }

        public static IReply CreateReply()
        {
            return new Reply();
        }

        public static IRoleMembers CreateRoleMembers()
        {
            return new RoleMembers();
        }

        public static IUserRole CreateRoles()
        {
            return new UserRole();
        }

        public static ISaleCount CreateSaleCount()
        {
            return new SaleCount();
        }

        public static ISaleList CreateSaleList()
        {
            return new SaleList();
        }

        public static IService CreateService()
        {
            return new Service();
        }

        public static IShoppingCart CreateShoppingCart()
        {
            return new ShoppingCart();
        }

        public static ISignInContent CreateSignInContent()
        {
            return new SignInContent();
        }

        public static ISignInLog CreateSignInLog()
        {
            return new SignInLog();
        }

        public static ISource CreateSourceInfo()
        {
            return new Source();
        }

        public static ISpecial CreateSpecial()
        {
            return new Special();
        }

        public static IStatistics CreateStatistics()
        {
            return new Statistics();
        }

        public static IStatus CreateStatus()
        {
            return new Status();
        }

        public static IStockItem CreateStockItem()
        {
            return new StockItem();
        }

        public static IStockManage CreateStockManage()
        {
            return new StockManage();
        }

        public static ISurveyManager CreateSurvey()
        {
            return new SurveyManager();
        }

        public static ISurveyCreate CreateSurveyCreate()
        {
            return new SurveyCreate();
        }

        public static ISurveyField CreateSurveyField()
        {
            return new SurveyField();
        }

        public static ISurveyRecord CreateSurveyRecord()
        {
            return new SurveyRecord();
        }

        public static ISurveyVote CreateSurveyVote()
        {
            return new SurveyVote();
        }

        public static ITimeReport CreateTimeReport(string statName)
        {
            string typeName = path + ".Analytics.Report." + statName;
            return (ITimeReport) Assembly.Load(path).CreateInstance(typeName);
        }

        public static ITrademark CreateTrademark()
        {
            return new Trademark();
        }

        public static ITransferLog CreateTransferLog()
        {
            return new TransferLog();
        }

        public static IUserDataReport CreateUserDataReport(string statName)
        {
            string typeName = path + ".Analytics.Report." + statName;
            return (IUserDataReport) Assembly.Load(path).CreateInstance(typeName);
        }

        public static IUserDate CreateUserDateCreate()
        {
            return new UserDate();
        }

        public static IUserFriend CreateUserFriend()
        {
            return new UserFriend();
        }

        public static IUserGroups CreateUserGroups()
        {
            return new UserGroups();
        }

        public static IUserMoney CreateUserMoneyCreate()
        {
            return new UserMoney();
        }

        public static IUserPoint CreateUserPointCreate()
        {
            return new UserPoint();
        }

        public static IUserPointLog CreateUserPointLog()
        {
            return new UserPointLog();
        }

        public static IUsers CreateUsers()
        {
            return new Users();
        }

        public static IUserValidLog CreateUserValidLog()
        {
            return new UserValidLog();
        }

        public static IVotes CreateVote()
        {
            return new Votes();
        }

        public static IWordReplace CreateWordReplace()
        {
            return new WordReplace();
        }

        public static IWorkFlows CreateWorkFlows()
        {
            return new WorkFlow();
        }

        public static ILabelProc LabelCode()
        {
            return new LabelSql();
        }
    }
}

