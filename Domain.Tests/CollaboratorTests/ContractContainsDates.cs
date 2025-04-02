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
        DateTime _initDate,
        DateTime _finalDate
    )
    {
        // Arrange
        Mock<IUser> user = new Mock<IUser>();
        DateTime collaboratorInitDate = new DateTime(2020, 1, 1);
        DateTime collaboratorFinalDate = new DateTime(2021, 1, 1);

        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();
        periodDateTime.Setup(p => p.GetInitDate()).Returns(_initDate);
        periodDateTime.Setup(p => p.GetFinalDate()).Returns(_finalDate);

        Collaborator collaborator = new Collaborator(user.Object, periodDateTime.Object);

        // Act
        bool result = collaborator.ContractContainsDates(_initDate, _finalDate);
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
        DateTime _initDate,
        DateTime _finalDate
    )
    {
        // Arrange
        Mock<IUser> user = new Mock<IUser>();
        DateTime collaboratorInitDate = new DateTime(2020, 1, 1);
        DateTime collaboratorFinalDate = new DateTime(2021, 1, 1);

        Mock<IPeriodDateTime> periodDateTime = new Mock<IPeriodDateTime>();
        periodDateTime.Setup(p => p.GetInitDate()).Returns(_initDate);
        periodDateTime.Setup(p => p.GetFinalDate()).Returns(_finalDate);

        Collaborator collaborator = new Collaborator(user.Object, periodDateTime.Object);
        // Act
        bool result = collaborator.ContractContainsDates(_initDate, _finalDate);
        // Assert
        Assert.False(result);
    }
}