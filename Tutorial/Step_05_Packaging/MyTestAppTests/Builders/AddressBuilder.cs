namespace MyTestAppTests.Builders;


[GenerateDataBuilder(typeof(Address))]
public partial class AddressBuilder
{
	public static AddressBuilder Typical()
	{
		return new AddressBuilder()
			.WithAddress1("123 Main")
			.WithCity("Raleigh")
			.WithState(StateBuilder.Typical().Build())
			.WithPostalCode("12345")
			.SetDefaultAddress1()
			;
	}


	public static Func<List<Address>> GenerateAddresses(int min, int max) =>
	() => GenerateData(min, max, Typical())().ToList();

	public static Func<List<Address>> GenerateAddresses2(int min, int max) =>
		() => new List<Address>(GenerateData(min, max, Typical())());

	public static Func<List<Address>> GenerateAddresses3(int min, int max)
	{
		return () =>
		{
			var func = GenerateData(min, max, Typical());
			var enumerable = func();
			return new List<Address>(enumerable);
		};
	}
}
