using Moq;
using Domain.Interfaces;
using Domain.Models;

namespace Domain.Tests.CollaboratorTests;

public class ContractContainsDates
{
    public static IEnumerable<object[]> ContainsDates_ValidDates()
    {
        yield return new object[] { new DateTime(2020, 1, 1), new DateTime(2021, 1, 1) };
        yield return new object[] { new DateTime(2020, 1, 2), new DateTime(2020, 12, 31) };
    }

    [Theory]
    [MemberData(nameof(ContainsDates_ValidDates))]
    public void WhenPassingValidDatesToContainsDates_ThenResultIsTrue(
        DateTime searchInitDate,
        DateTime searchEndDate
    )
    {
        // Arrange
        Mock<IUser> user = new Mock<IUser>();
        DateTime collaboratorInitDate = new DateTime(2020, 1, 1);
        DateTime collaboratorFinalDate = new DateTime(2021, 1, 1);

        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();
        periodDateTime.Setup(p => p.GetInitDate()).Returns(collaboratorInitDate);
        periodDateTime.Setup(p => p.GetFinalDate()).Returns(collaboratorFinalDate);

        Collaborator collaborator = new Collaborator(user.Object, periodDateTime.Object);

        Mock<IPeriodDateTime> searchPeriodDateTime = new Mock<IPeriodDateTime>();
        searchPeriodDateTime.Setup(p => p.GetInitDate()).Returns(searchInitDate);
        searchPeriodDateTime.Setup(p => p.GetFinalDate()).Returns(searchEndDate);

        // Act
        bool result = collaborator.ContractContainsDates(searchPeriodDateTime.Object);
        // Assert
        Assert.True(result);
    }

    public static IEnumerable<object[]> ContainsDates_InvalidDates()
    {
        yield return new object[] { new DateTime(2019, 1, 1), new DateTime(2021, 1, 1) };
        yield return new object[] { new DateTime(2020, 1, 2), new DateTime(2022, 12, 31) };
    }

    [Theory]
    [MemberData(nameof(ContainsDates_InvalidDates))]
    public void WhenPassingInvalidDatesToContainsDates_ThenResultIsFalse(
        DateTime searchInitDate,
        DateTime searchEndDate
    )
    {
        // Arrange
        Mock<IUser> user = new Mock<IUser>();
        DateTime collaboratorInitDate = new DateTime(2020, 1, 1);
        DateTime collaboratorFinalDate = new DateTime(2021, 1, 1);

        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();
        periodDateTime.Setup(p => p.GetInitDate()).Returns(collaboratorInitDate);
        periodDateTime.Setup(p => p.GetFinalDate()).Returns(collaboratorFinalDate);

        Collaborator collaborator = new Collaborator(user.Object, periodDateTime.Object);

        Mock<IPeriodDateTime> searchPeriodDateTime = new Mock<IPeriodDateTime>();
        searchPeriodDateTime.Setup(p => p.GetInitDate()).Returns(searchInitDate);
        searchPeriodDateTime.Setup(p => p.GetFinalDate()).Returns(searchEndDate);
        // Act
        bool result = collaborator.ContractContainsDates(searchPeriodDateTime.Object);
        // Assert
        Assert.False(result);
    }
}