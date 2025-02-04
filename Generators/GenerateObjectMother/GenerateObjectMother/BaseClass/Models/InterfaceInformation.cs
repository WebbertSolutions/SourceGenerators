namespace WS.Gen.ObjectMother.Models;

public record InterfaceInformation
(
	string Namespace,
	string ClassName,
	bool GenerateSample,
	List<ConstructorInformation> Constructors,
	List<ClassMember> Properties,
	List<ClassMember> Fields,
	InterfaceInformation? Inherited
)
{
	public IEnumerable<IClassMemberInfo> GetMembers()
	{
		return GetMembers(this);
	}

	private static IEnumerable<IClassMemberInfo> GetMembers(InterfaceInformation? interfaceInformation)
	{
		if (interfaceInformation == null)
			return Array.Empty<IClassMemberInfo>();

		return interfaceInformation!.Properties.Cast<IClassMemberInfo>()
			.Union(interfaceInformation!.Fields.Cast<IClassMemberInfo>())
			.Union(GetMembers(interfaceInformation.Inherited));
	}


	public virtual bool Equals(InterfaceInformation? y)
		=> y != null && Equals(this, y);


	private static bool Equals(InterfaceInformation x, InterfaceInformation y)
	{
#if DEBUG
		//DebugHelpers.Write("InterfaceInformation.txt", $"Constructor: {x.Constructors.Matches(y.Constructors)}");
		//DebugHelpers.Write("InterfaceInformation.txt", $" Properties: {x.Properties.Matches(y.Properties)}");
		//DebugHelpers.Write("InterfaceInformation.txt", $"     Fields: {x.Fields.Matches(y.Fields)}");
		//DebugHelpers.Write("InterfaceInformation.txt", $"");
#endif

		return
			x.Namespace == y.Namespace &&
			x.ClassName == y.ClassName &&
			x.GenerateSample == y.GenerateSample &&
			x.Constructors.Matches(y.Constructors) &&
			x.Properties.Matches(y.Properties) &&
			x.Fields.Matches(y.Fields) &&
			x.Inherited == y.Inherited;
	}


	public override int GetHashCode()
	{
		return HashCode.Generate(
			Namespace,
			ClassName,
			GenerateSample,
			Constructors,
			Properties,
			Fields,
			Inherited
		);
	}
}
