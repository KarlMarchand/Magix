using System.Text.Json;

namespace magix_api.utils
{
    public static class GameServerAPI
    {
        public static async Task<T?> CallApi<T>(string service, Dictionary<string, string>? data = null)
        {
            string apiURL = "https://magix.apps-de-cours.com/api/" + service;

            var client = new HttpClient();

            HttpResponseMessage response;

            if (data != null)
            {
                var body = new FormUrlEncodedContent(data);
                response = await client.PostAsync(apiURL, body);
            }
            else
            {
                response = await client.GetAsync(apiURL);
            }

            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();

            if (json.Contains("<br"))
            {
                Console.WriteLine(json);
            }

            T? result = default;

            Console.WriteLine(json);
            try
            {
                result = JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions(JsonSerializerDefaults.Web));
            }
            catch (Exception e)
            {
                Console.WriteLine(json);
            }
            return result;

        }
    }


}