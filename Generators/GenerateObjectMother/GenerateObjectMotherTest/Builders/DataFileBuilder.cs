namespace GenerateObjectMotherTest.Builders;

[ObjectMotherBuilder(typeof(DataFile), true)]
public partial class DataFileBuilder
{
	public static DataFileBuilder Typical()
	{
		var text = "Hello World";
		var bytes = Encoding.UTF8.GetBytes(text);

		return new DataFileBuilder()
			.WithDataFileId(100)
			.WithFilename("foo")
			.WithContents(bytes)
			;
	}
}
