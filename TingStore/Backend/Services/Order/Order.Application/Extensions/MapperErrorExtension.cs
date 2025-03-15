using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Extensions
{
    public class MapperErrorExtension : Exception
    {
        public MapperErrorExtension(object from, object to)
       : base($"Error: Unable to map from {from} to {to}.")
        {
        }
        public MapperErrorExtension() : base("Error: Mapping failed.")
        {
        }
    }
}

