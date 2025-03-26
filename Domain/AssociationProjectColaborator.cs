namespace Domain;

public class AssociationProjectColaborator : IAssociationProjectColaborator
{
    private DateOnly _initDate;
    private DateOnly _finalDate;
    private IColaborator _colaborator;
    private IProject _project;

    public AssociationProjectColaborator(
        DateOnly initDate,
        DateOnly finalDate,
        IColaborator colaborator,
        IProject project
    )
    {
        if (CheckInputValues(initDate, finalDate, colaborator, project))
        {
            _initDate = initDate;
            _finalDate = finalDate;
            _colaborator = colaborator;
            _project = project;
        }
        else
            throw new ArgumentException("Invalid Arguments");
    }

    private bool CheckInputValues(
        DateOnly initDate,
        DateOnly finalDate,
        IColaborator colaborator,
        IProject project
    )
    {
        if (initDate > finalDate)
            return false;

        if (!project.ContainsDates(initDate, finalDate))
            return false;

        if (project.IsFinished())
            return false;

        DateTime associationInitDate = initDate.ToDateTime(TimeOnly.MinValue);
        DateTime associationFinalDate = finalDate.ToDateTime(TimeOnly.MinValue);
        if (!colaborator.ContainsDates(associationInitDate, associationFinalDate))
            return false;

        return true;
    }

    public IColaborator GetColaborator()
    {
        return _colaborator;
    }

    public IProject GetProject()
    {
        return _project;
    }

    public bool HasProject(IProject project)
    {
        return this._project.Equals(project);
    }

    public bool AssociationIntersectDates(DateOnly initDate, DateOnly finalDate)
    {
        return _initDate <= finalDate && initDate <= _finalDate;
    }
}
