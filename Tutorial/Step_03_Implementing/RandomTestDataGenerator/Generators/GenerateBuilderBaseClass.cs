namespace WebbertSolutions.Generators;

internal static class GenerateBuilderBaseClass
{
	public static void AddFile(IncrementalGeneratorPostInitializationContext context, string attributeNamespace)
	{
		context.AddSource($"{attributeNamespace}.Builder.g.cs",
		$@"
namespace {attributeNamespace};

public abstract class Builder<T> where T : class
{{
	private static readonly System.Random _random = new();

	protected Lazy<T> BuilderObject = null!;


	public abstract T Build();


	public Builder<T> WithObject(T value)
	{{
		return WithObject(() => value);
	}}


	public Builder<T> WithObject(Func<T> func)
	{{
		BuilderObject = new Lazy<T>(func);
		return this;
	}}


	protected virtual void PostBuild(T value)
	{{
	}}


	public static Func<IEnumerable<U>> GenerateData<U>(int min, int max, Builder<U> builder) where U : class
	{{
		return () => Enumerable.Range(1, GetRandom(min, max))
			.Select(_ => builder.Build());
	}}

	private static int GetRandom(int min, int max) => _random.Next(min, max);
}}
		");
	}
}