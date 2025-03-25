namespace Domain;

public interface IColaboratorRepository
{
    /**
    * Method that returns all the colaborators within the repository
    * Returns: IEnumerable<IColaborator>
    */
    IEnumerable<IColaborator> FindAllColaborators();

    /**
    * Method that returns all the colaborators that have a name
    * This can be the only string or just a substring of their name
    * It just has to be included - Users can have multiple names
    * Returns: IEnumerable<IColaborator>
    */
    IEnumerable<IColaborator> FindAllColaboratorsWithName(string name);

    /**
    * Method that returns all the colaborators that have a surname
    * This can be the only string or just a substring of their surname
    * It just has to be included - Users can have multiple surnames
    * Returns: IEnumerable<IColaborator>
    */
    IEnumerable<IColaborator> FindAllColaboratorsWithSurname(string surname);

    /**
    * Method that returns all the colaborators that have a name and surname
    * This can be the only string or just a substring of their name/surname
    * It just has to be included - Users can have multiple names/surnames
    * Returns: IEnumerable<IColaborator>
    */
    IEnumerable<IColaborator> FindAllColaboratorsWithNameAndSurname(string name, string surname);
}