//using Newtonsoft.Json;
using System.Text.Json;
using System.Xml.Serialization;

namespace StudentsDiary.WinFormsApp.Helpers;

internal class FileHelper<T> where T : new()
{
	private const string XmlExt = ".xml";
	private const string JsonExt = ".json";

	private readonly string _filePath;
	private readonly string _fileName;

	public FileHelper(string filePath, string fileName)
	{
		_filePath = filePath;
		_fileName = fileName;
	}

	public void SerializeToXML(T item)
	{
		string path = GetPathToXml();
		CreateDirectoryIfNotExist(path);
		using (StreamWriter stream = new(path))
		{
			XmlSerializer serializer = new(typeof(T));
			serializer.Serialize(stream, item);
		}
	}

	public T DeserializeFromXML()
	{
		string path = GetPathToXml();

		if (!File.Exists(path))
		{
			return new T();
		}

		using (StreamReader stream = new(path))
		{
			XmlSerializer serializer = new(typeof(T));
			var item = (T)serializer.Deserialize(stream);
			return item;
		}
	}

	public void SerializeToJSON(T item)
	{
		string path = GetPathToJson();
		CreateDirectoryIfNotExist(path);

		// using System.Text.Json;
		var json = JsonSerializer.Serialize(item, new JsonSerializerOptions { WriteIndented = true });

		// using Newtonsoft.Json;
		//var json = JsonConvert.SerializeObject(item, Formatting.Indented);

		File.WriteAllText(path, json);
	}

	public T DeserializeFromJSON()
	{
		string path = GetPathToJson();

		if (!File.Exists(path))
		{
			return new T();
		}

		var json = File.ReadAllText(path);

		// using System.Text.Json;
		return JsonSerializer.Deserialize<T>(json);

		// using Newtonsoft.Json;
		//return JsonConvert.DeserializeObject<T>(json);
	}

	private void CreateDirectoryIfNotExist(string path)
	{
		var directoryName = Path.GetDirectoryName(path);
		if (!Directory.Exists(directoryName))
		{
			Directory.CreateDirectory(directoryName);
		}
	}

	private string GetPathToXml() => Path.Combine(_filePath, _fileName + XmlExt);

	private string GetPathToJson() => Path.Combine(_filePath, _fileName + JsonExt);
}