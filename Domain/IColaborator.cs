public interface IColaborator
{
    public bool ContainsDates(DateTime _initDate, DateTime _finalDate);
    public bool HasNames(string names);
    public bool HasSurnames(string surnames);
}
