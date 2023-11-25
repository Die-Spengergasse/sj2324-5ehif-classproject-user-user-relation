using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using sj2324_5ehif_cooking_user.Application.Infrastructure;

namespace sj2324_5ehif_cooking_user.Webapi;

public class Startup
{
    private IConfigurationRoot Configuration { get; }
    public Startup(IConfigurationRoot configuration)
    {
        Configuration = configuration;
    }
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<UserContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("PostgresConnection")));
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cooking User", Version = "v1" });
        });
    }
    public async Task Configure(WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            await services.GetRequiredService<UserContext>().Database.EnsureCreatedAsync();
        }
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cooking User V1");
            });
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
    }
}