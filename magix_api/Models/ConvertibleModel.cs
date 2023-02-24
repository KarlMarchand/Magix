namespace magix_api;

public class ConvertibleModel
{
    private readonly Dictionary<string, string> _nameConversion;
    private readonly Dictionary<string, string> _reverseConversion;

    public string Name { get; set; } = default!;

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
            Console.WriteLine(string.Format("Can't seems to find the {0} string in specified converter.", originalData));
        }

        return result ?? originalData;
    }
}
