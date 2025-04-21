using Domain.Factory;
using Domain.Interfaces;
using Domain.Visitor;
namespace Domain.Models;



public class Subject : ISubject
{
    private long _id;
    private string _titulo;

    private string _descricao;

    public Subject(string titulo, string descricao)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            throw new ArgumentException("O título não pode ser nulo ou vazio.", nameof(titulo));

        if (titulo.Length > 20 || !titulo.All(char.IsLetterOrDigit))
            throw new ArgumentException("O título deve ter no máximo 20 caracteres alfanuméricos.", nameof(titulo));

        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("A descrição não pode ser nula ou vazia.", nameof(descricao));

        if (descricao.Length > 100 || !descricao.All(char.IsLetterOrDigit))
            throw new ArgumentException("A descrição deve ter no máximo 100 caracteres alfanuméricos.", nameof(descricao));

        _titulo = titulo;
        _descricao = descricao;
    }

    public Subject(long id, string titulo, string descricao)
    {
        _id = id;
        _titulo = titulo;
        _descricao = descricao;
    }

    public long GetId()
    {
        return _id;
    }
    public void SetId(long id)
    {
        _id = id;
    }
    public string GetTitulo()
    {
        return _titulo;
    }

    public string GetDescricao()
    {
        return _descricao;
    }
}