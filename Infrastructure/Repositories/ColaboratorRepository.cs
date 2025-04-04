using Infrastructure.Interfaces;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class CollaboratorRepository : ICollaboratorRepository
{
    private List<ICollaborator> _collaborators;

    public CollaboratorRepository()
    {
        _collaborators = new List<ICollaborator>();
    }

    public CollaboratorRepository(List<ICollaborator> collaborators) : this()
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
            ICollaborator currColaborator = collaborators[collabIndex1];
            var isRepeated = IsRepeated(currColaborator, collaborators.Skip(collabIndex1 + 1).ToList());

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
        return await Task.FromResult(_collaborators);
    }

    public async Task<IEnumerable<ICollaborator>> FindAllCollaboratorsWithNameAsync(string name)
    {
        var result = _collaborators.Where(c => c.HasNames(name));
        return await Task.FromResult(result);
    }

    public async Task<IEnumerable<ICollaborator>> FindAllCollaboratorsWithSurnameAsync(string surname)
    {
        var result = _collaborators.Where(c => c.HasSurnames(surname));
        return await Task.FromResult(result);
    }

    public async Task<IEnumerable<ICollaborator>> FindAllCollaboratorsWithNameAndSurnameAsync(string name, string surname)
    {
        var result = _collaborators.Where(c => c.HasNames(name) && c.HasSurnames(surname));
        return await Task.FromResult(result);
    }

}