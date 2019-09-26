using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryPattern
{
    /// <summary>
    /// This is the filter interface, which is used to select records from the database.
    /// The filter can be converted into any expression for a particular database.
    /// 
    /// For the sake of simplicity every return type will be string, but it can be any other filter
    /// type for a concrete database (e.g. FilterDefinition in mongoDB C# driver).
    /// </summary>
    interface IFilter
    {
        Predicate<T> ToListFilter<T>(); 
    }
}
