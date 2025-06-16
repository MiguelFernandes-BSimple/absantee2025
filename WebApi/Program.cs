using Application.DTO;
using Application.DTO.TrainingModule;
using Application.DTO.TrainingSubject;
using Application.Services;
using Domain.Factory;
using Domain.Factory.TrainingPeriodFactory;
using Domain.IRepository;
using Domain.Models;
using Infrastructure;
using Infrastructure.Repositories;
using Infrastructure.Resolvers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AbsanteeContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    );

//Services
builder.Services.AddTransient<TrainingPeriodService>();
builder.Services.AddTransient<TrainingSubjectService>();
builder.Services.AddTransient<TrainingModuleService>();

//Repositories
builder.Services.AddTransient<ITrainingPeriodRepository, TrainingPeriodRepositoryEF>();
builder.Services.AddTransient<ITrainingSubjectRepository, TrainingSubjectRepositoryEF>();
builder.Services.AddTransient<ITrainingModuleRepository, TrainingModuleRepositoryEF>();
//Factories
builder.Services.AddTransient<ITrainingPeriodFactory, TrainingPeriodFactory>();
builder.Services.AddTransient<ITrainingSubjectFactory, TrainingSubjectFactory>();
builder.Services.AddTransient<ITrainingModuleFactory, TrainingModuleFactory>();

//Mappers
builder.Services.AddTransient<TrainingPeriodDataModelConverter>();
builder.Services.AddTransient<TrainingSubjectDataModelConverter>();
builder.Services.AddTransient<TrainingModuleDataModelConverter>();
builder.Services.AddAutoMapper(cfg =>
{
    //DataModels
    cfg.AddProfile<DataModelMappingProfile>();

    //DTO
    cfg.CreateMap<TrainingPeriod, TrainingPeriodDTO>();
    cfg.CreateMap<TrainingPeriodDTO, TrainingPeriod>();
    cfg.CreateMap<TrainingSubject, TrainingSubjectDTO>();
    cfg.CreateMap<TrainingModule, TrainingModuleDTO>();
    cfg.CreateMap<TrainingPeriod, CreateTrainingPeriodDTO>()
            .ForMember(dest => dest.InitDate, opt => opt.MapFrom(src => src.PeriodDate.InitDate))
            .ForMember(dest => dest.FinalDate, opt => opt.MapFrom(src => src.PeriodDate.FinalDate));
});


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors(builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed((host) => true)
                .AllowCredentials());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
