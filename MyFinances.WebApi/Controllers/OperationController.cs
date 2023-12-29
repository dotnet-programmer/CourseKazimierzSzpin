using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFinances.WebApi.Models;
using MyFinances.WebApi.Models.Domains;

namespace MyFinances.WebApi.Controllers;

// INFO - oznaczenie kontrolera WebApi
[ApiController]

// INFO - wyznaczona ścieżka pod którą można dostać się do kontrolera, zamiast [controller] będzie wstawiana nazwa kontrolera
[Route("api/[controller]")]
public class OperationController : ControllerBase
{
	private readonly UnitOfWork _unitOfWork;
	public OperationController(UnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	[HttpGet]
	public IEnumerable<Operation> GetOperation()
	{
		return _unitOfWork.OperationRepository.GetOperations();
	}
}
