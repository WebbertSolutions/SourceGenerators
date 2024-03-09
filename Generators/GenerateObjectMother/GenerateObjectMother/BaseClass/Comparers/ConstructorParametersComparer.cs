namespace WS.Gen.ObjectMother;

public class ConstructorParametersComparer : BaseComparer, IEqualityComparer<ConstructorParameters>
{
	public static ConstructorParametersComparer Instance { get; } = new();


	public bool Equals(ConstructorParameters? left, ConstructorParameters? right)
	{
		return
			AreEqual(left?.DataType, right?.DataType) &&
			AreEqual(left?.ParameterName, right?.ParameterName);
	}


	public int GetHashCode(ConstructorParameters obj)
	{
		var hashCodes = new List<int>
		{
			obj.DataType.GetHashCode(),
			obj.ParameterName.GetHashCode()
		};

		return hashCodes.Aggregate(CombineHashCodes);
	}
}
