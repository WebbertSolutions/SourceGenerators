namespace GenerateObjectMotherTest.Models;

public class PublicDefault
{
	public string Address1 { get; set; } = string.Empty;
	public string Address2 { get; set; } = string.Empty;
	public string City { get; set; } = string.Empty;
	public State State { get; set; } = new();
	public string PostalCode { get; set; } = string.Empty;
}
