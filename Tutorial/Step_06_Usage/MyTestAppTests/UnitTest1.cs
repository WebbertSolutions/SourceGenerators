using MyTestAppTests.Builders;

namespace MyTestAppTests;

public class UnitTest1
{
	[Fact]
	public void Test1()
	{
		var address = AddressBuilder.Typical()
			.WithAddress2("APT 2")
			.SetDefaultState(new())
			.Build();

		address = null;
	}
}