namespace WS.Gen.ObjectMother;


[Generator]
public class ObjectMotherGenerator : BaseGenerator
{
	const string attributeNamespace = "WS.Gen.ObjectMother";
	const string attributeName = "ObjectMotherBuilderAttribute";
	const string fullAttributeName = $"{attributeNamespace}.{attributeName}";


	public ObjectMotherGenerator()
		: base(fullAttributeName, GeneratorInformationComparer.Instance)
	{
	}


	protected override ClassInformation? ScrapeInformation(INamedTypeSymbol classSymbol)
	{
		return GetClassInformation(classSymbol);
	}


	protected override ClassInformation? ProcessInterface(AttributeData generatorInterface)
	{
		var classSymbol = (INamedTypeSymbol?)generatorInterface?.ConstructorArguments[0].Value;

		return GetClassInformation(classSymbol!);
	}


	protected override void Execute(SourceProductionContext context, GeneratorInformation generatorInformation)
	{
		var classInfo = generatorInformation.InterfaceInformation!;
		var namespaceName = generatorInformation.ClassInformation!.Namespace;
		var className = classInfo.ClassName;
		var fileName = $"{namespaceName}.{className}.g.cs";

		var template = GenerateBuilder.GetTemplate(generatorInformation, attributeNamespace);
		context.AddSource(fileName, template);
	}


	protected override void PostInitializationOutput(IncrementalGeneratorPostInitializationContext context)
	{
		var filename = $"{attributeNamespace}.Attribute";
		GenerateInterfaceWithType.AddFile(context, filename, attributeNamespace, attributeName);

		filename = $"{attributeNamespace}.Builder";
		GenerateBuilderBaseClass.AddFile(context, filename, attributeNamespace);
	}
}
