using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryPattern.Filters.Base
{
    /// <summary>
    /// This is the filter interface, which is used to select records from the database.
    /// The filter can be converted into any expression for a particular database.
    /// 
    /// For the sake of simplicity the return type will be Predicate<T> 
    /// (as the only database we work with in this example is a List-based database),
    /// but it can be any other type for a concrete database 
    /// (e.g. FilterDefinition in mongoDB C# driver).
    /// </summary>
    interface IFilter
    {
        Predicate<T> ToListFilter<T>(); 
    }
}
