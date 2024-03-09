namespace WS.Gen.ObjectMother;

public class ClassInformationComparer : BaseComparer, IEqualityComparer<ClassInformation>
{
	public static ClassInformationComparer Instance { get; } = new();


	public bool Equals(ClassInformation x, ClassInformation y)
	{
		return
			AreEqual(x?.Namespace, y?.Namespace) &&
			AreEqual(x?.ClassName, y?.ClassName);
	}


	public int GetHashCode(ClassInformation obj)
	{
		HashCode hash = new();

		hash.Add(obj.Namespace);
		hash.Add(obj.ClassName);

		return hash.ToHashCode();
	}
}
