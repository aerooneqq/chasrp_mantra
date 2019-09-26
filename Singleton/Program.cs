using System;
using System.Collections.Generic;

namespace Singleton
{
    class Program
    {
        private static string ObjPropertName => "Obj";
        private static string ObjCountPropertyName => "ObjectsCount";
        private static IEnumerable<Type> SingletonTypes => new[]
        {
            typeof(DummySingleton),
            typeof(ThreadSafeSingleton),
            typeof(PreinitializedThreadSafeSingleton),
            typeof(NestedLazySingleton),
            typeof(TrueLazySingleton)
        };

        static void Main(string[] args)
        {
            TestSingletons();
        }

        static void TestSingletons()
        {
            foreach (Type singletonType in SingletonTypes)
            {
                dynamic firstObj = singletonType.GetProperty("Obj").GetValue(null);
                dynamic secondObj = singletonType.GetProperty("Obj").GetValue(null);

                Console.WriteLine($"First obj name = {firstObj.Name}, second obj name = {secondObj.Name}");
                Console.WriteLine($"{singletonType.Name} objects count = {singletonType.GetProperty(ObjCountPropertyName).GetValue(null)}");
            }
        }
    }
}
