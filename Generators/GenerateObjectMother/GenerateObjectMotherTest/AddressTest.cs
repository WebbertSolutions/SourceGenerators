using System.Text.Json;

namespace GenerateObjectMotherTest;

public class AddressTest
{
	[Fact]
	public void AddressBuilderTest()
	{
		var model = AddressBuilder.Typical().Build(3).ToList();


		Assert.NotNull(model);
	}


	[Fact]
	public void AddressBuilder_Json_Test()
	{
		var model = AddressBuilder.Typical().Build();
		
		Assert.NotNull(model);


		var json = JsonSerializer.Serialize(model);
		var newModel = AddressBuilder.Default(json).Build();

		Assert.NotNull(newModel);
	}
}
