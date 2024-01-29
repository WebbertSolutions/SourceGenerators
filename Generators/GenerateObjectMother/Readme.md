# Packaging the Source Generator into a NuGet Package
There are plenty of videos and articles out there on how to create a NuGet Package, so I will not really describe it here but will provide the basics of what is needed and the few specific changes that are necessary.


### Setup
Add the following 2 sections of code to the Source Generator project file.

````
  <PropertyGroup>
    <GeneratePackageOnBuild>true</GeneratePackageOnBuild>
    <IncludeBuildOutput>false</IncludeBuildOutput>
    <Version>1.0.0</Version>
    <Title>Test Data Mother Object Generator</Title>
    <Authors>David Elliott</Authors>
    <Company>Webbert Solutions, LLC</Company>
    <Description>A source code generator for creating mother objects for use in creating test code</Description>
  </PropertyGroup>
````

````
  <ItemGroup>
    <!-- Place the generator in the analyzer directory of the NuGet package -->
    <None Include="$(OutputPath)\$(AssemblyName).dll"
          Pack="true"
          PackagePath="analyzers/dotnet/cs"
          Visible="false" />
  </ItemGroup>
````

The first just provides general information about the NuGet package and is really unimportant with the exception of the IncludeBuildOutput.  This just says that the generator DLL should not be included in your project output.

The second provides information as to the fact that this is an analyzer and where to place it within the NuGet package, so when imported it can be handled appropriately.

</br>

### Create the NuGet Package
All you need to do is, build the generator in Release mode.

This will create  bin -> Release -> RandomTestDataGenerator.1.0.0.nupkg

