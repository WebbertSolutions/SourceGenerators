﻿namespace WS.Gen.ObjectMother.Models;

public interface IClassMemberInfo
{
	public string Name { get; }
	public string FieldName { get; }
	public string DataType { get; }
	public Accessibility Accessibility { get; }
	public bool IsReadOnly { get; }
	public ClassMemberType ClassMemberType { get; }
	public bool IsNullable { get; }
}
