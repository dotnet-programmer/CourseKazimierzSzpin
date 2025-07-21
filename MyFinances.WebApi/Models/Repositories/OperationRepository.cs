using MyFinances.WebApi.Models.Domains;

namespace MyFinances.WebApi.Models.Repositories;

public class OperationRepository(MyFinancesContext context)
{
	public IEnumerable<Operation> GetOperations()
		=> context.Operations;

	public IEnumerable<Operation> GetOperations(int recordCount, int page)
		=> context.Operations.Skip(recordCount * (page - 1)).Take(recordCount);

	public Operation GetOperation(int operationId)
		=> context.Operations.FirstOrDefault(x => x.OperationId == operationId);

	public void AddOperation(Operation operation)
	{
		operation.CreatedDate = DateTime.Now;
		context.Operations.Add(operation);
	}

	public void UpdateOperation(Operation operation)
	{
		var operationToUpdate = context.Operations.First(x => x.OperationId == operation.OperationId);
		operationToUpdate.CategoryId = operation.CategoryId;
		operationToUpdate.Description = operation.Description;
		operationToUpdate.Name = operation.Name;
		operationToUpdate.Value = operation.Value;
	}

	public void DeleteOperation(int operationId)
	{
		var operationToDelete = context.Operations.First(x => x.OperationId == operationId);
		context.Operations.Remove(operationToDelete);
	}
}