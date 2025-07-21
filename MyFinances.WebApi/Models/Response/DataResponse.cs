namespace MyFinances.WebApi.Models.Response;

// klasa odpowiedzi, zwracana w przypadku metod, które zwracają jakieś dane, wtedy trafiają do właściwości Data
public class DataResponse<T> : Response
{
    public T Data { get; set; }
}