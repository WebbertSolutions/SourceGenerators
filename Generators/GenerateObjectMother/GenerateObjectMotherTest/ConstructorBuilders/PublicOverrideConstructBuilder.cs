namespace GenerateObjectMotherTest.Builders;

[ObjectMotherBuilder(typeof(PublicOverrideConstruct))]
public partial class PublicOverrideConstructBuilder
{
	public static PublicOverrideConstructBuilder Typical()
	{
		return new PublicOverrideConstructBuilder()
			.WithAddress1("123 Main")
			.WithAddress2() 
			.WithCity("Raleigh")
			.WithState(StateBuilder.Typical().Build())
			.WithPostalCode("12345")
			;
	}


	public static Func<List<PublicOverrideConstruct>> GeneratePublicOverrideConstructs(int min, int max, PublicOverrideConstructBuilder? builder = null)
		=> GenerateData(min, max, builder ?? Typical());


	protected override PublicOverrideConstruct Construct()
	{
		var obj = CreateInstance(_address1());

		obj.Address2 = _address2();
		obj.City = _city();
		obj.State = _state();
		obj.PostalCode = _postalCode();

		return obj;
	}
}
