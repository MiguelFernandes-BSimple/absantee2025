using Application.Services;
using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;
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

//Services
builder.Services.AddTransient<CollaboratorService>();
//Repositories
builder.Services.AddTransient<ICollaboratorRepository, CollaboratorRepository>();
builder.Services.AddTransient<IAssociationProjectCollaboratorRepository, AssociationProjectCollaboratorRepositoryEF>();

//Mappers
builder.Services.AddTransient<IMapper<TrainingModule, TrainingModuleDataModel>, TrainingModuleMapper>();
builder.Services.AddTransient<GenericRepository<TrainingModule, TrainingModuleDataModel>>();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.CreateMap<Collaborator, CollaboratorDataModel>();
    cfg.CreateMap<CollaboratorDataModel, Collaborator>();
    cfg.CreateMap<AssociationProjectCollaborator, AssociationProjectCollaboratorDataModel>();
    cfg.CreateMap<AssociationProjectCollaboratorDataModel, AssociationProjectCollaborator>();
});

//Factories
builder.Services.AddTransient<ICollaboratorFactory, CollaboratorFactory>();
builder.Services.AddTransient<IAssociationProjectCollaboratorFactory, AssociationProjectCollaboratorFactory>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
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
