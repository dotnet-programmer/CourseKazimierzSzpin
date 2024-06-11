using System.Collections.Generic;
using System.Linq;
using StudentsDiary.WpfApp.Models;
using StudentsDiary.WpfApp.Models.Domains;

namespace StudentsDiary.WpfApp.Repositories;

internal class GroupRepository
{
	public List<Group> GetGroups()
	{
		using (AppDbContext context = new())
		{
			return context.Groups.ToList();
		}
	}
}