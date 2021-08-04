using System;
using System.Threading.Tasks;
using Hotel.Domain.Enums;

namespace Hotel.Core.Managers
{
    public interface ICheckoutManager
    {
        /// <summary>Determine the overstay rate for a room of specific type on a weekend or not. Throws InvalidArgumentException</summary>
        Task<decimal> GetOverstayRate(RoomType roomType, bool isWeekend);

        /// <summary>Determine the hourly overstay fee.</summary>
        Task<decimal> GetOverstayFee(decimal overstayRate, decimal hourlyRate);
        
    }    
}