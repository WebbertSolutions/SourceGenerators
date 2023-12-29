# Debugging the Source Generator
I've seen a couple of methods to debugging and the method I will show is by far, in my opinion, is the easiest and most straight forward approach.


### Setup
- Open the source generator project file and add the following to the PropertyGroup
	```` 
	<IsRoslynComponent>true</IsRoslynComponent>
	````
- Close Visual Studio
- Re-open the solution
- Open the properties for the source generator project
	- Click on or scroll down to the "Debug" section
	- Click on the "Open debug launch profiles UI"
	- Delete the existing profile
	- Add a new profile based on "Rosyln Component"
	- In the drop down select your test application
- close all windows

This added the profile to the Properties -> launchSettings.json file.

</br>

### Debugging
- Change the startup project to be the source generator
- Open the source code for the BaseGenerator.cs and set a break point at the beginning of the Initialize method.
- Launch the debugger by pressing F5 or however you normally do

At this point you should hit the break point.

Preceeding at this point is normal.  You have access to the watch windows and can set conditional break points.

</br>

### Examining the Microsoft Classes
There are 2 types of classes you can examine
- Syntax
- Semantic

Syntax is just that, it is the information produced by a parsing lexar to determine if your source code is valid.  Additional information can be found here: [Work with syntax](https://learn.microsoft.com/en-us/dotnet/csharp/roslyn-sdk/work-with-syntax).  If you would like to see what this looks like, in Visual Studio, select view -> Other Windows -> Syntax Visualizer.  You can then click on any class definition and then begin exploring.

Semantic provides additional information about the actual classes themselves which could include attributes, interfaces, base classes etc. Additional information can be found here: [Work with semantics](https://learn.microsoft.com/en-us/dotnet/csharp/roslyn-sdk/work-with-semantics).

These 2 pieces are at the heart of generation.  These are big topics and are impossible to described here in this getting started tutorial.

The best I can say is, look at the object you are currently working with while debugging.  You will be able to navigate up via a parent property or down via the child you are interested in.



</br>

### Final Words of Wisdom

I've given you some help by providing the **GetClassInformation()** method in the **BaseGenerator** class as how to get to some of the information.  

Source Generators are still new to me and therefore what I have provided is nowhere near complete for every possible thing you or I may need.  

It is meant to be a starting point for me as I create more generators.  I plan on continuing to refine, refactor and add helper methods in the future as I find a need for them.
