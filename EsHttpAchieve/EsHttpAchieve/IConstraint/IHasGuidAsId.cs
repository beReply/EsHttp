using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsHttpAchieve.IConstraint
{
    public interface IHasGuidAsId
    {
        public Guid Id { get; set; }
    }
}
