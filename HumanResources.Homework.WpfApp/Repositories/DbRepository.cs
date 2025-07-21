using HumanResources.Homework.WpfApp.Models;
using HumanResources.Homework.WpfApp.Models.Wrappers;
using Microsoft.EntityFrameworkCore;

namespace HumanResources.Homework.WpfApp.Repositories;

internal class DbRepository
{
	public bool IsValidConnectionToDataBase()
	{
		try
		{
			using (AppDbContext context = new())
			{
				context.Database.OpenConnection();
				context.Database.CloseConnection();
			}
			return true;
		}
		catch (Exception)
		{
			return false;
		}
	}

	public bool LoginToApplication(UserWrapper userWrapper, string password)
	{
		try
		{
			using (AppDbContext context = new())
			{
				var user = context.Users.First(x => x.Name == userWrapper.Name);
				if (user.Password.StartsWith(StringCipherHelper.NOT_ENCRYPTED_PASSWORD_PREFIX))
				{
					user.Password = StringCipherHelper.EncryptString(user.Password.Replace(StringCipherHelper.NOT_ENCRYPTED_PASSWORD_PREFIX, string.Empty));
					context.SaveChanges();
				}
				return StringCipherHelper.DecryptString(user.Password) == password;
			}
		}
		catch (Exception)
		{
			return false;
		}
	}

	//public static bool IsDatabaseExists()
	//{
	//	using (AppDbContext context = new())
	//	{
	//		return context.Database.CanConnect();
	//	}
	//}

	//public static bool CreateDatabase()
	//{
	//	var result = false;
	//	try
	//	{
	//		using (AppDbContext context = new())
	//		{
	//			result = context.Database.EnsureCreated();
	//		}
	//		return result;
	//	}
	//	catch (Exception)
	//	{
	//		return result;
	//	}
	//}

	//public static List<TableDescription> GetDescriptions()
	//{
	//	var connection = new SqlConnection($@"
	//		Server={UserSettings.GetStringFromConfig("ServerAddress")}\{UserSettings.GetStringFromConfig("ServerName")};
	//		Database={UserSettings.GetStringFromConfig("Database")};
	//		User Id={UserSettings.GetStringFromConfig("User")};
	//		Password={StringCipherHelper.DecryptStringFromConfig("Password")};
	//		TrustServerCertificate=True;");

	//	var command = new SqlCommand(@"
	//		select st.name [Table], sc.name [Column], sep.value [Description]
	//		from sys.tables st
	//			inner join sys.columns sc on st.object_id = sc.object_id
	//			inner join sys.extended_properties sep on st.object_id = sep.major_id
	//				and sc.column_id = sep.minor_id and sep.name = 'MS_Description'",
	//		connection);

	//	connection.Open();
	//	var reader = command.ExecuteReader();

	//	List<TableDescription> descriptions = new();

	//	while (reader.Read())
	//	{
	//		System.Windows.MessageBox.Show($"{reader["Table"]}");
	//		descriptions.Add(new TableDescription()
	//		{
	//			Table = reader["Table"].ToString(),
	//			Column = reader["Column"].ToString(),
	//			Description = reader["Description"].ToString()
	//		});
	//	}

	//	return descriptions;
	//}
}