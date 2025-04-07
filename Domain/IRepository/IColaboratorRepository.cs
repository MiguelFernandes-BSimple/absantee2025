using Domain.Interfaces;

namespace Domain.IRepository;

public interface ICollaboratorRepository
{
    /**
    * Method that returns all the collaborators within the repository
    * Returns: IEnumerable<ICollaborator>
    */
    IEnumerable<ICollaborator> FindAllCollaborators();
    Task<IEnumerable<ICollaborator>> FindAllCollaboratorsAsync();

    /**
    * Method that returns all the collaborators that have a name
    * This can be the only string or just a substring of their name
    * It just has to be included - Users can have multiple names
    * Returns: IEnumerable<ICollaborator>
    */
    IEnumerable<ICollaborator> FindAllCollaboratorsWithName(string name);
    Task<IEnumerable<ICollaborator>> FindAllCollaboratorsWithNameAsync(string name);

    /**
    * Method that returns all the collaborators that have a surname
    * This can be the only string or just a substring of their surname
    * It just has to be included - Users can have multiple surnames
    * Returns: IEnumerable<ICollaborator>
    */
    IEnumerable<ICollaborator> FindAllCollaboratorsWithSurname(string surname);
    Task<IEnumerable<ICollaborator>> FindAllCollaboratorsWithSurnameAsync(string surname);

    /**
    * Method that returns all the collaborators that have a name and surname
    * This can be the only string or just a substring of their name/surname
    * It just has to be included - Users can have multiple names/surnames
    * Returns: IEnumerable<ICollaborator>
    */
    IEnumerable<ICollaborator> FindAllCollaboratorsWithNameAndSurname(string name, string surname);
    Task<IEnumerable<ICollaborator>> FindAllCollaboratorsWithNameAndSurnameAsync(string name, string surname);

}