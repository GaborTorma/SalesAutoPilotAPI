using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SalesAutoPilotAPI.Models;

namespace SalesAutoPilotAPI.Requests
{
    public interface ISend : ICore
    {
        long SendMessages(List<MessageParamter> MessageParamter);
        long SendMessage(MessageParamter MessageParamter);
        long SendMessage(long SendId, long ContactId, string TextContent = null, string HTMLContent = null);
    }

    public class Send : Core, ISend
    {
        public Send(string apiurl, string username, string password)
            : base(apiurl, username, password)
        {
        }

        public long SendMessages(List<MessageParamter> MessageParamters)
        {
            return GenericPost<long>("sendmail", MessageParamters);
        }

        public long SendMessage(MessageParamter MessageParamter)
        {
            if (MessageParamter.ContactId == null)
                return 0;
            List<MessageParamter> MessageParamters = new List<MessageParamter>();
            MessageParamters.Add(MessageParamter);
            return SendMessage(MessageParamter);
        }

        public long SendMessage(long SendId, long ContactId, string TextContent = null, string HTMLContent = null)
        {
            MessageParamter MessageParamter = new MessageParamter(SendId, ContactId, TextContent, HTMLContent);
            return SendMessage(MessageParamter);
        }
    }
}