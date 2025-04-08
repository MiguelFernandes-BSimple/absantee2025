using Domain.IRepository;
using Domain.Interfaces;
using Domain.Models;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class CollaboratorRepository : ICollaboratorRepository, IGenericRepository<Collaborator>
{
    private List<ICollaborator> _collaborators;

    public CollaboratorRepository()
    {
        _collaborators = new List<ICollaborator>();
    }

    public CollaboratorRepository(List<ICollaborator> collaborators)
    {
        bool isAdded = Add(collaborators);
    
        if (!isAdded)
            throw new ArgumentException("Arguments are not valid!");
    }

    public IEnumerable<ICollaborator> FindAllCollaborators()
    {
        // Create a new list - to not share the pointer
        return [.. _collaborators];
    }

    public IEnumerable<ICollaborator> FindAllCollaboratorsWithName(string names)
    {
        return _collaborators.Where(c => c.HasNames(names));
    }

    public IEnumerable<ICollaborator> FindAllCollaboratorsWithSurname(string surnames)
    {
        return _collaborators.Where(c => c.HasSurnames(surnames));
    }

    public IEnumerable<ICollaborator> FindAllCollaboratorsWithNameAndSurname(string names, string surnames)
    {
        return _collaborators.Where(c => c.HasNames(names) && c.HasSurnames(surnames));
    }

    /**
    * Method to validate whether a collaborator can be insert in a given list or not
    */
    private bool IsRepeated(ICollaborator collaborator, List<ICollaborator> collabList)
    {
        bool isRepeated =
            collabList.Any(c => c.Equals(collaborator));

        return isRepeated;
    }

    /**
    * Method to add a single collaborator to the repository
    * Not in use right now!
    */
    private bool Add(ICollaborator collaborator)
    {
        bool isRepeated = IsRepeated(collaborator, _collaborators);
        if (!isRepeated)
            _collaborators.Add(collaborator);

        return !isRepeated;
    }

    private bool Add(List<ICollaborator> collaborators)
    {
        // Validate if all collaborators are valid
        for (int collabIndex1 = 0; collabIndex1 < collaborators.Count; collabIndex1++)
        {
            ICollaborator currCollaborator = collaborators[collabIndex1];
            var isRepeated = IsRepeated(currCollaborator, collaborators.Skip(collabIndex1 + 1).ToList());

            if (isRepeated)
                return false;
        }

        // If the list is valid -> insert Collborators in repo
        foreach (ICollaborator collabIndex2 in collaborators)
        {
            _collaborators.Add(collabIndex2);
        }

        return true;
    }

    public async Task<IEnumerable<ICollaborator>> FindAllCollaboratorsAsync()
    {
        var result = FindAllCollaborators();
        return await Task.FromResult(result);
    }

    public async Task<IEnumerable<ICollaborator>> FindAllCollaboratorsWithNameAsync(string name)
    {
        var result = FindAllCollaboratorsWithName(name);
        return await Task.FromResult(result);
    }

    public async Task<IEnumerable<ICollaborator>> FindAllCollaboratorsWithSurnameAsync(string surname)
    {
        var result = FindAllCollaboratorsWithSurname(surname);
        return await Task.FromResult(result);
    }

    public async Task<IEnumerable<ICollaborator>> FindAllCollaboratorsWithNameAndSurnameAsync(string name, string surname)
    {
        var result = FindAllCollaboratorsWithNameAndSurname(name, surname);
        return await Task.FromResult(result);
    }

    public Collaborator? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Collaborator? GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Collaborator> GetAll()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Collaborator> Find(Expression<Func<Collaborator, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public void Add(Collaborator entity)
    {
        throw new NotImplementedException();
    }

    public void AddRange(IEnumerable<Collaborator> entities)
    {
        throw new NotImplementedException();
    }

    public void Remove(Collaborator entity)
    {
        throw new NotImplementedException();
    }

    public void RemoveRange(IEnumerable<Collaborator> entities)
    {
        throw new NotImplementedException();
    }
}