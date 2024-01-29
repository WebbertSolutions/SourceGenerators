namespace GenerateObjectMotherTest.Builders;


[ObjectMotherBuilder(typeof(State))]
public partial class StateBuilder
{
	public static StateBuilder Typical()
	{
		return new StateBuilder()
			.WithId(1)
			.WithName("NC")
			;
	}
}
