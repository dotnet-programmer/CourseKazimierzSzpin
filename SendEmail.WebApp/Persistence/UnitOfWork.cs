using SendEmail.WebApp.Core;
using SendEmail.WebApp.Core.Repositories;
using SendEmail.WebApp.Persistence.Repositories;

namespace SendEmail.WebApp.Persistence;

public class UnitOfWork(IApplicationDbContext context) : IUnitOfWork
{
	public IContactRepository ContactRepository { get; } = new ContactRepository(context);
	public IEmailRepository EmailRepository { get; } = new EmailRepository(context);
	public ISettingsRepository SettingsRepository { get; } = new SettingsRepository(context);

	public async Task CompleteAsync()
		=> await context.SaveChangesAsync();
}