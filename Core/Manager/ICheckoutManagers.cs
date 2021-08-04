using System;
using System.Threading.Tasks;
using Hotel.Domain.Enums;
using Hotel.Core.Business;
using System.Collections.Generic;

namespace Hotel.Core.Managers
{
    public interface ICheckoutManager
    {
        /// <summary>Determine the overstay rate for a room of specific type on a weekend.</summary>
        Task<decimal> GetOverstayRate(long customerId, RoomType roomType);

        /// <summary>Determine the hourly overstay fee.</summary>
        Task<IEnumerable<BillModel>> GetOverstayFee(long customerId);

    }
}