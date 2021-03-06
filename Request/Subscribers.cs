﻿using System;
using System.Collections.Generic;
using System.Linq;
using SalesAutoPilotAPI.Models;

namespace SalesAutoPilotAPI.Requests
{
    public interface ISubscribers : ICore
    {
		long Add<T>(long ListId, T Subscriber, long FormId) where T : Subscriber;
        bool Modify<T>(long ListId, decimal Id, T Subscriber, long FormId = 0) where T : Subscriber;
		bool Modify<T>(long ListId, string Field, string Value, T Subscriber, long FormId = 0) where T : Subscriber;
        bool Modify<T>(long ListId, string Field, long? Value, T Subscriber, long FormId = 0) where T : Subscriber;
        long ModifyByField<T>(long ListId, string Field, string Value, T Subscriber, long FormId = 0) where T : Subscriber;
        long ModifyByField<T>(long ListId, string Field, long? Value, T Subscriber, long FormId = 0) where T : Subscriber;
        long? Unsubscribe(long ListId, decimal Id);
        long? Unsubscribe(long ListId, string Email);
        long? Unsubscribe(long ListId, string Field, string Value);
        T ById<T>(long ListId, decimal Id)  where T : Subscriber;
        List<T> All<T>(long ListId, Ordering Ordering = null) where T : Subscriber;
        List<T> ByField<T>(long ListId, string Field, string Value) where T : Subscriber;
        List<T> ByField<T>(long ListId, string Field, long? Value) where T : Subscriber; 
        List<T> BySegmentId<T>(long ListId, long SegmentId, Ordering Ordering = null) where T : Subscriber;
		List<T> BySegmentIds<T>(long ListId, long[] SegmentIds, Ordering Ordering = null) where T : Subscriber;
        long CountInSegmentBySegmentId(long SegmentId);
        bool Delete(long ListId, long Id);
    }

    public class Subscribers : Core, ISubscribers
    {
        public Subscribers(string apiurl, string username, string password, string logfile)
            : base(apiurl, username, password, logfile)
        {
        }

        // Adding new subscriber by ListId via FormId
        public long Add<T>(long ListId, T Subscriber, long FormId) where T : Subscriber
        {
            return GenericPost<long>(string.Format("subscribe/{0}/form/{1}", ListId, FormId), Subscriber);
        }

		public bool Modify<T>(long ListId, decimal Id, T Subscriber, long FormId = 0) where T : Subscriber
        {
            return GenericPut<bool>(string.Format("update/{0}/form/{1}/record/{2}", ListId, FormId, Id), Subscriber);
        }

		public bool Modify<T>(long ListId, string Field, string Value, T Subscriber, long FormId = 0) where T : Subscriber
        {
            return GenericPut<bool>(string.Format("update/{0}/form/{1}/field/{2}/value/{3}", ListId, FormId, Field, Value), Subscriber);
        }

        public bool Modify<T>(long ListId, string Field, long? Value, T Subscriber, long FormId = 0) where T : Subscriber
        {
            if (Value == null)
                return false;
            return GenericPut<bool>(string.Format("update/{0}/form/{1}/field/{2}/value/{3}", ListId, FormId, Field, Value.ToString()), Subscriber);
        }

        protected long ModifyByField<T>(long ListId, List<T> ModifyDatas, long FormId = 0)
        {
            return GenericPut<long>(string.Format("update/{0}/form/{1}", ListId, FormId), ModifyDatas);
        }

        protected long ModifyByField<T>(long ListId, T ModifyData, long FormId = 0)
        {
            List<T> ModifyDatas = new List<T>();
            ModifyDatas.Add(ModifyData);
            return ModifyByField<T>(ListId, ModifyDatas, FormId);
        }

        public long ModifyByField<T>(long ListId, string Field, string Value, T Subscriber, long FormId = 0) where T : Subscriber
        {
            ModifyData<T> ModifyData = new ModifyData<T>();
            ModifyData.Field = Field;
            ModifyData.Value = Value;
            ModifyData.Data = Subscriber;
            return ModifyByField(ListId, ModifyData, FormId);
        }

        public long ModifyByField<T>(long ListId, string Field, long? Value, T Subscriber, long FormId = 0) where T : Subscriber
        {
            if (Value == null)
                return 0;
            return ModifyByField(ListId, Field, Value.ToString(), Subscriber, FormId);
        }

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

        public List<T> All<T>(long ListId, Ordering Ordering = null) where T : Subscriber
        {
            T[] Subscribers = GenericGet<T[]>(string.Format("list/{0}{1}", ListId, Ordering == null ? null : Ordering.URLParam()));
            return Subscribers == null ? new List<T>() : Subscribers.ToList();
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

        public List<T> ByField<T>(long ListId, string Field, long? Value) where T : Subscriber 
        {
            if (Value == null)
                return new List<T>();
            return ByField<T>(ListId, Field, Value.ToString());
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
