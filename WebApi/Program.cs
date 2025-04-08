using Infrastructure;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AbsanteeContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    );

builder.Services.AddTransient<ProjectMapper>();
builder.Services.AddTransient<TrainingPeriodMapper>();
builder.Services.AddTransient<UserMapper>();
builder.Services.AddTransient<PeriodDateMapper>();
builder.Services.AddTransient<PeriodDateTimeMapper>();
builder.Services.AddTransient<ProjectManagerMapper>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
