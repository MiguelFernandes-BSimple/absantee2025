using Application.DTO;
using Application.Services;
using Domain.Factory;
using Domain.Factory.TrainingPeriodFactory;
using Domain.IRepository;
using Domain.Models;
using Infrastructure;
using Infrastructure.DataModel;
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
builder.Services.AddTransient<ProjectService>();

//Repositories
builder.Services.AddTransient<IUserRepository, UserRepositoryEF>();
builder.Services.AddTransient<ICollaboratorRepository, CollaboratorRepository>();
builder.Services.AddTransient<IAssociationProjectCollaboratorRepository, AssociationProjectCollaboratorRepositoryEF>();
builder.Services.AddTransient<ITrainingModuleRepository, TrainingModuleRepository>();
builder.Services.AddTransient<IProjectRepository, ProjectRepository>();
builder.Services.AddTransient<IHolidayPlanRepository, HolidayPlanRepositoryEF>();
builder.Services.AddTransient<ITrainingSubjectRepository, TrainingSubjectRepository>();
builder.Services.AddTransient<ITrainingModuleRepository, TrainingModuleRepository>();
builder.Services.AddTransient<ITrainingModuleCollaboratorsRepository, AssociationTrainingModuleCollaboratorRepository>();

//Factories
builder.Services.AddTransient<ICollaboratorFactory, CollaboratorFactory>();
builder.Services.AddTransient<ITrainingPeriodFactory, TrainingPeriodFactory>();
builder.Services.AddTransient<IAssociationProjectCollaboratorFactory, AssociationProjectCollaboratorFactory>();
builder.Services.AddTransient<ITrainingSubjectFactory, TrainingSubjectFactory>();
builder.Services.AddTransient<ITrainingModuleFactory, TrainingModuleFactory>();
builder.Services.AddTransient<IProjectFactory, ProjectFactory>();
builder.Services.AddTransient<IUserFactory, UserFactory>();
builder.Services.AddTransient<IHRManagerFactory, HRManagerFactory>();
builder.Services.AddTransient<IHolidayPeriodFactory, HolidayPeriodFactory>();
builder.Services.AddTransient<IHolidayPlanFactory, HolidayPlanFactory>();
builder.Services.AddTransient<IAssociationTrainingModuleCollaboratorFactory, AssociationTrainingModuleCollaboratorFactory>();

//Mappers
builder.Services.AddTransient<ProjectDataModelToProjectConverter>();
builder.Services.AddTransient<TrainingSubjectDataModelToTrainingSubjectConverter>();
builder.Services.AddTransient<CollaboratorDataModelToCollaboratorConverter>();
builder.Services.AddTransient<TrainingModuleDataModelToTrainingModuleConverter>();
builder.Services.AddTransient<HolidayPeriodDataModelToHolidayPeriodConverter>();
builder.Services.AddTransient<AssociationTrainingModuleCollaboratorDataModelConverter>();
builder.Services.AddTransient<UserDataModelToUserConverter>();
builder.Services.AddTransient<HRManagerDataModelToUserConverter>();
builder.Services.AddTransient<HolidayPlanDataModelToHolidayPlanConverter>();
builder.Services.AddTransient<HolidayPeriodDataModelToHolidayPeriodConverter>();
builder.Services.AddAutoMapper(cfg =>
{
    //DataModels
    cfg.AddProfile<DataModelMappingProfile>();

    //DTO
    cfg.CreateMap<ProjectDTO, Project>();
    cfg.CreateMap<Project, ProjectDTO>();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
