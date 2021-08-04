using System;

namespace Hotel.Core.Business
{
    public class BillModel
    {
        public ReservationModel Reservation { get; set; }
        public decimal Fees { get; set; }
        public double Stay { get; set; }
        /// <summary>In hours rounded up to the next hour</summary>
        public decimal Overstay { get; set; }
        public decimal OverstayRate { get; set; }
        public decimal OverstayFees { get; set; }
        public BillModel() { }

        public BillModel(ReservationModel reservation)
        {
            if (reservation != null)
            {
                Stay = (reservation.ExpectedCheckin - reservation.ExpectedCheckout).TotalHours;
                Fees = Convert.ToDecimal(Stay) * reservation.HourlyRate;
            }
        }

        public BillModel(ReservationModel reservation, decimal overstayRate, decimal overstay) : this(reservation)
        {
            if (reservation != null)
            {
                Reservation = reservation;
                OverstayRate = overstayRate;
                OverstayFees = overstay * (reservation.HourlyRate * overstayRate / 100);
            }
        }
    }

}