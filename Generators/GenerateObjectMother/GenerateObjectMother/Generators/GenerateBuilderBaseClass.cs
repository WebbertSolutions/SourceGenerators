namespace WS.Gen.ObjectMother;

internal static class GenerateBuilderBaseClass
{
	public static void AddFile(IncrementalGeneratorPostInitializationContext context, string filename, string attributeNamespace)
	{
		context.AddSource($"{filename}.g.cs",
		$@"
namespace {attributeNamespace};

public abstract class Builder<T> where T : class
{{
	private static readonly System.Random _random = new();


	public abstract T Build();


	public IEnumerable<T> Build(int number)
	{{
		for (var min = 0; min < number; min++)
			yield return Build();
	}}


	public IEnumerable<T> Build(int min, int max)
	{{
		for (; min < max; min++)
			yield return Build();
	}}


	protected abstract T Construct();


	protected virtual void PostBuild(T value)
	{{
	}}


	public static Func<List<U>> GenerateData<U>(int min, int max, Builder<U> builder) where U : class
	{{
		return () => Enumerable.Range(1, GetRandom(min, max))
			.Select(_ => builder.Build())
			.ToList();
	}}

	private static int GetRandom(int min, int max) => _random.Next(min, max);


	private static System.Reflection.ConstructorInfo _ctor;


	protected static T CreateInstance(params object[] parameters)
	{{
		_ctor ??= typeof(T)
			.GetConstructors(
				System.Reflection.BindingFlags.Instance | 
				System.Reflection.BindingFlags.Public | 
				System.Reflection.BindingFlags.NonPublic)
			.First();

		return (T)_ctor.Invoke(parameters);
	}}
}}
		");
	}
}