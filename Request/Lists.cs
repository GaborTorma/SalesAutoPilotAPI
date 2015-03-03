using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SalesAutoPilotAPI;
using SalesAutoPilotAPI.Models;

namespace SalesAutoPilotAPI.Requests
{
    public interface ILists : ICore
    {
        long AddSubscriber(long ListId, Subscriber Subscriber, long FormId = 0);
        long AddOrder(long ListId, Order Order, long FormId);
        long Modify(long ListId, decimal Id, Subscriber Subscriber, long FormId = 0);
        long Modify(long ListId, string Field, string Value, Subscriber Subscriber, long FormId = 0);
        long? Unsubscribe(long ListId, decimal Id);
        long? Unsubscribe(long ListId, string Email);
        long? Unsubscribe(long ListId, string Field, string Value);
        Subscriber SubscriberById(long Form, decimal Id);
        List<Subscriber> SubscribersByListId(long ListId, Ordering Ordering = null);
        List<Subscriber> SubscribersByField(long ListId, string Field, string Value); //, Ordering Ordering = null);  // Ordering not working in RESTful API
        List<Subscriber> SubscribersBySegmentId(long ListId, long SegmentId, Ordering Ordering = null);
        List<Subscriber> SubscribersBySegmentIds(long ListId, long[] SegmentIds, Ordering Ordering = null);
        long CountInSegmentBySegmentId(long SegmentId);
        bool Delete(long ListId, long Id);
    }

    public class Lists : Core, ILists
    {
        public Lists(string apiurl, string username, string password)
            : base(apiurl, username, password)
        {
        }

        // Adding new subscriber by ListId via FormId
        public long AddSubscriber(long ListId, Subscriber Subscriber, long FormId = 0)
        {
            return GenericPost<long>(string.Format("subscribe/{0}/form/{1}", ListId, FormId), Subscriber);
        }

        // Adding new order by ListId via FormId
        public long AddOrder(long ListId, Order Order, long FormId)
        {
            foreach (Product Product in Order.Products)
                if (Product.Id == null)
                {
                    SalesAutoPilot SalesAutoPilot = new SalesAutoPilot(APIURL, UserName, Password);
                    Product.Id = SalesAutoPilot.Products.Add(Product);
                    if (Product.Id == null)
                        return 0;
                }
                
            return GenericPost<long>(string.Format("saveOrder/{0}/form/{1}", ListId, FormId), Order);
        }

        public long Modify(long ListId, decimal Id, Subscriber Subscriber, long FormId = 0)
        {
            long? Result = GenericPut<long?>(string.Format("update/{0}/form/{1}/record/{2}", ListId, FormId, Id), Subscriber);
            return Result == null ? 0 : (long)Result;
        }

        public long Modify(long ListId, string Field, string Value, Subscriber Subscriber, long FormId = 0)
        {
            long? Result = GenericPut<long?>(string.Format("update/{0}/form/{1}/field/{2}/value/{3}", ListId, FormId, Field, Value), Subscriber);
            return Result == null ? 0 : (long)Result;
        }

        /* not supported     
        public long BatchUpdate(long ListId, string UpdateByFieldName, List<Subscriber> Subscribers, long FormId = 0)  
        {
            long? Result = GenericPut<long?>(string.Format("update/{0}/form/{1}", ListId, FormId), Subscribers);
            return Result == null ? 0 : (long)Result;
        }*/

        public long? Unsubscribe(long ListId, decimal Id)
        {
            return GenericGet<long?>(string.Format("unsubscribe/{0}/record/{1}", ListId, Id));
        }

        public long? Unsubscribe(long ListId, string Email)
        {
            return GenericGet<long?>(string.Format("unsubscribe/{0}/email/{1}", ListId, Email));
        }

        public long? Unsubscribe(long ListId, string Field, string Value)
        {
            return GenericGet<long?>(string.Format("unsubscribe/{0}/field/{1}/value/{2}", ListId, Field, Value));
        }

        public List<Subscriber> SubscribersByListId(long ListId, Ordering Ordering = null)
        {
            Subscriber[] Subscribers = GenericGet<Subscriber[]>(string.Format("list/{0}{1}", ListId,
                                                                              Ordering == null ? null : Ordering.URLParam()));
            return Subscribers == null ? new List<Subscriber>() : Subscribers.ToList();
        }

        public Subscriber SubscriberById(long ListId, decimal Id) 
        {
            return GenericGet<Subscriber>(string.Format("list/{0}/record/{1}", ListId, Id));
        }

        public List<Subscriber> SubscribersByField(long ListId, string Field, string Value) //, Ordering Ordering = null)  
        {
            /* Ordering not working in RESTful API
            Subscriber[] Subscribers = GenericGet<Subscriber[]>(string.Format("list/{0}/field/{1}/value/{2}{3}", ListId, Field, Value,
                                                                              Ordering == null ? null : Ordering.URLParam()));  */
            Subscriber[] Subscribers = GenericGet<Subscriber[]>(string.Format("list/{0}/field/{1}/value/{2}", ListId, Field, Value));
            return Subscribers == null ? new List<Subscriber>() : Subscribers.ToList();
        }

        public List<Subscriber> SubscribersBySegmentId(long ListId, long SegmentId)
        {
            return SubscribersBySegmentIds(ListId, new long[1] { SegmentId });
        }

        public List<Subscriber> SubscribersBySegmentId(long ListId, long SegmentId, Ordering Ordering = null)
        {
            return SubscribersBySegmentIds(ListId, new long[1] { SegmentId }, Ordering);
        }

        public List<Subscriber> SubscribersBySegmentIds(long ListId, long[] SegmentIds, Ordering Ordering = null)  // Segment union will be used.
        {
            Subscriber[] Subscribers = GenericPost<Subscriber[]>(string.Format("filteredlist/{0}{1}", ListId,
                                                                               Ordering == null ? null : Ordering.URLParam()),
                                                                 SegmentIds);                                                                               
            return Subscribers == null ? new List<Subscriber>() : Subscribers.ToList();
        }

        public long CountInSegmentBySegmentId(long SegmentId)
        {
            return GenericGet<long>(string.Format("getsegmentnum/{0}", SegmentId));
        }

        public bool Delete(long ListId, long Id)
        {
            return GenericDelete(string.Format("delete/{0}/record/{1}", ListId, Id));
        }
    }
}
