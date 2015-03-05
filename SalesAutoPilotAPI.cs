using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using SalesAutoPilotAPI.Requests;

namespace SalesAutoPilotAPI
{
    public interface ISalesAutoPilot
    {
        ILists Lists { get; }
        IProducts Products { get; }
        IProductCategories ProductCategories { get; }
        IGlobalVariables GlobalVariables { get; }
        ICoupons Coupons { get; }
        ISend Send { get; }

        string APIURL { get; }
    }

    public class SalesAutoPilot : ISalesAutoPilot
    {
        public ILists Lists { get; set; }
        public IProducts Products { get; set; }
        public IProductCategories ProductCategories { get; set; }
        public ISend Send { get; set; }
        public IGlobalVariables GlobalVariables { get; set; }
        public ICoupons Coupons { get; set; }

        public string APIURL { get; set; }
        
        public SalesAutoPilot(string BaseAPIURL, string UserName, string Password)
        {
            var APIURL = BaseAPIURL;//Uri(BaseAPIURL).AbsoluteUri;

            Lists = new Lists(APIURL, UserName, Password);
            Products = new Products(APIURL, UserName, Password);
            ProductCategories = new ProductCategories(APIURL, UserName, Password);
            GlobalVariables = new GlobalVariables(APIURL, UserName, Password);
            Coupons = new Coupons(APIURL, UserName, Password);
            Send = new Send(APIURL, UserName, Password);
        }
    }

}
