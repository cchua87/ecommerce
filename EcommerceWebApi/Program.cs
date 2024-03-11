using Ecommerce.Data;
using Ecommerce.Repositories;
using Ecommerce.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// add services to DI container
{
    var services = builder.Services;
    var env = builder.Environment;

    services.AddDbContext<DataContext>();
    services.AddCors();
    services.AddControllers().AddJsonOptions(x =>
    {
        // serialize enums as strings in api responses (e.g. Role)
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

        // ignore omitted parameters on models to enable optional params (e.g. User update)
        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    // configure DI for application services
    services.AddScoped<IProductService, ProductService>();
    services.AddScoped<ICartService, CartService>();
    services.AddScoped<IProductRepository, ProductRepository>();
    services.AddScoped<ICartRepository, CartRepository>();
    services.AddTransient<DbInitializer>();

    // Add session services
    services.AddDistributedMemoryCache(); // Use in-memory cache for session data
    services.AddSession(options =>
    {
        // Configure session options
        options.Cookie.Name = "CartId";
        options.Cookie.HttpOnly = false;
        options.Cookie.IsEssential = true; // Make the session cookie essential
        options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
    });

    // Add Logging
    services.AddLogging();
}

var app = builder.Build();
app.UseSession();

// configure HTTP request pipeline
{
    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    app.MapControllers();
}

// initialiser for dummy data
using var scope = app.Services.CreateScope();
var service = scope.ServiceProvider;
var initializer = service.GetRequiredService<DbInitializer>();

initializer.Run();

app.Run();
