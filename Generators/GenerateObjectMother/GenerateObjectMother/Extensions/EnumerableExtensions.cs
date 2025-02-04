
namespace WS.Gen.ObjectMother;

public static class EnumerableExtensions
{
	public static bool Matches<T>(this IEnumerable<T> x, IEnumerable<T> y)
	{
		return x.Count() == y.Count() &&
			x.All(l => y.Any(r => l!.Equals(r)));
	}
}
