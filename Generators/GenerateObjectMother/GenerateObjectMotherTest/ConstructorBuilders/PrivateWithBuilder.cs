namespace GenerateObjectMotherTest.Builders;

[ObjectMotherBuilder(typeof(PrivateWith), true)]
public partial class PrivateWithBuilder
{
	public static PrivateWithBuilder Typical()
	{
		return new PrivateWithBuilder()
			.WithAddress1("123 Main")
			.SetDefaultAddress2()
			.WithCity("Raleigh")
			.WithState(StateBuilder.Typical().Build())
			.WithPostalCode("12345")
			;
	}


	public static Func<List<PrivateWith>> GeneratePrivateWiths(int min, int max, PrivateWithBuilder? builder = null)
		=> GenerateData(min, max, builder ?? Typical());
}
