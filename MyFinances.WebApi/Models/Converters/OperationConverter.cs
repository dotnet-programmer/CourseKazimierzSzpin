using MyFinances.WebApi.Models.Domains;
using MyFinances.WebApi.Models.Dtos;

namespace MyFinances.WebApi.Models.Converters;

public static class OperationConverter
{
	public static OperationDto ToDto(this Operation model)
		=> new OperationDto
		{
			OperationId = model.OperationId,
			Name = model.Name,
			Description = model.Description,
			Value = model.Value,
			CreatedDate = model.CreatedDate,
			CategoryId = model.CategoryId,
		};

	public static IEnumerable<OperationDto> ToDtos(this IEnumerable<Operation> model)
		=> (model == null) ? Enumerable.Empty<OperationDto>() : model.Select(x => x.ToDto());

	public static Operation ToDao(this OperationDto model)
		=> new Operation
		{
			OperationId = model.OperationId,
			Name = model.Name,
			Description = model.Description,
			Value = model.Value,
			CreatedDate = model.CreatedDate,
			CategoryId = model.CategoryId,
		};
}