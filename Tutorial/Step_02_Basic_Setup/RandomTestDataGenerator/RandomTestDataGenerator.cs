namespace WebbertSolutions.Generators;


[Generator]
public class RandomTestDataGenerator : BaseGenerator
{
	const string attributeNamespace = "WebbertSolutions.Generators";
	const string attributeName = "GenerateDataBuilderAttribute";
	const string fullAttributeName = $"{attributeNamespace}.{attributeName}";


	public RandomTestDataGenerator() 
		: base(fullAttributeName, GeneratorInformationComparer.Instance)
	{
	}

	protected override void Execute(SourceProductionContext context, GeneratorInformation generatorInformation)
	{ 
	}


	protected override void PostInitializationOutput(IncrementalGeneratorPostInitializationContext context)
	{
	}
}
