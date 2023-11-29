using System.ComponentModel.DataAnnotations.Schema;

namespace magix_api;

// The class has a limitation where ServerResponse<string> will always be valid
[NotMapped]
public class ServerResponse<T>
{
    private readonly T? _content;
    private readonly string? _error;

    public ServerResponse(T validContent)
    {
        _content = validContent;
        _error = null;
    }

    private ServerResponse(string errorMessage)
    {
        _content = default;
        _error = errorMessage;
    }

    public static ServerResponse<T> GetServerResponseFromError(string errorMessage)
    {
        return new ServerResponse<T>(errorMessage);
    }

    public bool IsError => _error != null;
    public bool IsValid => !IsError;

    public T? Content => _content;
    public string? Error => _error;
}
