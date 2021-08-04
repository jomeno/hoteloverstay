using System;
using Hotel.Domain.Enums;

namespace Hotel.Core.Business
{
    public class ReservationModel
    {
        public long Id { get; set; }
        public RoomType RoomType { get; set; }
        public long CustomerId { get; set; }
        public decimal HourlyRate { get; set; }
        public ReservationStatus Status { get; set; }
        public DateTimeOffset ExpectedCheckin { get; set; }
        public DateTimeOffset ExpectedCheckout { get; set; }
    }

}