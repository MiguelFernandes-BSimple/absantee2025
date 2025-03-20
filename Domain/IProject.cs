public interface IProject {
    public bool IsInside(DateOnly dataInicio, DateOnly dataFim);
    public bool IsFinished();
}