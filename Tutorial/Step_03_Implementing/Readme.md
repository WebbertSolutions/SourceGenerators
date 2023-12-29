# Implementing the Source Generator

### Before implementation
As odd as this sounds, you really should create the generator before creating the actual Roslyn source generator. If you are following along from the beginning, **Step 1 - Planning** laid out the groundwork for the current step.

The reason for this is sanity.  The less time you spend writing, working in or modifying the Roslyn source generator the better overall experience you will have, as it can be a rather frustrating endeavor.

In order to stay out of the actual generator, the next step entails creation of a template using a T4 Template.
[Code Generation and T4 Text Templates](https://learn.microsoft.com/en-us/visualstudio/modeling/code-generation-and-t4-text-templates?view=vs-2022).  This is really the old and mostly manual way of creating generated objects.  While it is the old way it really still holds value as to how to create something the new way.  By using this technology, you will be able to rapidly flesh out the code that you will need to create your source generator.


</br>

### Creating the T4 Template
Creating the template is nothing more than adding a new item to your project, which is name "Text Template".

The following code should be added to the bottom of the newly generated file. The code creates the basic format that you should follow.

````
<#= Run() #>
<#+
	//
	//	REQUIRED DATA STRUCTURES
	//


	//
	//	RUN METHOD
	//
	public static string Run()
	{
	}


	//
	//	START OF TEMPLATE
	//

#>
````

The basics are this
- ````<#= Run() #>```` this will run the template and produce the output.  Any text, including line breaks, before or after it will also be included.
- **Section:** Data Structures - this is where you should drop in your data structures that are required.  This should be a cut/paste operation from **Step 2 - Basic Setup** BaseClass\Models.
- **Section:** Run Method - This should be a cut/paste operation from **Step 1 - Planning** of the Program.Run() method.
- **Section:** Start of Template - This should be a cut/paste operation from **Step 1 - Planning** FileGenerator.cs.

The **Templates\BuilderTemplate.tt** shows the fully populated example of what the first stage of this looks like.

</br>

### Fleshing out the T4 Template
This is where the real work begins on creating your source generator.  

Provided that you didn't fully flesh out your implementation in **Step 1 - Planning**, this is where you will do it.  As soon as you make changes and save the template file, the output is generated. The output file can be viewed by clicking on the arrow next the template file.  It will have the same name as the template with a .txt extension.

**==<span style="color:red">WARNING:</span>==** Do not make big changes. If you make changes and something doesn't work it can be very difficult to figure out what you did to break it.

The T4 Template provides rapid development just by means of saving the template file.  There is no need to build, run and debug.  I typically have both the template and the resulting file both pinned on the screen at the same time so I can edit, save and immediately see the result.

Continue making small changes and saving while you continue to build your final template.  Add methods and / or fill in the stubs that already exist.  This is very much an iterative process.

You may wish to go back and modify the class in **Step 1 - Planning** if you run into problems with what you are building.  It compiles and runs appropriately, you can copy the class back in whole to the template file for use.

</br>

### Checking the Validity of the Output
The default, out of box, format for a T4 Template is text.  You can change this to be the format of what you are expecting:
- .cs
- .xml
- .json
- etc

by modifying the extension: `` <#@ output extension=".txt" #> ``.

When you open up the resulting output, you will get all the benefits of the IDE that you are using.  It will tell you all the places that you have a syntax error or where you can streamline your code.

Make the changes that you want / need to make in your T4 Template and continue to iterate until it is fully functional.

**==<span style="color:red">WARNING:</span>==** Change the format back to text (.txt) when you are done and verify by looking in the folder that the file format you changed it to, is no longer on disk.  If you created a CSharp file it will get compiled even if it is on disk but not showing in your solution.


</br>

### A Word to the Wise
If you have completed all of the following steps and suggestions, you are now ready to begin creating your source generator.

**==<span style="color:red">WARNING:</span>==** I can not stress enough the importance of following all the previous steps.  If you don't, that is fine, but be aware that you will be in for a lot of headaches.

Working in the actual source generator and making changes **can / will be**  frustrating.  There is no way around it.  This technology is new and still evolving and right now, caching is your biggest enemy.

By doing all the pre-work, you will spend less time in the generator, therefore you will have less headaches along the way.

When in things are not working they way you entend, do a full solution clean, shut down Visual Studio and restart the solution.  On rare occasion, you may have to delete the hidden .VS folder from the disk before opening the solution again.

</br>

### Implementing the Actual Source Generator

- Copy the class from your T4 Template which produces the output into an actual CSharp file (see GenerateBuilder.cs)
- Create another class for outputing your required interface (see GenerateInterface.cs).  Lines 16-22 are optional depending on your needs.  This will be discussed later.
- Update your data structures if necessary to match what is in your T4 Template

</br>

#### Defining the Interface
During the generation of object(s) an attribute will be added to a new test class.  This attribute will be used to generate the code.

The basic structure is this:

````
[GenerateDataBuilder(typeof(Address))]
public partial class AddressBuilder
{
}
````

And the attribute which is also generated will look like this

````
namespace WebbertSolutions.Generators;

internal class GenerateDataBuilderAttribute : System.Attribute 
{
	public Type ClassType { get; }


	public GenerateDataBuilderAttribute(System.Type type)
	{
		ClassType = type;
	}
}
````

This attribute will be unique to every source generator.  You may not have need for the ClassType field.  This really depends on what ***your*** generator needs to do.

Once you have your attribute defined, you can copy / paste it into the "GenerateInterface.cs" file and fix it up to be parameterized.


#### Updating the Generator Class
Open your class generator (see RandomTestDataGenerator.cs)

This is where you will need to spend some time implementing custom code.
- Fill in PostInitializationOutput with the calls to output the interface and actual file generator created in the previous sections.
- Implement ScrapeInformation and/or ProcessInterface - This is place to fill in your data structure which will be used to generate your output.
	- ScrapeInformation - grab the information from a class as definded by your interface (e.g. Address class).
	- ProcessInterface - grab the information from a class that the attribute is attached to (e.g. AddressBuilder class).

	Both of these classes in my example are calling into a base class to populate the data structures.  You may have to add, modify or completely replace the innards of these methods to fit your needs.  Looking at the base implementation will give you some ideas about how to get to the information that you need.

	If you are unable to find the information that you need for the 2 methods above, stub them out so they will compile and preceed with the following steps.  You will have to wait to fill it in until **Step 4 - Debugging**.


At this point, you should try to build your solution and fix up any syntax errors and references that are necessary.


</br>
</br>

# Testing the Source Generator

<font size="5"><span style="color:red">***Hic sunt dracones...***</span> (Here be dragons...)
<span style="color:red">**The path to madness begins here...**</span></font>


Changes made from this point forward will likely entail shutting down Visual Studio and re-opening the project in order to see the change or make the generator work.  This will be done ***ad nauseam***, so get used to it.

All the pre-work up to this point is to minimize this time consuming / frustrating step.

- Create a new library for testing (MyTestAppTests)
	- add a project reference to the Source Generator - This would be managed through adding a NuGet package for a real implementation, but we aren't at that step yet.
	- Open up the project and add the following to the newly added project reference to the source generator

		```` 
		OutputItemType="Analyzer" ReferenceOutputAssembly="false"
		````

		This makes sure that generator DLL is not output as part of the final product and that it should be recognized as an analyzer.

- Rebuild the full solution and make sure there is no errors.
- Create a test class in your new library and drop in what is required for your generator to run (see PersonBuilder.cs)
	- Leave the attribute commented out
	- Add the namespace of the attribute as well. I have mine in the _GlobalUsings.cs file.
- Do a build again and then shut down Visual Studio.
- Reload the solution
- Uncomment the namespace and attribute

At this point, fingers crossed, everything should just work. This is predicated on the fact that you followed all the previous sections and steps.


</br>


# Viewing the Output
There are 2 ways to see the output
- Look under the Analyzers in the test project
	- MyTestAppTests -> Dependencies -> Analyzers -> RandomTestDataGenerator
- Emit the output
	- Open the test project and add the following in the PropertyGroup section

		````
		<EmitCompilerGeneratedFiles>true</EmitCompilerGeneratedFiles>
		````
		**==<span style="color:red">WARNING:</span>==** Do not leave this set to true long term. This will prevent the generator running automatically.  In order to see the output a build is required.
	- MyTestAppTests -> obj -> Debug -> <.net version> -> generated -> RandomTestDataGenerator



</br>


# Looking for Performance Issues
Located within RandomTestDataGenerator is the Execute() method.  Contained within is a dictionary to count the number of times the generator has been run for each object that is being generated.  This means that the AddressBuilder, PersonBuilder and StateBuilder should all have different values over time.  The counter is reset every time that the solution is opened in Visual Studio.

The generator contains a paramter to output the value.  This is not required and is only useful for determining if the source generator is thrashing every time you make a change.

The only changes that should be causing the generator to run is something like
- adding / removing / renaming a property / field
- changing the data type of a property / field
- changing the namespace of the object being examined

Not
- adding a comment or space in the same file

This entirely depends on what you are actually capturing and storing in the classes of the Models folder.

By making changes, including comments and seeing when the date/time stamp changes or the counter increases, you will be able to determine if your comparers are complete and working correctly.

</br>


# Troubleshooting
There really is no magic formula to help here.  

This tutorial is setup such that you can use a tool like WinMerge to diff the provided tutorial folder structure and see what meaningful changes were made between the steps.  Hopefully you will see something you missed in yours and be able to correct the problem.

If you are missing data for your structure or something is not proceeding correctly during the generation, like a null reference exception, you will need to setup the generator so you can debug into it. 

Debugging is covered in the next section.