namespace BaseSourceGenerator;

public abstract class BaseGenerator : IIncrementalGenerator 
{
	private readonly IEqualityComparer<GeneratorInformation>  _comparer;

	public string AttributeName { get; }


	public BaseGenerator(string attributeName, IEqualityComparer<GeneratorInformation> comparer)
	{
		AttributeName = attributeName;
		_comparer = comparer;
	}


	protected abstract void Execute(SourceProductionContext context, GeneratorInformation generatorInformation);
	protected abstract void PostInitializationOutput(IncrementalGeneratorPostInitializationContext context);

	protected virtual ClassInformation? ScrapeInformation(INamedTypeSymbol classSymbol) => null;
	protected virtual ClassInformation? ProcessInterface(AttributeData generatorInterface) => null;


	public void Initialize(IncrementalGeneratorInitializationContext context)
	{
		var generatorInformation = context.SyntaxProvider
			.ForAttributeWithMetadataName(AttributeName,
				predicate: static (node, _) => IsSyntaxTarget(node),
				transform:  (ctx, _) => GetSemanticTarget(ctx))
			.Where(static (target) => target is not null)
			.WithComparer(_comparer);

		context.RegisterSourceOutput(generatorInformation,
			(ctx, source) => Execute(ctx, source!));

		context.RegisterPostInitializationOutput(
			(ctx) => PostInitializationOutput(ctx));
	}


	private static bool IsSyntaxTarget(SyntaxNode node)
	{
		return node is ClassDeclarationSyntax classDeclarationSyntax
			&& classDeclarationSyntax.AttributeLists.Count > 0;
	}


	private GeneratorInformation GetSemanticTarget(GeneratorAttributeSyntaxContext context)
	{
		var classDeclarationSyntax = (ClassDeclarationSyntax)context.TargetNode;
		var classSymbol = (INamedTypeSymbol)context.TargetSymbol;

		Debug.Assert(classDeclarationSyntax.AttributeLists.Count > 0);

		var generatorInterface = context.Attributes
			.Where(a => a.AttributeClass?.ToDisplayString() == AttributeName)
			.First();


		return new GeneratorInformation
		{
			ClassInformation = ScrapeInformation(classSymbol),
			InterfaceInformation = ProcessInterface(generatorInterface)
		};
	}


	protected static ClassInformation GetClassInformation(INamedTypeSymbol classSymbol)
	{
		var namespaceName = classSymbol.ContainingNamespace.ToDisplayString();
		var className = classSymbol.Name;
		var properties = new List<PropertyInformation>();

		foreach (var memberSymbol in classSymbol.GetMembers().OfType<IPropertySymbol>())
		{
			if (memberSymbol.Kind == SymbolKind.Property
				&& memberSymbol.DeclaredAccessibility == Accessibility.Public)
			{
				var nts = memberSymbol.Type as INamedTypeSymbol;

				properties.Add(new PropertyInformation
				{
					DataType = memberSymbol.Type.ToDisplayString(),
					PropertyName = memberSymbol.Name,
					IsValueType = memberSymbol.Type.IsValueType,
					IsGenericType = nts?.IsGenericType ?? false,
					IsCollection = false //memberSymbol.Type.ContainingModule.ToDisplayString().Contains("Collection")
				});
			}
		}

		return new ClassInformation
		{
			Namespace = namespaceName,
			ClassName = className,
			Properties = properties
		};
	}
}
