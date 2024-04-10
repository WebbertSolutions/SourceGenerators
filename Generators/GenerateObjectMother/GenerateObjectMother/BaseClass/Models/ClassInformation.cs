
namespace WS.Gen.ObjectMother.Models;

public class ClassInformation
{
	public string Namespace { get; set; } = string.Empty;
	public string ClassName { get; set; } = string.Empty;
	public NullableContextOptions NullableContextOptions { get; set; } = NullableContextOptions.Disable;
}
