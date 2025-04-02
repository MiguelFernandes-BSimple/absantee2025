using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Repositories;
using Moq;

namespace Infrastructure.Tests.HolidayPlanRepositoryTests
{
    public class FindAllCollaboratorsWithHolidayPeriodsBetweenDates
    {
        // US14

        [Fact]
        public void WhenCollaboratorHasHolidayPeriodWithinDateRange_ThenReturnsHolidayPlan()
        {
            // Arrange
            var collaborator = new Mock<ICollaborator>();

            var holidayPeriod = new Mock<IHolidayPeriod>();

            var holidayPlan = new Mock<IHolidayPlan>();
            holidayPlan.Setup(hp => hp.HasCollaborator(collaborator.Object)).Returns(true);
            holidayPlan
                .Setup(hp => hp.GetHolidayPeriods())
                .Returns(new List<IHolidayPeriod> { holidayPeriod.Object });

            holidayPeriod.Setup(hperiod => hperiod.Intersects(It.IsAny<IPeriodDate>())).Returns(true);

            holidayPlan.Setup(hp => hp.GetCollaborator()).Returns(collaborator.Object);

            var hpRepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlan.Object });

            var expected = new List<IHolidayPlan>(){ holidayPlan.Object };
            // Act
            var result = hpRepo.FindAllCollaboratorsWithHolidayPeriodsBetweenDates(It.IsAny<IPeriodDate>());

            // Assert
            Assert.True(result.SequenceEqual(expected));
        }



        [Fact]
        public void WhenMultipleCollaboratorsHaveHolidayPeriodsWithinDateRange_ThenReturnsAllCollaborators()
        {
            // Arrange
            var collaborator1 = new Mock<ICollaborator>();
            var collaborator2 = new Mock<ICollaborator>();

            var holidayPeriod1 = new Mock<IHolidayPeriod>();

            var holidayPeriod2 = new Mock<IHolidayPeriod>();

            var holidayPlan1 = new Mock<IHolidayPlan>();
            holidayPlan1.Setup(hp => hp.HasCollaborator(collaborator1.Object)).Returns(true);
            holidayPlan1
                .Setup(hp => hp.GetHolidayPeriods())
                .Returns(new List<IHolidayPeriod> { holidayPeriod1.Object });
            holidayPeriod1.Setup(hperiod => hperiod.Intersects(It.IsAny<IPeriodDate>())).Returns(true);

            holidayPlan1.Setup(hp => hp.GetCollaborator()).Returns(collaborator1.Object);

            var holidayPlan2 = new Mock<IHolidayPlan>();
            holidayPlan2.Setup(hp => hp.HasCollaborator(collaborator2.Object)).Returns(true);
            holidayPlan2
                .Setup(hp => hp.GetHolidayPeriods())
                .Returns(new List<IHolidayPeriod> { holidayPeriod2.Object });
            holidayPeriod2.Setup(hperiod => hperiod.Intersects(It.IsAny<IPeriodDate>())).Returns(true);

            holidayPlan2.Setup(hp => hp.GetCollaborator()).Returns(collaborator2.Object);

            var hpRepo = new HolidayPlanRepository(
                new List<IHolidayPlan> { holidayPlan1.Object, holidayPlan2.Object }
            );

            var expected = new List<IHolidayPlan>() { holidayPlan1.Object, holidayPlan2.Object };

            // Act
            var result = hpRepo.FindAllCollaboratorsWithHolidayPeriodsBetweenDates(It.IsAny<IPeriodDate>());

            // Assert
            Assert.True(result.SequenceEqual(expected));
        }


        [Fact]
        public void WhenCollaboratorHasPeriodsBothInsideAndOutsideRange_ThenReturnsCollaborator()
        {
            // Arrange
            var collaborator = new Mock<ICollaborator>();

            var holidayPeriod1 = new Mock<IHolidayPeriod>();

            var holidayPeriod2 = new Mock<IHolidayPeriod>();

            var holidayPlan = new Mock<IHolidayPlan>();
            holidayPlan.Setup(hp => hp.HasCollaborator(collaborator.Object)).Returns(true);
            holidayPlan
                .Setup(hp => hp.GetHolidayPeriods())
                .Returns(new List<IHolidayPeriod> { holidayPeriod1.Object, holidayPeriod2.Object });

            holidayPeriod1.Setup(hperiod => hperiod.Intersects(It.IsAny<IPeriodDate>())).Returns(false);
            holidayPeriod2.Setup(hperiod => hperiod.Intersects(It.IsAny<IPeriodDate>())).Returns(true);


            holidayPlan.Setup(hp => hp.GetCollaborator()).Returns(collaborator.Object);

            var hpRepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlan.Object });
            var expected = new List<IHolidayPlan>() { holidayPlan.Object };

            // Act
            var result = hpRepo.FindAllCollaboratorsWithHolidayPeriodsBetweenDates(It.IsAny<IPeriodDate>());

            // Assert
            Assert.True(result.SequenceEqual(expected));
        }

        [Fact]
        public void WhenNoCollaboratorsHaveHolidayPeriodsInDateRange_ThenReturnsEmptyList()
        {
            // Arrange
            var collaborator = new Mock<ICollaborator>();

            var holidayPeriod = new Mock<IHolidayPeriod>();

            var holidayPlan = new Mock<IHolidayPlan>();
            holidayPlan.Setup(hp => hp.HasCollaborator(collaborator.Object)).Returns(true);
            holidayPlan
                .Setup(hp => hp.GetHolidayPeriods())
                .Returns(new List<IHolidayPeriod> { holidayPeriod.Object });
            holidayPeriod.Setup(hperiod => hperiod.Intersects(It.IsAny<IPeriodDate>())).Returns(false);
            holidayPlan.Setup(hp => hp.GetCollaborator()).Returns(collaborator.Object);

            var hpRepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlan.Object });
            // Act
            var result = hpRepo.FindAllCollaboratorsWithHolidayPeriodsBetweenDates(It.IsAny<IPeriodDate>());

            // Assert
            Assert.Empty(result);
        }
    }
}
