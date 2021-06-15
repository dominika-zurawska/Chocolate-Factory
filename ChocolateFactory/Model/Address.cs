using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocolateFactory.Model
{
    public class Address
    {
        public Guid AddressId { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string HouseNumber { get; set; }
    }
}
