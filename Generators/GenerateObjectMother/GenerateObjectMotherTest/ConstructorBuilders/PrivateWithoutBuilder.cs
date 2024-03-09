namespace GenerateObjectMotherTest.Builders;

[ObjectMotherBuilder(typeof(PrivateWithout), true)]
public partial class PrivateWithoutBuilder
{
	public static PrivateWithoutBuilder Typical()
	{
		return new PrivateWithoutBuilder()
			.WithAddress1("123 Main")
			.SetDefaultAddress2()
			.WithCity("Raleigh")
			.WithState(StateBuilder.Typical().Build())
			.WithPostalCode("12345")
			;
	}


	public static Func<List<PrivateWithout>> GeneratePrivateWithouts(int min, int max, PrivateWithoutBuilder? builder = null)
		=> GenerateData(min, max, builder ?? Typical());
}
