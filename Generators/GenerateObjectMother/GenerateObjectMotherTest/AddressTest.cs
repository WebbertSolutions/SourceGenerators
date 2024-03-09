namespace GenerateObjectMotherTest;

public class AddressTest
{
	[Fact]
	public void AddressBuilderTest()
	{
		var model = AddressBuilder.Typical().Build();
		Assert.NotNull(model);
	}
}
