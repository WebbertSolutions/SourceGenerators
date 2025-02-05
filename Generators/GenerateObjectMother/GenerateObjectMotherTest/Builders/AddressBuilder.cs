namespace GenerateObjectMotherTest.Builders;


[ObjectMotherBuilder(typeof(Address), true)]
public partial class AddressBuilder
{
	public static AddressBuilder Typical()
	{
		return new AddressBuilder()
			.WithAddress1(() => GetRandomValue.String(10, 50))
			.WithAddress2()
			.WithCity(() => GetRandomValue.String(10, 50))
			.WithState(() => StateBuilder.Typical().Build())
			.WithPostalCode(() => GetRandomValue.Int32(10000, 99999).ToString())
			;
	}


	public static Func<List<Address>> GenerateAddresses(int min, int max, AddressBuilder? builder = null)
		=> GenerateData(min, max, builder ?? Typical());

}


