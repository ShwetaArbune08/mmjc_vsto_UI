using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sundeus.models
{
   public class ContractType:Base
    {
        public int ContractTypeId { get;  set; }
        public string Name { get;  set; }
        public string Description { get;  set; }
        public string Status { get;  set; }
        public double Version { get;  set; }
        public int ContractTypeStatusId { get; set; }
        public List<Attributes> Attributes { get;  set; }
    }
}
