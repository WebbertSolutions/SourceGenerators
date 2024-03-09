namespace WS.Gen.ObjectMother;

public class InterfaceInformationComparer : BaseComparer, IEqualityComparer<InterfaceInformation>
{
	public static InterfaceInformationComparer Instance { get; } = new();

	private static readonly ConstructorComparer _constructorComparer = ConstructorComparer.Instance;
	private static readonly ClassMemberComparer _classMemberComparer = ClassMemberComparer.Instance;


	public bool Equals(InterfaceInformation left, InterfaceInformation right)
	{
		return
			AreEqual(left?.Namespace, right?.Namespace) &&
			AreEqual(left?.ClassName, right?.ClassName) &&
			AreListsEqual(left?.Constructors, right?.Constructors, _constructorComparer) &&
			AreListsEqual(left?.Properties, right?.Properties, _classMemberComparer);
	}


	public int GetHashCode(InterfaceInformation obj)
	{
		var hashCodes = new List<int>();

		HashCode hash = new();
		hash.Add(obj.Namespace);
		hash.Add(obj.ClassName);
		hashCodes.Add(hash.ToHashCode());

		hashCodes.Add(GetListHashCode(obj.Constructors));
		hashCodes.Add(GetListHashCode(obj.Properties));

		return hashCodes.Aggregate(CombineHashCodes);
	}
}
