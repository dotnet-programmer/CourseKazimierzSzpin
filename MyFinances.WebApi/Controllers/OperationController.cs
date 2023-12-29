using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFinances.WebApi.Models;
using MyFinances.WebApi.Models.Converters;
using MyFinances.WebApi.Models.Domains;
using MyFinances.WebApi.Models.Dtos;
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
	public DataResponse<IEnumerable<OperationDto>> GetOperations()
	{
		DataResponse<IEnumerable<OperationDto>> response = new();

		try
		{
			response.Data = _unitOfWork.OperationRepository.GetOperations().ToDtos();
		}
		catch (Exception ex)
		{
			// logowanie do pliku...
			response.Errors.Add(new Error(ex.Source, ex.Message));
		}

		return response;
	}

	/// <summary>
	/// Get operation by Operation Id
	/// </summary>
	/// <param name="operationId">Operation Id</param>
	/// <returns>DataResponse - Operation Dto</returns>
	// INFO - nazwa parametru, który ma zostać przekazany po nazwie kontrolera
	[HttpGet("{operationId}")]
	public DataResponse<OperationDto> GetOperation(int operationId)
	{
		DataResponse<OperationDto> response = new();

		try
		{
			response.Data = _unitOfWork.OperationRepository.GetOperation(operationId)?.ToDto();
		}
		catch (Exception ex)
		{
			// logowanie do pliku...
			response.Errors.Add(new Error(ex.Source, ex.Message));
		}

		return response;
	}

	[HttpPost]
	public DataResponse<int> AddOperation(OperationDto operationDto)
	{
		DataResponse<int> response = new();

		try
		{
			var operation = operationDto.ToDao();
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
	public Response UpdateOperation(OperationDto operation)
	{
		Response response = new();

		try
		{
			_unitOfWork.OperationRepository.UpdateOperation(operation.ToDao());
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
