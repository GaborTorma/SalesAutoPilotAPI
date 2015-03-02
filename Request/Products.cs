using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        Product ProductById(long? Id);
        List<Product> AllProduct();
    }

	public class Products : Core, IProducts
    {
        public Products(string apiurl, string username, string password)
            : base(apiurl, username, password)
        {}

        /// <summary> Új termék létrehozása. </summary>
        /// <param name="Product">
        /// Az új termék tulajdonságait tartalmazó osztály.
        /// Required: Name, Price, Vat, Currency, SKU
        /// Usable: CategoryId, CategoryIds
        /// </param>
        /// <returns> Az új termék azonosítója a SalesAutoPilot rendszerben. </returns>
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

        /// <summary> Létező termék módosítása. </summary>
        /// <param name="Product">
        /// A módosítandó értékeket tartalmazó osztály.
        /// Az objektum Id tulajdonságnak /property/ a módosítandó termék azonosítóját kell tartalmaznia a SalesAutoPilot rendszerből. http://media.salesautopilot.com/knowledge-base/get-product-id.png 
        /// Csak annak a tulajdonságnak /property/ kell értéket adni, melyet módosítani akrsz.
        /// </param>
        /// <returns> If success then True else False. </returns>
        public bool Modify(Product Product)
        {
            return GenericPost<bool>(string.Format("modifyproduct/{0}", Product.Id), Product);
        }

        /// <summary> Létező termék módosítása. </summary>
        /// <param name="Id">
        /// A termék azonosítója a SalesAutoPilot rendszerből. http://media.salesautopilot.com/knowledge-base/get-product-id.png
        /// </param>
        /// <param name="Product">
        /// A módosítandó tulajdonságokat tartalmazó objektum. 
        /// Csak annak a tulajdonságnak /property/ kell értéket adni, melyet módosítani akrsz.
        /// </param>
        /// <returns> If success then True else False. </returns>
        public bool Modify(long? Id, Product Product)
        {
            if (Id == null)
                return false;
            Product.Id = Id;
            return Modify(Product);
        }

        /// <summary> Termék törlése. </summary>
        /// <param name="Id">
        /// A termék azonosítója a SalesAutoPilot rendszerből. http://media.salesautopilot.com/knowledge-base/get-product-id.png
        /// </param>
        /// <returns> If success then True else False. </returns>
        public bool Delete(long? Id)
        {
            if (Id == null)
                return false;
            return GenericDelete(string.Format("deleteproduct/{0}", Id));
        }

        /// <summary> Minden termék törlése. </summary>
        /// <returns> Törölt elemek száma. </returns>
        public long Clear()
        {
            long Result = 0;
            List<Product> Products = AllProduct();
            foreach (Product Product in Products)
                Result += Convert.ToInt64(Delete(Product.Id));
            return Result;
        }

        /// <summary> Termék tulajdonságainak lekérdezése azonosító alapján. </summary>
        /// <param name="Id">
        /// A termék azonosítója a SalesAutoPilot rendszerből. http://media.salesautopilot.com/knowledge-base/get-product-id.png
        /// </param>
        /// <returns> A termék tulajdonságait tartalmazó objektum. </returns>
        public Product ProductById(long? Id)  
        {
            if (Id == null)
                return null;
            return GenericGet<Product>(string.Format("getproduct/{0}", Id));
        }

        /// <summary> Minden termék lekérdezése. </summary>
        /// <returns> A termékek tulajdonságait tartalmazó objektumok listája. </returns>
        public List<Product> AllProduct()
        {
            return GenericGet<List<Product>>("listproducts");
        }
    }
}
