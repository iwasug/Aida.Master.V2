using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Radyalabs.Core.Model;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Radyalabs.Core.Helper
{
    public static class JsonHelper
    {
        public static HttpResponseMessage JsonResponse(JsonModel json, HttpRequestMessage request)
        {
            string stringJson = JsonConvert.SerializeObject(json, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, ContractResolver = new JsonLowerCaseUnderscoreContractResolver() });
            var response = request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(stringJson, Encoding.UTF8, "application/json");

            return response;
        }

        public static HttpResponseMessage JsonResponseImage(JsonModel json, HttpRequestMessage request)
        {
            string stringJson = JsonConvert.SerializeObject(json);
            var response = request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(stringJson, Encoding.UTF8, "application/json");

            return response;
        }

        private class JsonLowerCaseUnderscoreContractResolver : DefaultContractResolver
        {
            private Regex regex = new Regex("(?!(^[A-Z]))([A-Z])");

            protected override string ResolvePropertyName(string propertyName)
            {
                return regex.Replace(propertyName, "_$2").ToLower();
            }
        }

        public static HttpResponseMessage JsonResponse(Task<JsonModel> task, HttpRequestMessage request)
        {
            throw new NotImplementedException();
        }
    }
}
