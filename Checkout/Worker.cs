using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hotel.Core.Business;
using Hotel.Core.Managers;
using Hotel.Core.Repos;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Checkout
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _log;
        private readonly ICheckoutManager _checkout;
        private readonly IReservation _repo;

        public Worker(ILogger<Worker> log, ICheckoutManager checkout, IReservation repo)
        {
            _log = log;
            _checkout = checkout;
            _repo = repo;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var reservations = await _repo.GetReservations();
                _log.LogInformation("\nCheckout service is running at: {time}\n", DateTimeOffset.Now);
                foreach (var reservation in reservations)
                {
                    var result = await _checkout.GetOverstayFee(reservation.CustomerId);
                    var firstResult = result.FirstOrDefault();
                    _log.LogInformation($"CustomerId: {reservation.CustomerId} | Overstay Fee NGN: {(firstResult.OverstayFees / 100).ToString("N0")}\n");
                }

                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
