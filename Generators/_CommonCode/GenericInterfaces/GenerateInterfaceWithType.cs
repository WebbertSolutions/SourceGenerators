namespace BaseSourceGenerator;

internal static class GenerateInterfaceWithType
{
	public static void AddFile(IncrementalGeneratorPostInitializationContext context,
		string filename,
		string attributeNamespace,
		string attributeName)
	{
		context.AddSource($"{filename}.g.cs",
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
