namespace WS.Gen.ObjectMother;

public abstract class BaseGenerator<T> : IIncrementalGenerator where T : class
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
			(
				c.DeclaredAccessibility,
				c.Parameters.ToDictionary( p => p.Name.ToLower(), p => new ConstructorParameters(p.Type.ToDisplayString(), p.Name, p.Ordinal))
			))
			.OrderByDescending(c => c.Accessibility switch
			{
				Accessibility.Public => Accessibility.Public,
				Accessibility.Private => Accessibility.Private,
				_ => Accessibility.Protected
			})
			.ToList();
	}


	protected static List<ClassMember> GetPropertyInformation(INamedTypeSymbol classSymbol)
	{
		// https://learn.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.nullableannotation?view=roslyn-dotnet-4.9.0

		return classSymbol.GetMembers().OfType<IPropertySymbol>()
			.Select(mem =>
			{
				var nts = mem.Type as INamedTypeSymbol;

				//// Variable
				//var v_namespace = ps.Type.ContainingNamespace.ToDisplayString();
				//var v_type = ps.Type.Name;
				//var v_Name = ps.Name;

				//// Generic Parameter -   <T, U> 
				//var gen_Parameter = nts?.TypeArguments.Select (t => new KeyValuePair<string, string> (t.ContainingNamespace.ToDisplayString(), t.Name)).ToList();

				return new ClassMember(
					mem.DeclaredAccessibility,
					ClassMemberType.Property,
					mem.Type.ToDisplayString(),
					mem.Name,
					mem.IsReadOnly,
					mem.Type.IsValueType,
					nts?.IsGenericType ?? false,
					mem.Type.ContainingModule?.ToDisplayString().Contains("Collection") ?? false,
					mem.NullableAnnotation == NullableAnnotation.Annotated
				);
			})
			.ToList();
	}


	protected static List<ClassMember> GetFieldInformation(INamedTypeSymbol classSymbol)
	{
		return classSymbol.GetMembers().OfType<IFieldSymbol>()
			.Select(mem =>
			{
				var nts = mem.Type as INamedTypeSymbol;

				return new ClassMember(
					mem.DeclaredAccessibility,
					ClassMemberType.Field,
					mem.Type.ToDisplayString(),
					mem.Name,
					mem.IsReadOnly,
					mem.Type.IsValueType,
					nts?.IsGenericType ?? false,
					mem.Type.ContainingModule?.ToDisplayString().Contains("Collection") ?? false,
					mem.NullableAnnotation == NullableAnnotation.Annotated
				);
			})
			.ToList();
	}


	protected static NullableContextOptions GetNullableContextOptions(INamedTypeSymbol classSymbol)
	{
		var assembly = classSymbol.ContainingModule.ContainingAssembly as ISourceAssemblySymbol;

		return assembly?.Compilation.Options.NullableContextOptions ?? NullableContextOptions.Disable;
	}
}
