using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryPattern
{
    /// <summary>
    /// Test model
    /// </summary>
    class Model : UniqueEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }

        #region Constructors
        public Model() { }

        public Model(string name, int age)
        {
            Name = name;
            Age = age;
        }
        #endregion

        public override string ToString()
        {
            return $"{Name}, {Age}";
        }
    }
}
