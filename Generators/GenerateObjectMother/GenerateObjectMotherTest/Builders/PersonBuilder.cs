namespace GenerateObjectMotherTest.Builders;

[ObjectMotherBuilder(typeof(Person))]
public partial class PersonBuilder
{
	public static PersonBuilder Typical()
	{
		return new PersonBuilder()
			.WithFirstName(GetRandomValue.String(10, 50))
			.WithLastName("The Builder")
			.WithAddresses(AddressBuilder.GenerateAddresses(1, 3))
			;
	}
}
