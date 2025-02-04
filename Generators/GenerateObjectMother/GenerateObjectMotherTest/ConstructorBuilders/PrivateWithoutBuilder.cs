namespace GenerateObjectMotherTest.Builders;

[ObjectMotherBuilder(typeof(PrivateWithout), true)]
public partial class PrivateWithoutBuilder
{
	public static PrivateWithoutBuilder Required()
	{
		return new PrivateWithoutBuilder()
			.WithAddress1(() => "123 Main")
			.SetDefaultAddress2()
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

		obj.Address1 = _address1.Value;
		obj.Address2 = _address2.Value;
		obj.City = _city.Value;
		obj.State = _state.Value;
		obj.PostalCode = _postalCode.Value;

		return obj;
	}


	public static Func<List<PrivateWithout>> GeneratePrivateWithouts(int min, int max, PrivateWithoutBuilder? builder = null)
		=> GenerateData(min, max, builder ?? Typical());
}
