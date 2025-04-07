namespace Infrastructure.DataModel;

public class HolidayPlanDataModel
{
    public long collaboratorId { get; set; }

    public List<HolidayPerioDataModel> holidayPeriod { get; set; }

    public HolidayPlanDataModel()
    {
    }
}