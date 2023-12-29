namespace BaseSourceGenerator;

public class GeneratorInformationComparer : IEqualityComparer<GeneratorInformation>
{
    public static GeneratorInformationComparer Instance { get; } = new();

    public bool Equals(GeneratorInformation x, GeneratorInformation y)
    {
        return false;
    }

    public int GetHashCode(GeneratorInformation obj) => obj.GetHashCode();


    public bool AreEqual<T>(T left, T right)
    {
        return false;
    }
}
