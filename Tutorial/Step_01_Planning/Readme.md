# Preparing to Create a Source Generator

### Produce a working version of the final product
Working backwards is the easiest way to create a generated object.  
Doing so will help you to 
- Identify sections that need to be repeated
- Parts that need need to be replaced dynamically
- Verify the final product is valid for what you need
</br>

### Create a Throw-a-way Application
Create a simple console application that lays things out in the general format that you would like.  
This will include  
- A "Run" method which will be useful for testing (Program.cs)
- Classes to hold the information that you need to produce the final product (Models folder)
- A class which to house all the methods and string interpolations to create the final product (FileGenerator.cs)

At this point in time, FileGenerator contains stub methods that returns placeholder data.

When creating your own generator, add as much or as little as you would like. I prefer to make meaningful changes by the method in **Step 3 - Implementation**.

This code will be used during **Step 3 - Implementation**, so make sure it compiles and is capable of returning output in a string format.
</br>

### Run the Application
Make sure that the application runs and returns something in the general form that you need.


