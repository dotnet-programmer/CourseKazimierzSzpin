﻿using MyTasks.WebApp.Core;
using MyTasks.WebApp.Core.Repositories;
using MyTasks.WebApp.Persistence.Repositories;

namespace MyTasks.WebApp.Persistence;

public class UnitOfWork : IUnitOfWork
{
	private readonly IApplicationDbContext _context;

	public UnitOfWork(IApplicationDbContext context)
	{
		_context = context;
		TaskRepository = new TaskRepository(context);
		CategoryRepository = new CategoryRepository(context);
	}

	// INFO - obiekty repozytoryjne, jeżeli więcej repozytoriów, to więcej takich właściwości
	public ITaskRepository TaskRepository { get; set; }
	public ICategoryRepository CategoryRepository { get; set; }

	public void Complete() 
		=> _context.SaveChanges();
}