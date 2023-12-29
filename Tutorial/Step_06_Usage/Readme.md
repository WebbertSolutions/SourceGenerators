# How to use the NuGet Source Generator
There are plenty of videos and articles out there on how to import a NuGet Package that you can view, but I will show you an easy way to test locally without the need to publish it somewhere.


### Setup
- Create a location on your local hard drive.  I'm using D:\Temp\NuGet
- In Visual Studio select  Tools -> Options -> NuGet Package Manager -> Package Sources
	- Click the "+" button in the top right corner and fill in the information
		- Name: Local Repository
		- Source: D:\Temp\NuGet
	- Click the "Ok" button at the bottom
	- Close all other windows

</br>

### Add the NuGet Package
- Copy the output file from **Step 5 - Packaging** to the new folder
- Create a clone of the test application and add it to the solution
	- Remove the reference to the Source Generator
	- Go through the process of adding a NuGet package using the local repository as the source
- Shutdown Visual Studio
- Restart the solution
- Do a full rebuild all of the solution

Your classes should still be generated and you can view them using one of the methods described in **Step 3 - Implementing**.

You should now be able to 
- Uncomment the body of the PersonBuilder class, see that there is a build error
- Uncomment the attribute and see the error go away
- Right click on PersonBuilder, select "Go to definition (F12)", select the one that implements the generic Builder<T> and see what was generated.

