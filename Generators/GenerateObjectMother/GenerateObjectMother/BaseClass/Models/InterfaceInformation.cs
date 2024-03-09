namespace WS.Gen.ObjectMother.Models;

public class InterfaceInformation
{
	public string Namespace { get; set; } = string.Empty;
	public string ClassName { get; set; } = string.Empty;

	public bool GenerateSample { get; set; }

	public List<ConstructorInformation> Constructors { get; set; } = new();
	public List<ClassMember> Properties { get; set; } = new();
	public List<ClassMember> Fields { get; set; } = new();
}