using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;

namespace PiDevEsprit.ReverseProxy_Service
{
    public static  class Forward_Proxy_Service
    {
        private  static HttpClient client;
       
        public static async Task<System.Web.Mvc.JsonResult> ForwardToApi(string endpoint, object payload, Func<IEnumerable<KeyValuePair<string, JToken>>, System.Web.Mvc.JsonResult> success, Func<string, System.Web.Mvc.JsonResult> failure)
        {
            var str = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(payload), System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await client.PostAsync(endpoint, str);
            var responseJson = await responseMessage.Content.ReadAsStringAsync();
            var json_response_fields = JObject.Parse(responseJson) as IEnumerable<KeyValuePair<string, JToken>>;
            if (responseMessage.IsSuccessStatusCode)
            {
                return success(json_response_fields);
            }
            else
            {
                var mes = json_response_fields.Where(x => x.Key == "message").Select(x => x.Value).First().ToString();
                return failure(mes);
            }
        }
    }
}