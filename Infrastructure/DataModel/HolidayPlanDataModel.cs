namespace Infrastructure.DataModel;

public class HolidayPlanDataModel
{
    public long Id { get; set; }
    public long CollaboratorId { get; set; }
    public List<HolidayPeriodDataModel> holidayPeriod { get; set; }

    public HolidayPlanDataModel()
    {
    }
}