using SendEmail.WebApp.Core;
using SendEmail.WebApp.Core.Repositories;

namespace SendEmail.WebApp.Persistence.Repositories;

public class SettingsRepository(IApplicationDbContext context) : ISettingsRepository
{
}