using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using SalesAutoPilotAPI.Models;

namespace SalesAutoPilotAPI
{
	public interface ICore
	{
		T GetByPageUrl<T>(string pageUrl, int perPage=100);
		T RunRequest<T>(string resource, string requestMethod, object body = null);
		RequestResult RunRequest(string resource, string requestMethod, object body = null);
    }

	public class Core : ICore
	{
        protected string UserName;
        protected string Password;
        protected string APIURL;
        
        public Core(string apiurl, string username, string password)
        {
            UserName = username;
            Password = password;
            APIURL= apiurl;
        }

        internal IWebProxy Proxy;

        public T GetByPageUrl<T>(string pageUrl, int perPage = 100)
        {
            if (string.IsNullOrEmpty(pageUrl))
                return JsonConvert.DeserializeObject<T>("");

            var resource = Regex.Split(pageUrl, "api/v2/").Last() + "&per_page=" + perPage;
            return RunRequest<T>(resource, "GET");
        }

        public bool IsJson(object value)
        {
            if (value != null && (value.ToString()[0] == '{' || value.ToString()[0] == '['))
                return true;
            else
                return false;
        }

        public bool IsNumber(object value)
        {
            double Number;
            return double.TryParse(value.ToString(), out Number);
        }

        public T ConvertObject<T>(object obj)
        {
            Type t = typeof(T);
            Type u = Nullable.GetUnderlyingType(t);
            if (u == null)
                return (T)Convert.ChangeType(obj, t);
            else
            {
                if (obj == null)
                    return default(T);
                return (T)Convert.ChangeType(obj, u);
            }
        }
        
        public T RunRequest<T>(string resource, string requestMethod, object body = null)
        {
            var response = RunRequest(resource, requestMethod, body);
            var obj = (object)response.Content;
            if (IsJson(obj))
                obj = JsonConvert.DeserializeObject<T>(response.Content, new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
            else if (!IsNumber(obj) && typeof(T) != typeof(string))
                {
                    if (Nullable.GetUnderlyingType(typeof(T)) != null)
                        obj = null;
                    else
                        obj = 0;
                }
            return ConvertObject<T>(obj);
        }

        public RequestResult RunRequest(string resource, string requestMethod, object body = null)
        {
            try
            {
                var requestUrl = APIURL;
                if (!requestUrl.EndsWith("/"))
                    requestUrl += "/";

                requestUrl += resource;

                HttpWebRequest req = WebRequest.Create(requestUrl) as HttpWebRequest;
                req.ContentType = "application/json";

                if (this.Proxy != null)
                    req.Proxy = this.Proxy;

                req.Headers["Authorization"] = GetAuthHeader(UserName, Password);
                req.PreAuthenticate = true;

                req.Method = requestMethod; //GET POST PUT DELETE
                req.Accept = "application/json";//, application/xml, text/json, text/x-json, text/javascript, text/xml";

                req.ContentLength = 0;
                
                if (body != null)
                {
                    var json = JsonConvert.SerializeObject(body, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

                    byte[] formData = UTF8Encoding.UTF8.GetBytes(json);
                    req.ContentLength = formData.Length;

                    var dataStream = req.GetRequestStream();
                    dataStream.Write(formData, 0, formData.Length);
                    dataStream.Close();
                }
                var res = req.GetResponse();
                HttpWebResponse response = res as HttpWebResponse;
                var responseStream = response.GetResponseStream();
                var reader = new StreamReader(responseStream);
                string responseFromServer = reader.ReadToEnd();

                return new RequestResult()
                {
                    Content = responseFromServer,
                    HttpStatusCode = response.StatusCode
                };
            }
            catch (WebException ex)
            {
                throw new WebException(ex.Message + " " + ex.Response.Headers.ToString(), ex);
            }
        }

        protected T GenericGet<T>(string resource)
        {

            return RunRequest<T>(resource, "GET");
        }

        protected T GenericPagedGet<T>(string resource, int? perPage = null, int? page = null)
        {
            var parameters = new Dictionary<string, string>();

            var paramString = "";
            if (perPage.HasValue)
            {
                parameters.Add("per_page", perPage.Value.ToString(CultureInfo.InvariantCulture));
            }

            if (page.HasValue)
            {
                parameters.Add("page", page.Value.ToString(CultureInfo.InvariantCulture));
            }

            if (parameters.Any())
            {
                paramString = "?";
                foreach (string parameter in parameters.Select(x => x.Key + "=" + x.Value))
                    paramString = paramString + "&" + parameter;
            }


            return GenericGet<T>(resource + paramString);
        }

        protected T GenericPagedSortedGet<T>(string resource, int? perPage = null, int? page = null, string sortCol = null, bool? sortAscending = null)
        {
            var parameters = new Dictionary<string, string>();

            var paramString = "";
            if (perPage.HasValue)
            {
                parameters.Add("per_page", perPage.Value.ToString(CultureInfo.InvariantCulture));
            }

            if (page.HasValue)
            {
                parameters.Add("page", page.Value.ToString(CultureInfo.InvariantCulture));
            }

            if (!string.IsNullOrEmpty(sortCol))
            {
                parameters.Add("sort_by", sortCol);
            }

            if (sortAscending.HasValue)
            {
                parameters.Add("sort_order", sortAscending.Value ? "" : "desc");
            }

            if (parameters.Any())
            {
                paramString = "?";
                foreach (string parameter in parameters.Select(x => x.Key + "=" + x.Value))
                    paramString = paramString + "&" + parameter;
            }


            return GenericGet<T>(resource + paramString);
        }
        
        protected bool GenericDelete(string resource)
        {
            var res = RunRequest(resource, "DELETE");
            return res.HttpStatusCode == HttpStatusCode.OK && res.Content == "1";//|| res.HttpStatusCode == HttpStatusCode.NoContent;
        }

        protected T GenericPost<T>(string resource, object body = null)
        {
            var res = RunRequest<T>(resource, "POST", body);
            return res;
        }

        protected bool GenericBoolPost(string resource, object body = null)
        {
            var res = RunRequest(resource, "POST", body);
            return res.HttpStatusCode == HttpStatusCode.OK;
        }

        protected T GenericPut<T>(string resource, object body = null)
        {
            var res = RunRequest<T>(resource, "PUT", body);
            return res;
        }

        protected bool GenericBoolPut(string resource, object body = null)
        {
            var res = RunRequest(resource, "PUT", body);
            return res.HttpStatusCode == HttpStatusCode.OK;
        }

        protected string GetAuthHeader(string userName, string password)
        {
            string auth = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", userName, password)));
            return string.Format("Basic {0}", auth);
        }

    }
}
