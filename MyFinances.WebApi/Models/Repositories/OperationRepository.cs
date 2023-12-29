using MyFinances.WebApi.Models.Domains;

namespace MyFinances.WebApi.Models.Repositories;

public class OperationRepository
{
	private readonly MyFinancesContext _context;

	public OperationRepository(MyFinancesContext context)
    {
        _context = context;
    }

    public IEnumerable<Operation> GetOperations()
	{
		return _context.Operations;
	}

	public Operation GetOperation(int operationId)
	{
		return _context.Operations.FirstOrDefault(x => x.OperationId == operationId);
	}

	public void AddOperation(Operation operation)
	{
		operation.CreatedDate = DateTime.Now;
		_context.Operations.Add(operation);
	}

	public void UpdateOperation(Operation operation)
	{
		var operationToUpdate = _context.Operations.First(x => x.OperationId == operation.OperationId);
		operationToUpdate.CategoryId = operation.CategoryId;
		operationToUpdate.Description = operation.Description;
		operationToUpdate.Name = operation.Name;
		operationToUpdate.Value = operation.Value;
	}

	public void DeleteOperation(int operationId)
	{
		var operationToDelete = _context.Operations.First(x => x.OperationId == operationId);
		_context.Operations.Remove(operationToDelete);
	}
}
