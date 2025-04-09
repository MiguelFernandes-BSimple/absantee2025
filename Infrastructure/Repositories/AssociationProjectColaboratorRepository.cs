using Domain.IRepository;
using Domain.Interfaces;
using Domain.Models;
using System.Linq.Expressions;


namespace Infrastructure.Repositories;

public class AssociationProjectCollaboratorRepository : IAssociationProjectCollaboratorRepository, IGenericRepository<IAssociationProjectCollaborator>
{
    private List<IAssociationProjectCollaborator> _associationsProjectCollaborator;

    public AssociationProjectCollaboratorRepository(List<IAssociationProjectCollaborator> associationsProjectCollaborator)
    {
        if (CheckInputValue(associationsProjectCollaborator))
            _associationsProjectCollaborator = associationsProjectCollaborator;
        else
            throw new ArgumentException("Invalid Arguments");
    }

    public IEnumerable<IAssociationProjectCollaborator> FindAllByProject(IProject project)
    {
        return _associationsProjectCollaborator.Where(a => a.HasProject(project));
    }
    public IAssociationProjectCollaborator? FindByProjectAndCollaborator(IProject project, ICollaborator collaborator)
    {
        return _associationsProjectCollaborator.Where(a => a.HasProject(project) && a.HasCollaborator(collaborator)).FirstOrDefault();
    }

    public IAssociationProjectCollaborator? FindByProjectAndCollaborator(long projectId, long collaboratorId)
    {
        return _associationsProjectCollaborator.Where(a => a.GetProjectId() == projectId && a.GetCollaboratorId() == collaboratorId).FirstOrDefault();
    }

    public IEnumerable<IAssociationProjectCollaborator> FindAllByProjectAndBetweenPeriod(IProject project, IPeriodDate periodDate)
    {
        return _associationsProjectCollaborator
                .Where(a => a.HasProject(project)
                    && a.AssociationIntersectPeriod(periodDate));
    }

    public bool Add(IAssociationProjectCollaborator newAssociation)
    {
        if (IsRepeated(newAssociation))
        {
            return false;
        }
        else
        {
            _associationsProjectCollaborator.Add(newAssociation);
            return true;
        }
    }

    public async Task<bool> AddAsync(IAssociationProjectCollaborator newAssociation)
    {
        var result = Add(newAssociation);
        return await Task.FromResult(result);
    }

    private bool CheckInputValue(List<IAssociationProjectCollaborator> associationsProjectCollaborator)
    {
        for (int i = 0; i < associationsProjectCollaborator.Count - 1; i++)
        {
            if (IsRepeated(
                    associationsProjectCollaborator[i],
                    associationsProjectCollaborator.Skip(i + 1).ToList()))
            {
                return false;
            }
        }
        return true;
    }

    private bool IsRepeated(IAssociationProjectCollaborator newAssociation)
    {
        return IsRepeated(newAssociation, _associationsProjectCollaborator);
    }

    private bool IsRepeated(IAssociationProjectCollaborator newAssociation, List<IAssociationProjectCollaborator> associationsProjectCollaborator)
    {
        return associationsProjectCollaborator.Any(a => a.Equals(newAssociation));
    }

    public async Task<IEnumerable<IAssociationProjectCollaborator>> FindAllByProjectAsync(IProject project)
    {
        var result = FindAllByProject(project);
        return await Task.FromResult(result);
    }

    public async Task<IAssociationProjectCollaborator?> FindByProjectAndCollaboratorAsync(IProject project, ICollaborator collaborator)
    {
        var result = FindByProjectAndCollaborator(project, collaborator);
        return await Task.FromResult(result);
    }

    public async Task<IEnumerable<IAssociationProjectCollaborator>> FindAllByProjectAndBetweenPeriodAsync(IProject project, IPeriodDate periodDate)
    {
        var result = FindAllByProjectAndBetweenPeriod(project, periodDate);
        return await Task.FromResult(result);
    }

    public IAssociationProjectCollaborator? GetById(int id)
    {
        return _associationsProjectCollaborator.FirstOrDefault(a => a.GetId() == id);
    }

    public IAssociationProjectCollaborator? GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<IAssociationProjectCollaborator> GetAll()
    {
        return new List<IAssociationProjectCollaborator>(_associationsProjectCollaborator);
    }

    public IEnumerable<IAssociationProjectCollaborator> Find(Expression<Func<IAssociationProjectCollaborator, bool>> expression)
    {
        return _associationsProjectCollaborator.Where(expression.Compile());
    }

    void IGenericRepository<IAssociationProjectCollaborator>.Add(IAssociationProjectCollaborator entity)
    {
        throw new NotImplementedException();
    }

    public void AddRange(IEnumerable<IAssociationProjectCollaborator> entities)
    {
        throw new NotImplementedException();
    }

    public void Remove(IAssociationProjectCollaborator entity)
    {
        _associationsProjectCollaborator.Remove(entity);
    }

    public void RemoveRange(IEnumerable<IAssociationProjectCollaborator> entities)
    {
        foreach (var entity in entities)
        {
            _associationsProjectCollaborator.Remove(entity);
        }
    }
}
