using MyTestAppTests.Helpers;

namespace MyTestAppTests.Builders;

[GenerateDataBuilder(typeof(State))]
public partial class StateBuilder
{
	public static StateBuilder Typical()
	{
		return new StateBuilder()
			.WithId(GetRandomValue.Int32(1,50))
			.WithName(GetRandomValue.String(1,20))
			;
	}
}
