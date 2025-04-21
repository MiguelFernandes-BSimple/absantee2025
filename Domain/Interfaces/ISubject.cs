using Domain.Models;

namespace Domain.Interfaces;


public interface ISubject
{
    public long GetId();
    public string GetTitulo();
    public string GetDescricao();
    

}