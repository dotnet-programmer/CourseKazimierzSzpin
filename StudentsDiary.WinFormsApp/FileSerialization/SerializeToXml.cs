using System.Xml.Serialization;

namespace StudentsDiary.WinFormsApp.FileSerialization;

internal class SerializeToXml<T>(string filePath, string fileName) : SerializeToFile<T>(filePath, fileName, ".xml") where T : new()
{
	public override void Serialize(T item)
	{
		string path = GetPath();
		CreateDirectoryIfNotExist(path);
		using (StreamWriter stream = new(path))
		{
			XmlSerializer serializer = new(typeof(T));
			serializer.Serialize(stream, item);
		}
	}

	public override T Deserialize()
	{
		string path = GetPath();

		if (!File.Exists(path))
		{
			return new T();
		}

		using (StreamReader stream = new(path))
		{
			XmlSerializer serializer = new(typeof(T));
			return (T)serializer.Deserialize(stream);
		}
	}
}