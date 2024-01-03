﻿using Cafe.Domain;
using Cafe.Infrustructure.Context;
using Cafe.Infrustructure.Repositoriy.Interface;
using Microsoft.EntityFrameworkCore;

namespace Cafe.Infrustructure.Repositoriy;

public class DishRepository : IDishRepository
{
    private readonly CafeDbContext _dbContext;

    public DishRepository(CafeDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<Dish>> GetAllDish()
    {
        var result = await _dbContext.Dishes.ToListAsync();
        return result;
    }
    public async Task<Dish> GetDishById(int id)
    {
        var result = await _dbContext.Dishes.FirstOrDefaultAsync(p => p.DishId == id);
        return result;
    }
    public async Task AddNewDish(Dish dish)
    {
        await _dbContext.Dishes.AddAsync(dish);
        await _dbContext.SaveChangesAsync();
    }
    public async Task ChangeDish(Dish dish)
    {
        _dbContext.Dishes.Update(dish);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> DeleteDishById(int id)
    {
        var dish = await _dbContext.Dishes.FirstOrDefaultAsync(p => p.DishId == id);

        if (dish == null)
        {
            return true;
        }
        
        _dbContext.Dishes.Remove(dish);
        await _dbContext.SaveChangesAsync();
        return false;
    }
}