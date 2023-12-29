namespace MyFinances.WebApi.Models.Response;

public class Response
{
	public List<Error> Errors { get; } = [];
	public bool IsSuccess => Errors == null || !Errors.Any();
}
