namespace GenerateObjectMotherTest.Builders;

[ObjectMotherBuilder(typeof(PrivateWithout), true)]
public partial class PrivateWithoutBuilder
{
	public static PrivateWithoutBuilder Required()
	{
		return new PrivateWithoutBuilder()
			.WithAddress1(() => "123 Main")
			.WithAddress2() 
			.WithState(() => StateBuilder.Typical().Build())
			.WithPostalCode(() => "12345")
			;
	}


	public static PrivateWithoutBuilder Typical()
	{
		return Required()
			.WithCity(() => "Raleigh")
			;
	}


	protected override PrivateWithout Construct()
	{
		var obj = CreateInstance();

		obj.Address1 = _address1();
		obj.Address2 = _address2();
		obj.City = _city();
		obj.State = _state();
		obj.PostalCode = _postalCode();

		return obj;
	}


	public static Func<List<PrivateWithout>> GeneratePrivateWithouts(int min, int max, PrivateWithoutBuilder? builder = null)
		=> GenerateData(min, max, builder ?? Typical());
}
