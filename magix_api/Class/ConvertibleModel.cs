namespace magix_api.Class;

public class ConvertibleModel
{
    private readonly Dictionary<string, string> _nameConversion;
    private readonly Dictionary<string, string> _reverseConversion;

    public string Name { get; set; } = "";

    protected ConvertibleModel(Dictionary<string, string> nameConversion)
    {
        _nameConversion = nameConversion;
        _reverseConversion = nameConversion.ToDictionary((i) => i.Value, (i) => i.Key);
    }

    public string ToFrontend()
    {
        Name = ConvertName(Name, _nameConversion);
        return Name;
    }

    public string ToServer()
    {
        Name = ConvertName(Name, _reverseConversion);
        return Name;
    }

    public static string ConvertName(string originalData, Dictionary<string, string> dict)
    {
        string? result;

        if (!dict.TryGetValue(originalData, out result))
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(string.Format("Can't seems to find the {0} string in specified converter.", originalData));
            Console.ForegroundColor = ConsoleColor.White;
        }

        return result ?? originalData;
    }
}
