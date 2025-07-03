namespace GenerateObjectMotherTest.Builders;


[ObjectMotherBuilder(typeof(StateRecord))]
public partial class StateRecordBuilder
{
	public static StateRecordBuilder Typical()
	{
		return new StateRecordBuilder()
			.WithStateId(1)
			.WithStateCode("CA")
			;
	}
}
