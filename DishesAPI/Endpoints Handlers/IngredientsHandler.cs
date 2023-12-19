using AutoMapper;
using DishesAPI.DBContexts;
using DishesAPI.Entities;
using DishesAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace DishesAPI.Endpoints_Handlers
{
    public class IngredientsHandler
    {
        public static async Task<Ok<IEnumerable<IngredientDTO>>> GetALlIngredientsAsync(ApplicationDbContext context, IMapper _mapper,Guid id, ILogger<DishDTO> _logger)
        {
            _logger.LogInformation("Getting All Ingredients");
            return TypedResults.Ok(_mapper.Map<IEnumerable<IngredientDTO>>((await context.Dishes
                                   .Include(d => d.Ingredients)
                                   .FirstOrDefaultAsync(s => s.Id == id))?.Ingredients));
        }
    }
}
