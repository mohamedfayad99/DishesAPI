using DishesAPI.Endpoints_Handlers;

namespace DishesAPI.Extensions
{
    public static class EndpointExtensions
    {
        public static void RegisterDishesEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
        {

            var dishesgroup = endpointRouteBuilder.MapGroup("/dishes").RequireAuthorization("RequireAdminFromBelgan");
            var dishesgroupwithid = endpointRouteBuilder.MapGroup("dishes/{id:guid}");

            dishesgroup.MapGet("", DishesHandler.GetALlDishesAsync);
            dishesgroupwithid.MapGet("", DishesHandler.GetDishById);
            dishesgroup.MapGet("/{name}", DishesHandler.GetDishByName);
            dishesgroup.MapPost("", DishesHandler.CreateDishAsync);
            dishesgroupwithid.MapPut("", DishesHandler.UpdateDishAsync);
            dishesgroupwithid.MapDelete("", DishesHandler.DeleteDishAsync);
        }
        public static void RegisterIngredientsEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            var ingredientgroup = endpointRouteBuilder.MapGroup("dishes/{id:guid}/Ingredients");

            ingredientgroup.MapGet("", IngredientsHandler.GetALlIngredientsAsync);
            ingredientgroup.MapPost("", () =>
            {
                throw new NotImplementedException();
            });
        }

    }
}
