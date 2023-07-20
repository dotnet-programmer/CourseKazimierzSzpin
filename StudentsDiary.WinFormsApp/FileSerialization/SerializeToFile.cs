namespace StudentsDiary.WinFormsApp.FileSerialization;

internal abstract class SerializeToFile<T> where T : new()
{
	private readonly string _filePath;
	private readonly string _fileName;
	private readonly string _fileExtension;

	public SerializeToFile(string filePath, string fileName, string fileExtension)
	{
		_filePath = filePath;
		_fileName = fileName;
		_fileExtension = fileExtension;
	}

	public abstract void Serialize(T item);

	public abstract T Deserialize();

	protected string GetPath() => Path.Combine(_filePath, _fileName + _fileExtension);

	protected void CreateDirectoryIfNotExist(string path)
	{
		var directoryName = Path.GetDirectoryName(path);
		if (!Directory.Exists(directoryName))
		{
			Directory.CreateDirectory(directoryName);
		}
	}
}