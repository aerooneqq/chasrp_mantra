using System;
using System.Collections.Generic;
using System.Text;

namespace MementoMoriPattern
{
    /// <summary>
    /// This is the state of a very important class in our system
    /// (Originator)
    /// </summary>
    public class State
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public State(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public State(State state)
        {
            Name = state.Name;
            Age = state.Age;
        }

        public override string ToString() => $"State. Name: {Name}, Age: {Age}";
    }
}
