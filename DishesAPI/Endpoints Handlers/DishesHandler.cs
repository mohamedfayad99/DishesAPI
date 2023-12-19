using AutoMapper;
using DishesAPI.DBContexts;
using DishesAPI.Entities;
using DishesAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace DishesAPI.Endpoints_Handlers;

public static class DishesHandler
{

    public static async Task<Ok<IEnumerable<DishDTO>>> GetALlDishesAsync(ApplicationDbContext context, IMapper _mapper,ILogger<DishDTO> _logger)
    {
        _logger.LogInformation("Getting All Dishes");
        return TypedResults.Ok(_mapper.Map<IEnumerable<DishDTO>>(await context.Dishes.ToListAsync()));
    }
    public static async Task<Results<Ok<DishDTO>, NotFound>> GetDishById(ApplicationDbContext context, IMapper _mapper, Guid id, ILogger<DishDTO> _logger)
    {
        var dish = await context.Dishes.FirstOrDefaultAsync(d => d.Id == id);
        if (dish == null)
        {
            _logger.LogError("this dish not found");
            return TypedResults.NotFound();
        }
        _logger.LogInformation("Getting The Dish that have id : {id}", id);
        return TypedResults.Ok(_mapper.Map<DishDTO>(dish));
    }
    public static async Task<Results<Ok<DishDTO>, NotFound>> GetDishByName(ApplicationDbContext context, IMapper _mapper, string name, ILogger<DishDTO> _logger)
    {
        var dish = await context.Dishes.FirstOrDefaultAsync(d => d.Name == name);
        if (dish == null)
        {
            _logger.LogError("this dish not found");
            return TypedResults.NotFound();
        }
        _logger.LogInformation("Getting The Dish that have name : {name}", name);
        return TypedResults.Ok(_mapper.Map<DishDTO>(dish));

    }
    public static async Task<Results<BadRequest<string>, Created>> CreateDishAsync(ApplicationDbContext context, IMapper _mapper, DishCreateDto dishDTO, ILogger<DishDTO> _logger)
    {
        if (string.IsNullOrEmpty(dishDTO.Name))
        {
            _logger.LogWarning("please Enter Name");
            return TypedResults.BadRequest("please Enter Name");
        }

        if (await context.Dishes.FirstOrDefaultAsync(d => d.Name == dishDTO.Name) != null)
        {
            _logger.LogWarning("this Name { name} is already exist", dishDTO.Name);
            return TypedResults.BadRequest($"this Name {dishDTO.Name} is already exist ");
        }
        var dish = _mapper.Map<Dish>(dishDTO);
        context.Dishes.Add(dish);
        await context.SaveChangesAsync();
        _logger.LogInformation("Creating Dish with id {id}", dish.Id);
        return TypedResults.Created($"https://localhost:7069/" + "Dishes/" + dish.Id);
    }
    public static async Task<Results<BadRequest<string>, NoContent, NotFound>> UpdateDishAsync(ApplicationDbContext context, IMapper _mapper, DishUpdateDto dishDTO, Guid id, ILogger<DishDTO> _logger)
    {
        var updateddish = context.Dishes.Find(id);
        if (updateddish == null)
        {
            _logger.LogError("this dish not found");
            return TypedResults.NotFound();
        }
        if (string.IsNullOrEmpty(dishDTO.Name))
        {
            _logger.LogWarning("this dish not found");
            return TypedResults.BadRequest("please Enter Name");
        }

        if (await context.Dishes.FirstOrDefaultAsync(d => d.Name == dishDTO.Name) != null)
        {
            _logger.LogWarning("this Name { name} is already exist", dishDTO.Name);
            return TypedResults.BadRequest($"this Name {dishDTO.Name} is already exist ");
        }

        _mapper.Map(dishDTO, updateddish);
        await context.SaveChangesAsync();
        _logger.LogInformation("update the dish that have id {id}", id);
        return TypedResults.NoContent();
    }
    public static async Task<Results<NoContent, NotFound>> DeleteDishAsync(ApplicationDbContext context, IMapper _mapper, Guid id, ILogger<DishDTO> _logger)
    {
        var dish = await context.Dishes.FindAsync(id);
        if (dish == null)
        {
            _logger.LogError("this dish not found");
            return TypedResults.NotFound();
        }
        context.Dishes.Remove(dish);
        await context.SaveChangesAsync();
        _logger.LogInformation("Delete dish that have id { id}", id);
        return TypedResults.NoContent();
    }
}
