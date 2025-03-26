namespace Domain;

public interface IAssociationProjectColaborator 
{

    public List<IColaborator> GetCollaborators(IProject project);
    
}
