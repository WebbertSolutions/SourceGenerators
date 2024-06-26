﻿namespace WS.Gen.ObjectMother.Models;

public class ClassMember
{
	public Accessibility Accessibility { get; set; }

	public string DataType { get; set; } = string.Empty;
	public string PropertyName { get; set; } = string.Empty;
	public string FieldName => GetFieldName();

	public bool IsReadOnly { get; set; }	
	public bool IsValueType { get; set; }
	public bool IsGenericType { get; set; }
	public bool IsCollection { get; set; }


	private string GetFieldName()
	{
		return $"_{ToLowerChar()}{PropertyName.Substring(1)}";
	}

	private string ToLowerChar()
	{
		return PropertyName.ToLower().Substring(0, 1);
	}
}