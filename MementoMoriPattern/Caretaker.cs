using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MementoMoriPattern
{
    /// <summary>
    /// This is the caretaker which stores all snapshots of the origiantor
    /// </summary>
    public class Caretaker
    {
        private readonly List<IMemento> mementoes;
        private int index;

        private readonly Originator originator;

        public Caretaker(Originator originator)
        {
            this.originator = originator;
            mementoes = new List<IMemento>();
            index = 0;
        }

        /// <summary>
        /// Saves the state of the origiantor
        /// </summary>
        public void Save()
        {
            mementoes.Add(originator.GetMemento());
            index++;
        }

        /// <summary>
        /// Undoes the last applied change
        /// </summary>
        /// <exception cref="IndexOutOfRangeException">
        /// If there are no mementoes in the caretaker
        /// </exception>"
        public void Undo()
        {
            if (index - 1 < 0)
            {
                throw new IndexOutOfRangeException("There are no mementoes in the caretaker");
            }

            index--;
            originator.RestoreMemento(mementoes[index]);
        }

        public void PrintMementoes()
        {
            foreach (IMemento memento in mementoes)
            {
                Console.WriteLine(memento);
            }
        }
    }
}
