using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Extensions
{
    public class OrderNotFoundExtension : Exception
    {
        public OrderNotFoundExtension(string name , object key) : base($"Entity {name} - {key} is not find")
        {
        }
    }
}
