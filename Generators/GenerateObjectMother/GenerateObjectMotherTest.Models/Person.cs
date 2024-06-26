﻿namespace GenerateObjectMotherTest.Models;

public class Person
{
	public string FirstName { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;
	public List<Address> Addresses { get; set; } = new();

	public string FullName => $"{FirstName} {LastName}";
}
