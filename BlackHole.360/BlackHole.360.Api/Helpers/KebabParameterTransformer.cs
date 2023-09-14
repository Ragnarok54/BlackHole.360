using System.Text.RegularExpressions;

namespace BlackHole._360.Api.Helpers;

public partial class KebabParameterTransformer : IOutboundParameterTransformer
{
    [GeneratedRegex("([a-z])([A-Z])", RegexOptions.Compiled)]
    private static partial Regex MyRegex();

    private static readonly Regex _camelCaseRegex = MyRegex();

    public string? TransformOutbound(object? value)
    {
        if (value is string stringValue)
        {
            return _camelCaseRegex.Replace(stringValue!.ToString(), "$1-$2").ToLower();
        }
        else
        {
            return null;
        }
    }
}
