using System;
using System.Collections.Generic;
using System.Linq;
using StudentsDiary.WpfApp.Models.Domains;
using StudentsDiary.WpfApp.Models.Wrappers;

namespace StudentsDiary.WpfApp.Models.Converters;

public static class StudentConverter
{
	public static StudentWrapper ToWrapper(this Student student)
		=> new()
		{
			Id = student.Id,
			FirstName = student.FirstName,
			LastName = student.LastName,
			Comments = student.Comments,
			Activities = (bool)student.Activities,
			Group = new GroupWrapper { Id = student.Group.Id, Name = student.Group.Name },
			Math = string.Join(", ", student.Ratings.Where(x => x.SubjectId == (int)Subject.Math).Select(x => x.Rate)),
			Physics = string.Join(", ", student.Ratings.Where(x => x.SubjectId == (int)Subject.Physics).Select(x => x.Rate)),
			PolishLang = string.Join(", ", student.Ratings.Where(x => x.SubjectId == (int)Subject.PolishLang).Select(x => x.Rate)),
			ForeignLang = string.Join(", ", student.Ratings.Where(x => x.SubjectId == (int)Subject.ForeignLang).Select(x => x.Rate)),
			Technology = string.Join(", ", student.Ratings.Where(x => x.SubjectId == (int)Subject.Technology).Select(x => x.Rate)),
		};

	// obiekty domenowe to tzw. DAO (Data Access Object)
	public static Student ToDao(this StudentWrapper studentWrapper)
		=> new()
		{
			Id = studentWrapper.Id,
			FirstName = studentWrapper.FirstName,
			LastName = studentWrapper.LastName,
			GroupId = studentWrapper.Group.Id,
			Comments = studentWrapper.Comments,
			Activities = studentWrapper.Activities,
		};

	public static List<Rating> ToRatingDao(this StudentWrapper studentWrapper)
	{
		List<Rating> ratings = [];
		foreach (var subject in Enum.GetValues<Subject>())
		{
			FillRatings(studentWrapper, ratings, subject);
		}
		return ratings;
	}

	private static void FillRatings(StudentWrapper studentWrapper, List<Rating> ratings, Subject subject)
		=> studentWrapper
			.GetType()
			.GetProperty(subject.ToString())
			.GetValue(studentWrapper)?
			.ToString()
			.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
			.ToList()
			.ForEach(rate => ratings.Add(new Rating { Rate = int.Parse(rate), StudentId = studentWrapper.Id, SubjectId = (int)subject }));
}