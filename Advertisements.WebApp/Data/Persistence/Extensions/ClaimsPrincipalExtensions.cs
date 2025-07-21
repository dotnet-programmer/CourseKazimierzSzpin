using System.Security.Claims;

namespace Advertisements.WebApp.Data.Persistence.Extensions;

public static class ClaimsPrincipalExtensions
{
	public static string GetUserId(this ClaimsPrincipal model)
		=> model.FindFirstValue(ClaimTypes.NameIdentifier);
}