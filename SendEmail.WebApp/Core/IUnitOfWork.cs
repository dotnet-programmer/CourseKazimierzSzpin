using SendEmail.WebApp.Core.Repositories;

namespace SendEmail.WebApp.Core;

public interface IUnitOfWork
{
	IEmailRepository EmailRepository { get; }
	ISettingsRepository SettingsRepository { get; }
	void Complete();
}