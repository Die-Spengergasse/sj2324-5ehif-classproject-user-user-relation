using sj2324_5ehif_cooking_user_relations.Application.Services;
using sj2324_5ehif_cooking_user_relations.Webapi;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IFollowService, FollowService>();

var app = builder.Build();
await startup.Configure(app);

app.Run();