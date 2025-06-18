using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YemekhaneApp.Domain.BaseEntities
{
    public class EntityBase
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
