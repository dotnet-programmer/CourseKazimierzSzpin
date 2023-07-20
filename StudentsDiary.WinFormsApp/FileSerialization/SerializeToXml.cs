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

//internal class SerializeToXml<T> : ISerializable<T> where T : new()
//{
//	private readonly string _filePath;
//	private readonly string _fileName;
//	private const string XmlExt = ".xml";

//	public SerializeToXml(string filePath, string fileName)
//	{
//		_filePath = filePath;
//		_fileName = fileName;
//	}

//	public void Serialize(T item)
//	{
//		string path = GetPath();
//		CreateDirectoryIfNotExist(path);
//		using (StreamWriter stream = new(path))
//		{
//			XmlSerializer serializer = new(typeof(T));
//			serializer.Serialize(stream, item);
//		}
//	}

//	public T Deserialize()
//	{
//		string path = GetPath();
//		if (!File.Exists(path))
//		{
//			return new T();
//		}
//		using (StreamReader stream = new(path))
//		{
//			XmlSerializer serializer = new(typeof(T));
//			var item = (T)serializer.Deserialize(stream);
//			return item;
//		}
//	}

//	private void CreateDirectoryIfNotExist(string path)
//	{
//		var directoryName = Path.GetDirectoryName(path);
//		if (!Directory.Exists(directoryName))
//		{
//			Directory.CreateDirectory(directoryName);
//		}
//	}

//	private string GetPath() => Path.Combine(_filePath, _fileName + XmlExt);
//}