﻿namespace MyTasks.WebApp.Core.Models;

public class GetTasksParams
{
	public string UserId { get; set; }
	public bool IsExecuted { get; set; }
	public int CategoryId { get; set; }
	public string Title { get; set; }
}