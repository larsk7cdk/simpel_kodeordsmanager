namespace SimpelKodeordsmanager.API.Controllers.Shared;

public class LowerCaseParameterTransformer : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value)
    {
        if (value == null) return null;
        return value.ToString()?.ToLowerInvariant();
    }
}