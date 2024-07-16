using System.Net;
using Cafe.Application.DTO.DishDTOs.Request;
using Cafe.Application.DTOs.DishDTOs.Request;
using Cafe.Application.Mappers;
using Cafe.Application.Services.External.Interface;
using Cafe.Application.Validations;
using Cafe.Application.Validations.Dish;
using Cafe.Domain;
using Cafe.Domain.Exceptions;
using Cafe.Infrastructure.Repository.Interface;
using Cafe.Infrustructure.Repositoriy.Interface;

namespace Cafe.Application.Services.External;

public class DishService : IDishService
{
    private readonly IDishRepository _dishRepository;

    public DishService(IDishRepository dishRepository)
    {
        _dishRepository = dishRepository;
    }

    public async Task<IEnumerable<Dish>> GetAllDish()
    {
        var result = await _dishRepository.GetAllDish();

        if (result == null)
        {
            throw new ApiException(HttpStatusCode.NotFound,"Error or no dishes in the database");
        }

        return result;
    }
    public async Task<Dish> GetDishById(int  id)
    {
        var result = await _dishRepository.GetDishById(id);

        if (result == null)
        {
            throw new ApiException(HttpStatusCode.NotFound,"Failed get Dish by Id");
        }
        return result;
    }

    public async Task AddNewDish(AddDishRequest dish)
    {
        var validationResult = await new AddDishValidator().ValidateAsync(dish);
        if (!validationResult.IsValid)
        {
            throw new ApiException(HttpStatusCode.BadRequest, "Validation Errors", validationResult.Errors);
        }
        await _dishRepository.AddNewDish(dish.MapDishAddRequest());
    }

    public async Task ChangeDish(PutDishRequest dishChange)
    {
        var validationResult = await new PutDishValidator().ValidateAsync(dishChange);
        if (!validationResult.IsValid)
        {
            throw new ApiException(HttpStatusCode.BadRequest, "Validation Errors", validationResult.Errors);
        }

        var dish = await _dishRepository.GetDishById(dishChange.Id);
        await _dishRepository.ChangeDish(dishChange.MapDishPutRequest(dish));
    }

    public async Task DeleteById(int id)
    {
        var result = await _dishRepository.DeleteDishById(id);

        if (result)
        {
            throw new ApiException(HttpStatusCode.BadRequest, "Error in delete dish.");
        }
    }
}