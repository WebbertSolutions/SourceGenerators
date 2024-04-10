namespace GenerateObjectMotherTest.Builders;

[ObjectMotherBuilder(typeof(Employee), true)]
public partial class InheritenceBuilder
{
	public static InheritenceBuilder Typical()
	{
		return new InheritenceBuilder()
			.WithEmployeeId(default(int))
			.WithOfficeLocationId(default(int?))
			.WithFirstName(default(string))
			.WithLastName(default(string))
			.WithAddresses(default(System.Collections.Generic.List<GenerateObjectMotherTest.Models.Address>))
			;
	}
}
