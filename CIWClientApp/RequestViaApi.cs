using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mime;
using System.Security.Policy;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CIWClientApp
{
    internal class RequestViaApi
    {
        const string URI = "http://localhost:58112/api/convertintowords";

        public static async Task<string> RequestAsync(string numerical)
        {
            string response;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    response = await client.GetStringAsync($"{URI}/{numerical}");
                }
            }
            catch (Exception ex)
            {
                //myCustomLogger.LogException(ex);
                response = "server request failed";
            }
            return response;
        }
    }
}
