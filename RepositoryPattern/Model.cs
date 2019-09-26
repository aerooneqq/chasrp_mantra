using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryPattern
{
    class Model : UniqueEntity
    {
        public string Name { get; set; }

        public Model() { }
        public Model(string name)
        {
            Name = name;
        }
    }
}
