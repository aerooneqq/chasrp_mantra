using System;
using System.Collections.Generic;
using System.Text;

namespace MementoMoriPattern
{
    class OriginatorMemento : IMemento
    {
        public DateTime Date { get; }
        public string Name { get; }

        #region Constructors
        public State State { get; }

        public OriginatorMemento(string name, State state)
        {
            Name = name;
            State = state;

            Date = DateTime.Now;
        }
        #endregion

        public override string ToString() => $"Name: {Name}, Date: {Date.ToShortTimeString()}";
    }
}
