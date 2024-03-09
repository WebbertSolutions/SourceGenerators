namespace WS.Gen.ObjectMother;

public class ConstructorComparer : BaseComparer, IEqualityComparer<ConstructorInformation>
{
	public static ConstructorComparer Instance { get; } = new();

	private static readonly ConstructorParametersComparer _constructorParametersComparer = ConstructorParametersComparer.Instance;



	public bool Equals(ConstructorInformation? left, ConstructorInformation? right)
	{
		return
			AreEqual(left?.Accessibility, right?.Accessibility) &&
			AreListsEqual(left?.Parameters, right?.Parameters, _constructorParametersComparer);
	}


	public int GetHashCode(ConstructorInformation obj)
	{
		var hashCodes = new List<int>
		{
			obj.Accessibility.GetHashCode(),
			GetListHashCode(obj.Parameters)
		};

		return hashCodes.Aggregate(CombineHashCodes);
	}
}
