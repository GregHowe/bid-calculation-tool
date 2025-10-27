using BidCalculator.API.Services;
using BidCalculator.API.Interfaces;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddOpenApi();
builder.Services.AddScoped<IFeeCalculator, FeeCalculatorService>();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});    
builder.Services.AddScoped<IBasicFeeCalculator, BasicFeeCalculator>();
builder.Services.AddScoped<ISpecialFeeCalculator, SpecialFeeCalculator>();
builder.Services.AddScoped<IAssociationFeeCalculator, AssociationFeeCalculator>();
builder.Services.AddScoped<IStorageFeeCalculator, StorageFeeCalculator>();
builder.Services.AddScoped<IFeeCalculator, FeeCalculatorService>();

var app = builder.Build();

app.UseCors("AllowFrontend");

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
