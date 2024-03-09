namespace GenerateObjectMotherTest.Models;

public class PublicOverrideConstruct
{
	public PublicOverrideConstruct(string someOtherAddressName)
	{
		Address1 = someOtherAddressName;
	}


	public string Address1 { get; set; } = string.Empty;
	public string Address2 { get; set; } = string.Empty;
	public string City { get; set; } = string.Empty;
	public State State { get; set; } = new();
	public string PostalCode { get; set; } = string.Empty;
}