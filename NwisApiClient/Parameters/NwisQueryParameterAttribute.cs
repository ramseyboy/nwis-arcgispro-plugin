namespace NwisApiClient.Parameters;

public class NwisQueryParameterAttribute: Attribute
{
    public string Name { get; set; }

    public NwisParameterType ParameterType { get; set; }

    public NwisQueryParameterAttribute(string name, NwisParameterType parameterType)
    {
        Name = name;
        ParameterType = parameterType;
    }
}