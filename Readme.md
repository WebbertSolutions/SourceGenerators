# Source Generators

This is a collection of source generators that I've created.  

If you don't know what a Source Generator is or how to build one, check out my tutorial **[Source Generator Tutorial](https://github.com/WebbertSolutions/SourceGeneratorTutorial)**

The all files in the BaseClass folder and both of the current generate interface source files are **`linked source code`**, so any changes made will propagate through all the source generators.  I plan on continuing my journey of understanding source generators and therefore, these files will be changing.  The plan is to minimize any breaking changes.

<br/>

### Collection 

- **Object Mothers** -- Generate builder objects.  

	- This is useful for creating objects to create test data.  
	
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
