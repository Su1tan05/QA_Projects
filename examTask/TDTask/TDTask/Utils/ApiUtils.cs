using System.Collections.Generic;
using RestSharp;

namespace TDTask.Utils
{
    public static class ApiUtils
    {
        private static RestClient _client;
        private static RestRequest _request;

        public static IRestResponse SendPostRequest(string url, Dictionary<string,string> parameters, DataFormat dataFormat)
        {
            _client = new RestClient(url);
            _request = new RestRequest(string.Empty, Method.POST, dataFormat);
            if (parameters != null)
            {
                foreach (var item in parameters)
                {
                    _request.AddParameter(item.Key, item.Value);
                }
            }
            return _client.Execute(_request);
        }
    }
}
