namespace WS.Gen.ObjectMother;

public abstract class BaseComparer
{
	protected static bool AreEqual<T>(T left, T right)
	{
		var leftIsNull = left is null;
		var rightIsNull = right is null;

		if (leftIsNull && rightIsNull)
			return true;

		if (leftIsNull ^ rightIsNull)
			return false;

		return left!.Equals(right);
	}


	protected static bool AreObjectsEqual<T>(T? left, T? right, IEqualityComparer<T> comparer)
	{
		var leftIsNull = left is null;
		var rightIsNull = right is null;

		if (leftIsNull && rightIsNull)
			return true;

		if (leftIsNull ^ rightIsNull)
			return false;

		return comparer.Equals(left!, right!);
	}
	
	
	protected static bool AreListsEqual<T>(List<T>? left, List<T>? right, IEqualityComparer<T> comparer)
	{
		var leftIsNull = left is null;
		var rightIsNull = right is null;

		if (leftIsNull && rightIsNull)
			return true;

		if (leftIsNull ^ rightIsNull)
			return false;

		if (left!.Count != right!.Count)
			return false;

		return left.All(xx => right.Any(yy => comparer.Equals(xx, yy)));
	}


	protected static int GetListHashCode<T>(IEnumerable<T> obj)
	{
		return obj
			.Where(x => x != null)
			.Select(x => x!.GetHashCode())
			.Aggregate(CombineHashCodes);
	}


	protected static int CombineHashCodes(int left, int right)
	{
		unchecked
		{
			// Copied from - https://referencesource.microsoft.com/#System.Web/Util/HashCodeCombiner.cs
			return ((left << 5) + left) ^ right;
		}
	}
}
