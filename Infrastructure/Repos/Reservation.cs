using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Hotel.Core.Repos;
using Hotel.Core.Business;
using Hotel.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace Hotel.Infrastructure.Repos
{
    public class Reservation : IReservation
    {
        private readonly ILogger<Reservation> _log;
        public Reservation(ILogger<Reservation> log)
        {
            _log = log;
        }

        public async Task<IEnumerable<RateModel>> GetRates()
        {
            return await Task.Run(() =>
            {
                var rates = new List<RateModel>(){
                    new RateModel(){ RoomType = RoomType.Regular, WeekdayRateMarkup = 7, WeekendRateMarkup = 10},
                    new RateModel(){ RoomType = RoomType.Deluxe, WeekdayRateMarkup = 8.5m, WeekendRateMarkup = 12},
                    new RateModel(){ RoomType = RoomType.Palatial, WeekdayRateMarkup = 11, WeekendRateMarkup = 16}
                };
                return rates;
            });
        }

        public async Task<IEnumerable<ReservationModel>> GetReservations()
        {
            return await Task.Run(() =>
            {
                var reservations = new List<ReservationModel>(){
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
                return reservations;
            });

        }

    }

}