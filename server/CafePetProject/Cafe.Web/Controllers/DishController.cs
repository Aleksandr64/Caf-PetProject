using Cafe.Application.DTO.DishDTOs.Request;
using Cafe.Application.DTOs.DishDTOs.Request;
using Cafe.Application.Services.External.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Cafe.Web.Controllers;

public class DishController : BaseApiController
{
    private readonly IDishService _dishService;
    
    public DishController(IDishService dishService)
    {
        _dishService = dishService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllDish()
    {
        var result = await _dishService.GetAllDish();
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetDishById(int id)
    {
        var result = await _dishService.GetDishById(id);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddNewDish([FromBody] AddDishRequest dish)
    {
        await _dishService.AddNewDish(dish);
        return NoContent();    
    }

    [HttpPut]
    public async Task<IActionResult> ChangeDish([FromBody] PutDishRequest dish)
    {
        await _dishService.ChangeDish(dish);
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteById(int id)
    {
        await _dishService.DeleteById(id);
        return NoContent();
    }
}