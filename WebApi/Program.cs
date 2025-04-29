using Application.DTO;
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
builder.Services.AddTransient<ProjectService>();
builder.Services.AddTransient<CollaboratorService>();
builder.Services.AddTransient<TrainingPeriodService>();
builder.Services.AddTransient<CollaboratorService>();
builder.Services.AddTransient<HolidayPlanService>();
builder.Services.AddTransient<AssociationProjectCollaboratorService>();

//Repositories
builder.Services.AddTransient<IUserRepository, UserRepositoryEF>();
builder.Services.AddTransient<ICollaboratorRepository, CollaboratorRepositoryEF>();
builder.Services.AddTransient<IAssociationProjectCollaboratorRepository, AssociationProjectCollaboratorRepositoryEF>();
builder.Services.AddTransient<IProjectRepository, ProjectRepositoryEF>();
builder.Services.AddTransient<IAssociationTrainingModuleCollaboratorsRepository, AssociationTrainingModuleCollaboratorRepositoryEF>();
builder.Services.AddTransient<ITrainingPeriodRepository, TrainingPeriodRepositoryEF>();
builder.Services.AddTransient<ITrainingSubjectRepository, TrainingSubjectRepositoryEF>();
builder.Services.AddTransient<ITrainingModuleRepository, TrainingModuleRepositoryEF>();
builder.Services.AddTransient<IHolidayPlanRepository, HolidayPlanRepositoryEF>();

//Factories
builder.Services.AddTransient<IUserFactory, UserFactory>();
builder.Services.AddTransient<IHRManagerFactory, HRManagerFactory>();
builder.Services.AddTransient<IProjectManagerFactory, ProjectManagerFactory>();
builder.Services.AddTransient<ICollaboratorFactory, CollaboratorFactory>();
builder.Services.AddTransient<IAssociationProjectCollaboratorFactory, AssociationProjectCollaboratorFactory>();
builder.Services.AddTransient<IProjectFactory, ProjectFactory>();
builder.Services.AddTransient<IAssociationTrainingModuleCollaboratorFactory, AssociationTrainingModuleCollaboratorFactory>();
builder.Services.AddTransient<ITrainingPeriodFactory, TrainingPeriodFactory>();
builder.Services.AddTransient<ITrainingSubjectFactory, TrainingSubjectFactory>();
builder.Services.AddTransient<ITrainingModuleFactory, TrainingModuleFactory>();
builder.Services.AddTransient<IHolidayPlanFactory, HolidayPlanFactory>();
builder.Services.AddTransient<IHolidayPeriodFactory, HolidayPeriodFactory>();

//Mappers
builder.Services.AddTransient<UserDataModelConverter>();
builder.Services.AddTransient<HRManagerDataModelConverter>();
builder.Services.AddTransient<ProjectManagerDataModelConverter>();
builder.Services.AddTransient<CollaboratorDataModelConverter>();
builder.Services.AddTransient<AssociationProjectCollaboratorDataModelConverter>();
builder.Services.AddTransient<ProjectDataModelConverter>();
builder.Services.AddTransient<AssociationTrainingModuleCollaboratorDataModelConverter>();
builder.Services.AddTransient<TrainingPeriodDataModelConverter>();
builder.Services.AddTransient<TrainingSubjectDataModelConverter>();
builder.Services.AddTransient<TrainingModuleDataModelConverter>();
builder.Services.AddTransient<HolidayPlanDataModelConverter>();
builder.Services.AddTransient<HolidayPeriodDataModelConverter>();
builder.Services.AddAutoMapper(cfg =>
{
    //DataModels
    cfg.AddProfile<DataModelMappingProfile>();

    //DTO
    cfg.CreateMap<Collaborator, CollaboratorDTO>();
    cfg.CreateMap<AssociationProjectCollaborator, AssociationProjectCollaboratorDTO>();
    cfg.CreateMap<AssociationProjectCollaboratorDTO, AssociationProjectCollaborator>();
    cfg.CreateMap<Project, ProjectDTO>();
    cfg.CreateMap<ProjectDTO, Project>();
    cfg.CreateMap<TrainingPeriod, TrainingPeriodDTO>();
    cfg.CreateMap<TrainingPeriodDTO, TrainingPeriod>();
    cfg.CreateMap<TrainingPeriod, CreateTrainingPeriodDTO>()
            .ForMember(dest => dest.InitDate, opt => opt.MapFrom(src => src.PeriodDate.InitDate))
            .ForMember(dest => dest.FinalDate, opt => opt.MapFrom(src => src.PeriodDate.FinalDate));
    cfg.CreateMap<HolidayPeriod, HolidayPeriodDTO>();
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
