namespace WS.Gen.ObjectMother;

public class PropertyGenerator
{
	private static readonly StringBuilder sb = new (10000);


	public static string Generate(string builderName, InterfaceInformation interfaceInformation)
	{
		return GetAllWithProperties(builderName, interfaceInformation);
	}


	private static string GetAllWithProperties(string builderName, InterfaceInformation interfaceInformation)
	{
		sb.Clear();

		foreach (var item in interfaceInformation.GetMembers())
		{
			if (item.IsReadOnly || item.Accessibility != Accessibility.Public)
				continue;

			sb.Append(GetWithProperties(builderName, item));
		}

		if (sb.Length > Utility.NewlineLength)
			sb.Length -= Utility.NewlineLength;

		return sb.ToString();
	}


	private static string GetWithProperties(string builderName, IClassMemberInfo member)
	{
		return $@"

	//
	//	{member.Name}
	//
	    
    public {builderName} With{member.Name}({member.DataType} value = default)
		=> With{member.Name}(() => value);
    
    public {builderName} With{member.Name}(Func<{member.DataType}> func)
    {{
        {member.FieldName} = func;
        return this;
    }}
";
	}
}
