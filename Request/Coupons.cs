using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SalesAutoPilotAPI.Models;

namespace SalesAutoPilotAPI.Requests
{
    public interface ICoupons : ICore
    {
        CouponCheck Check(CouponCheck CouponCheck);
        CouponCheck Check(string CouponCode, List<Product> OrderedProducts);
        double NetDiscountByCode(string CouponCode, List<Product> OrderedProducts = null);
        double GrossDiscountByCode(string CouponCode, List<Product> OrderedProducts = null);
    }

    public class Coupons : Core, ICoupons
    {
        public Coupons(string apiurl, string username, string password)
            : base(apiurl, username, password)
        {
        }

        /// <summary> Kuponkedvezmény lekérdezése. </summary>
        /// <param name="CouponCheck"> A kupon kódot és a megrendelt termékekt tartalamzó objektum. </param>
        /// <returns> NetDiscount and GrossDiscount. </returns>
        public CouponCheck Check(CouponCheck CouponCheck)
        {
            return GenericPost<CouponCheck>("couponcheck", CouponCheck);
        }

        /// <summary> Kuponkedvezmény lekérdezése. </summary>
        /// <param name="CouponCode"> A kupon kódja a SalesAutoPilot rendszerből. </param>
        /// <param name="OrderedProducts">
        /// A megrendelt termékeket tartalmazó lista.
        /// Szükséges, ha a kupon kedvezmény %-ban van megadva vagy, ha termékhez, termékkategóriához van rendelve.
        /// Szükséges tulajdonságok: Id, Price, Quantity </param>
        /// <returns> NetDiscount and GrossDiscount. </returns>
        public CouponCheck Check(string CouponCode, List<Product> OrderedProducts = null)
        {
            CouponCheck CouponCheck = new CouponCheck();
            CouponCheck.Code = CouponCode;
            CouponCheck.OrderedProducts = OrderedProducts;
            return Check(CouponCheck);
        }

        public double NetDiscountByCode(string CouponCode, List<Product> OrderedProducts = null)
        {
            CouponCheck CouponCheck = Check(CouponCode, OrderedProducts);
            if (CouponCheck == null)
                return 0;
            return CouponCheck.NetDiscount ?? 0;
        }

        public double GrossDiscountByCode(string CouponCode, List<Product> OrderedProducts = null)
        {
            CouponCheck CouponCheck = Check(CouponCode, OrderedProducts);
            if (CouponCheck == null)
                return 0;
            return CouponCheck.GrossDiscount ?? 0;
        }
    }
}
