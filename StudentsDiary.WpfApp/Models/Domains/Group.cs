using System.Collections.Generic;
using System.Collections.ObjectModel;

// INFO - EF Konfiguracja 3 - atrybuty w klasie domenowej - niezalecane rozwiązanie, uzależnia model domenowy od bazy danych
//using System.ComponentModel.DataAnnotations;

namespace StudentsDiary.WpfApp.Models.Domains;

public class Group
{
	public int Id { get; set; }

	//[Required]
	public string Name { get; set; }

	public ICollection<Student> Students { get; set; } = new Collection<Student>();
}