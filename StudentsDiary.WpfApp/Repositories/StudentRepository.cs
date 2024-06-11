using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StudentsDiary.WpfApp.Models;
using StudentsDiary.WpfApp.Models.Converters;
using StudentsDiary.WpfApp.Models.Domains;
using StudentsDiary.WpfApp.Models.Wrappers;

namespace StudentsDiary.WpfApp.Repositories;

internal class StudentRepository
{
	public List<StudentWrapper> GetStudents(int groupId)
	{
		using (AppDbContext context = new())
		{
			var students = context.Students
				.Include(x => x.Group)
				.Include(x => x.Ratings)
				.AsQueryable();

			if (groupId != 0)
			{
				students = students.Where(x => x.GroupId == groupId);
			}

			return students
				.ToList()
				.Select(x => x.ToWrapper())
				.ToList();
		}
	}

	public void AddStudent(StudentWrapper studentWrapper)
	{
		var student = studentWrapper.ToDao();
		var ratings = studentWrapper.ToRatingDao();

		using (AppDbContext context = new())
		{
			var dbStudent = context.Students.Add(student);

			context.SaveChanges();

			ratings.ForEach(x =>
			{
				x.StudentId = dbStudent.Entity.Id;
				context.Ratings.Add(x);
			});

			context.SaveChanges();
		}
	}

	public void DeleteStudent(int id)
	{
		using (AppDbContext context = new())
		{
			var studentToDelete = context.Students.Find(id);
			context.Students.Remove(studentToDelete);
			context.SaveChanges();
		}
	}

	public void UpdateStudent(StudentWrapper studentWrapper)
	{
		var student = studentWrapper.ToDao();
		var ratings = studentWrapper.ToRatingDao();

		using (AppDbContext context = new())
		{
			UpdateStudentProperties(student, context);
			context.SaveChanges();

			var studentRatings = GetStudentRatings(student, context);

			UpdateRate(student, ratings, context, studentRatings, Subject.Math);
			UpdateRate(student, ratings, context, studentRatings, Subject.Technology);
			UpdateRate(student, ratings, context, studentRatings, Subject.Physics);
			UpdateRate(student, ratings, context, studentRatings, Subject.PolishLang);
			UpdateRate(student, ratings, context, studentRatings, Subject.ForeignLang);

			context.SaveChanges();
		}
	}

	private static List<Rating> GetStudentRatings(Student student, AppDbContext context) 
		=> context.Ratings
			.Where(x => x.StudentId == student.Id)
			.ToList();

	private static void UpdateStudentProperties(Student student, AppDbContext context)
	{
		var studentToUpdate = context.Students.Find(student.Id);
		studentToUpdate.FirstName = student.FirstName;
		studentToUpdate.LastName = student.LastName;
		studentToUpdate.Comments = student.Comments;
		studentToUpdate.Activities = student.Activities;
		studentToUpdate.GroupId = student.GroupId;
	}

	private static void UpdateRate(Student student, List<Rating> newRatings, AppDbContext context, List<Rating> studentRatings, Subject subject)
	{
		var subjectRatings = studentRatings
			.Where(x => x.SubjectId == (int)subject)
			.Select(x => x.Rate);

		var newSubjectRatings = newRatings
			.Where(x => x.SubjectId == (int)subject)
			.Select(x => x.Rate);

		var subjectRatingsToDelete = GetSubjectRatingsToDelete(subjectRatings, newSubjectRatings);
		var subjectRatingsToAdd = GetSubjectRatingsToAdd(subjectRatings, newSubjectRatings);

		subjectRatingsToDelete.ForEach(x =>
		{
			var ratingToDelete = context.Ratings.First(y => y.Rate == x && y.StudentId == student.Id && y.StudentId == (int)subject);
			context.Ratings.Remove(ratingToDelete);
		});

		subjectRatingsToAdd.ForEach(x =>
		{
			var ratingToAdd = new Rating { Rate = x, StudentId = student.Id, SubjectId = (int)subject };
			context.Ratings.Add(ratingToAdd);
		});
	}

	private static List<int> GetSubjectRatingsToDelete(IEnumerable<int> oldSubRatings, IEnumerable<int> newSubRatings)
	{
		List<int> subRatingsToDelete = [];
		List<int> newListCopy = new(newSubRatings);
		foreach (var item in oldSubRatings)
		{
			var itemInNewList = newListCopy.FirstOrDefault(x => x == item);
			if (itemInNewList != 0)
			{
				newListCopy.Remove(itemInNewList);
			}
			else
			{
				subRatingsToDelete.Add(item);
			}
		}
		return subRatingsToDelete;
	}

	private static List<int> GetSubjectRatingsToAdd(IEnumerable<int> oldSubRatings, IEnumerable<int> newSubRatings)
	{
		List<int> subRatingsToAdd = [];
		List<int> oldListCopy = new(oldSubRatings);
		foreach (var item in newSubRatings)
		{
			var itemInOldList = oldListCopy.FirstOrDefault(x => x == item);
			if (itemInOldList != 0)
			{
				oldListCopy.Remove(itemInOldList);
			}
			else
			{
				subRatingsToAdd.Add(item);
			}
		}
		return subRatingsToAdd;
	}
}