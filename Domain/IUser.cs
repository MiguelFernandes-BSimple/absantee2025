public interface IUser
{
    public bool IsDeactivated();
    public bool DeactivationDateIsBefore(DateTime date);
    public bool DeactivateUser();
    /**
    * Method to verify if a given string is found inside the names of an User
    * It just needs to contain the string, it doesn't need to be the exact same
    * Returns true  -> if the User parameter contains the string names
    * Returns false -> otherwise; or in case its a invalid string (null, whitespaces or empty)
    */
    public bool HasNames(string names);

    /**
    * Method to verify if a given string is found inside the surnames of an User
    * It just needs to contain the string, it doesn't need to be the exact same
    * Returns true  -> if the User parameter contains the string surnames
    * Returns false -> otherwise; or in case its a invalid string (null, whitespaces or empty)
    */
    public bool HasSurnames(string surnames);

}