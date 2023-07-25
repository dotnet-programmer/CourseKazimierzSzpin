using System.Xml.Serialization;

namespace StudentsDiary.WinFormsApp.FileSerialization;

internal class SerializeToXml<T> : SerializeToFile<T> where T : new()
{
	private const string XmlExt = ".xml";

	public SerializeToXml(string filePath, string fileName) : base(filePath, fileName, XmlExt)
	{
	}

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
			var item = (T)serializer.Deserialize(stream);
			return item;
		}
	}
}