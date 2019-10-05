using System;

namespace MementoMoriPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Originator originator = new Originator(new State("Initial state", 20));

            Console.WriteLine("Originator's states (we add 4 states and save first 3 of them in the caretaker): ");

            originator.PrintState();
            Caretaker caretaker = new Caretaker(originator);

            caretaker.Save();

            originator.ChangeState(new State("Second state", 21));
            originator.PrintState();

            caretaker.Save();

            originator.ChangeState(new State("Third state", 22));
            originator.PrintState();

            caretaker.Save();

            originator.ChangeState(new State("Fourth state", 22));
            originator.PrintState();

            Console.WriteLine();

            PrintMementoes(caretaker);

            Console.WriteLine("UNDO THE LAST CHANGE");
            caretaker.Undo();

            Console.WriteLine("Mementoes after undo: ");
            PrintMementoes(caretaker);

            Console.WriteLine();

            Console.WriteLine("Originator's state after undo: ");
            originator.PrintState();

            Console.WriteLine();
            Console.WriteLine("Press any key to continue");
            Console.ReadKey(true);
        }

        static void PrintMementoes(Caretaker caretaker)
        {
            Console.WriteLine("Mementoes: ");
            caretaker.PrintMementoes();
        }
    }
}
