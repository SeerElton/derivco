using System;
using System.Collections.Generic;
using System.Text;

namespace derivco.Data.Models
{
    public class NamedEntityBase : EntityBase
    {
        public string Name { get; set; }

        public NamedEntityBase()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
