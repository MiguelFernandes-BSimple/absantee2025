public interface ICollaborator
{
    public bool ContractContainsDates(DateTime _initDate, DateTime _finalDate);

    /**
    * Method to verify if a given string is found inside the names of an User
    * It uses the parameter on the class User
    * Returns true  -> if the User parameter contains the string names
    * Returns false -> otherwise; or in case its a invalid string (null, whitespaces or empty)
    */
    public bool HasNames(string names);

    /**
    * Method to verify if a given string is found inside the surnames of an User
    * It uses the parameter on the class User
    * Returns true  -> if the User parameter contains the string surnames
    * Returns false -> otherwise; or in case its a invalid string (null, whitespaces or empty)
    */
    public bool HasSurnames(string surnames);

    /**
    * Getter for email parameter
    */
    public string GetEmail();
}
