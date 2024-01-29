namespace BaseSourceGenerator;

public class ClassInformationComparer : IEqualityComparer<ClassInformation>
{
	public static ClassInformationComparer Instance { get; } = new();

	public bool Equals(ClassInformation x, ClassInformation y) => x.Equals(y);
	public int GetHashCode(ClassInformation obj) => obj.GetHashCode();
}