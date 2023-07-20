using System.Text.Json;

namespace StudentsDiary.WinFormsApp.FileSerialization;

internal class SerializeToJson<T> : SerializeToFile<T> where T : new()
{
	private const string JsonExt = ".json";

	public SerializeToJson(string filePath, string fileName) : base(filePath, fileName, JsonExt)
	{
	}

	public override void Serialize(T item)
	{
		string path = GetPath();
		CreateDirectoryIfNotExist(path);
		var json = JsonSerializer.Serialize(item, new JsonSerializerOptions { WriteIndented = true });
		File.WriteAllText(path, json);
	}

	public override T Deserialize()
	{
		string path = GetPath();
		if (!File.Exists(path))
		{
			return new T();
		}
		var json = File.ReadAllText(path);
		return JsonSerializer.Deserialize<T>(json);
	}
}

//internal class SerializeToJson<T> : ISerializable<T> where T : new()
//{
//	private readonly string _filePath;
//	private readonly string _fileName;
//	private const string JsonExt = ".json";

//	public SerializeToJson(string filePath, string fileName)
//	{
//		_filePath = filePath;
//		_fileName = fileName;
//	}

//	public void Serialize(T item)
//	{
//		string path = GetPath();
//		CreateDirectoryIfNotExist(path);
//		var json = JsonSerializer.Serialize(item, new JsonSerializerOptions { WriteIndented = true });
//		File.WriteAllText(path, json);
//	}

//	public T Deserialize()
//	{
//		string path = GetPath();
//		if (!File.Exists(path))
//		{
//			return new T();
//		}
//		var json = File.ReadAllText(path);
//		return JsonSerializer.Deserialize<T>(json);
//	}

//	private void CreateDirectoryIfNotExist(string path)
//	{
//		var directoryName = Path.GetDirectoryName(path);
//		if (!Directory.Exists(directoryName))
//		{
//			Directory.CreateDirectory(directoryName);
//		}
//	}

//	private string GetPath() => Path.Combine(_filePath, _fileName + JsonExt);
//}