namespace WS.Gen.ObjectMother.Models;

public class ConstructorInformation
{
	public Accessibility Accessibility { get; set; }
	public List<ConstructorParameters> Parameters { get; set; } = new();

	public bool IsParameterless => !Parameters.Any();
}
