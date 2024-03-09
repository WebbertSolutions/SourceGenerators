namespace GenerateObjectMotherTest.Models;

public class PrivateWith
{
    private PrivateWith(string address1, string city, string postalCode)
    {
        Address1 = address1;
        City = city;
        PostalCode = postalCode;
    }

    public static PrivateWith Create() => new("a", "b", "c");


    public string Address1 { get; set; } = string.Empty;
    public string Address2 { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public State State { get; set; } = new();
    public string PostalCode { get; set; } = string.Empty;
}
