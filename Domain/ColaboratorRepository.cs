
namespace Domain;

public class CollaboratorRepository : ICollaboratorRepository
{
    private List<ICollaborator> _collaborators;

    public CollaboratorRepository(List<ICollaborator> collaborators)
    {
        _collaborators = new List<ICollaborator>(collaborators);
    }

    public IEnumerable<ICollaborator> FindAllCollaborators()
    {
        // Create a new list - to not share the pointer
        return new List<ICollaborator>(_collaborators);
    }

    public IEnumerable<ICollaborator> FindAllCollaboratorsWithName(string names)
    {
        // Where returns an enumerable - whereas FindAll returns a list
        return _collaborators.Where(c => c.HasNames(names));
    }

    public IEnumerable<ICollaborator> FindAllCollaboratorsWithSurname(string surnames)
    {
        // Where returns an enumerable - whereas FindAll returns a list
        return _collaborators.Where(c => c.HasSurnames(surnames));
    }

    public IEnumerable<ICollaborator> FindAllCollaboratorsWithNameAndSurname(string names, string surnames)
    {
        // Where returns an enumerable - whereas FindAll returns a list
        return _collaborators.Where(c => c.HasNames(names) && c.HasSurnames(surnames));
    }
}