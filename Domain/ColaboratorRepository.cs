
namespace Domain;

public class ColaboratorRepository : IColaboratorRepository
{
    private List<IColaborator> _colaborators;

    public ColaboratorRepository(List<IColaborator> colaborators)
    {
        _colaborators = new List<IColaborator>(colaborators);
    }

    public IEnumerable<IColaborator> FindAllColaborators()
    {
        // Create a new list - to not share the pointer
        return new List<IColaborator>(_colaborators);
    }

    public IEnumerable<IColaborator> FindAllColaboratorsWithName(string names)
    {
        // Where returns an enumerable - whereas FindAll returns a list
        return _colaborators.Where(c => c.HasNames(names));
    }

    public IEnumerable<IColaborator> FindAllColaboratorsWithSurname(string surnames)
    {
        // Where returns an enumerable - whereas FindAll returns a list
        return _colaborators.Where(c => c.HasSurnames(surnames));
    }

    public IEnumerable<IColaborator> FindAllColaboratorsWithNameAndSurname(string names, string surnames)
    {
        // Where returns an enumerable - whereas FindAll returns a list
        return _colaborators.Where(c => c.HasNames(names) && c.HasSurnames(surnames));
    }
}