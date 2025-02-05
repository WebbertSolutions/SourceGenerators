namespace WS.Gen.ObjectMother;

public class BuilderConstructorGenerator
{
	private static readonly StringBuilder sb = new (10000);


	public static string Generate(string builderName, InterfaceInformation interfaceInformation)
	{
		sb.Clear();

		string setters = GetSetters(interfaceInformation);

		return $@"
    //
    //  Builder Constructors
    //

    public static {builderName} Default()
    {{
        return new {builderName}();
    }}


    public static {builderName} Default(string json)
    {{
		var obj = System.Text.Json.JsonSerializer.Deserialize<{interfaceInformation!.ClassName}>(json);
        return Default(obj);
    }}


    public static {builderName} Default({interfaceInformation!.ClassName} obj)
    {{
        var builder = new {builderName}()
{setters};

		return builder;
    }}	
";
	}


	private static string GetSetters(InterfaceInformation interfaceInformation)
	{
		var tabs = Utility.Tabs(3);

		foreach (var item in interfaceInformation.GetMembers())
		{
			if (item.IsReadOnly || item.Accessibility != Accessibility.Public)
				continue;

			sb.AppendLine(GetSetter(tabs, item));
		}

		if (sb.Length > Utility.NewlineLength)
			sb.Length -= Utility.NewlineLength;


		return sb.ToString();
	}


	private static string GetSetter(string tabs, IClassMemberInfo member)
	{
		return $@"{tabs}.With{member.Name}(obj.{member.Name})";
	}
}
