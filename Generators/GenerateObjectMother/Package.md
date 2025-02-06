# **Object Mothers** - Generate Builder Objects

For full information, please visit: https://github.com/WebbertSolutions/SourceGenerators/tree/main/Generators/GenerateObjectMother


## Quick Start

- Add namespace: **using WS.Gen.ObjectMother;**
	
- Create a class in your test project 
  - Name doesn't matter 
  - "Builder" not required
- Add attribute
- Type should be your class you want a builder for

````
using WS.Gen.ObjectMother;

[ObjectMotherBuilder(typeof(Address), true)]
public partial class AddressBuilder
{
}
````

- Under your test project navigate down  Anzlyzers -> GenerateObjectMother -> WS.Gen.ObjectMother.ObjectMotherGenerator 
- Double click on your class builder.
- Copy the code at the bottom and paste into your class

````
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
````

- Change the parameter value **xx** to suite your needs
- Add additional methods as needed

````
public static AddressBuilder BeverlyHills()
{
    return new AddressBuilder()
        .WithAddress1("123 Main Street")
        .WithAddress2("Suite 200")
        .WithCity("Beverly Hills")
        .WithState(() => StateBuilder.Typical().WithName("CA").Build())
        .WithPostalCode("90210")
        ;
}
````

