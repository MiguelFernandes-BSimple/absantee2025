using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Domain;
public class Utilizador : IUtilizador {
    private string Nomes;
    private string Apelidos;
    private string Email;
    private DateTime DataCriacao;
    private DateTime DataDesativacao;

    public Utilizador(string nomes, string apelidos, string email, DateTime? dataDesativacao)
    {
        dataDesativacao ??= DateTime.MaxValue;

        if (CheckInputValues(nomes, apelidos, email, (DateTime)dataDesativacao)){
            Nomes = nomes;
            Apelidos = apelidos;
            Email = email;
            DataCriacao = DateTime.Now;
            DataDesativacao = (DateTime)dataDesativacao;
        } else {
            throw new ArgumentException("Invalid Arguments");
        }
    }

    private bool CheckInputValues(string nomes, string apelidos, string email, DateTime dataDesativacao)
    {
        Regex nameRegex = new Regex(@"^[A-Za-zÀ-ÖØ-öø-ÿ\s]{1,50}$");

        if (!nameRegex.IsMatch(nomes) || !nameRegex.IsMatch(apelidos))
        {
            return false;
        }

        try
        {
            var emailValidator = new MailAddress(email);
        } catch (Exception)
        {
            return false;
        }

        // Date validation
        if (DateTime.Now >= dataDesativacao)
        {
            return false;
        }

        return true;
    }

    public bool IsDesativo(){
        if (DateTime.Now >= DataDesativacao)
            return true;
        else
            return false;
    }

    public bool IsBiggerThenDataDesativacao(DateTime date){
        return date > DataDesativacao;
    }
}