using System;
using System.Collections.Generic;
using System.Text;

namespace MementoMoriPattern
{
    /// <summary>
    /// This class is the main class in this pattern. It contains the important state which can
    /// be changed. We will create snapshots of the state of this class.
    /// </summary>
    public class Originator
    {
        private State state;

        public Originator(State state)
        {
            this.state = state;
        }

        public void ChangeState(State state)
        {
            this.state = new State(state);
        }

        public void PrintState()
        {
            Console.WriteLine(state);
        }

        /// <summary>
        /// Creates the snapshot of this object state.
        /// </summary>
        public IMemento GetMemento()
        {
            return new OriginatorMemento(state.Name, state);
        }

        /// <summary>
        /// Restores the state of the pbject with the given snapshot
        /// </summary>
        public void RestoreMemento(IMemento memento)
        {
            state = new State(memento.State);
        }
    }
}
