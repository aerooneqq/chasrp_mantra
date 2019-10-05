using System;
using System.Collections.Generic;
using System.Text;

namespace MementoMoriPattern
{
    /// <summary>
    /// This interface is used to save the snapshots of the class
    /// </summary>
    public interface IMemento
    {
        State State { get; }

        string Name { get; }
        DateTime Date { get; }
    }
}
