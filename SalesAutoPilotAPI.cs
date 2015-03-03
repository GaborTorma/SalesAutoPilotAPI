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
        IProducts Products { get; }
        ILists Lists { get; }
        IGlobalVariables GlobalVariables { get; }
        ISend Send { get; }
        ICoupons Coupons { get; }

        string APIURL { get; }
    }

    public class SalesAutoPilot : ISalesAutoPilot
    {
        public IProducts Products { get; set; }
        public ILists Lists { get; set; }
        public IGlobalVariables GlobalVariables { get; set; }
        public ISend Send { get; set; }
        public ICoupons Coupons { get; set; }

        public string APIURL { get; set; }
        
        public SalesAutoPilot(string BaseAPIURL, string UserName, string Password)
        {
            var APIURL = BaseAPIURL;//Uri(BaseAPIURL).AbsoluteUri;

            Products = new Products(APIURL, UserName, Password);
            Lists = new Lists(APIURL, UserName, Password);
            GlobalVariables = new GlobalVariables(APIURL, UserName, Password);
            Send = new Send(APIURL, UserName, Password);
            Coupons = new Coupons(APIURL, UserName, Password);
        }
    }

}
