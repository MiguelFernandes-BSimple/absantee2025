using Domain.Models;
using Domain.Visitor;
using Domain.IRepository;
using System.Text.RegularExpressions;
using Domain.Interfaces;
using System.Threading.Tasks;

namespace Domain.Factory;
public class ProjectFactory : IProjectFactory
{
    private readonly IProjectRepository _projectRepository;
    public ProjectFactory(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }
    public async Task<Project> Create(string title, string acronym, PeriodDate periodDate)
    {
        if (!await _projectRepository.CheckIfAcronymIsUnique(acronym))
            throw new ArgumentException("Invalid Arguments");

        Project project = new Project(title, acronym, periodDate);
        return project;
    }

    public Project Create(IProjectVisitor visitor)
    {
        return new Project(visitor.Id, visitor.Title, visitor.Acronym, visitor.PeriodDate);
    }
}