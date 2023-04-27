using System;
using System.Net.Http;
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
                //TODO add proper logging
                Console.WriteLine(ex.Message);
                response = "server request failed";
            }
            return response;
        }
    }
}
