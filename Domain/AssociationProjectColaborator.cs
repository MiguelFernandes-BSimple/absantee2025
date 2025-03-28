
namespace Domain;

public class AssociationProjectCollaborator : IAssociationProjectCollaborator
{
    private DateOnly _initDate;
    private DateOnly _finalDate;
    private ICollaborator _collaborator;
    private IProject _project;

    public AssociationProjectCollaborator(DateOnly initDate, DateOnly finalDate, ICollaborator collaborator, IProject project)
    {
        if (CheckInputValues(initDate, finalDate, collaborator, project))
        {
            _initDate = initDate;
            _finalDate = finalDate;
            _collaborator = collaborator;
            _project = project;
        }
        else
            throw new ArgumentException("Invalid Arguments");
    }

    public DateOnly GetInitDate()
    {
        return _initDate;
    }

    public DateOnly GetFinalDate()
    {
        return _finalDate;
    }

    private bool CheckInputValues(DateOnly initDate, DateOnly finalDate, ICollaborator collaborator, IProject project)
    {
        if (initDate > finalDate)
            return false;

        if (!project.ContainsDates(initDate, finalDate))
            return false;

        if (project.IsFinished())
            return false;

        DateTime associationInitDate = initDate.ToDateTime(TimeOnly.MinValue);
        DateTime associationFinalDate = finalDate.ToDateTime(TimeOnly.MinValue);
        if (!collaborator.ContractContainsDates(associationInitDate, associationFinalDate))
            return false;

        return true;
    }

    public ICollaborator GetCollaborator()
    {
        return this._collaborator;
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