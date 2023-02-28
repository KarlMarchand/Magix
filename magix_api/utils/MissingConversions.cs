using System.Text.Json;

namespace magix_api.utils
{
    public class MissingConversions
    {
        public static async void AddNewItem<T>(T missingObject)
        {
            if (missingObject is null)
            {
                return;
            }
            
            string path = @"Data\MissingConversions";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            
            Type itemType = missingObject.GetType();
            string namePrefix = itemType.Name + "-";
            
            string? id = itemType.GetProperty("Id")?.GetValue(missingObject)?.ToString();
            if (id != null && id != string.Empty)
            {
                namePrefix+=id;
            } 
            else
            {
                string fileNumber = "01";
                string? lastFileNumber = Directory.EnumerateFiles(path).Max()?.Split(".")[0];
                if (lastFileNumber is not null && Int32.TryParse(lastFileNumber, out int numValue)) {
                    fileNumber = (numValue+1).ToString("00");
                }
                namePrefix+=fileNumber;
            }
            
            string fileName = Path.Combine(path, namePrefix+".json");

            if (!File.Exists(fileName))
            {
                using FileStream createStream = File.Create(fileName);
                var options = new JsonSerializerOptions { WriteIndented = true };
                await JsonSerializer.SerializeAsync<T>(createStream, missingObject, options);
                await createStream.DisposeAsync();
            }
        }
    }
}