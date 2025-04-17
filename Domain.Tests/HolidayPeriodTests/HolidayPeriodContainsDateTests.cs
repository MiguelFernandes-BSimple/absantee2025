using Domain.Models;
using Moq;
using Domain.Interfaces;
namespace Domain.Tests.HolidayPeriodTests;

public class HolidayPeriodContainsDateTests
{
    /**
    * Test to verify if date is contained in the holiday period
    * Its contained - true
    */
    
    [Fact]
    public void WhenPeriodIsFullyContained_ThenReturnsTrue()
    {
        // Arrange
        DateOnly dateToVerify = DateOnly.FromDateTime(DateTime.Now);

        // Criar uma subclasse concreta de PeriodDate para o teste
        var start = dateToVerify.AddDays(-1);
        var end = dateToVerify.AddDays(1);
        var period = new PeriodDate(start, end);

        // Instanciar HolidayPeriod com esse período
        var hPeriod = new HolidayPeriod(period);

        // Act
        bool result = hPeriod.ContainsDate(dateToVerify);

        // Assert
        Assert.True(result);
    }


    /**
    * Test to verify if date is contained in the holiday period
    * It's not contained - False
    */
    [Fact]
    public void WhenPeriodIsNotContained_ThenReturnsFalse()
    {
        // Arrange
        DateOnly dateToVerify = DateOnly.FromDateTime(DateTime.Now);

        // Criar um intervalo que NÃO contém a data
        // Por exemplo, um intervalo no passado
        var start = dateToVerify.AddDays(-10);
        var end = dateToVerify.AddDays(-5);
        var period = new PeriodDate(start, end);

        // Instanciar HolidayPeriod com esse período
        var hPeriod = new HolidayPeriod(period);

        // Act
        bool result = hPeriod.ContainsDate(dateToVerify);

        // Assert
        Assert.False(result);
    }

}