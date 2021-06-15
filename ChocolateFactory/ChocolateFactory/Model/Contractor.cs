using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocolateFactory.Model
{
    public class Contractor
    {
        public Guid ContractorId { get; set; }
        public string Name { get; set; }
        public string Nip { get; set; }
        public Address Address { get; set; }
    }
}
