namespace WS.Gen.ObjectMother;

public class GeneratorInformationComparer : BaseComparer, IEqualityComparer<GeneratorInformation>
{
	public static GeneratorInformationComparer Instance { get; } = new();

	private static readonly ClassInformationComparer _classInfoComparer = ClassInformationComparer.Instance;
	private static readonly InterfaceInformationComparer _interfaceInfoComparer = InterfaceInformationComparer.Instance;


	public bool Equals(GeneratorInformation x, GeneratorInformation y)
	{
		return
			AreObjectsEqual(x?.ClassInformation, y?.ClassInformation, _classInfoComparer) &&
			AreObjectsEqual(x?.InterfaceInformation, y?.InterfaceInformation, _interfaceInfoComparer);
	}


	public int GetHashCode(GeneratorInformation obj)
	{
		var hashCodes = new List<int>
		{
			obj.ClassInformation.GetHashCode(),
			obj.InterfaceInformation.GetHashCode()
		};

		return hashCodes.Aggregate(CombineHashCodes);
	}
}
