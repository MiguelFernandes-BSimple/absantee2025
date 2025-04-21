using System.Net.Cache;
using Domain.Models;

namespace Domain.Factory;

public class TrainingSubjectFactory : ITrainingSubjectFactory
{
    public TrainingSubjectFactory() {

    }
    
    public async Task<TrainingSubject> Create(string title, string description)
    {
        if(!CheckString(title, 20))
            throw new ArgumentException("Invalid Title");

        if(!CheckString(description, 100))
            throw new ArgumentException("Invalid Description");

        TrainingSubject subject = new TrainingSubject(title, description);

        //await subjectRepo.IsRepeated(subject);
        
        return subject;
    }

    /*public TrainingSubject Create(ITrainingSubjectVisitor visitor) {
        return new TrainingSubject(visitor.id, visitor.title, visitor.description)
    }*/
    
    private bool CheckString(string str, long length) {
        if(str.Length > length)
            return false;
        if(string.IsNullOrEmpty(str))
            return false;
        return str.All(char.IsLetterOrDigit);
    }
}
