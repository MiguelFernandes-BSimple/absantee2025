using Domain.Models;
using Domain.Interfaces;
using Moq;

namespace Domain.Tests.FormationModuleTests;

public class FormationModuleConstructorTests
{
    [Fact]
    public void WhenPassingValidSinglePeriod_ThenFormationModuleIsCreated()
    {
        // Arrange
        Mock<IFormationPeriod> formationPeriodDouble = new Mock<IFormationPeriod>();

        formationPeriodDouble.Setup(fp => fp._periodDate).Returns(It.IsAny<PeriodDate>());

        var formationPeriods = new List<IFormationPeriod> { formationPeriodDouble.Object };

        // Act
        FormationModule formationModule = new FormationModule(It.IsAny<long>(), formationPeriods);

        // Assert
    }

    [Fact]
    public void WhenPassingValidMultiplePeriods_ThenHolidayPlanIsCreated()
    {
        // Arrange
        Mock<IFormationPeriod> formationPeriodDouble1 = new Mock<IFormationPeriod>();

        formationPeriodDouble1.Setup(fp => fp._periodDate).Returns(It.IsAny<PeriodDate>());

        Mock<IFormationPeriod> formationPeriodDouble2 = new Mock<IFormationPeriod>();
        formationPeriodDouble2.Setup(fp => fp._periodDate).Returns(It.IsAny<PeriodDate>());

        Mock<IFormationPeriod> formationPeriodDouble3 = new Mock<IFormationPeriod>();
        formationPeriodDouble3.Setup(fp => fp._periodDate).Returns(It.IsAny<PeriodDate>());

        formationPeriodDouble1
            .Setup(fp1 => fp1.Contains(It.IsAny<IFormationPeriod>()))
            .Returns(false);
        formationPeriodDouble2
            .Setup(fp2 => fp2.Contains(It.IsAny<IFormationPeriod>()))
            .Returns(false);
        formationPeriodDouble3
            .Setup(fp3 => fp3.Contains(It.IsAny<IFormationPeriod>()))
            .Returns(false);

        List<IFormationPeriod> formationPeriods = new List<IFormationPeriod>
        {
            formationPeriodDouble1.Object,
            formationPeriodDouble2.Object,
            formationPeriodDouble3.Object,
        };

        // Act
        FormationModule formationModule = new FormationModule(It.IsAny<long>(), formationPeriods);

        // Assert
    }
}