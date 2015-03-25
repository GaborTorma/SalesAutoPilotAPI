using System;
using System.Collections.Generic;
using SalesAutoPilotAPI.Models;

namespace SalesAutoPilotAPI.Requests
{
    public interface IProducts : ICore
    {
        long? Add(Product Product);
        bool Modify(Product Product);
        bool Modify(long? Id, Product Product);
        bool Delete(long? Id);
        long Clear();
        Product ById(long? Id);
        List<Product> AllProduct();
    }

	public class Products : Core, IProducts
    {
        public Products(string apiurl, string username, string password, string logfile)
            : base(apiurl, username, password, logfile)
        {}

        /// <summary> Create new product. </summary>
        /// <param name="Product">
        /// Class containing the properties of the new product.
        /// Required: Name, Price, Vat, Currency, SKU
        /// Usable: CategoryId, CategoryIds
        /// </param>
        /// <returns> ID of the new product in the SalesAutoPilot system. </returns>
        public long? Add(Product Product)
        {
            if (Product.Name == null ||
                Product.Price == null ||
                Product.Vat == null ||
                Product.Currency == null ||
                Product.SKU == null)
                return null;
            return GenericPost<long?>("createproduct", Product);
        }

        /// <summary> Modify existing product. </summary>
        /// <param name="Product">
        /// Class containing the values to change.
        /// The object's ID property /property/ has to contain the ID of the product to change from the SalesAutoPilot system. http://media.salesautopilot.com/knowledge-base/get-product-id.png 
        /// Values only required for the property /property/ you'd like to modify.
        /// </param>
        /// <returns> If success then True else False. </returns>
        public bool Modify(Product Product)
        {
            return GenericPost<bool>(string.Format("modifyproduct/{0}", Product.Id), Product);
        }

        /// <summary> Modify existing product. </summary>
        /// <param name="Id">
        /// The product's ID from the SalesAutoPilot system. http://media.salesautopilot.com/knowledge-base/get-product-id.png
        /// </param>
        /// <param name="Product">
        /// Object containing the properties to change. 
        /// Values only required for the property /property/ you'd like to modify.
        /// </param>
        /// <returns> If success then True else False. </returns>
        public bool Modify(long? Id, Product Product)
        {
            if (Id == null)
                return false;
            Product.Id = Id;
            return Modify(Product);
        }

        /// <summary> Delete product. </summary>
        /// <param name="Id">
        /// The product's ID from the SalesAutoPilor system. http://media.salesautopilot.com/knowledge-base/get-product-id.png
        /// </param>
        /// <returns> If success then True else False. </returns>
        public bool Delete(long? Id)
        {
            if (Id == null)
                return false;
            return GenericDelete(string.Format("deleteproduct/{0}", Id));
        }

        /// <summary> Delete all products. </summary>
        /// <returns> Number of deleted items. </returns>
        public long Clear()
        {
            long Result = 0;
            List<Product> Products = AllProduct();
            if (Products != null)
                foreach (Product Product in Products)
                    Result += Convert.ToInt64(Delete(Product.Id));
            return Result;
        }

        /// <summary> Retrieve the properties of the product by it's ID. </summary>
        /// <param name="Id">
        /// The product's ID from the SalesAutoPilot system. http://media.salesautopilot.com/knowledge-base/get-product-id.png
        /// </param>
        /// <returns> Object containing the properties of the product. </returns>
        public Product ById(long? Id)  
        {
            if (Id == null)
                return null;
            return GenericGet<Product>(string.Format("getproduct/{0}", Id));
        }

        /// <summary> Retrieve all products. </summary>
        /// <returns> List of objects containing the properties of the products. </returns>
        public List<Product> AllProduct()
        {
            return GenericGet<List<Product>>("listproducts");
        }
    }
}
