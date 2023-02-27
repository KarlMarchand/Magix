using System.Text.Json;

namespace magix_api.utils
{
    public class MissingConversions
    {
        public static async void AddNewItem<T>(T missingObject)
        {
            string path = @"..\Data\MissingConversions";
            string fileName = "01.json";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string? lastFileNumber = Directory.EnumerateFiles(path).Max()?.Split(".")[0];
            if (lastFileNumber is not null && Int32.TryParse(lastFileNumber, out int numValue)) {
                fileName = (numValue+1).ToString("00")+".json";
            }

            using FileStream createStream = File.Create(fileName);
            var options = new JsonSerializerOptions { WriteIndented = true };
            await JsonSerializer.SerializeAsync<T>(createStream, missingObject, options);
            await createStream.DisposeAsync();        
        }
    }
}