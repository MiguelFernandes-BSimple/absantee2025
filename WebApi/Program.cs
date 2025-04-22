using Domain.Factory;
using Domain.Factory.TrainingPeriodFactory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Infrastructure;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AbsanteeContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    );

//Repositories
builder.Services.AddTransient<IAssociationProjectCollaboratorRepository, AssociationProjectCollaboratorRepositoryEF>();
builder.Services.AddTransient<IAssociationTrainingModuleCollaboratorRepository, AssociationTrainingModuleCollaboratorRepositoryEF>();
builder.Services.AddTransient<ICollaboratorRepository, CollaboratorRepository>();
builder.Services.AddTransient<IHolidayPlanRepository, HolidayPlanRepositoryEF>();
builder.Services.AddTransient<IProjectRepository, ProjectRepository>();
builder.Services.AddTransient<ITrainingModuleRepository, TrainingModuleRepositoryEF>();
builder.Services.AddTransient<ITrainingSubjectRepository, TrainingSubjectRepositoryEF>();
builder.Services.AddTransient<IUserRepository, UserRepositoryEF>();

//Mappers
builder.Services.AddTransient<IMapper<AssociationProjectCollaborator, AssociationProjectCollaboratorDataModel>, AssociationProjectCollaboratorMapper>();
builder.Services.AddTransient<IMapper<AssociationTrainingModuleCollaborator, AssociationTrainingModuleCollaboratorDataModel>, AssociationTrainingModuleCollaboratorMapper>();
builder.Services.AddTransient<IMapper<Collaborator, CollaboratorDataModel>, CollaboratorMapper>();
builder.Services.AddTransient<IMapper<IHolidayPeriod, HolidayPeriodDataModel>, HolidayPeriodMapper>();
builder.Services.AddTransient<IMapper<IHolidayPlan, HolidayPlanDataModel>, HolidayPlanMapper>();
builder.Services.AddTransient<IMapper<HRManager, HRManagerDataModel>, HRManagerMapper>();
builder.Services.AddTransient<IMapper<ProjectManager, ProjectManagerDataModel>, ProjectManagerMapper>();
builder.Services.AddTransient<IMapper<Project, ProjectDataModel>, ProjectMapper>();
builder.Services.AddTransient<IMapper<TrainingModule, TrainingModuleDataModel>, TrainingModuleMapper>();
builder.Services.AddTransient<IMapper<TrainingPeriod, TrainingPeriodDataModel>, TrainingPeriodMapper>();
builder.Services.AddTransient<IMapper<TrainingSubject, TrainingSubjectDataModel>, TrainingSubjectMapper>();
builder.Services.AddTransient<IMapper<User, UserDataModel>, UserMapper>();

//Factories
builder.Services.AddTransient<IAssociationProjectCollaboratorFactory, AssociationProjectCollaboratorFactory>();
builder.Services.AddTransient<IAssociationTrainingModuleCollaboratorFactory, AssociationTrainingModuleCollaboratorFactory>();
builder.Services.AddTransient<ICollaboratorFactory, CollaboratorFactory>();
builder.Services.AddTransient<ICollaboratorFactory, CollaboratorFactory>();
builder.Services.AddTransient<ITrainingPeriodFactory, TrainingPeriodFactory>();

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
