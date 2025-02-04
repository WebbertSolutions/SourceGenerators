namespace WS.Gen.ObjectMother.Models;

public record ConstructorInformation
(
	Accessibility Accessibility,
	IDictionary<string, ConstructorParameters> ConstructorParameters
)
{
	public bool IsParameterless => !ConstructorParameters.Values.Any();


	public virtual bool Equals(ConstructorInformation? y)
		=> y != null && Equals(this, y);


	private static bool Equals(ConstructorInformation x, ConstructorInformation y)
	{
#if DEBUG
		//DebugHelpers.Write("ConstructorInformation.txt", $"Accessibility:  {x.Accessibility == y.Accessibility}");
		//DebugHelpers.Write("ConstructorInformation.txt", $"   Properties:  {x.ConstructorParameters.Values.Matches(y.ConstructorParameters.Values)}");
		//DebugHelpers.Write("ConstructorInformation.txt", $"");
#endif

		return
			x.Accessibility == y.Accessibility &&
			x.ConstructorParameters.Values.Matches(y.ConstructorParameters.Values);
	}


	public override int GetHashCode()
		=> HashCode.Generate(Accessibility, ConstructorParameters);
}
