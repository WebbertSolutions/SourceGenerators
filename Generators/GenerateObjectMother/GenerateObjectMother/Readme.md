# Object Mothers

Generate builder objects.  This is useful for creating objects to create test data.  	

````
[ObjectMotherBuilder(typeof(Address))]
public partial class AddressBuilder
{
    public static AddressBuilder Typical()
    {
        return new AddressBuilder()
            .WithAddress1("123 Main")
            .WithCity("Raleigh")
            .WithState(StateBuilder.Typical().Build())
            .WithPostalCode("12345")
            .SetDefaultAddress1()
            ;
    }

    public static Func<List<Address>> GenerateAddresses(int min, int max, AddressBuilder? builder = null)
        => GenerateData(min, max, builder ?? Typical());	
}
````
