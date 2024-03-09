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

</br>

You can pass an additional parameter to the interface to generate the code for the Typical method.  You will have to navigate down into the code in order to copy it.  Once done, you should remove the parameter to prevent extra code from being generated.

````
public ObjectMotherBuilderAttribute(System.Type type, bool generateSample = false)

[ObjectMotherBuilder(typeof(Address), true)]
````

</br>

The generator will create the best constructor based on this order
- public - without parameters
- public - with parameters
- private - without parameters
- private - with parameters
- can't determine constructor - add override to your builder

I'm finding there is a problem with the private constructors.  
The generator works on some computers and not others.  
The problem lies within the Rosyln call to <b>`INamedTypeSymbol.Constructors`</b>.  
The call will return no information even though 1 or more constructors are in the class type.

I'm looking for a resolution on this, but in the mean time, if you see this in your code
````
if (BuilderObject?.IsValueCreated != true)
{
    // Couldn't create constructor.  
    // Please Override Construct() in the partial Builder class
    // 	protected override <class> Construct() => CreateInstance(_param1.Value, _param2.Value, etc. );

    BuilderObject = new Lazy<PrivateWith>(() => Construct());
}
````
you will have to add the override in your class so that it can build the object successfully.