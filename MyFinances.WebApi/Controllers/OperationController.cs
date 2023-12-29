using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFinances.WebApi.Models;
using MyFinances.WebApi.Models.Domains;
using MyFinances.WebApi.Models.Response;

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

	//[HttpGet]
	//public IEnumerable<Operation> GetOperation2()
	//{
	//	return _unitOfWork.OperationRepository.GetOperations();
	//}

	//[HttpGet]
	//public IActionResult GetOperation3()
	//{
	//	if (true)
	//	{
	//		return BadRequest();
	//	}

	//	if (true)
	//	{
	//		return NotFound();
	//	}

	//	return Ok(_unitOfWork.OperationRepository.GetOperations());
	//}

	[HttpGet]
	// INFO - nazwa nie ma znaczenia
	public DataResponse<IEnumerable<Operation>> GetOperations()
	{
		DataResponse<IEnumerable<Operation>> response = new();

		try
		{
			response.Data = _unitOfWork.OperationRepository.GetOperations();
		}
		catch (Exception ex)
		{
			// logowanie do pliku...
			response.Errors.Add(new Error(ex.Source, ex.Message));
		}

		return response;
	}

	// INFO - nazwa parametru, który ma zostać przekazany po nazwie kontrolera
	[HttpGet("{operationId}")]
	public DataResponse<Operation> GetOperation(int operationId)
	{
		DataResponse<Operation> response = new();

		try
		{
			response.Data = _unitOfWork.OperationRepository.GetOperation(operationId);
		}
		catch (Exception ex)
		{
			// logowanie do pliku...
			response.Errors.Add(new Error(ex.Source, ex.Message));
		}

		return response;
	}

	[HttpPost]
	public DataResponse<int> AddOperation(Operation operation)
	{
		DataResponse<int> response = new();

		try
		{
			_unitOfWork.OperationRepository.AddOperation(operation);
			_unitOfWork.Complete();
			response.Data = operation.OperationId;
		}
		catch (Exception ex)
		{
			// logowanie do pliku...
			response.Errors.Add(new Error(ex.Source, ex.Message));
		}

		return response;
	}

	[HttpPut]
	public Response UpdateOperation(Operation operation)
	{
		Response response = new();

		try
		{
			_unitOfWork.OperationRepository.UpdateOperation(operation);
			_unitOfWork.Complete();
		}
		catch (Exception ex)
		{
			// logowanie do pliku...
			response.Errors.Add(new Error(ex.Source, ex.Message));
		}

		return response;
	}

	[HttpDelete("{operationId}")]
	public Response DeleteOperation(int operationId)
	{
		Response response = new();

		try
		{
			_unitOfWork.OperationRepository.DeleteOperation(operationId);
			_unitOfWork.Complete();
		}
		catch (Exception ex)
		{
			// logowanie do pliku...
			response.Errors.Add(new Error(ex.Source, ex.Message));
		}

		return response;
	}
}
