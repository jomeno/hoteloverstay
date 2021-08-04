using System;
using Hotel.Domain.Enums;

namespace Hotel.Core.Business
{
    public class RateModel{
        public RoomType RoomType { get; set; }
        public decimal WeekdayRateMarkup { get; set; }
        public decimal WeekendRateMarkup { get; set; }
    }
    
}