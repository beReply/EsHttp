using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EsHttpAchieve.IConstraint;

namespace EsHttpAchieve.Entities
{
    public class TestEntity : IHasGuidAsId
    {
        public Guid Id { get; set; }

        public string Message { get; set; }

        public DateTime CreateTime { get; set; }

        public string Remark { get; set; }

    }
}
