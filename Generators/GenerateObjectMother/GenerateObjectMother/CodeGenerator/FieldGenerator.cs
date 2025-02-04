namespace WS.Gen.ObjectMother;

public class FieldGenerator
{
	private static readonly StringBuilder sb = new (10000);


	public static string Generate(InterfaceInformation interfaceInformation)
	{
		return GetAllFieldBackers(interfaceInformation);
	}


	private static string GetAllFieldBackers(InterfaceInformation interfaceInformation)
	{
		sb.Clear();
		
		foreach (var item in interfaceInformation.GetMembers())
		{
			if (item.IsReadOnly || item.Accessibility != Accessibility.Public)
				continue;

			sb.AppendLine(GetFieldBackers(item));
		}

		return sb.ToString();
	}


	private static string GetFieldBackers(IClassMemberInfo property)
	{
		return $"\tprivate Lazy<{property.DataType}> {property.FieldName} = new(default({property.DataType}));";
	}
}
