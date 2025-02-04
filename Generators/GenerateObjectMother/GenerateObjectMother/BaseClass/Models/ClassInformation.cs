namespace WS.Gen.ObjectMother.Models;

public record ClassInformation
(
	string Namespace,
	string ClassName,
	NullableContextOptions NullableContextOptions = NullableContextOptions.Disable
);