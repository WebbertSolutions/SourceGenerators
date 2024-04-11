namespace GenerateObjectMotherTest.Models;

public class DataFile
{
	public int DataFileId { get; set; }
	public string Filename { get; set; } = null!;
	public byte[] Contents { get; set; } = null!;
}
