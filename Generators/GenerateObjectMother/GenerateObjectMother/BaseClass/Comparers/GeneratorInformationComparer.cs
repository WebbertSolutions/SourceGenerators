namespace WS.Gen.ObjectMother;

public class GeneratorInformationComparer : IEqualityComparer<GeneratorInformation>
{
	public static GeneratorInformationComparer Instance { get; } = new();


	public bool Equals(GeneratorInformation x, GeneratorInformation y)
	{
#if DEBUG
		//DebugHelpers.Write("xComparer.txt", $"                Name: {x.ClassInformation.ClassName}  {y.ClassInformation.ClassName}");
		//DebugHelpers.Write("xComparer.txt", $"    ClassInformation: {x.ClassInformation == y.ClassInformation}");
		//DebugHelpers.Write("xComparer.txt", $"InterfaceInformation: {x.InterfaceInformation == y.InterfaceInformation}");
		//DebugHelpers.Write("xComparer.txt", $"");
#endif

		return
			x.ClassInformation == y.ClassInformation &&
			x.InterfaceInformation == y.InterfaceInformation;
	}


	public int GetHashCode(GeneratorInformation obj)
	{
		return HashCode.Generate(obj.ClassInformation, obj.InterfaceInformation);
	}
}
