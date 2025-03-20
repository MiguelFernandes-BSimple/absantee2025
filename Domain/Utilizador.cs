using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Domain;
public class Utilizador : IUtilizador {
    public Utilizador(string nomes, string apelidos, string email, DateTime? dataDesativacao = null)
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


    private string Nomes;
    private string Apelidos;
    private string Email;
    private DateTime DataCriacao;
    private DateTime DataDesativacao;

    private bool CheckInputValues(string nomes, string apelidos, string email, DateTime dataDesativacao)
    {
        // Regex to allow only letters (including accented characters) and spaces, max 50 chars
        Regex nameRegex = new Regex(@"^[A-Za-zÀ-ÖØ-öø-ÿ\s]{1,50}$");

        if (!nameRegex.IsMatch(nomes) || !nameRegex.IsMatch(apelidos))
        {
            return false; // nomes or apelidos contain invalid characters or exceed 50 chars
        }

        // Email validation
        try
        {
            var emailValidator = new MailAddress(email);
        } catch (Exception)
        {
            return false;
        }

        // Date validation
        if (IsDesativo())
        {
            return false; // dataDesativacao must be in the future
        }

        return true; // All validations passed
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