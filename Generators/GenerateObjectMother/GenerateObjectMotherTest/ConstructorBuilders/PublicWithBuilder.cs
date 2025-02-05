namespace GenerateObjectMotherTest.Builders;

[ObjectMotherBuilder(typeof(PublicWith), true)]
public partial class PublicWithBuilder
{
	public static PublicWithBuilder Typical()
	{
		return new PublicWithBuilder()
			.WithStreetNumber(123)
			.WithAddress1("123 Main")
			.WithAddress2() 
			.WithCity("Raleigh")
			.WithState(StateBuilder.Typical().Build())
			.WithPostalCode("12345")
			;
	}


	public static Func<List<PublicWith>> GeneratePublicWiths(int min, int max, PublicWithBuilder? builder = null)
		=> GenerateData(min, max, builder ?? Typical());
}