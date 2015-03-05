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

        /// <summary> Retrieving coupon discount. </summary>
        /// <param name="CouponCheck"> Object containing the coupon code and the ordered product. </param>
        /// <returns> NetDiscount and GrossDiscount. </returns>
        public CouponCheck Check(CouponCheck CouponCheck)
        {
            return GenericPost<CouponCheck>("couponcheck", CouponCheck);
        }

        /// <summary> Retrieving coupon discount. </summary>
        /// <param name="CouponCode"> Coupon code from the SalesAutoPilot system. </param>
        /// <param name="OrderedProducts">
        /// A list of the products ordered.
        /// Required, if the coupon discount is given in percentage or it has been assigned to a product or a category.
        /// Required parameters: Id, Price, Quantity </param>
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
