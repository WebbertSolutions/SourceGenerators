namespace GenerateObjectMotherTest.Builders;


[ObjectMotherBuilder(typeof(Address), true)]
public partial class AddressBuilder
{
	public static AddressBuilder Typical()
	{
		return new AddressBuilder()
			//.WithAddress1("123 Main")
			//.SetDefaultAddress2()
			//.WithCity("Raleigh")
			//.WithState(StateBuilder.Typical().Build())
			//.WithPostalCode("12345")
			;
	}


	public static Func<List<Address>> GenerateAddresses(int min, int max, AddressBuilder? builder = null)
		=> GenerateData(min, max, builder ?? Typical());

}


