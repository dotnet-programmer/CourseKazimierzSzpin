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