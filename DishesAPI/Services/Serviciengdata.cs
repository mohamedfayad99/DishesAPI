using DishesAPI.DBContexts;
using Microsoft.EntityFrameworkCore;

namespace DishesAPI.Services;

public static class Serviciengdata
{
    public static IServiceCollection AddExtensionsMethod(this IServiceCollection services,
            IConfiguration confiq)
    {
        services.AddDbContext<ApplicationDbContext>(_ => _.UseSqlite(
            confiq.GetConnectionString("DefaultConnetion")));
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddAuthentication().AddJwtBearer();
        services.AddAuthorization();
        services.AddAuthorizationBuilder()
            .AddPolicy("RequireAdminFromBelgan", policy =>
            policy.RequireRole("admin")
            .RequireClaim("country","Belguim"));
        
        return services;
    }
}
