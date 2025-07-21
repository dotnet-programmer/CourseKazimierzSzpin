using System.Text.Json;
using System.Xml.Serialization;

namespace StudentsDiary.WinFormsApp.Helpers;

internal class FileHelper<T>(string filePath, string fileName) where T : new()
{
	private const string XmlExt = ".xml";
	private const string JsonExt = ".json";

	private static readonly JsonSerializerOptions _jsonSerializerOptions = new() { WriteIndented = true };
	
	private readonly string _filePath = filePath;
	private readonly string _fileName = fileName;

	public void SerializeToXML(T item)
	{
		string path = GetPathToXml();
		CreateDirectoryIfNotExist(path);
		using StreamWriter stream = new(path);
		(new XmlSerializer(typeof(T))).Serialize(stream, item);
	}

	public T DeserializeFromXML()
	{
		string path = GetPathToXml();

		if (!File.Exists(path))
		{
			return new T();
		}

		using StreamReader stream = new(path);
		return (T)(new XmlSerializer(typeof(T))).Deserialize(stream);
	}

	public void SerializeToJSON(T item)
	{
		string path = GetPathToJson();
		CreateDirectoryIfNotExist(path);
		File.WriteAllText(path, JsonSerializer.Serialize(item, _jsonSerializerOptions));
	}

	public T DeserializeFromJSON()
	{
		string path = GetPathToJson();

		if (!File.Exists(path))
		{
			return new T();
		}

		return JsonSerializer.Deserialize<T>(File.ReadAllText(path));
	}

	private void CreateDirectoryIfNotExist(string path)
	{
		var directoryName = Path.GetDirectoryName(path);
		if (!Directory.Exists(directoryName))
		{
			Directory.CreateDirectory(directoryName);
		}
	}

	private string GetPathToXml() 
		=> Path.Combine(_filePath, _fileName + XmlExt);

	private string GetPathToJson() 
		=> Path.Combine(_filePath, _fileName + JsonExt);
}