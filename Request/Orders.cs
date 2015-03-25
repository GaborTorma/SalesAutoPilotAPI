using System;
using System.Collections.Generic;
using System.Linq;
using SalesAutoPilotAPI.Models;

namespace SalesAutoPilotAPI.Requests
{
    public interface IOrders : ISubscribers
    {
		long Add<T>(long ListId, T Order, long FormId) where T : Subscriber;
        long AddItems(long ListId, long Id, List<Item> Items);
        bool AddItem(long ListId, long Id, Item Item);
        bool AddItem(long ListId, Item Item);
		long DeleteItems(long ListId, long?[] ItemIds);
		long DeleteItems(long ListId, List<long?> ItemIds);
		long DeleteItems(long ListId, List<Item> Items);
		bool DeleteItem(long ListId, long? ItemId);
		bool DeleteItem(long ListId, Item Item);
		bool ModifyItem(long ListId, long ItemId, Item Item);
		bool ModifyItem(long ListId, Item Item);
		Item ById(long ListId, long ItemId);
    }

    public class Orders : Subscribers, IOrders
    {
        public Orders(string apiurl, string username, string password, string logfile)
            : base(apiurl, username, password, logfile)
        {
        }

		// Adding new order by ListId via FormId
        public long Add<T>(long ListId, T Order, long FormId) where T : Subscriber
        {
			return GenericPost<long>(string.Format("saveOrder/{0}/form/{1}", ListId, FormId), Order);
		}

        public long AddItems(long ListId, long Id, List<Item> Items)  // use Items only with same OrderId
		{
			Order Order = new Order();
			Order.Items = Items;
            return GenericPost<long>(string.Format("orderaddproduct/{0}/{1}/oi", ListId, Id), Order);
		}

        public bool AddItem(long ListId, long Id, Item Item)
		{
			List<Item> Items = new List<Item>();
			Items.Add(Item);
			return AddItems(ListId, Id, Items) > 0;
		}

        public bool AddItem(long ListId, Item Item)
		{
			if (Item == null || Item.OrderId == null)
				return false;
			return AddItem(ListId, Item.OrderId ?? -1, Item);
		}

		public long DeleteItems(long ListId, long?[] ItemIds)
		{
			if (ItemIds.Length < 1)
				return 0;
			return GenericPost<long>(string.Format("orderdelproduct/{0}/oi", ListId), ItemIds);
		}

		public long DeleteItems(long ListId, List<long?> ItemIds)
		{
			return DeleteItems(ListId, ItemIds);
		}

		public long DeleteItems(long ListId, List<Item> Items)
		{
			if (Items == null)
				return 0;
			return DeleteItems(ListId, Items.Select(I => I.Id).ToArray());
		}

		public bool DeleteItem(long ListId, long? ItemId)
		{
			return DeleteItems(ListId, new long?[] { ItemId }) > 0;
		}

		public bool DeleteItem(long ListId, Item Item)
		{
			if (Item == null)
				return false;
			return DeleteItem(ListId, Item.Id);
		}

		public bool ModifyItem(long ListId, long ItemId, Item Item)
		{
			if (Item == null)
				return false;
			List<Item> Items = new List<Item>();
			Items.Add(Item);
			Order Order = new Order();
			Order.Items = Items;
			return GenericPost<bool>(string.Format("ordermodproduct/{0}/oi/{1}", ListId, ItemId), Order);
		}

		public bool ModifyItem(long ListId, Item Item)
		{
			if (Item == null || Item.Id == null)
				return false;
			return ModifyItem(ListId, Item.Id ?? -1, Item);
		}

		public Item ById(long ListId, long ItemId)
		{
			return GenericGet<Item>(string.Format("orderlistproducts/{0}/{1}", ListId, ItemId));
		}
    }
}
