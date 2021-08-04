using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Hotel.Core.Managers;
using Hotel.Domain.Enums;
using Hotel.Core.Repos;
using System.Collections.Generic;
using Hotel.Core.Business;

namespace Tests
{
    public class CheckoutTests
    {
        private IEnumerable<RateModel> _rates;
        private IEnumerable<ReservationModel> _reservations;
        public CheckoutTests()
        {
            _reservations = new List<ReservationModel>(){
                    new ReservationModel(){
                        Id = 12000,
                        RoomType = RoomType.Deluxe,
                        CustomerId = 12323,
                        HourlyRate = 230000,
                        Status = ReservationStatus.Paid,
                        ExpectedCheckin = new DateTime(2020, 12, 12, 12, 0, 0),
                        ExpectedCheckout = new DateTime(2021, 01, 01, 11, 0, 0)
                    },
                    new ReservationModel(){
                        Id = 12001,
                        RoomType = RoomType.Regular,
                        CustomerId = 12324,
                        HourlyRate = 150000,
                        Status = ReservationStatus.Paid,
                        ExpectedCheckin = new DateTime(2020, 12, 12, 12, 0, 0),
                        ExpectedCheckout = new DateTime(2021, 01, 01, 11, 0, 0)
                    },
                    new ReservationModel(){
                        Id = 12002,
                        RoomType = RoomType.Palatial,
                        CustomerId = 12100,
                        HourlyRate = 560000,
                        Status = ReservationStatus.Paid,
                        ExpectedCheckin = new DateTime(2020, 12, 12, 12, 0, 0),
                        ExpectedCheckout = new DateTime(2021, 01, 01, 11, 0, 0)
                    },
                    new ReservationModel(){
                        Id = 12003,
                        RoomType = RoomType.Regular,
                        CustomerId = 12323,
                        HourlyRate = 200000,
                        Status = ReservationStatus.Paid,
                        ExpectedCheckin = new DateTime(2020, 12, 25, 12, 0, 0),
                        ExpectedCheckout = new DateTime(2021, 01, 04, 11, 0, 0)
                    }
                };

            _rates = new List<RateModel>(){
                new RateModel(){ RoomType = RoomType.Regular, WeekdayRateMarkup = 7, WeekendRateMarkup = 10},
                new RateModel(){ RoomType = RoomType.Deluxe, WeekdayRateMarkup = 8.5m, WeekendRateMarkup = 12},
                new RateModel(){ RoomType = RoomType.Palatial, WeekdayRateMarkup = 11, WeekendRateMarkup = 16m}
            };
        }

        [Fact]
        public async Task GetRegularWeekendRateReturnsDecimal()
        {
            // Arrange            
            var log = new Mock<ILogger<CheckoutManager>>();
            var repo = new Mock<IReservation>();
            repo.Setup(r => r.GetRates()).ReturnsAsync(_rates);
            repo.Setup(r => r.GetReservations()).ReturnsAsync(_reservations);
            var checkoutManager = new CheckoutManager(log.Object, repo.Object);

            // Act
            // Get the overstay rate for a regular room on a weekend
            var rate = await checkoutManager.GetOverstayRate(12324, RoomType.Regular);

            // Assert
            Assert.Equal(7m, rate);

        }
        [Fact]
        public async Task GetDeluxWeekendRateReturnsDecimal()
        {
            // Arrange            
            var log = new Mock<ILogger<CheckoutManager>>();
            var repo = new Mock<IReservation>();
            repo.Setup(r => r.GetRates()).ReturnsAsync(_rates);
            repo.Setup(r => r.GetReservations()).ReturnsAsync(_reservations);
            var checkoutManager = new CheckoutManager(log.Object, repo.Object);

            // Act
            // Get the overstay rate for a delux room on a weekend
            var rate = await checkoutManager.GetOverstayRate(12323, RoomType.Deluxe);

            // Assert
            Assert.Equal(8.5m, rate);

        }
        [Fact]
        public async Task GetPalatialWeekendRateReturnsDecimal()
        {
            // Arrange            
            var log = new Mock<ILogger<CheckoutManager>>();
            var repo = new Mock<IReservation>();
            repo.Setup(r => r.GetRates()).ReturnsAsync(_rates);
            repo.Setup(r => r.GetReservations()).ReturnsAsync(_reservations);
            var checkoutManager = new CheckoutManager(log.Object, repo.Object);

            // Act
            // Get the overstay rate for a palatial room on a weekend
            var rate = await checkoutManager.GetOverstayRate(12100, RoomType.Palatial);

            // Assert
            Assert.Equal(11.0m, rate);

        }
    }
}
