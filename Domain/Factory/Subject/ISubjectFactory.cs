using System.Security.Cryptography.X509Certificates;
using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;


public interface ISubjectFactory
{
    public Task <Subject> Create(string titulo, string descricao);

    public Subject Create(ISubjectVisitor subjectVisitor);   
}