using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Events
{
    public class UpdateOrderStatusEvent
    {
        public string OrderId { get; set; }
        public string Status { get; set; }
    }
}
