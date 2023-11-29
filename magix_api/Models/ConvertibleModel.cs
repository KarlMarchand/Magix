using System.ComponentModel.DataAnnotations.Schema;

namespace magix_api;

public abstract class ConvertibleModel
{
    [NotMapped]
    private readonly Dictionary<string, string> _nameConversion;
    [NotMapped]
    private readonly Dictionary<string, string> _reverseConversion;

    public string Name { get; set; } = default!;

    protected ConvertibleModel(Dictionary<string, string> nameConversion)
    {
        _nameConversion = nameConversion;
        _reverseConversion = nameConversion.ToDictionary((i) => i.Value, (i) => i.Key);
    }

    public void ToFrontend()
    {
        Name = GetFrontendName();
    }

    public void ToServer()
    {
        Name = GetServerName();
    }

    public string GetFrontendName()
    {
        return ConvertName(Name, _nameConversion);
    }

    public string GetServerName()
    {
        return ConvertName(Name, _reverseConversion);
    }

    private string ConvertName(string originalData, Dictionary<string, string> dict)
    {
        string? result;

        if (!dict.TryGetValue(originalData, out result))
        {
            result = originalData;
            MissConversion();
        }

        return result;
    }

    protected abstract void MissConversion();
}
