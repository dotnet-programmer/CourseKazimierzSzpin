using SendEmail.WebApp.Core;
using SendEmail.WebApp.Core.Repositories;

namespace SendEmail.WebApp.Persistence.Repositories;

public class EmailRepository(IApplicationDbContext context) : IEmailRepository
{
}