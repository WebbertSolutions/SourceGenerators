namespace WS.Gen.ObjectMother;

public static class HashCode
{
	public static int Generate(params object?[] objects)
	{

		var hashCodes = new List<int>();

		foreach (var o in objects)
			hashCodes.Add(o == null ? 0 : o.GetHashCode());

		return hashCodes.Aggregate(CombineHashCodes);
	}

	private static int CombineHashCodes(int left, int right)
	{
		unchecked
		{
			// Copied from - https://referencesource.microsoft.com/#System.Web/Util/HashCodeCombiner.cs
			return ((left << 5) + left) ^ right;
		}
	}
}
