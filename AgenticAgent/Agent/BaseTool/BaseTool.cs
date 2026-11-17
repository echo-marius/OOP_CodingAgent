using System.Reflection;
using System.Text.Json;
using AgenticAgent.Models.Tools;

//with the help of ai
public abstract class BaseTool
{
    private readonly MethodInfo _executeMethod;
    private readonly ToolMetadataAttribute _metadata;
    
    protected BaseTool()
    {
        Type concreteType = GetType();

        _executeMethod = concreteType
                             .GetMethods(BindingFlags.Public | BindingFlags.NonPublic  | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                             .FirstOrDefault(m => m.GetCustomAttribute<ToolMetadataAttribute>() is not null)
                         ?? throw new InvalidOperationException($"{concreteType.Name} must have a method decorated with [ToolMetadata].");

        _metadata = _executeMethod.GetCustomAttribute<ToolMetadataAttribute>()!;
    }
    public string Name => _metadata.Name;

    public ToolDefinition GetSchema()
    {
        var properties = new Dictionary<string, ToolProperty>();
        var required = new List<string>();

        foreach (ParameterInfo param in _executeMethod.GetParameters())
        {
            var paramAttr = param.GetCustomAttribute<ToolParamAttribute>();

            properties[param.Name!] = new ToolProperty
            {
                Type = MapClrType(param.ParameterType),
                Description = paramAttr?.Description,
            };

            if (!param.HasDefaultValue)
            {
                required.Add(param.Name!);
            }
        }

        return new ToolDefinition
        {
            Function = new FunctionDefinition
            {
                Name = _metadata.Name,
                Description = _metadata.Description,
                Parameters = new ToolParameters
                {
                    Properties = properties,
                    Required = required.Count > 0 ? required : null
                }
            }
        };
    }

    public async Task<string> InvokeAsync(JsonElement arguments)
    {
        ParameterInfo[] parameters = _executeMethod.GetParameters();
        object?[] callArgs = new object?[parameters.Length];

        for (int i = 0; i < parameters.Length; i++)
        {
            ParameterInfo param = parameters[i];

            if (arguments.ValueKind == JsonValueKind.Object &&
                arguments.TryGetProperty(param.Name!, out JsonElement propValue))
            {
                callArgs[i] = propValue.Deserialize(param.ParameterType);
            }
            else if (param.HasDefaultValue)
            {
                callArgs[i] = param.DefaultValue;
            }
            else
            {
                throw new ArgumentException($"Missing required parameter '{param.Name}' for tool '{Name}'.");
            }
        }

        object? result = _executeMethod.Invoke(this, callArgs);
        return result?.ToString() ?? string.Empty;
    }

    private static string MapClrType(Type type) =>
        Type.GetTypeCode(Nullable.GetUnderlyingType(type) ?? type) switch
        {
            TypeCode.Int16 or TypeCode.Int32 or TypeCode.Int64 => "integer",
            TypeCode.Single or TypeCode.Double or TypeCode.Decimal => "number",
            TypeCode.Boolean => "boolean",
            _ => "string"
        };
}