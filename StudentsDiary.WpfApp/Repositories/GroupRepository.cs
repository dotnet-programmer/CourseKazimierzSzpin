using System.Collections.Generic;
using System.Linq;
using StudentsDiary.WpfApp.Models.Domains;

namespace StudentsDiary.WpfApp.Repositories;

internal class GroupRepository
{
	public List<Group> GetGroups()
	{
		using (var context = new AppDbContext())
		{
			return context.Groups.ToList();
		}
	}
}