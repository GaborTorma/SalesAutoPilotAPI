using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using SalesAutoPilotAPI.Models;

namespace SalesAutoPilotAPI
{
	public interface ICore
	{
		T RunRequest<T>(string resource, string requestMethod, object body = null);
		RequestResult RunRequest(string resource, string requestMethod, object body = null);
    }

	public class Core : ICore
	{
        protected string UserName;
        protected string Password;
        protected string APIURL;

        protected string LogFile;
        
        public Core(string apiurl, string username, string password, string logfile)
        {
            UserName = username;
            Password = password;
            APIURL= apiurl;
            LogFile = logfile;
        }

        internal IWebProxy Proxy;

        public bool IsJson(object value)
        {
            if (value != null && value.ToString().Length > 2 && (value.ToString()[0] == '{' || value.ToString()[0] == '['))
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
                if (obj == null)
                    return default(T);
            return (T)Convert.ChangeType(obj, u);
        }

        public T RunRequest<T>(string resource, string requestMethod, object body = null)
        {
            Type Type = typeof(T);
            var response = RunRequest(resource, requestMethod, body);
            Log("Response", response.Content);
            var obj = (object)response.Content;
            if (IsJson(obj))
                obj = JsonConvert.DeserializeObject<T>(response.Content, new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
            else
                if (Type != typeof(string))
                    if (Type.IsClass)
                        obj = null;
                    else
                        if (IsNumber(obj))
                            obj = Convert.ToDouble(obj);
                        else
                            if (Nullable.GetUnderlyingType(Type) != null)
                                obj = null;
                            else
                                obj = 0;
            Log("Result", obj.ToString());
            return ConvertObject<T>(obj);
        }

        protected void Log(string Title, string Message)
        {
            if (LogFile != null)
                try
                {
                    System.IO.StreamWriter File = new System.IO.StreamWriter(LogFile, true);
                    File.WriteLine(string.Format("{0} | {1}: {2}", DateTime.Now, Title, Message));
                    File.Close();
                }
                catch
                { }
        }

        public RequestResult RunRequest(string resource, string requestMethod, object body = null)
        {
			string json = null;
			try
            {
                var requestUrl = APIURL;
                if (!requestUrl.EndsWith("/"))
                    requestUrl += "/";

                requestUrl += resource;

                Log("requestUrl",requestUrl);

                HttpWebRequest req = WebRequest.Create(requestUrl) as HttpWebRequest;
                req.ContentType = "application/json";

                if (this.Proxy != null)
                    req.Proxy = this.Proxy;

                req.Headers["Authorization"] = GetAuthHeader(UserName, Password);
                req.PreAuthenticate = true;

                req.Method = requestMethod; //GET POST PUT DELETE
                req.Accept = "application/json";//, application/xml, text/json, text/x-json, text/javascript, text/xml";

                Log("requestMethod", requestMethod);

                req.ContentLength = 0;

                if (body != null)
                {
                    json = JsonConvert.SerializeObject(body, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                    Log("body", json);

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

                Log("responseFromServer", responseFromServer);
                return new RequestResult()
                {
                    Content = responseFromServer,
                    HttpStatusCode = response.StatusCode
                };
            }
            catch (WebException ex)
            {
				throw new WebException(ex.Message + " " + ex.Response.Headers.ToString() + Environment.NewLine + json, ex);
            }
        }

        protected T GenericGet<T>(string resource)
        {

            return RunRequest<T>(resource, "GET");
        }

        protected bool GenericDelete(string resource)
        {
            var res = RunRequest(resource, "DELETE");
            return res.HttpStatusCode == HttpStatusCode.OK && res.Content == "1";  //|| res.HttpStatusCode == HttpStatusCode.NoContent;
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
