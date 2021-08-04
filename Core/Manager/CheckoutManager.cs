using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Hotel.Domain.Enums;

namespace Hotel.Core.Managers
{
    public class CheckoutManager : ICheckoutManager{
        private readonly ILogger<CheckoutManager> _log;
        public CheckoutManager(ILogger<CheckoutManager> log){
            _log = log;
        }

        public async Task<decimal> GetOverstayRate(RoomType roomType, bool isWeekend = false){
            return await Task.Run(()=>{
                if(RoomType.Regular == roomType) return 7.0m/100;
                if(RoomType.Delux == roomType) return 8.5m/100;
                if(RoomType.Palatial == roomType) return 11.0m/100;
                throw new ArgumentException();
            });
        }

        public async Task<decimal> GetOverstayFee(decimal overstayRate, decimal hourlyRate){
            return await Task.Run(()=>{
                return 2.5m;
            });
        }
    }
}