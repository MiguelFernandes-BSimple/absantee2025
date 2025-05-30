using Application.Services;
using Moq;
using Domain.Models;
using Domain.Interfaces;
using System.Collections.Generic;
using Application.DTO.Collaborators;

namespace Application.Tests.CollaboratorServiceTests
{
    public class CollaboratorServiceFindAllWithHolidayPeriodsLongerThan : CollaboratorServiceTestBase
    {
        // There is an error fetching the elements from DB
        // Exception is thrown
        [Fact]
        public async Task FindAllWithHolidayPeriodsLongerThan_ReturnsFailure_WhenExceptionIsThrown()
        {
            // Arrange
            HolidayPlanRepositoryDouble
                .Setup(repo => repo.FindAllWithHolidayPeriodsLongerThanAsync(It.IsAny<int>()))
                .ThrowsAsync(new Exception("DB error"));

            // Act
            var result = await CollaboratorService.FindAllWithHolidayPeriodsLongerThan(It.IsAny<int>());

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("DB error", result.Error!.Message);
        }

        // Happy path
        [Fact]
        public async Task FindAllWithHolidayPeriodsLongerThan_ReturnsCorrectCollaborators()
        {
            // Arrange
            Mock<IHolidayPlan> holidayPlan1 = new Mock<IHolidayPlan>();
            Mock<IHolidayPlan> holidayPlan2 = new Mock<IHolidayPlan>();
            Mock<IHolidayPlan> holidayPlan3 = new Mock<IHolidayPlan>();

            IEnumerable<IHolidayPlan> holidayPlanList = new List<IHolidayPlan> { holidayPlan1.Object, holidayPlan2.Object, holidayPlan3.Object };

            HolidayPlanRepositoryDouble.Setup(repo => repo.FindAllWithHolidayPeriodsLongerThanAsync(It.IsAny<int>())).ReturnsAsync(holidayPlanList);

            Guid collabId1 = new Guid();
            Guid collabId2 = new Guid();
            Guid collabId3 = new Guid();

            holidayPlan1.Setup(hp => hp.CollaboratorId).Returns(collabId1);
            holidayPlan2.Setup(hp => hp.CollaboratorId).Returns(collabId2);
            holidayPlan3.Setup(hp => hp.CollaboratorId).Returns(collabId3);

            IEnumerable<Guid> collabIdsList = new List<Guid> { collabId1, collabId2, collabId3 };

            Mock<ICollaborator> collab1 = new Mock<ICollaborator>();
            Mock<ICollaborator> collab2 = new Mock<ICollaborator>();
            Mock<ICollaborator> collab3 = new Mock<ICollaborator>();

            collab1.Setup(c => c.Id).Returns(collabId1);
            collab2.Setup(c => c.Id).Returns(collabId2);
            collab3.Setup(c => c.Id).Returns(collabId3);

            IEnumerable<ICollaborator> expectedCollabList = new List<ICollaborator> { collab1.Object, collab2.Object, collab3.Object };

            CollaboratorRepositoryDouble.Setup(repo => repo.GetByIdsAsync(collabIdsList)).ReturnsAsync(expectedCollabList);

            var dto1 = new CollaboratorDTO(collabId1, It.IsAny<Guid>(), It.IsAny<PeriodDateTime>());
            var dto2 = new CollaboratorDTO(collabId2, It.IsAny<Guid>(), It.IsAny<PeriodDateTime>());
            var dto3 = new CollaboratorDTO(collabId3, It.IsAny<Guid>(), It.IsAny<PeriodDateTime>());

            MapperDouble.Setup(mp => mp.Map<CollaboratorDTO>(collab1.Object)).Returns(dto1);
            MapperDouble.Setup(mp => mp.Map<CollaboratorDTO>(collab2.Object)).Returns(dto2);
            MapperDouble.Setup(mp => mp.Map<CollaboratorDTO>(collab3.Object)).Returns(dto3);

            IEnumerable<CollaboratorDTO> expectedResult = new List<CollaboratorDTO> { dto1, dto2, dto3 };

            // Act
            var result = await CollaboratorService.FindAllWithHolidayPeriodsLongerThan(It.IsAny<int>());

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.True(result.Value.SequenceEqual(expectedResult));
        }

        [Fact]
        public async Task FindAllWithHolidayPeriodsLongerThan_WhenCollaboratorsDontHavePeriosLongerThan_ReturnsEmptyList()
        {
            // arrange
            var holidayPlans = new List<HolidayPlan> { };

            HolidayPlanRepositoryDouble.Setup(repo => repo.FindAllWithHolidayPeriodsLongerThanAsync(It.IsAny<int>())).ReturnsAsync(holidayPlans);

            var collabIdsList = new List<Guid>();
            var expectedList = new List<Collaborator>();

            CollaboratorRepositoryDouble.Setup(repo => repo.GetByIdsAsync(collabIdsList)).ReturnsAsync(expectedList);

            // act
            var result = await CollaboratorService.FindAllWithHolidayPeriodsLongerThan(It.IsAny<int>());

            // assert
            Assert.Empty(result.Value);
        }
    }
}