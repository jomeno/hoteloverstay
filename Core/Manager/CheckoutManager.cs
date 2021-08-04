using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Hotel.Domain.Enums;
using Hotel.Core.Repos;
using Hotel.Core.Business;

namespace Hotel.Core.Managers
{
    public class CheckoutManager : ICheckoutManager
    {
        private readonly ILogger<CheckoutManager> _log;
        private readonly IReservation _repo;
        public CheckoutManager(ILogger<CheckoutManager> log, IReservation repo)
        {
            _log = log;
            _repo = repo;
        }

        public async Task<decimal> GetOverstayRate(long customerId, RoomType roomType)
        {
            var reservations = await _repo.GetReservations();
            var rates = await _repo.GetRates();
            var reserveRate = (from res in reservations
                               where res.CustomerId == customerId && res.RoomType == roomType
                               join rat in rates on res.RoomType equals rat.RoomType
                               select new { Reservation = res, Rate = rat }).FirstOrDefault();

            if (reserveRate == null) return 1;

            // Determine if ExpectedCheckout date is a weekend
            var reservation = reserveRate.Reservation;
            var rate = reserveRate.Rate;
            var isWeekend = false;
            if (reservation.ExpectedCheckout.DayOfWeek == DayOfWeek.Saturday || reservation.ExpectedCheckout.DayOfWeek == DayOfWeek.Sunday) isWeekend = true;

            return isWeekend ? rate.WeekendRateMarkup : rate.WeekdayRateMarkup;
        }

        public async Task<decimal> GetOverstayFee(decimal overstayRate, decimal hourlyRate)
        {
            return await Task.Run(() =>
            {
                return 2.5m;
            });
        }
    }
}