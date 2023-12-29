namespace MyTestAppTests.Helpers;


public static class GetRandomValue
{
	public static Random Random { get; set; } = new Random();


	public static string String(int maxLength)
	{
		return String(0, maxLength);
	}

	public static string String(int minLength, int maxLength)
	{
		var length = Int32(minLength, maxLength);

		var sb = new StringBuilder(string.Empty, length);

		for (var index = 0; index < length; index++)
			sb.Append(Convert.ToChar(Int32(65, 90)));

		return sb.ToString();
	}


	public static int Int32(int minValue, int maxValue)
	{
		return Random.Next(minValue, maxValue);
	}
}