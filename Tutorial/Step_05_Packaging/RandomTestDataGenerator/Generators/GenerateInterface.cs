namespace WebbertSolutions.Generators;

internal static class GenerateInterface
{
	public static void AddFile(IncrementalGeneratorPostInitializationContext context,
		string fullAttributeName,
		string attributeNamespace,
		string attributeName)
	{
		context.AddSource($"{fullAttributeName}.g.cs",
			$@"
namespace {attributeNamespace};

internal class {attributeName} : System.Attribute 
{{
	public Type ClassType {{ get; }}


	public {attributeName}(System.Type type)
	{{
		ClassType = type;
	}}
}}
");
	}
}
