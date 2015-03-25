using System;
using System.Collections.Generic;
using System.Linq;
using SalesAutoPilotAPI.Models;

namespace SalesAutoPilotAPI.Requests
{
    public interface ISubscribers : ICore
    {
		long Add<T>(long ListId, T Subscriber, long FormId) where T : Subscriber;
		long Modify<T>(long ListId, decimal Id, T Subscriber, long FormId = 0) where T : Subscriber;
		long Modify<T>(long ListId, string Field, string Value, T Subscriber, long FormId = 0) where T : Subscriber;
        long? Unsubscribe(long ListId, decimal Id);
        long? Unsubscribe(long ListId, string Email);
        long? Unsubscribe(long ListId, string Field, string Value);
        T ById<T>(long ListId, decimal Id)  where T : Subscriber;
        T ByListId<T>(long ListId, Ordering Ordering = null)  where T : Subscriber;
		List<T> ByField<T>(long ListId, string Field, string Value) where T : Subscriber; //, Ordering Ordering = null);  // Ordering not working in RESTful API
		List<T> BySegmentId<T>(long ListId, long SegmentId, Ordering Ordering = null) where T : Subscriber;
		List<T> BySegmentIds<T>(long ListId, long[] SegmentIds, Ordering Ordering = null) where T : Subscriber;
        long CountInSegmentBySegmentId(long SegmentId);
        bool Delete(long ListId, long Id);
    }

    public class Subscribers : Core, ISubscribers
    {
        public Subscribers(string apiurl, string username, string password)
            : base(apiurl, username, password)
        {
        }

        // Adding new subscriber by ListId via FormId
        public long Add<T>(long ListId, T Subscriber, long FormId) where T : Subscriber
        {
            return GenericPost<long>(string.Format("subscribe/{0}/form/{1}", ListId, FormId), Subscriber);
        }

		public long Modify<T>(long ListId, decimal Id, T Subscriber, long FormId = 0) where T : Subscriber
        {
            long? Result = GenericPut<long?>(string.Format("update/{0}/form/{1}/record/{2}", ListId, FormId, Id), Subscriber);
            return Result == null ? 0 : (long)Result;
        }

		public long Modify<T>(long ListId, string Field, string Value, T Subscriber, long FormId = 0) where T : Subscriber
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

		public T ByListId<T>(long ListId, Ordering Ordering = null) where T : Subscriber
        {
            return GenericGet<T>(string.Format("list/{0}{1}", ListId, Ordering == null ? null : Ordering.URLParam()));
        }

		public T ById<T>(long ListId, decimal Id) where T : Subscriber
        {
            return GenericGet<T>(string.Format("list/{0}/record/{1}", ListId, Id));
        }

		public List<T> ByField<T>(long ListId, string Field, string Value) where T : Subscriber  //, Ordering Ordering = null)  
        {
            /* Ordering not working in RESTful API
            Subscriber[] Subscribers = GenericGet<Subscriber[]>(string.Format("list/{0}/field/{1}/value/{2}{3}", ListId, Field, Value,
                                                                              Ordering == null ? null : Ordering.URLParam()));  */
            T[] Subscribers = GenericGet<T[]>(string.Format("list/{0}/field/{1}/value/{2}", ListId, Field, Value));
            return Subscribers == null ? new List<T>() : Subscribers.ToList();
        }

		public List<T> BySegmentId<T>(long ListId, long SegmentId) where T : Subscriber
        {
			return BySegmentIds<T>(ListId, new long[1] { SegmentId });
        }

		public List<T> BySegmentId<T>(long ListId, long SegmentId, Ordering Ordering = null) where T : Subscriber
        {
			return BySegmentIds<T>(ListId, new long[1] { SegmentId }, Ordering);
        }

		public List<T> BySegmentIds<T>(long ListId, long[] SegmentIds, Ordering Ordering = null) where T : Subscriber // Segment union will be used.
        {
            T[] Subscribers = GenericPost<T[]>(string.Format("filteredlist/{0}{1}", ListId,
				Ordering == null ? null : Ordering.URLParam()),
				SegmentIds);                                                                               
            return Subscribers == null ? new List<T>() : Subscribers.ToList();
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
