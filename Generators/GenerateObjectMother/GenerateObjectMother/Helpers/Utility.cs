namespace WS.Gen.ObjectMother;

public static class Utility
{
	public static readonly int NewlineLength;
	public static readonly string Newline;


	static Utility()
	{
		var sb = new StringBuilder();
		sb.AppendLine("");

		Newline = sb.ToString();
		NewlineLength = Newline.Length;
	}


	public static string Tabs(int count)
	{
		return count switch
		{
			5 => "\t\t\t\t\t",
			4 => "\t\t\t\t",
			3 => "\t\t\t",
			2 => "\t\t",
			_ => "\t",
		};
	}
}
