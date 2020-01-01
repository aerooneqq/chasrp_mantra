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


        public int Add(int x, int y)
        {
            return x + y;
        }

        public void Print() => Console.WriteLine("Hello from test class!");
        public int GetZero() => 0;
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
 
           Method test:
           Improved reflection: 00:00:00.0009560
           Default reflection: 00:00:00.0078839
        */
        static void Main(string[] args)
        {
            Console.WriteLine("GET method tests: ");
            CompareGetPropValueTime();

            Console.WriteLine("SET method tests: ");
            CompareSetPropValueTime();

            Console.WriteLine("Method test: ");
            TestMethodInvokationTime();
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
                    for (int i = 0; i < 10000; i++)
                    {
                        var res = getPropValueDel(testObj);
                    }
                });

                Console.WriteLine($"Improved reflection: {takenTime}");

                var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance;
                takenTime = Time.MeasureExecutionTime(() =>
                {
                    for (int i = 0; i < 10000; i++)
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
                    for (int i = 0; i < 10000; i++)
                    {
                        setDelegate(testObj, type.GetDefaultValue());
                    }
                });

                Console.WriteLine($"Improved reflection: {takenTime}");

                var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance;
                takenTime = Time.MeasureExecutionTime(() =>
                {
                    for (int i = 0; i < 10000; i++)
                    {
                        type.GetProperty(propName, bindingFlags).SetMethod.Invoke(testObj, new object[] { type.GetDefaultValue() });
                    }
                });

                Console.WriteLine($"Default reflection: {takenTime}");
                Console.WriteLine();
            }
        }

        static void TestMethodInvokationTime()
        {
            Type type = typeof(TestClass);
            TestClass testClass = new TestClass();

            var func = type.GetMethodFunc<Func<object, object, object, object>>("Add");

            TimeSpan takenTime = Time.MeasureExecutionTime(() =>
            {
                for (int i = 0; i < 10000; ++i)
                {
                    var res = func(testClass, 3, 4);
                }
            });

            Console.WriteLine($"Improved reflection: {takenTime}");

            takenTime = Time.MeasureExecutionTime(() =>
            {
                for (int i = 0; i < 10000; ++i)
                {
                    var res = type.GetMethod("Add").Invoke(testClass, new object[] { 3, 4 });
                }
            });

            Console.WriteLine($"Default reflection: {takenTime}");
        }
    }
}
