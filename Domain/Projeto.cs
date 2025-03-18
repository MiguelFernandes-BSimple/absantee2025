using System.Text.RegularExpressions;

namespace Domain;
public class Projeto : IProjeto
{
    private string Titulo;
    private string Sigla;
    private DateOnly DataInicio;
    private DateOnly DataFim;

    public Projeto(string titulo, string sigla, DateOnly dataInicio, DateOnly dataFim)
    {
        if (CheckInputValues(titulo, sigla, dataInicio, dataFim))
        {
            this.Titulo = titulo;
            this.Sigla = sigla;
            this.DataInicio = dataInicio;
            this.DataFim = dataFim;
        }
        else
            throw new ArgumentException("Invalid Arguments");
    }

    private bool CheckInputValues(string titulo, string sigla, DateOnly dataInicio, DateOnly dataFim)
    {
        if (dataInicio > dataFim)
            return false;

        Regex tituloRegex = new Regex(@"^.{1,50}$");
        Regex siglaRegex = new Regex(@"^[A-Z0-9]{1,10}$");

        if (!tituloRegex.IsMatch(titulo) || !siglaRegex.IsMatch(sigla))
        {
            return false;
        }

        return true;
    }

    public int CompareWithDataInicio(DateOnly dataInicio)
    {
        return dataInicio.CompareTo(this.DataInicio);
    }

    public int CompareWithDataFim(DateOnly dataFim)
    {
        return dataFim.CompareTo(this.DataFim);
    }
}