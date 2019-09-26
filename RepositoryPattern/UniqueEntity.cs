using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryPattern
{
    class UniqueEntity
    {
        public long ID { get; set; }

        public UniqueEntity() { }
        public UniqueEntity(long id)
        {
            ID = id;
        }
    }
}
