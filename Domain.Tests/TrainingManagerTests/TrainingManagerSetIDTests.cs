using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Moq;
using Xunit;

namespace Domain.Tests.TrainingManagerTests;

public class TrainingManagerSetIDTests
{
    [Fact]
    public void WhenSettingId_ThenIdIsUpdated()
    {
        // arrange
        var initialId = 1;
        var newId = 2;
        var userId = 4;
        var TrainingManager = new TrainingManager(initialId, userId, It.IsAny<PeriodDateTime>());

        // act
        TrainingManager.SetId(newId);
        var result = TrainingManager.GetId();

        // assert
        Assert.Equal(newId, result);
    }
}
