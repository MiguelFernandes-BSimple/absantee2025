using Domain.Factory;
using Domain.Factory.TrainingPeriodFactory;
using Domain.IRepository;
using Domain.Models;
using Infrastructure;
using Infrastructure.DataModel;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AbsanteeContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    );

builder.Services.AddTransient<UserService>();


//Repositories
builder.Services.AddTransient<IUserRepository, UserRepositoryEF>();
builder.Services.AddTransient<ICollaboratorRepository, CollaboratorRepository>();
builder.Services.AddTransient<IAssociationProjectCollaboratorRepository, AssociationProjectCollaboratorRepositoryEF>();
builder.Services.AddTransient<ITrainingModuleRepository, TrainingModuleRepository>();
builder.Services.AddTransient<IProjectRepository, ProjectRepository>();
builder.Services.AddTransient<IHolidayPlanRepository, HolidayPlanRepositoryEF>();
builder.Services.AddTransient<ITrainingSubjectRepository, TrainingSubjectRepository>();
builder.Services.AddTransient<ITrainingModuleRepository, TrainingModuleRepository>();
builder.Services.AddTransient<ITrainingModuleCollaboratorsRepository, TrainingModuleCollaboratorsRepository>();

//Mappers
builder.Services.AddAutoMapper(cfg =>
{
    cfg.CreateMap<Collaborator, CollaboratorDataModel>();
    cfg.CreateMap<CollaboratorDataModel, Collaborator>();
    cfg.CreateMap<AssociationProjectCollaborator, AssociationProjectCollaboratorDataModel>();
    cfg.CreateMap<AssociationProjectCollaboratorDataModel, AssociationProjectCollaborator>();
    cfg.CreateMap<TrainingModule, TrainingModuleDataModel>();
    cfg.CreateMap<TrainingModuleDataModel, TrainingModule>();
    cfg.CreateMap<TrainingSubjectDataModel, TrainingSubject>();
    cfg.CreateMap<TrainingSubject, TrainingSubjectDataModel>();
    cfg.CreateMap<User, UserDataModel>();
    cfg.CreateMap<UserDataModel, User>();
});

//Factories
builder.Services.AddTransient<ICollaboratorFactory, CollaboratorFactory>();
builder.Services.AddTransient<IUserFactory, UserFactory>();
builder.Services.AddTransient<ITrainingPeriodFactory, TrainingPeriodFactory>();
builder.Services.AddTransient<IAssociationProjectCollaboratorFactory, AssociationProjectCollaboratorFactory>();
builder.Services.AddTransient<ITrainingModuleFactory, TrainingModuleFactory>();

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
