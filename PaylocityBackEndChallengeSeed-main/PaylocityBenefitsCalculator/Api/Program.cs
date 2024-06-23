using System.Reflection;
using Api.CommandQueryImp;
using Api.DataContext;
//using Api.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(opt => {
        opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    }); ;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Employee Benefit Cost Calculation Api",
        Description = "Api to support employee benefit cost calculations"
    });
});

builder.Services.AddDbContext<ApplicationContext>(opt =>
{
    opt.UseInMemoryDatabase("TestDb");
});


// Mediatr
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
RegisterHandlers(builder.Services);
// Automapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var allowLocalhost = "allow localhost";
builder.Services.AddCors(options =>
{
    options.AddPolicy(allowLocalhost,
        policy => { policy.WithOrigins("http://localhost:3000", "http://localhost"); });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    dbContext.Seed();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(allowLocalhost);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// Query / Command Register
void RegisterHandlers(IServiceCollection services)
{
    // Register all IQueryHandler<> implementations
    var assembly = Assembly.GetExecutingAssembly();
    var queryHandlerTypes = assembly.GetTypes()
        .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>)))
        .ToList();

    foreach (var handlerType in queryHandlerTypes)
    {
        var interfaceTypes = handlerType.GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>)).ToList();
        foreach (var interfaceType in interfaceTypes)
        {
            services.AddTransient(interfaceType, handlerType);
        }
    }

    // Register all ICommandHandler<> implementations
    var commandHandlerTypes = assembly.GetTypes()
        .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<>)))
        .ToList();

    foreach (var handlerType in commandHandlerTypes)
    {
        var interfaceTypes = handlerType.GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<>)).ToList();
        foreach (var interfaceType in interfaceTypes)
        {
            services.AddTransient(interfaceType, handlerType);
        }
    }
}

