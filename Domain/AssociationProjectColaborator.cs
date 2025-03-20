namespace Domain;

public class AssociationProjectColaborator {
    private DateOnly _initDate;
    private DateOnly _finalDate;
    private IColaborator _colaborator;
    private IProject _project;

    public AssociationProjectColaborator(DateOnly initDate, DateOnly finalDate, IColaborator colaborator, IProject project)
    {
        if(CheckInputValues(initDate, finalDate, colaborator, project)){
           this._initDate = initDate;
            this._finalDate = finalDate;
            _colaborator = colaborator;
            _project = project; 
        } else  	
            throw new ArgumentException("Invalid Arguments");        
    }

    private bool CheckInputValues(DateOnly initDate, DateOnly finalDate, IColaborator colaborator, IProject project){
        if(initDate > finalDate)
            return false;
        
        if(project.IsInside(initDate, finalDate))
            return false;

        if(project.IsFinished())
            return false;

        if(!colaborator.IsInside(initDate.ToDateTime(TimeOnly.MinValue), finalDate.ToDateTime(TimeOnly.MinValue)))
            return false;

        return true;
    }

}