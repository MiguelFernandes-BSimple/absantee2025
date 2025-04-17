using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Moq;
using Xunit;

namespace Domain.Tests.ProjectManagerTests;

public class ProjectManagerSetIDTests
{
    [Fact]
    public void WhenSettingId_ThenIdIsUpdated()
    {
        // arrange
        var initialId = 1;
        var newId = 2;
        var userId = 4;
        var projectManager = new ProjectManager(initialId, userId, It.IsAny<PeriodDateTime>());

        // act
        projectManager.SetId(newId);
        var result = projectManager.GetId();

        // assert
        Assert.Equal(newId, result);
    }
}
