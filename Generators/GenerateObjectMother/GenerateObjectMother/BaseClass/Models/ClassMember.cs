namespace WS.Gen.ObjectMother.Models;

public record ClassMember 
(
	Accessibility Accessibility,
	ClassMemberType ClassMemberType,
	string DataType,
	string Name,
	bool IsReadOnly,
	bool IsValueType,
	bool IsGenericType,
	bool IsCollection,
	bool IsNullable
) : IClassMemberInfo
{
	private string? _fieldName;

	public string FieldName => _fieldName ??= GetFieldName();

	private string GetFieldName()
		=> $"_{ToLowerChar()}{Name.Substring(1)}";

	private string ToLowerChar()
		=> Name.ToLower().Substring(0, 1);



	public virtual bool Equals(ClassMember? y)
		=> y != null && Equals(this, y);

	private static bool Equals(ClassMember x, ClassMember y)
	{
		//DebugHelpers.Write("ClassMember.txt", $"  Name:          {x.Name} {y.Name}");
		//DebugHelpers.Write("ClassMember.txt", $"  Accessibility: {x.Accessibility == y.Accessibility}");
		//DebugHelpers.Write("ClassMember.txt", $"ClassMemberType: {x.ClassMemberType == y.ClassMemberType}");
		//DebugHelpers.Write("ClassMember.txt", $"       DataType: {x.DataType == y.DataType}");
		//DebugHelpers.Write("ClassMember.txt", $"           Name: {x.Name == y.Name}");
		//DebugHelpers.Write("ClassMember.txt", $"     IsReadOnly: {x.IsReadOnly == y.IsReadOnly}");
		//DebugHelpers.Write("ClassMember.txt", $"    IsValueType: {x.IsValueType == y.IsValueType}");
		//DebugHelpers.Write("ClassMember.txt", $"  IsGenericType: {x.IsGenericType == y.IsGenericType}");
		//DebugHelpers.Write("ClassMember.txt", $"   IsCollection: {x.IsCollection == y.IsCollection}");
		//DebugHelpers.Write("ClassMember.txt", $"     IsNullable: {x.IsNullable == y.IsNullable}");
		//DebugHelpers.Write("ClassMember.txt", $"");

		return
			x.Accessibility == y.Accessibility &&
			x.ClassMemberType == y.ClassMemberType &&
			x.DataType == y.DataType &&
			x.Name == y.Name &&
			x.IsReadOnly == y.IsReadOnly &&
			x.IsValueType == y.IsValueType &&
			x.IsGenericType == y.IsGenericType &&
			x.IsCollection == y.IsCollection &&
			x.IsNullable == y.IsNullable;
	}


	public override int GetHashCode()
	{
		return HashCode.Generate(
			Accessibility,
			ClassMemberType,
			DataType,
			Name,
			IsReadOnly,
			IsValueType,
			IsGenericType,
			IsCollection,
			IsNullable
		);
	}
}
