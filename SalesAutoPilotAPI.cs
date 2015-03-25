using SalesAutoPilotAPI.Requests;

namespace SalesAutoPilotAPI
{
    public interface ISalesAutoPilot
    {
		ISubscribers Subscribers { get; }
		IOrders Orders { get; }
		IProducts Products { get; }
        IProductCategories ProductCategories { get; }
        IGlobalVariables GlobalVariables { get; }
        ICoupons Coupons { get; }
        ISend Send { get; }

        string APIURL { get; }
    }

    public class SalesAutoPilot : ISalesAutoPilot
    {
        public ISubscribers Subscribers { get; set; }
		public IOrders Orders { get; set; }
        public IProducts Products { get; set; }
        public IProductCategories ProductCategories { get; set; }
        public ISend Send { get; set; }
        public IGlobalVariables GlobalVariables { get; set; }
        public ICoupons Coupons { get; set; }

        public string APIURL { get; set; }
        
        public SalesAutoPilot(string BaseAPIURL, string UserName, string Password, string LogFile = null)
        {
            var APIURL = BaseAPIURL;//Uri(BaseAPIURL).AbsoluteUri;

            Subscribers = new Subscribers(APIURL, UserName, Password, LogFile);
            Orders = new Orders(APIURL, UserName, Password, LogFile);
            Products = new Products(APIURL, UserName, Password, LogFile);
            ProductCategories = new ProductCategories(APIURL, UserName, Password, LogFile);
            GlobalVariables = new GlobalVariables(APIURL, UserName, Password, LogFile);
            Coupons = new Coupons(APIURL, UserName, Password, LogFile);
            Send = new Send(APIURL, UserName, Password, LogFile);
        }
    }

}
