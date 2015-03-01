using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SalesAutoPilotAPI.Models;

namespace SalesAutoPilotAPI.Requests
{
    public interface IProducts : ICore
    {
        Product ProductById(decimal Id);
        long? Modify(long Id, Product Product);
        Product[] GetAll();
        long? Add(Product Product);
        bool Delete(decimal Id);
    }

	public class Products : Core, IProducts
    {
        public Products(string apiurl, string username, string password)
            : base(apiurl, username, password)
        {
        }

        public Product ProductById(decimal Id)  // Works
        {
            return GenericGet<Product>(string.Format("getproduct/{0}", Id));
        }


        public Product[] GetAll()  // not working
        {
            return GenericGet<Product[]>("listproducts");
        }

        public long? Add(Product Product)  // Works
        {
            if (Product.Name == null ||
                Product.Price == null ||
                Product.Vat == null ||
                Product.Currency == null ||
                Product.SKU == null)
                return null;
            return GenericPost<long?>("createproduct", Product);
        }

        public long? Modify(long Id, Product Product) 
        {
            return GenericPost<long?>(string.Format("modifyproduct/{0}", Id), Product);
        }

        public bool Delete(decimal id) 
        {
            return GenericDelete(string.Format("deleteproduct/{0}", id));  // Works
        }
    }
}
