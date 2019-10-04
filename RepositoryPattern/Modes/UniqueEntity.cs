using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryPattern
{
    /// <summary>
    /// The base class for all unique entities.
    /// </summary>
    class UniqueEntity
    {
        public long ID { get; set; }

        #region Constructors
        public UniqueEntity() { }

        public UniqueEntity(long id)
        {
            ID = id;
        }
        #endregion
    }
}
