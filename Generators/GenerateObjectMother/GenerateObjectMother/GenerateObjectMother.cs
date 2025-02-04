namespace WS.Gen.ObjectMother;

[Generator]
public class ObjectMotherGenerator : BaseGenerator<GeneratorInformation>
{
	const string attributeNamespace = "WS.Gen.ObjectMother";
	const string attributeName = "ObjectMotherBuilderAttribute";
	const string fullAttributeName = $"{attributeNamespace}.{attributeName}";


	public ObjectMotherGenerator()
		: base(fullAttributeName, GeneratorInformationComparer.Instance)
	{
	}


	protected override GeneratorInformation ProcessAttribute(AttributeData generatorInterface, INamedTypeSymbol classSymbol)
	{
		return new GeneratorInformation
		(
			ProcessAttribute(classSymbol),
			ProcessAttributeClassParameter(generatorInterface)
		);
	}


	protected ClassInformation ProcessAttribute(INamedTypeSymbol classSymbol)
	{
		return new ClassInformation
		(
			GetNamespace(classSymbol),
			GetClassName(classSymbol),
			GetNullableContextOptions(classSymbol)
		);
	}


	protected InterfaceInformation ProcessAttributeClassParameter(AttributeData generatorInterface)
	{
		var classSymbol = (INamedTypeSymbol?)generatorInterface?.ConstructorArguments[0].Value!;
		var generateSample = (bool)generatorInterface?.ConstructorArguments[1].Value!;

		return GetInterfaceInformation(generateSample, classSymbol);
	}


	private static InterfaceInformation GetInterfaceInformation(bool generateSample, INamedTypeSymbol classSymbol)
	{
		var ns = GetNamespace(classSymbol);
		var className = GetClassName(classSymbol);
		var constructors = GetConstructorInformation(classSymbol);
		var properties = GetPropertyInformation(classSymbol);
		var fields = GetFieldInformation(classSymbol);
		var inherited = GetInheritedInformation(generateSample, classSymbol.BaseType);

		return new InterfaceInformation(ns, className, generateSample, constructors, properties, fields, inherited);
	}


	private static InterfaceInformation? GetInheritedInformation(bool generateSample, INamedTypeSymbol? classSymbol)
	{
		if (classSymbol == null || classSymbol.SpecialType != SpecialType.None)
			return null;
		
		return GetInterfaceInformation(generateSample, classSymbol);
	}


	protected override void GenerateTemplate(SourceProductionContext context, GeneratorInformation generatorInformation)
	{
		var classInfo = generatorInformation.InterfaceInformation!;
		var namespaceName = generatorInformation.ClassInformation!.Namespace;
		var className = classInfo.ClassName;
		var fileName = $"{namespaceName}.{className}.g.cs";

		var template = GenerateBuilder.GetTemplate(generatorInformation, attributeNamespace);
		context.AddSource(fileName, template);
	}


	protected override void GenerateAdditionalTemplates(IncrementalGeneratorPostInitializationContext context)
	{
		var filename = $"{attributeNamespace}.Attribute";
		GenerateInterfaceWithType.AddFile(context, filename, attributeNamespace, attributeName);

		filename = $"{attributeNamespace}.Builder";
		GenerateBuilderBaseClass.AddFile(context, filename, attributeNamespace);
	}
}
