using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Hotel.Core.Managers;
using Hotel.Domain.Enums;

namespace Tests
{
    public class CheckoutTests
    {
        [Fact]
        public async Task GetRegularWeekendRateReturns85()
        {
            // Arrange            
            var log = new Mock<ILogger<CheckoutManager>>();
            var checkoutManager = new CheckoutManager(log.Object);

            // Act
            // Get the overstay rate for a delux room on a weekend
            var rate = await checkoutManager.GetOverstayRate(RoomType.Regular, true);
            
            // Assert
            Assert.Equal(rate.ToString(), (7.0m/100).ToString());

        }
        [Fact]
        public async Task GetDeluxWeekendRateReturns85()
        {
            // Arrange            
            var log = new Mock<ILogger<CheckoutManager>>();
            var checkoutManager = new CheckoutManager(log.Object);

            // Act
            // Get the overstay rate for a delux room on a weekend
            var rate = await checkoutManager.GetOverstayRate(RoomType.Delux, true);
            
            // Assert
            Assert.Equal(rate.ToString(), (8.5m/100).ToString());

        }
        [Fact]
        public async Task GetPalatialWeekendRateReturns85()
        {
            // Arrange            
            var log = new Mock<ILogger<CheckoutManager>>();
            var checkoutManager = new CheckoutManager(log.Object);

            // Act
            // Get the overstay rate for a delux room on a weekend
            var rate = await checkoutManager.GetOverstayRate(RoomType.Palatial, true);
            
            // Assert
            Assert.Equal(rate.ToString(), (11.0/100).ToString());

        }
    }
}
