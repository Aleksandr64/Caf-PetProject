using Cafe.Application.DTO.DishDTOs.Request;
using Cafe.Application.DTOs.DishDTOs.Request;
using Cafe.Domain;

namespace Cafe.Application.Mappers;

public static class DishMapper
{
    public static Dish MapDishAddRequest(this AddDishRequest addDish)
    {
        return new Dish
        {
            Title = addDish.Title,
            Description = addDish.Description,
            Price = addDish.Price,
            ImageUrl = addDish.ImageUrl,
            DateCreate = DateTime.UtcNow,
            DateUpdate = DateTime.UtcNow
        };
    }

    public static Dish MapDishPutRequest(this PutDishRequest putDish, Dish dish)
    {
        dish.Title = putDish.Title;
        dish.Description = putDish.Description;
        dish.Price = putDish.Price;
        dish.ImageUrl = putDish.ImageUrl;
        dish.DateUpdate = DateTime.UtcNow;
        return dish;
    }
}