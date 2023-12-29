using MyTestAppTests.Helpers;

namespace MyTestAppTests.Builders;


[GenerateDataBuilder(typeof(Address))]
public partial class AddressBuilder
{
	public static AddressBuilder Typical()
	{
		return new AddressBuilder()
			.WithAddress1(GetRandomValue.String(50))
			.SetDefaultAddress2()
			.WithCity(GetRandomValue.String(50))
			.WithState(StateBuilder.Typical().Build())
			.WithPostalCode(GetRandomValue.String(50))
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
