namespace WS.Gen.ObjectMother;

public class TypicalGenerator
{
	private static readonly StringBuilder sb = new (10000);


	public static string Generate(string builderName, InterfaceInformation interfaceInformation)
	{
		var all = interfaceInformation.GetMembers()
			.Where(item => item.IsReadOnly == false && item.Accessibility == Accessibility.Public);

		var nullable = all.Where(item => item.IsNullable).OrderBy(item => item.Name).ToList();
		var notNullable = all.Where(item => !item.IsNullable).OrderBy(item => item.Name).ToList();

		if (!interfaceInformation.GenerateSample)
			return string.Empty;

		sb.Clear();
		sb.Append("/*");

		GetRequired(builderName, notNullable);
		GetTypical(builderName, nullable);

		sb.AppendLine("*/");

		return sb.ToString();
	}


	private static void GetRequired(string builderName, List<IClassMemberInfo> members)
	{
		sb.Append($@"
	public static {builderName} Required()
	{{
		return new {builderName}()
");

		foreach (var item in members)
			sb.AppendLine($"\t\t\t.With{item.Name}(() => xx)");

		sb.Length -= Utility.NewlineLength;

		if (members.Count > 0)
			sb.AppendLine($@"
			;");
		else
			sb.AppendLine($@";");

		sb.AppendLine($@"	}}");
	}


	private static void GetTypical(string builderName, List<IClassMemberInfo> members)
	{
		sb.Append($@"

	public static {builderName} Typical()
	{{
		return Required()
");

		foreach (var item in members)
			sb.AppendLine($"\t\t\t.With{item.Name}(() => xx)");

		sb.Length -= Utility.NewlineLength;

		if (members.Count > 0)
			sb.AppendLine($@"
			;");
		else
			sb.AppendLine($@";");
		
		sb.AppendLine($@"	}}");
	}
}
