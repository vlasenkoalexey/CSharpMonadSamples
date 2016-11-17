using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monads
{
    class MaybeCommonWaySample
    {
        public class User
        {
            public string UserName { get; set; }
            public Order UserOrder { get; set; }
        }

        public class Order
        {
            public OrderTracking Tracking { get; set; }
        }

        public class OrderTracking
        {
            public string TrackingNumber { get; set; }
            public string Carrier { get; set; }
        }

        public string GetTrackingNumber(User user)
        {
            if (user != null)
            {
                var order = user.UserOrder;
                if (order != null)
                {
                    var tracking = order.Tracking;
                    if (tracking != null)
                    {
                        return tracking.TrackingNumber;
                    }
                }
            }

            return null;
        }

        public string GetTrackingNumberShort(User user)
        {
            return user?.UserOrder?.Tracking?.TrackingNumber;
        }
    }
}
