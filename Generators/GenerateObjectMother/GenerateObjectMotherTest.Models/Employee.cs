namespace GenerateObjectMotherTest.Models;

public class Employee : Person
{ 
	public int EmployeeId { get; set; }
	public int? OfficeLocationId { get; set; }
}