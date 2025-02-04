namespace WS.Gen.ObjectMother;


#if DEBUG

public class DebugHelpers
{
	//
	// DEBUG CODE
	//

	public static void Write<T>(string filename, string text, T? x, T? y) where T : class
	{
		Write(filename, $"{text}:  {x == y}  {x!.Equals(y!)}\n");
	}


	public static void Write(string filename, string? text)
	{
		const string path = "D:\\temp\\GenerateObjectMother\\Output\\";

		File.AppendAllText(Path.Combine(path, filename), $"{text}\n");
	}
}

#endif