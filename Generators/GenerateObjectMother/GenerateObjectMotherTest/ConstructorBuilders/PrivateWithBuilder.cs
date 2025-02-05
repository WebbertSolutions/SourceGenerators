namespace GenerateObjectMotherTest.Builders;

[ObjectMotherBuilder(typeof(PrivateWith), true)]
public partial class PrivateWithBuilder
{
	public static PrivateWithBuilder Typical()
	{
		return new PrivateWithBuilder()
			.WithAddress1("123 Main")
			.WithAddress2()
			.WithCity("Raleigh")
			.WithState(StateBuilder.Typical().Build())
			.WithPostalCode("12345")
			;
	}


	public static Func<List<PrivateWith>> GeneratePrivateWiths(int min, int max, PrivateWithBuilder? builder = null)
		=> GenerateData(min, max, builder ?? Typical());

	protected override PrivateWith Construct()
	{
		var obj = CreateInstance(_address1(), _city(), _postalCode());

		obj.Address2 = _address2();
		obj.State = _state();

		return obj;
	}
}
