using Monads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monads
{
    public class MaybeSample
    {
        public class User
        {
            public string UserName { get; set; }
            public Maybe<Order> UserOrder { get; set; }
        }

        public class Order
        {
            public Maybe<OrderTracking> Tracking { get; set; }
        }

        public class OrderTracking
        {
            public Maybe<string> TrackingNumber { get; set; }
            public string Carrier { get; set; }
        }

        public Maybe<string> GetTrackingNumber(User user)
        {
            return user.UserOrder.Bind<Order, OrderTracking>((order) => order.Tracking).Bind<OrderTracking, String>((tracking) => tracking.TrackingNumber);
        }

    }
}
