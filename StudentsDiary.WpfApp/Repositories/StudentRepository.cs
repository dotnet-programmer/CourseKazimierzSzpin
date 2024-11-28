using System;
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
		Student student = studentWrapper.ToDao();
		List<Rating> ratings = studentWrapper.ToRatingDao();

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
		Student student = studentWrapper.ToDao();
		List<Rating> newRatings = studentWrapper.ToRatingDao();

		using (AppDbContext context = new())
		{
			UpdateStudentProperties(context, student);
			UpdateStudentRatings(context, student, newRatings);
			context.SaveChanges();
		}
	}

	private static void UpdateStudentProperties(AppDbContext context, Student student)
	{
		var studentToUpdate = context.Students.Find(student.Id);
		studentToUpdate.FirstName = student.FirstName;
		studentToUpdate.LastName = student.LastName;
		studentToUpdate.Comments = student.Comments;
		studentToUpdate.Activities = student.Activities;
		studentToUpdate.GroupId = student.GroupId;
	}

	private static void UpdateStudentRatings(AppDbContext context, Student student, List<Rating> newRatings)
	{
		List<Rating> oldRatings = context.Ratings
			.Where(x => x.StudentId == student.Id)
			.ToList();

		foreach (var subject in Enum.GetValues<Subject>())
		{
			UpdateSubjectRatings(context, student, newRatings, oldRatings, subject);
		}
	}

	private static void UpdateSubjectRatings(AppDbContext context, Student student, List<Rating> newRatings, List<Rating> oldRatings, Subject subject)
	{
		var oldSubjectRatings = oldRatings
			.Where(x => x.SubjectId == (int)subject)
			.Select(x => x.Rate);

		var newSubjectRatings = newRatings
			.Where(x => x.SubjectId == (int)subject)
			.Select(x => x.Rate);

		var subjectRatingsToDelete = GetSubjectRatingsToDelete(oldSubjectRatings, newSubjectRatings);
		var subjectRatingsToAdd = GetSubjectRatingsToAdd(oldSubjectRatings, newSubjectRatings);

		subjectRatingsToDelete.ForEach(x =>
		{
			var ratingToDelete = context.Ratings.First(y => y.Rate == x && y.StudentId == student.Id && y.SubjectId == (int)subject);
			context.Ratings.Remove(ratingToDelete);
		});

		subjectRatingsToAdd.ForEach(x =>
		{
			var ratingToAdd = new Rating { Rate = x, StudentId = student.Id, SubjectId = (int)subject };
			context.Ratings.Add(ratingToAdd);
		});
	}

	private static List<int> GetSubjectRatingsToDelete(IEnumerable<int> oldSubjectRatings, IEnumerable<int> newSubjectRatings)
	{
		List<int> subjectRatingsToDelete = [];
		List<int> newListCopy = new(newSubjectRatings);
		foreach (var item in oldSubjectRatings)
		{
			var itemInNewList = newListCopy.FirstOrDefault(x => x == item);
			if (itemInNewList != 0)
			{
				newListCopy.Remove(itemInNewList);
			}
			else
			{
				subjectRatingsToDelete.Add(item);
			}
		}
		return subjectRatingsToDelete;
	}

	private static List<int> GetSubjectRatingsToAdd(IEnumerable<int> oldSubjectRatings, IEnumerable<int> newSubjectRatings)
	{
		List<int> subRatingsToAdd = [];
		List<int> oldListCopy = new(oldSubjectRatings);
		foreach (var item in newSubjectRatings)
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