using System;

namespace Hotel.Core.Business
{
    public class BillModel
    {
        public ReservationModel Reservation { get; set; }
        public decimal Fees { get; set; }
        public double Stay { get; set; }
        /// <summary>In hours rounded up to the next hour</summary>
        public double Overstay { get; set; }
        public decimal OverstayRate { get; set; }
        public decimal OverstayFees { get; set; }
        public BillModel() { }

        public BillModel(ReservationModel reservation)
        {
            if (reservation != null)
            {
                Stay = (reservation.ExpectedCheckout - reservation.ExpectedCheckin).TotalHours;
                Fees = Convert.ToDecimal(Stay) * reservation.HourlyRate;
            }
        }

        public BillModel(ReservationModel reservation, decimal overstayRate, double overstay) : this(reservation)
        {
            if (reservation != null)
            {
                Reservation = reservation;
                OverstayRate = overstayRate;
                Overstay = overstay;
                OverstayFees = Convert.ToDecimal(overstay) * (reservation.HourlyRate * overstayRate / 100);
            }
        }
    }

}