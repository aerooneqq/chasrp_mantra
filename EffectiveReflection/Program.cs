using System;
using System.Reflection;
using Infrastructure;

namespace EffectiveReflection
{
    class TestClass
    {
        public static string Name { get; set; } = "John";
        public int Age { get; set; } = 2019;
        public int Year { get; set; } = 2002;
        private int Password { get; set; }  = 3201;
    }
    class Program
    {
        /*
           Sample output:

           GET method tests:
           Property: AGE
           Improved reflection: 00:00:00.0034034
           Default reflection: 00:00:00.0351398

           Property: YEAR
           Improved reflection: 00:00:00.0019219
           Default reflection: 00:00:00.0341300

           Property: PASSWORD
           Improved reflection: 00:00:00.0021932
           Default reflection: 00:00:00.0341035

           SET method tests:
           Property: AGE
           Improved reflection: 00:00:00.0113967
           Default reflection: 00:00:00.0899024

           Property: YEAR
           Improved reflection: 00:00:00.0231877
           Default reflection: 00:00:00.0761624

           Property: PASSWORD
           Improved reflection: 00:00:00.0088557
           Default reflection: 00:00:00.0485557
         */
        static void Main(string[] args)
        {
            Console.WriteLine("GET method tests: ");
            CompareGetPropValueTime();

            Console.WriteLine("SET method tests: ");
            CompareSetPropValueTime();
        }

        static void CompareGetPropValueTime()
        {
            string[] propNames = { "Age", "Year", "Password" };
            Type type = typeof(TestClass);
            TestClass testObj = new TestClass();

            foreach (string propName in propNames)
            {
                Console.WriteLine($"Property: {propName.ToUpper()}");

                GetPropValueDel getPropValueDel = type.GetGetPropValueDel(propName);

                TimeSpan takenTime = Time.MeasureExecutionTime(() =>
                {
                    for (int i = 0; i < 99999; i++)
                    {
                        var res = getPropValueDel(testObj);
                    }
                });

                Console.WriteLine($"Improved reflection: {takenTime}");

                var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance;
                takenTime = Time.MeasureExecutionTime(() =>
                {
                    for (int i = 0; i < 99999; i++)
                    {
                        var res = type.GetProperty(propName, bindingFlags).GetMethod.Invoke(testObj, Array.Empty<object>());
                    }
                });

                Console.WriteLine($"Default reflection: {takenTime}");
                Console.WriteLine();
            }
        }

        static void CompareSetPropValueTime()
        {
            string[] propNames = { "Age", "Year", "Password" };
            Type type = typeof(TestClass);
            TestClass testObj = new TestClass(); 

            foreach (string propName in propNames)
            {
                Console.WriteLine($"Property: {propName.ToUpper()}");

                SetPropertyValueDel setDelegate = type.GetSetPropValueDel(propName);

                TimeSpan takenTime = Time.MeasureExecutionTime(() =>
                {
                    for (int i = 0; i < 99999; i++)
                    {
                        setDelegate(testObj, type.GetDefaultValue());
                    }
                });

                Console.WriteLine($"Improved reflection: {takenTime}");

                var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance;
                takenTime = Time.MeasureExecutionTime(() =>
                {
                    for (int i = 0; i < 99999; i++)
                    {
                        type.GetProperty(propName, bindingFlags).SetMethod.Invoke(testObj, new object[] { type.GetDefaultValue() });
                    }
                });

                Console.WriteLine($"Default reflection: {takenTime}");
                Console.WriteLine();
            }
        }
    }
}
