namespace WebbertSolutions.Generators;


[Generator]
public class RandomTestDataGenerator : BaseGenerator
{
	private static Dictionary<string, int> _countPerFileName = new();

	const string attributeNamespace = "WebbertSolutions.Generators";
	const string attributeName = "GenerateDataBuilderAttribute";
	const string fullAttributeName = $"{attributeNamespace}.{attributeName}";


	public RandomTestDataGenerator() 
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

		if (_countPerFileName.ContainsKey(fileName))
			_countPerFileName[fileName]++;
		else
			_countPerFileName.Add(fileName, 1);


		var template = GenerateBuilder.GetTemplate( generatorInformation, _countPerFileName[fileName]);

		context.AddSource(fileName, template);
	}


	protected override void PostInitializationOutput(IncrementalGeneratorPostInitializationContext context)
	{
		GenerateInterface.AddFile(context, fullAttributeName, attributeNamespace, attributeName);
		GenerateBuilderBaseClass.AddFile(context, attributeNamespace);
	}
}
