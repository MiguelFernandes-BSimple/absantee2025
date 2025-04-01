using Infrastructure.Interfaces;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class CollaboratorRepository : ICollaboratorRepository
{
    private List<ICollaborator> _collaborators;

    public CollaboratorRepository()
    {
        _collaborators = new List<ICollaborator>();
    }

    public CollaboratorRepository(List<ICollaborator> collaborators) : this()
    {
        bool isValid = true;

        //Validate if collaborator list is valid
        // All collaborators have to be valid
        for (int collabIndex1 = 0; collabIndex1 < collaborators.Count; collabIndex1++)
        {
            if (!isValid)
                break;

            ICollaborator currColaborator = collaborators[collabIndex1];
            isValid = CanInsert(currColaborator, collaborators.Skip(collabIndex1 + 1).ToList());
        }

        // If the list is valid -> insert Collborators in repo
        if (isValid)
        {
            foreach (ICollaborator collabIndex2 in collaborators)
            {
                AddCollaborator(collabIndex2);
            }
        }
        else
        {
            throw new ArgumentException("Arguments are not valid!");
        }

    }

    public IEnumerable<ICollaborator> FindAllCollaborators()
    {
        // Create a new list - to not share the pointer
        return new List<ICollaborator>(_collaborators);
    }

    public IEnumerable<ICollaborator> FindAllCollaboratorsWithName(string names)
    {
        return _collaborators.Where(c => c.HasNames(names));
    }

    public IEnumerable<ICollaborator> FindAllCollaboratorsWithSurname(string surnames)
    {
        return _collaborators.Where(c => c.HasSurnames(surnames));
    }

    public IEnumerable<ICollaborator> FindAllCollaboratorsWithNameAndSurname(string names, string surnames)
    {
        return _collaborators.Where(c => c.HasNames(names) && c.HasSurnames(surnames));
    }

    /**
    * Method to validate whether a collaborator can be insert in a given list or not
    */
    private bool CanInsert(ICollaborator collaborator, List<ICollaborator> collabList)
    {
        bool alreadyExists =
            collabList.Any(c => c.GetEmail().Equals(collaborator.GetEmail()));

        return !alreadyExists;
    }

    /**
    * Method to add a single collaborator to the repository
    */
    public bool AddCollaborator(ICollaborator collaborator)
    {
        bool canInsert = CanInsert(collaborator, _collaborators);
        if (canInsert)
            _collaborators.Add(collaborator);

        return canInsert;
    }
}