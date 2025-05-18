using BlockedCountriesApi.BackgroundTasks;
using BlockedCountriesApi.Middleware;
using BlockedCountriesApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register your custom services
builder.Services.AddSingleton<IBlockedCountryService, BlockedCountryService>();
builder.Services.AddSingleton<IBlockedAttemptsLogger, BlockedAttemptsLogger>();
builder.Services.AddHttpClient<IGeoLocationService, GeoLocationService>();

builder.Services.AddHostedService<TemporalBlockCleaner>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<IpBlockingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
