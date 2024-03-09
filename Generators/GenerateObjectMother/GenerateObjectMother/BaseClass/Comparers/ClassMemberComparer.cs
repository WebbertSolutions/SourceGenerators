namespace WS.Gen.ObjectMother;

public class ClassMemberComparer : BaseComparer, IEqualityComparer<ClassMember>
{
	public static ClassMemberComparer Instance { get; } = new();


	public bool Equals(ClassMember? left, ClassMember? right)
	{
		return
			AreEqual(left?.Accessibility, right?.Accessibility) &&
			AreEqual(left?.DataType, right?.DataType) &&
			AreEqual(left?.PropertyName, right?.PropertyName) &&
			AreEqual(left?.IsValueType, right?.IsValueType) &&
			AreEqual(left?.IsGenericType, right?.IsGenericType) &&
			AreEqual(left?.IsCollection, right?.IsCollection);
	}


	public int GetHashCode(ClassMember obj)
	{
		var hashCodes = new List<int>
		{
			obj.Accessibility.GetHashCode(),
			obj.DataType.GetHashCode(),
			obj.PropertyName.GetHashCode(),
			obj.IsValueType.GetHashCode(),
			obj.IsGenericType.GetHashCode(),
			obj.IsCollection.GetHashCode()
		};

		return hashCodes.Aggregate(CombineHashCodes);
	}
}
