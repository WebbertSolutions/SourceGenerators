namespace BaseSourceGenerator;

public class GeneratorInformationComparer : IEqualityComparer<GeneratorInformation>
{
	public static GeneratorInformationComparer Instance { get; } = new();

	public bool Equals(GeneratorInformation x, GeneratorInformation y)
	{
		return AreEqual(x.ClassInformation, y.ClassInformation) &&
			AreEqual(x.InterfaceInformation, y.InterfaceInformation);
	}

	public int GetHashCode(GeneratorInformation obj) => obj.GetHashCode();


	public bool AreEqual<T>(T left, T right)
	{
		var leftIsNull = left is null;
		var rightIsNull = right is null;

		if (leftIsNull && rightIsNull)
			return true;

		if (leftIsNull ^ rightIsNull)
			return false;

		return left!.Equals(right);
	}
}