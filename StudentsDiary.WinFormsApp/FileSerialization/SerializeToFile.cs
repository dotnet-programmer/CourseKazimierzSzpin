namespace StudentsDiary.WinFormsApp.FileSerialization;

internal abstract class SerializeToFile<T>(string filePath, string fileName, string fileExtension) where T : new()
{
	private readonly string _filePath = filePath;
	private readonly string _fileName = fileName;
	private readonly string _fileExtension = fileExtension;

	public abstract void Serialize(T item);

	public abstract T Deserialize();

	protected string GetPath() 
		=> Path.Combine(_filePath, _fileName + _fileExtension);

	protected static void CreateDirectoryIfNotExist(string path)
	{
		var directoryName = Path.GetDirectoryName(path);
		if (!Directory.Exists(directoryName))
		{
			Directory.CreateDirectory(directoryName);
		}
	}
}