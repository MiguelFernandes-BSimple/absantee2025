using Domain.Models;

namespace Domain.Interfaces;

public interface ITrainingModule {
    List<PeriodDateTime> periods {get;}

    long GetId();
    long GetSubjectId();
}
