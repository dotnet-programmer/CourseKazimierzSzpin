using Microsoft.AspNetCore.Mvc;
using MyFinances.WebApi.Models;
using MyFinances.WebApi.Models.Converters;
using MyFinances.WebApi.Models.Dtos;
using MyFinances.WebApi.Models.Response;

namespace MyFinances.WebApi.Controllers;

/// <summary>
/// Controller to manage operations.
/// </summary>
/// <param name="unitOfWork"></param>

// INFO - oznaczenie kontrolera WebApi
[ApiController]

// INFO - wyznaczona ścieżka pod którą można dostać się do kontrolera, zamiast [controller] będzie wstawiana nazwa kontrolera
[Route("api/[controller]")]
public class OperationController(UnitOfWork unitOfWork) : ControllerBase
{
	// inny typ zwracanej wartości - tutaj po prostu lista danych zwróconych z DB
	//[HttpGet]
	//public IEnumerable<Operation> GetOperation2()
	//{
	//	return _unitOfWork.OperationRepository.GetOperations();
	//}

	// inny typ zwracanej wartości - tutaj StatusCode
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

	/// <summary>
	/// Get all operations.
	/// </summary>
	/// <returns></returns>
	// INFO - nazwa akcji nie ma znaczenia
	[HttpGet]
	public DataResponse<IEnumerable<OperationDto>> GetOperations()
	{
		DataResponse<IEnumerable<OperationDto>> response = new();

		try
		{
			response.Data = unitOfWork.OperationRepository.GetOperations().ToDtos();
		}
		catch (Exception ex)
		{
			// logowanie do pliku...
			response.Errors.Add(new Error(ex.Source, ex.Message));
		}

		return response;
	}

	/// <summary>
	/// Get paginated list of operations.
	/// </summary>
	/// <param name="recordCount"></param>
	/// <param name="page"></param>
	/// <returns></returns>
	/// <exception cref="ArgumentException"></exception>
	[HttpGet("{recordCount}/{page}")]
	public DataResponse<IEnumerable<OperationDto>> GetOperations(int recordCount, int page)
	{
		DataResponse<IEnumerable<OperationDto>> response = new();

		try
		{
			if (recordCount <= 0)
			{
				throw new ArgumentException("Wartość nie może być mniejsza lub równa 0!", nameof(recordCount));
			}
			if (page <= 0)
			{
				throw new ArgumentException("Wartość nie może być mniejsza lub równa 0!", nameof(page));
			}
			response.Data = unitOfWork.OperationRepository.GetOperations(recordCount, page).ToDtos();
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
	// adres/api/operation/id
	[HttpGet("{operationId}")]
	public DataResponse<OperationDto> GetOperation(int operationId)
	{
		DataResponse<OperationDto> response = new();

		try
		{
			response.Data = unitOfWork.OperationRepository.GetOperation(operationId)?.ToDto();
		}
		catch (Exception ex)
		{
			// logowanie do pliku...
			response.Errors.Add(new Error(ex.Source, ex.Message));
		}

		return response;
	}

	/// <summary>
	/// Add new operation to DB
	/// </summary>
	/// <param name="operationDto"></param>
	/// <returns></returns>
	[HttpPost]
	public DataResponse<int> AddOperation(OperationDto operationDto)
	{
		// wartością zwracaną jest int - jest to Id nowo dodanego rekordu do bazy danych
		DataResponse<int> response = new();

		try
		{
			var operation = operationDto.ToDao();
			unitOfWork.OperationRepository.AddOperation(operation);
			unitOfWork.Complete();
			response.Data = operation.OperationId;
		}
		catch (Exception ex)
		{
			// logowanie do pliku...
			response.Errors.Add(new Error(ex.Source, ex.Message));
		}

		return response;
	}

	/// <summary>
	/// Update operation
	/// </summary>
	/// <param name="operation"></param>
	/// <returns></returns>
	[HttpPut]
	public Response UpdateOperation(OperationDto operation)
	{
		Response response = new();

		try
		{
			unitOfWork.OperationRepository.UpdateOperation(operation.ToDao());
			unitOfWork.Complete();
		}
		catch (Exception ex)
		{
			// logowanie do pliku...
			response.Errors.Add(new Error(ex.Source, ex.Message));
		}

		return response;
	}

	/// <summary>
	/// Delete operation
	/// </summary>
	/// <param name="operationId"></param>
	/// <returns></returns>
	[HttpDelete("{operationId}")]
	public Response DeleteOperation(int operationId)
	{
		Response response = new();

		try
		{
			unitOfWork.OperationRepository.DeleteOperation(operationId);
			unitOfWork.Complete();
		}
		catch (Exception ex)
		{
			// logowanie do pliku...
			response.Errors.Add(new Error(ex.Source, ex.Message));
		}

		return response;
	}
}