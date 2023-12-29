# Create the Basic Infrastructure of the Source Generator

### Create the Source Generator Library
Create a new **.NET Standard 2.0 Class Library** and add it to your existing solution.

**==<span style="color:red">WARNING:</span>==**  It is very important that you create it as a **.NET Standard 2.0 Class Library**.  If you do not, the generator will not run.
**==<span style="color:red">WARNING:</span>==**  Changing the library type after creation has lead to odd results.  Best to delete project and start over before you get too far.

</br>

### Project Modifications
- Add NuGet package - **Microsoft.CodeAnalysis.CSharp**
- Add the following to the PropertyGroup in the project file
	````
	<LangVersion>Latest</LangVersion>
	<EnforceExtendedAnalyzerRules>true</EnforceExtendedAnalyzerRules>
	<Nullable>enable</Nullable>
	````
	The last one is optional and you can specify an actual version of at least 10.0.
</br>

### Add the Basic Infrastructure
Take a look at the source code provided. 

These objects layout the basic structure for any source source generator.

You will see the following structure
- BaseClass
	- Comparers
		- ClassInformationComparer
		- GeneratorInformationComparer
	- Models
		- ClassInformation
		- GeneratorInformation
		- PropertyInformation
	- BaseGenerator
- RandomTestDataGenerator

These objects are my attempt to start to normalize the creation of a basic infrastructure that can be re-used for multiple projects.  While the names are unimportant, the general structure and contents will be important.

Models and comparers are necessary in order to prevent unnecessary thrashing of the generator, so do not skip using them.

- Models - Hold the information that you will need for your generator to work properly
- Comparers - Compare your models to determine if a ***meaningful*** change has been made.  By using this in conjuction with ``.WithComparer(_comparer);`` , adding a comment to the item being investigated will ***not*** cause the generator to run.
- BaseGenerator - Contains the basic flow of the generator.  The majority of this file can be left alone.

</br>



### Making Modifications

**BaseGenerator** - This largely could be left alone.  If you don't plan on using the GeneratorInformation class, then you will need to do a search on GeneratorInformation and replace it with the class name you plan on using.

The parts you might need to change
- GetSemanticTarget - Depending on what your generator needs to look at, you may need information from the class that the attribute is attached to, or information about another class as defined within the interface itself.  For the generator we will be building, both will be necessary.
- GetClassInformation - This is just a helper method for extracting information about a class out of the Microsoft Semantic Model.</br></br>This method will give you a good idea of how to get to the data you need.

If you don't intend to use my BaseGenerator class, you will need to derive from `IIncrementalGenerator`. At this point, I highly recommend leaving both: Initialize and IsSyntaxTarget in whole.  This is boiler plate stuff that you definitely need.
</br>


**RandomTestDataGenerator** 
Change the following
- Name of the class to your generator name
- Class namespace
- Constant values

The attribute `[Generator]` is very much needed, so leave it in place.  
