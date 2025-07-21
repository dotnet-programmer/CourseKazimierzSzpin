namespace MyFinances.WebApi.Models.Response;

// klasa odpowiedzi, zwracana w przypadku metod typu void
public class Response
{
	public List<Error> Errors { get; } = [];
	public bool IsSuccess => Errors == null || Errors.Count == 0;
}