namespace WS.Gen.ObjectMother;

public abstract class BaseGenerator<T> : IIncrementalGenerator where T: class
{
	private readonly IEqualityComparer<T>  _comparer;

	public string AttributeName { get; }


	public BaseGenerator(string attributeName, IEqualityComparer<T> comparer)
	{
		AttributeName = attributeName;
		_comparer = comparer;
	}


	protected abstract void GenerateTemplate(SourceProductionContext context, T data);
	protected abstract void GenerateAdditionalTemplates(IncrementalGeneratorPostInitializationContext context);
	protected abstract T ProcessAttribute(AttributeData generatorInterface, INamedTypeSymbol classSymbol);	


	public void Initialize(IncrementalGeneratorInitializationContext context)
	{
		var generatorInformation = context.SyntaxProvider
			.ForAttributeWithMetadataName<T>(AttributeName,
				predicate: static (node, _) => IsSyntaxTarget(node),
				transform:  (ctx, _) => GetSemanticTarget(ctx))
			.Where(static (target) => target is not null)
			.WithComparer(_comparer);

		context.RegisterSourceOutput(generatorInformation,
			(ctx, source) => GenerateTemplate(ctx, source!));

		context.RegisterPostInitializationOutput(
			(ctx) => GenerateAdditionalTemplates(ctx));
	}


	private static bool IsSyntaxTarget(SyntaxNode node)
	{
		return node is ClassDeclarationSyntax classDeclarationSyntax
			&& classDeclarationSyntax.AttributeLists.Count > 0;
	}


	private T GetSemanticTarget(GeneratorAttributeSyntaxContext context)
	{
		var classDeclarationSyntax = (ClassDeclarationSyntax)context.TargetNode;
		var classSymbol = (INamedTypeSymbol)context.TargetSymbol;

		Debug.Assert(classDeclarationSyntax.AttributeLists.Count > 0);

		var generatorInterface = context.Attributes
			.Where(a => a.AttributeClass?.ToDisplayString() == AttributeName)
			.First();

		return ProcessAttribute(generatorInterface, classSymbol);
	}


	//
	//	Helper Methods
	//

	protected static string GetNamespace(INamedTypeSymbol classSymbol)
		=> classSymbol.ContainingNamespace.ToDisplayString();


	protected static string GetClassName(INamedTypeSymbol classSymbol)
		=> classSymbol.Name;


	protected static List<ConstructorInformation> GetConstructorInformation(INamedTypeSymbol classSymbol)
	{
		return classSymbol.Constructors
			.Select(c => new ConstructorInformation
			{
				Accessibility = c.DeclaredAccessibility,
				Parameters = c.Parameters.Select(p => new ConstructorParameters(p.Type.ToDisplayString(), p.Name)).ToList()
			})
			.ToList();
	}


	protected static List<ClassMember> GetPropertyInformation(INamedTypeSymbol classSymbol)
	{
		return classSymbol.GetMembers().OfType<IPropertySymbol>()
			.Select(ps =>
			{
				var nts = ps.Type as INamedTypeSymbol;

				return new ClassMember
				{
					Accessibility = ps.DeclaredAccessibility,
					DataType = ps.Type.ToDisplayString(),
					PropertyName = ps.Name,
					IsReadOnly = ps.IsReadOnly,
					IsValueType = ps.Type.IsValueType,
					IsGenericType = nts?.IsGenericType ?? false,
					IsCollection = ps.Type.ContainingModule?.ToDisplayString().Contains("Collection") ?? false
				};
			})
			.ToList();
	}


	protected static List<ClassMember> GetFieldInformation(INamedTypeSymbol classSymbol)
	{
		return classSymbol.GetMembers().OfType<IFieldSymbol>()
			.Select(fs =>
			{
				var nts = fs.Type as INamedTypeSymbol;

				return new ClassMember
				{
					Accessibility = fs.DeclaredAccessibility,
					DataType = fs.Type.ToDisplayString(),
					PropertyName = fs.Name,
					IsReadOnly = fs.IsReadOnly,
					IsValueType = fs.Type.IsValueType,
					IsGenericType = nts?.IsGenericType ?? false,
					IsCollection = fs.Type.ContainingModule?.ToDisplayString().Contains("Collection") ?? false
				};
			})
			.ToList();
	}


	protected static NullableContextOptions GetNullableContextOptions(INamedTypeSymbol classSymbol)
	{
		var assembly = classSymbol.ContainingModule.ContainingAssembly as ISourceAssemblySymbol;
	
		return assembly?.Compilation.Options.NullableContextOptions ?? NullableContextOptions.Disable;
	}
}
