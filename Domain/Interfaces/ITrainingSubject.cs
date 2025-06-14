﻿namespace Domain.Interfaces;

public interface ITrainingSubject
{
    Guid Id { get; }
    string Subject { get; }
    string Description { get; }
}
