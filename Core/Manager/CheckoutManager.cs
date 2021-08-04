using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Hotel.Domain.Enums;
using Hotel.Core.Repos;
using Hotel.Core.Business;
using System.Collections.Generic;

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

        public async Task<IEnumerable<BillModel>> GetOverstayFee(long customerId)
        {
            var reservations = await _repo.GetReservations();
            var rates = await _repo.GetRates();
            var reserveRates = (from res in reservations
                                where res.CustomerId == customerId
                                join rat in rates on res.RoomType equals rat.RoomType
                                select new { Reservation = res, Rate = rat }).ToList();

            var billModels = reserveRates.Select(reserveRate =>
            {
                // Determine if ExpectedCheckout date is a weekend
                var reservation = reserveRate.Reservation;
                var rate = reserveRate.Rate;
                var isWeekend = false;
                if (reservation.ExpectedCheckout.DayOfWeek == DayOfWeek.Saturday || reservation.ExpectedCheckout.DayOfWeek == DayOfWeek.Sunday) isWeekend = true;
                var overstay = (DateTimeOffset.Now - reservation.ExpectedCheckout).TotalHours;
                // Over stay is due at the begining of the hour
                overstay = Math.Ceiling(overstay);
                var overstayRate = isWeekend ? rate.WeekendRateMarkup : rate.WeekdayRateMarkup;

                return new BillModel(reservation, overstayRate, overstay);

            }).ToList();

            return billModels;


        }
    }
}