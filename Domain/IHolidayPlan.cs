namespace Domain;

public interface IHolidayPlan
{
    bool AddHolidayPeriod(IHolidayPeriod holidayPeriod);
    public bool HasCollaborator(IColaborator colab);
    IHolidayPeriod GetHolidayPeriodContainingDate(DateOnly date);
}