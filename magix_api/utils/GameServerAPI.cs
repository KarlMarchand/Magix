using System.Text.Json;

namespace magix_api.utils
{
    public static class GameServerAPI
    {
        public static async Task<ServerResponse<T>> CallApi<T>(string service, Dictionary<string, string>? data = null)
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

            ServerResponse<T> result;
            try
            {
                T answer = JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions(JsonSerializerDefaults.Web))!;
                result = new(answer);
            }
            catch (JsonException)
            {
                // Either failed entirely or the answer from the game server is a string detailing a bad request
                if (json.Contains("<br"))
                {
                    Console.WriteLine(json);
                }
                result = new(json);
            }
            
            return result;
        }
    }


}