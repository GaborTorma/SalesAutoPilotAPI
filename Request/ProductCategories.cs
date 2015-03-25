using System;
using System.Collections.Generic;
using SalesAutoPilotAPI.Models;

namespace SalesAutoPilotAPI.Requests
{
    public interface IProductCategories : ICore
    {
        long? Add(ProductCategory ProductCategory);
        long? Add(string Name);
        bool Modify(ProductCategory ProductCategory);
        bool Modify(long? Id, ProductCategory ProductCategory);
        bool Modify(long? Id, string Name);
		bool Delete(long? Id);
		long Clear();
		ProductCategory ById(long? Id);
        List<ProductCategory> All();
    }

    public class ProductCategories : Core, IProductCategories
    {
        public ProductCategories(string apiurl, string username, string password, string logfile)
            : base(apiurl, username, password, logfile)
        { }

        /// <summary> Create new product category. </summary>
        /// <param name="ProductCategory">
        /// Class containing the properties of the new product category.
        /// Required: Name
        /// </param>
        /// <returns> Id of the new product category in the SalesAutoPilot system. </returns>
        public long? Add(ProductCategory ProductCategory)
        {
            if (string.IsNullOrEmpty(ProductCategory.Name))
                return null;
            long? Result = GenericPost<long?>("createprodcategory", ProductCategory);
            if (ProductCategory.Order != null)
                Modify(ProductCategory);
            return Result;
        }

        /// <summary> Create new product category. </summary>
        /// <param name="Name">
        /// The neme of product category.
        /// </param>
        /// <returns> Id of the new product category in the SalesAutoPilot system. </returns>
        public long? Add(string Name)
        {
            ProductCategory ProductCategory = new ProductCategory();
            ProductCategory.Name = Name;
            return Add(ProductCategory);
        }

        /// <summary> Modify existing product. </summary>
        /// <param name="ProductCategory">
        /// Class containing the values to change.
        /// The object's Id property has to contain the ID of the product category to change from the SalesAutoPilot system.
        /// Values only required for the property you'd like to modify.
        /// </param>
        /// <returns> If success then True else False. </returns>
        public bool Modify(ProductCategory ProductCategory)
        {
            if (ProductCategory.Id == null || string.IsNullOrEmpty(ProductCategory.Name))
                return false;
            return GenericPost<bool>(string.Format("modprodcategory/{0}", ProductCategory.Id), ProductCategory);
        }

        /// <summary> Modify existing product. </summary>
        /// <param name="Id">
        /// The product category's ID from the SalesAutoPilot system.
        /// </param>
        /// <param name="ProductCategory">
        /// Object containing the properties to change. 
        /// Values only required for the property you'd like to modify.
        /// </param>
        /// <returns> If success then True else False. </returns>
        public bool Modify(long? Id, ProductCategory ProductCategory)
        {
            ProductCategory.Id = Id;
            return Modify(ProductCategory);
        }

        /// <summary> Modify existing product. </summary>
        /// <param name="Id">
        /// The product category's ID from the SalesAutoPilot system. 
        /// </param>
        /// <param name="Name">
        /// The new name of product category. 
        /// </param>
        /// <returns> If success then True else False. </returns>
        public bool Modify(long? Id, string Name)
        {
            ProductCategory ProductCategory = new ProductCategory();
            ProductCategory.Id = Id;
            ProductCategory.Name = Name;
            return Modify(ProductCategory);
        }

		/// <summary> Delete product category. </summary>
		/// <param name="Id">
		/// The product category's ID from the SalesAutoPilor system.
		/// </param>
		/// <returns> If success then True else False. </returns>
		public bool Delete(long? Id)
		{
			if (Id == null)
				return false;
			return GenericGet<bool>(string.Format("delprodcategory/{0}", Id));
		}

		/// <summary> Delete all product categories. </summary>
		/// <returns> Number of deleted items. </returns>
		public long Clear()
		{
			long Result = 0;
			List<ProductCategory> ProductCategories = All();
			if (ProductCategories != null)
				foreach (ProductCategory ProductCategory in ProductCategories)
					Result += Convert.ToInt64(Delete(ProductCategory.Id));
			return Result;
		}
		
		/// <summary> Retrieve the properties of the product category by Id. </summary>
        /// <param name="Id"> The product category's ID from the SalesAutoPilot system. </param>
        /// <returns> Object containing the properties of the product category. </returns>
        public ProductCategory ById(long? Id)
        {
            if (Id == null)
                return null;
            return GenericGet<ProductCategory>(string.Format("listprodcategories/{0}", Id));
        }

        /// <summary> Retrieve all products. </summary>
        /// <returns> List of objects containing the properties of the product categories. </returns>
        public List<ProductCategory> All()
        {
            return GenericGet<List<ProductCategory>>("listprodcategories");
        }
    }
}
