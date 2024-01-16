using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using sj2324_5ehif_cooking_user_relations.Application.Infrastructure;
using sj2324_5ehif_cooking_user_relations.Application.Model;
using sj2324_5ehif_cooking_user_relations.Application.Repository;

namespace sj2324_5ehif_cooking_user_relations.Webapi;

public class Startup
{
    private IConfigurationRoot Configuration { get; }

    public Startup(IConfigurationRoot configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IRepository<Feedback>, Repository<Feedback>>();
        services.AddScoped<IRepository<Follow>, Repository<Follow>>();
        services.AddScoped<IRepository<RecipeShare>, Repository<RecipeShare>>();
        services.AddScoped<IRepository<Recipe>, Repository<Recipe>>();
        services.AddScoped<IRepository<User>, Repository<User>>();

        services.AddDbContext<UserRelationsContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("PostgresConnection")));
        
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cooking User-Relations", Version = "v1" });
        });

        services.AddControllers();
    }

    public async Task Configure(WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            if (app.Environment.IsDevelopment())
            {
                services.GetRequiredService<UserRelationsContext>().Database.EnsureDeleted();
            }
            services.GetRequiredService<UserRelationsContext>().Database.EnsureCreated();
        }

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cooking User-Relations V1"); });
        }

        app.UseAuthentication();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
    }
}