﻿using Cafe.Domain;

namespace Cafe.Infrastructure.Repository.Interface;

public interface IDishRepository
{
    public Task<IEnumerable<Dish>> GetAllDish();
    public Task<Dish> GetDishById(int id);
    public Task AddNewDish(Dish dish);
    public Task ChangeDish(Dish dish);
    public Task<bool> DeleteDishById(int id);

}