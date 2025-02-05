namespace GenerateObjectMotherTest.Builders;

[ObjectMotherBuilder(typeof(PublicWithout), true)]
public partial class PublicWithoutBuilder
{
	public static PublicWithoutBuilder Typical()
	{
		return new PublicWithoutBuilder()
			.WithAddress1("123 Main")
			.WithAddress2() 
			.WithCity("Raleigh")
			.WithState(StateBuilder.Typical().Build())
			.WithPostalCode("12345")
			;
	}


	public static Func<List<PublicWithout>> GeneratePublicWithouts(int min, int max, PublicWithoutBuilder? builder = null)
		=> GenerateData(min, max, builder ?? Typical());
}
