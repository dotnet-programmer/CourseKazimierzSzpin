using SendEmail.WebApp.Core;
using SendEmail.WebApp.Core.Services;

namespace SendEmail.WebApp.Persistence.Services;

public class EmailService(IUnitOfWork unitOfWork) : IEmailService
{

}