using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hotel.Core.Business;


namespace Hotel.Core.Repos
{
    public interface IReservation
    {
        /// <summary>Get booked reservations.</summary>
        Task<IEnumerable<ReservationModel>> GetReservations();

        /// <summary>Get all rates.</summary>
        Task<IEnumerable<RateModel>> GetRates();
    }

}