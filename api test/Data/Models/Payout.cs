using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace derivco.Data.Models
{
    public class Payout: EntityBase
    {
        public Player Player { set; get; }

        public Session Session { set; get; }

        public double Paylout { set; get; }
    }
}
