using Domain.Models;

namespace Domain.Interfaces;

public interface ISubject
{
    public long GetId();
    public void SetId(long id);
    public string GetTitle();
    public string GetDescription();
}