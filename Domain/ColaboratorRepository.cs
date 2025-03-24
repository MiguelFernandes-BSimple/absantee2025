
namespace Domain;

public class ColaboratorRepository : IColaboratorRepository
{
    private List<IColaborator> _colaborators;

    public IEnumerable<IColaborator> FindAllColaborators()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<IColaborator> FindAllColaboratorsWithName(string name)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<IColaborator> FindAllColaboratorsWithNameAndSurname(string name, string surname)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<IColaborator> FindAllColaboratorsWithSurname(string surname)
    {
        throw new NotImplementedException();
    }
}