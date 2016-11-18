using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monads
{
    class CombinedAsyncSample
    {
        public class User
        {
            public string UserName { get; set; }
            public async Task<Order> GetUserOrderAsync()
            {
                return await Task<Order>.Factory.StartNew(() => new Order());
            }
        }

        public class Order
        {
            public OrderTracking Tracking { get; set; }
            public async Task<OrderTracking> GetTrackingAsync()
            {
                return await Task<OrderTracking>.Factory.StartNew(() => new OrderTracking());
            }
        }

        public class OrderTracking
        {
            public string TrackingNumber { get; set; }
            public string Carrier { get; set; }
        }

        public async Task<string> GetTrackingNumberAsync(User user)
        {
            if (user != null)
            {
                var order = await user.GetUserOrderAsync();
                if (order != null)
                {
                    var tracking = await order.GetTrackingAsync();
                    if (tracking != null)
                    {
                        return tracking.TrackingNumber;
                    }
                }
            }

            return null;
        }
    }
}
