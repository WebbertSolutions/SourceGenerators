# Tutorial

### Overview
This tutorial is laid out with folders which are label in order of importance and what will be discussed.  Each step is then built upon by the next, so it would be wise to step through each one the first time through this tutorial.

Each folder has an additional Readme.md file that discusses in greater detail the purpose of the step. Additionally, you can use a tool like WinMerge to diff the folders in order to see the important changes.

The source code in each step should be examine to fully understand what is being done.

</br>

### Requirements
The majority of this tutorial will apply to either Visual Studio or VS Code.
However, I'm not a VS Code guy so you may have to do some research on your own as to how to proceed.

The 2 areas that may be a problem are
- **Tooling in general** - In a minute, you will see an option you will need for Visual Studio.  VS Code may have a similar requirement.
- **Step 4 - Debugging** - This may only be available in Visual Studio.

These are the minium requirements given the implementation, using method **ForAttributeWithMetadataName**, that I will be showing.

|              | Version     |
| ------------ | ----------- |
| Roslyn       | 4.3.1       | 
| .NET SDK     | 6.0.400     | 
| C#           | 10.0        | 
| Visual Studio| 17.3        | 
| C# VS Code   | 1.25.0      | 

After upgrading Visual Studio to at least 17.3, you will have to add an additional feature through the installer.
Individual Components -> Compilers, build tools, and runtimes -> .NET Compiler Platform SDK

</br>
