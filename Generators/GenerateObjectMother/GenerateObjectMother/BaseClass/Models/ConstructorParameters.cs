namespace WS.Gen.ObjectMother.Models;

public class ConstructorParameters
{
	public string DataType { get; set; } = string.Empty;
	public string ParameterName { get; set; } = string.Empty;


	public ConstructorParameters(string dataType, string parameterName)
	{
		DataType = dataType;
		ParameterName = parameterName;
	}
}
