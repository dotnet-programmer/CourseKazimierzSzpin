using SendEmail.WebApp.Core.Repositories;

namespace SendEmail.WebApp.Core;

public interface IUnitOfWork
{
	IContactRepository ContactRepository { get; }
	IEmailRepository EmailRepository { get; }
	ISettingsRepository SettingsRepository { get; }

	Task CompleteAsync();
}