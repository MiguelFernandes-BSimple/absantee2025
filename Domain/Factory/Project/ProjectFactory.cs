using Domain.Models;
using Domain.Visitor;
using Domain.IRepository;
using System.Text.RegularExpressions;

namespace Domain.Factory;
public class ProjectFactory : IProjectFactory
{
    private readonly IProjectRepository _projectRepository;
    public ProjectFactory(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }
    public Project Create(long id, string title, string acronym, PeriodDate periodDate)
    {
        Regex tituloRegex = new Regex(@"^.{1,50}$");
        Regex siglaRegex = new Regex(@"^[A-Z0-9]{1,10}$");
        if (tituloRegex.IsMatch(title) && siglaRegex.IsMatch(acronym))
        {
            throw new ArgumentException("Invalid Arguments");
        }
        Project project = new Project(title, acronym, periodDate);
        return project;
    }

    public Project Create(IProjectVisitor visitor)
    {
        return new Project(visitor.Title, visitor.Acronym, visitor.PeriodDate);
    }

}