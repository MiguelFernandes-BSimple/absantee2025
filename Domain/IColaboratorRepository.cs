namespace Domain;

public interface IColaboratorRepository
{
    IEnumerable<IColaborator> FindAllColaborators();
    IEnumerable<IColaborator> FindAllColaboratorsWithName(string name);
    IEnumerable<IColaborator> FindAllColaboratorsWithSurname(string surname);
    IEnumerable<IColaborator> FindAllColaboratorsWithNameAndSurname(string name, string surname);
}