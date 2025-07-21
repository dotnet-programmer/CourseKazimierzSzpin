using System.Text.Json;

namespace StudentsDiary.WinFormsApp.FileSerialization;

internal class SerializeToJson<T>(string filePath, string fileName) : SerializeToFile<T>(filePath, fileName, ".json") where T : new()
{
	private static readonly JsonSerializerOptions _jsonSerializerOptions = new() { WriteIndented = true };

	public override void Serialize(T item)
	{
		string path = GetPath();
		CreateDirectoryIfNotExist(path);
		string json = JsonSerializer.Serialize(item, _jsonSerializerOptions);
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