using System;
using System.Collections.Generic;
using System.Text;

namespace derivco.Data.Models
{
    public class Bet : EntityBase
    {
        public Guid PlayerId { get; set; }

        public Guid SessionId { get; set; }

        public DateTime CreatedAt { get; set; }

        public BetPosition Position { get; set; }
    }

    public class BetPosition
    {
        public int Number { get; set; }

        public PositionColor Color { get; set; }
    }

    public enum PositionColor { 
        black,
        red
    }
}
