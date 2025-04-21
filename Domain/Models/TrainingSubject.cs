using System.ComponentModel.Design;
using Domain.Interfaces;

namespace Domain.Models;

public class TrainingSubject : ITrainingSubject {
    private long _id;
    public string _title;
    public string _description;

    public TrainingSubject(string Title, string Desc)
    {
        _title = Title;
        _description = Desc;
    }

    public long GetId() {
        return _id;
    }

    public string GetTitle()
    {
        return _title;
    }

    public string GetDescription()
    {
        return _description;
    }
}
