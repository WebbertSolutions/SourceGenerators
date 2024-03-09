namespace GenerateObjectMotherTest;

public class ConstructorTest
{
	[Fact]
	public void PrivateWithBuilderTest()
	{
		var model = PrivateWithBuilder.Typical().Build();
		Assert.NotNull(model);
	}


	[Fact]
	public void PrivateWithoutBuilderTest()
	{
		var model = PrivateWithoutBuilder.Typical().Build();
		Assert.NotNull(model);
	}


	[Fact]
	public void PublicDefaultBuilderTest()
	{
		var model = PublicDefaultBuilder.Typical().Build();
		Assert.NotNull(model);
	}


	[Fact]
	public void PublicOverrideConstructBuilderTest()
	{
		var model = PublicOverrideConstructBuilder.Typical().Build();
		Assert.NotNull(model);
	}


	[Fact]
	public void PublicWithBuilderTest()
	{
		var model = PublicWithBuilder.Typical().Build();
		Assert.NotNull(model);
	}


	[Fact]
	public void PublicWithoutBuilderTest()
	{
		var model = PublicWithoutBuilder.Typical().Build();
		Assert.NotNull(model);
	}
}
