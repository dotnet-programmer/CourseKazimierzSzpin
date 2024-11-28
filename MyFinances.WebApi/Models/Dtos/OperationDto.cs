namespace MyFinances.WebApi.Models.Dtos;

public class OperationDto
{
	public int OperationId { get; set; }
	public string Name { get; set; } = null!;
	public string Description { get; set; }
	public decimal Value { get; set; }
	public DateTime CreatedDate { get; set; }
	public int CategoryId { get; set; }
}
