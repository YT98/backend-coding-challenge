using CitySearch.Utils;

var config = new ConfigurationBuilder().AddEnvironmentVariables().Build();
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IConfiguration>(config);
builder.Services.AddSingleton<IDataLoader, DataLoader>();
builder.Services.AddSingleton<ICityTrie, CityTrie>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

