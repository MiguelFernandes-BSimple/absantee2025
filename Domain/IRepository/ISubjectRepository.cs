using Domain.Interfaces;

public interface ISubjectRepository{

    Task<ISubject?> GetByTituloAsync(string names);
    ISubject? GetById(long id);
    Task<ISubject?> GetByIdAsync(long id);
}