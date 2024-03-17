# **Object Mothers** - Generate Builder Objects

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
    .SetDefaultAddresses(new())
    .SetDefaultFirstName()
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
public static AddressBuilder Typical()
{
    return new AddressBuilder()
        .WithAddress1(default(string))
        .WithAddress2(default(string))
        .WithCity(default(string))
        .WithState(default(GenerateObjectMotherTest.Models.State))
        .WithPostalCode(default(string))
        ;
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
public override PrivateWith Build()
{
    if (BuilderObject?.IsValueCreated != true)
    {
        #if SomethingRandom

        // Please copy the following override into the non generated Builder class

        protected override Lazy<PrivateWith> Construct()
        {
            return new Lazy<PrivateWith>(() =>
            {
                var obj = CreateInstance(_address1.Value, _city.Value, _postalCode.Value);

                obj.Address2 = _address2.Value;
                obj.State = _state.Value;

                return obj;
            });
        }

        #endif

        BuilderObject = Construct()

    }

    PostBuild(BuilderObject.Value);

    return BuilderObject.Value;
}
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
