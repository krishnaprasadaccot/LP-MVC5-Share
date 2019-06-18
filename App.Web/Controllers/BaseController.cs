
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace App.Web.Controllers
{
  
    public class BaseController : Controller
    {
        
        public BaseController()
        {

        }
        protected T GetSession<T>(string key)
        {
            return Session[key] != null ? (T)Session[key] : default;
        }
        protected void SetSession(string key,object value)
        {
           Session[key]  = value;
        }

        protected HttpResponseMessage ApiPost<T>(string address,string uri,T data)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(address);
                    var postData = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                    var response = client.PostAsync(uri, postData);
                    response.Wait();
                    return response.Result;
                    
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        protected T ApiGet<T>(string address, string uri)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(address);
                    var response =  client.GetAsync(uri).Result;
                    return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}