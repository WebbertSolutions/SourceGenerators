# **Object Mothers** - Generate Builder Objects

See history at the bottom for breaking changes

## Usage

- This is useful for creating test data through the use of a builder object.
	
- Add a randomizer or lookup to populate the data for truly exceptional data.
	
 
````
public static PersonBuilder Typical()
{
    return new PersonBuilder()
        .WithFirstName(GetRandomValue.String(10, 50))
        .WithLastName("The Builder")
        .WithAddresses(AddressBuilder.GenerateAddresses(1, 3))
        ;
}
````

- As a performance improvement, data is not actually generated until the .Build() method is called.  This allows you to reuse an existing builder (e.g. Typical) and make the modifications you need directly in your test.  After all the modifications have been made, call the Build() method to create the object.
	
 
````
Person person = PersonBuilder.Typical()
    .WithAddresses(new())
    .WithFirstName()
    .Build();
````

</br>

## Help Creating the Typical Builder
You can pass **true** to the constructor to generated the code for you.  Once you have copied the code, you should probably remove it.  This is to prevent the code from being generated all the time when it is no longer needed.

````
[ObjectMotherBuilder(typeof(Address), true)]
public partial class AddressBuilder
````

At the bottom of the generated code you will see something like the follow which you will copy into the non generated partial builder class.

````
/*
public static AddressBuilder Required()
{
    return new AddressBuilder()
        .WithAddress1(() => xx)
        .WithAddress2(() => xx)
        .WithCity(() => xx)
        .WithPostalCode(() => xx)
        .WithState(() => xx)
        ;
}

public static AddressBuilder Typical()
{
    return Required();
}
*/
````

Remove the items not wanted as part of the Typical() method and change the values to meet your requirements.
        
</br>


## Creating list of objects
You may need to create a list of objects.
Create a helper method like the following.

````
public static Func<List<Address>> GenerateAddresses(int min, int max, AddressBuilder? builder = null)
    => GenerateData(min, max, builder ?? Typical());
````

Then you can call it from where you need it

````
public static PersonBuilder Typical()
{
    return new PersonBuilder()
        .WithFirstName(GetRandomValue.String(10, 50))
        .WithLastName("The Builder")
        .WithAddresses(AddressBuilder.GenerateAddresses(1, 3))
        ;
}
````
Version 2 now has support for creating lists natively.

````
var model = AddressBuilder.Typical().Build(3).ToList();
````

</br>

## Class Constructors

The generator will instantiate an object using the best constructor based on this order
- public  - without parameters
- public  - with parameters
- private - without parameters - **override Construct() required**
- private - with parameters - **override Construct() required**
- can't determine constructor - **override Construct() required**

</br>

## Private Constructors

If a builder is generating code on a class that ***only*** has a private constructor, you will have to add an override.  There is an issue where private constructors are not added to metadata and therefore not available under all instances.  You can read more about the issue here: https://github.com/dotnet/roslyn/issues/72473

In an effort to help with this, you can look at the generated code and you will see in the Build() method the code you will need to copy/paste into the non-generated partial class.

````
    #if SomethingRandom

    // Please copy the following override into the non generated Builder class

    protected override PrivateWith Construct()
    {
        var obj = CreateInstance(_address1(), _city(), _postalCode());
        obj.Address2 = _address2();
        obj.State = _state();

        return obj;
    }

    #endif
````

</br>

The CreateInstance() method exists in the base class and will construct the object with the values that you pass.  The data type and order must match the constructor that is being called.

````
    private PrivateWith(string address1, string city, string postalCode)
    {
        Address1 = address1;
        City = city;
        PostalCode = postalCode;
    }
````


## History

**Version 2** - is now out and has breaking changes.

- Internal code has been completely rewritten to make it easier to work in.
- All Lazy<T> code has been removed with will allow a single instance builder to produce multiple distinct objects.
- There are now 3 default builder overloads
    - Parameterless
    - Json
    - Existing object
    
    that can be use to start the creation of the builder.
````
    public static PostalAddressBuilder Default()
    public static PostalAddressBuilder Default(string json)
    public static PostalAddressBuilder Default(PostalAddress obj)
````
- The SetDefaultxxx methods have all been removed.  Replace with the standard Withxxx method.
- The sample code generated to help you with the Typical method is broken into Required and Typical and will be filled in appropriate based on whether or not a data type is nullable.