﻿namespace GenerateObjectMotherTest.Builders;

[ObjectMotherBuilder(typeof(PublicOverrideConstruct))]
public partial class PublicOverrideConstructBuilder
{
	public static PublicOverrideConstructBuilder Typical()
	{
		return new PublicOverrideConstructBuilder()
			.WithAddress1("123 Main")
			.SetDefaultAddress2()
			.WithCity("Raleigh")
			.WithState(StateBuilder.Typical().Build())
			.WithPostalCode("12345")
			;
	}


	public static Func<List<PublicOverrideConstruct>> GeneratePublicOverrideConstructs(int min, int max, PublicOverrideConstructBuilder? builder = null)
		=> GenerateData(min, max, builder ?? Typical());

	protected override PublicOverrideConstruct Construct()
		=> CreateInstance(_address1.Value);
}