namespace GenerateObjectMotherTest.Builders;

[ObjectMotherBuilder(typeof(PublicDefault))]
public partial class PublicDefaultBuilder
{
	public static PublicDefaultBuilder Typical()
	{
		return new PublicDefaultBuilder()
			.WithAddress1("123 Main")
			.SetDefaultAddress2()
			.WithCity("Raleigh")
			.WithState(StateBuilder.Typical().Build())
			.WithPostalCode("12345")
			;
	}


	public static Func<List<PublicDefault>> GeneratePublicDefaults(int min, int max, PublicDefaultBuilder? builder = null)
		=> GenerateData(min, max, builder ?? Typical());
}
