using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Nuba.Finance.Google.Util
{
    internal class RestCaller
    {
        public string Get(string url)
        {
            HttpClient client = new HttpClient();
            var getTask = client.GetAsync(url);
            getTask.Wait();
            var response = getTask.Result;

            if (!response.IsSuccessStatusCode)
                return null;

            if (response.StatusCode == HttpStatusCode.NotFound)
                return string.Empty;

            var responseContentTask = response.Content.ReadAsStringAsync();
            responseContentTask.Wait();
            return responseContentTask.Result.Replace("\n", Environment.NewLine);
        }
    }
}
