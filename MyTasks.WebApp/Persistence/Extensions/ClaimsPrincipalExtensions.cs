using System.Security.Claims;

namespace MyTasks.WebApp.Persistence.Extensions;

public static class ClaimsPrincipalExtensions
{
	public static string GetUserId(this ClaimsPrincipal model) => model.FindFirstValue(ClaimTypes.NameIdentifier);
}