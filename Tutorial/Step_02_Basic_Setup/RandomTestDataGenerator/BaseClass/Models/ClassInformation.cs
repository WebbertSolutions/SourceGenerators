namespace BaseSourceGenerator.Models;

public class ClassInformation
{
	public string Namespace { get; set; } = string.Empty;
	public string ClassName { get; set; } = string.Empty;

	public List<PropertyInformation> Properties { get; set; } = new();
}
