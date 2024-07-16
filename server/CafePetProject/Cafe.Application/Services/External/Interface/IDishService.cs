using Cafe.Application.DTO.DishDTOs.Request;
using Cafe.Application.DTOs.DishDTOs.Request;
using Cafe.Domain;

namespace Cafe.Application.Services.External.Interface;

public interface IDishService
{
    public Task<IEnumerable<Dish>> GetAllDish();
    public Task<Dish> GetDishById(int id);
    public Task AddNewDish(AddDishRequest dish);
    public Task ChangeDish(PutDishRequest dish);
    public Task DeleteById(int id);
}