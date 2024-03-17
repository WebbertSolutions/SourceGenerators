# Source Generators

This is a collection of source generators that I've created.  

If you don't know what a Source Generator is or how to build one, check out my tutorial **[Source Generator Tutorial](https://github.com/WebbertSolutions/SourceGeneratorTutorial)**

I'm planning on reusing the folder BaseClass in the generator GenerateObjectMother throughout the generators.  It has been optimized in such a way that I can copy it from project to project and reuse it as is.

<br/>

## Collection 

### Object Mothers - Generate builder objects

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

