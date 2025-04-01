using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.HRManagerTests
{
    public class Constructor
    {
        public static IEnumerable<object[]> ValidData()
        {
            yield return new object[] { DateTime.Now, DateTime.Now.AddDays(10) };
            yield return new object[] { DateTime.Now.AddDays(10), DateTime.Now.AddDays(20) };
            yield return new object[] { DateTime.Now.AddYears(1), DateTime.Now.AddYears(2) };
            yield return new object[] { DateTime.Now, null! };
            yield return new object[] { DateTime.Now, DateTime.Now };
            yield return new object[] { DateTime.Now, DateTime.MaxValue };
            yield return new object[] { DateTime.MinValue, DateTime.Now };
            yield return new object[] { DateTime.MinValue, DateTime.MaxValue };
        }

        [Theory]
        [MemberData(nameof(ValidData))]
        public void WhenCreatingHRManagerWithValidData_ThenShouldBeInstantiated(DateTime dataInicio, DateTime? dataFim)
        {
            //arrange
            Mock<IUser> user = new Mock<IUser>();
            user.Setup(u => u.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(false);
            user.Setup(u => u.IsDeactivated()).Returns(false);

            //act
            new HRManager(user.Object, dataInicio, dataFim);

            //assert
        }

        public static IEnumerable<object[]> InvalidData()
        {
            yield return new object[] { DateTime.Now, DateTime.Now.AddDays(-10) };
            yield return new object[] { DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-20) };
            yield return new object[] { DateTime.Now.AddYears(-1), DateTime.Now.AddYears(-2) };
            yield return new object[] { DateTime.MaxValue, DateTime.MinValue };
            yield return new object[] { DateTime.MinValue, DateTime.MinValue };
            yield return new object[] { DateTime.MaxValue, DateTime.MaxValue };
            yield return new object[] { DateTime.MaxValue, null! };
        }

        [Theory]
        [MemberData(nameof(InvalidData))]
        public void WhenCreatingHRManagerWithInvalidData_ThenThrowsException(DateTime dataInicio, DateTime dataFim)
        {
            //arrange
            Mock<IUser> user = new Mock<IUser>();
            user.Setup(u => u.DeactivationDateIsBefore(It.IsAny<DateTime>())).Returns(false);
            user.Setup(u => u.IsDeactivated()).Returns(false);

            //assert
            ArgumentException exception = Assert.Throws<ArgumentException>(() =>
                //act
                new HRManager(user.Object, dataInicio, dataFim));

            Assert.Equal("Invalid Arguments", exception.Message);
        }

        [Fact]
        public void WhenCreatingHRManagerWithEndDateAfterDeactivationData_ThenThrowsException()
        {
            //arrange
            DateTime dataInicio = DateTime.Now;
            DateTime dataFim = DateTime.Now.AddDays(10);

            Mock<IUser> user = new Mock<IUser>();
            user.Setup(u => u.DeactivationDateIsBefore(dataFim)).Returns(true);
            user.Setup(u => u.IsDeactivated()).Returns(false);

            //assert
            ArgumentException exception = Assert.Throws<ArgumentException>(() =>
                //act
                new HRManager(user.Object, dataInicio, dataFim));

            Assert.Equal("Invalid Arguments", exception.Message);
        }

        [Fact]
        public void WhenCreatingHRManagerWithInactiveUser_ThenThrowsException()
        {
            //arrange
            DateTime dataInicio = DateTime.Now;
            DateTime dataFim = DateTime.Now.AddDays(10);

            Mock<IUser> user = new Mock<IUser>();
            user.Setup(u => u.DeactivationDateIsBefore(dataFim)).Returns(false);
            user.Setup(u => u.IsDeactivated()).Returns(true);

            //assert
            ArgumentException exception = Assert.Throws<ArgumentException>(() =>
                //act
                new HRManager(user.Object, dataInicio, dataFim));

            Assert.Equal("Invalid Arguments", exception.Message);
        }

        [Fact]
        public void WhenCreatingHRManagerWithEndDateAfterDeactivationDataAndInactiveUser_ThenThrowsException()
        {
            //arrange
            DateTime dataInicio = DateTime.Now;
            DateTime dataFim = DateTime.Now.AddDays(10);

            Mock<IUser> user = new Mock<IUser>();
            user.Setup(u => u.DeactivationDateIsBefore(dataFim)).Returns(true);
            user.Setup(u => u.IsDeactivated()).Returns(true);

            //assert
            ArgumentException exception = Assert.Throws<ArgumentException>(() =>
                //act
                new HRManager(user.Object, dataInicio, dataFim));

            Assert.Equal("Invalid Arguments", exception.Message);
        }
    }
}