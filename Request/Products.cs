﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SalesAutoPilotAPI.Models;

namespace SalesAutoPilotAPI.Requests
{
    public interface IProducts : ICore
    {
        long? Add(Product Product);
        long? Modify(long Id, Product Product);
        bool Delete(decimal Id);
        Product ProductById(decimal Id);
        //Product[] GetAll();
    }

	public class Products : Core, IProducts
    {
        public Products(string apiurl, string username, string password)
            : base(apiurl, username, password)
        {
        }

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

        public long? Modify(long Id, Product Product)
        {
            return GenericPost<long?>(string.Format("modifyproduct/{0}", Id), Product);
        }

        public bool Delete(decimal id)
        {
            return GenericDelete(string.Format("deleteproduct/{0}", id));
        }

        public Product ProductById(decimal Id)  
        {
            return GenericGet<Product>(string.Format("getproduct/{0}", Id));
        }

        /*public Product[] GetAll()  // not working
        {
            return GenericGet<Product[]>("listproducts");
        }*/
    }
}