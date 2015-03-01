using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SalesAutoPilotAPI.Models
{
    public enum OrderDirection
    {
        Ascending,
        Descending
    };


    public class Ordering
    {
        public string OrderByFieldName { get; set; }
        public OrderDirection OrderDirection { get; set; }
        public long? Limit { get; set; }
        public Ordering(string orderbyfieldname = "Id", OrderDirection orderdirection = OrderDirection.Ascending, long? limit = null)
        {
            OrderByFieldName = orderbyfieldname;
            OrderDirection = orderdirection;
            Limit = limit;
        }
        protected readonly string[] OrderDirectionString = new string[2] { "asc", "desc" };
        public string URLParam()
        {
            string Result = string.Format("/order/{0}/{1}",
                                          OrderByFieldName,
                                          OrderDirectionString[(Byte)OrderDirection]);
            if (Limit != null)
                Result += string.Format("/{0}", Limit);
            return Result;
        }
    };
}
