using Cafe.Application.DTOs.DishDTOs.Request;
using Cafe.Domain;
using Cafe.Domain.ResultModels;

namespace Cafe.Application.Services.Inteface;

public interface IDishService
{
    public Task<Result<IEnumerable<Dish>>> GetAllDish();
    public Task<Result<Dish>> GetDishById(int id);
    public Task<Result<string>> AddNewDish(AddDishRequest dish);
    public Task<Result<string>> ChangeDish(PutDishRequest dish);
    public Task<Result<string>> DeleteById(int id);
}