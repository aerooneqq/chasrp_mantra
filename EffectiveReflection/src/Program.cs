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
        private const int ITER_COUNT = 1_000_000;
        /*
           Sample output:

            GET method tests: 
            Property: AGE
            Improved reflection: 00:00:00.0142251
            Default reflection: 00:00:00.1999234

            Property: YEAR
            Improved reflection: 00:00:00.0134401
            Default reflection: 00:00:00.1793289

            Property: PASSWORD
            Improved reflection: 00:00:00.0128743
            Default reflection: 00:00:00.1864165

            SET method tests: 
            Property: AGE
            Improved reflection: 00:00:00.1352005
            Default reflection: 00:00:00.2590783

            Property: YEAR
            Improved reflection: 00:00:00.1258112
            Default reflection: 00:00:00.2447622

            Property: PASSWORD
            Improved reflection: 00:00:00.1286078
            Default reflection: 00:00:00.2402738
            
            Method test: 
            Improved reflection: 00:00:00.0450473
            Default reflection: 00:00:00.2973304
            Native call: 00:00:00.0039790
            Delegate DynamicInvoke call: 00:00:00.7341062
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
                    for (int i = 0; i < ITER_COUNT; i++)
                    {
                        var res = getPropValueDel(testObj);
                    }
                });

                Console.WriteLine($"Improved reflection: {takenTime}");

                var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance;
                takenTime = Time.MeasureExecutionTime(() =>
                {
                    for (int i = 0; i < ITER_COUNT; i++)
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
                    for (int i = 0; i < ITER_COUNT; i++)
                    {
                        var defaultValue = type.GetProperty(propName, TypeExtensions.PropertySelectionFlags)
                            .PropertyType.GetDefaultValue();
                        setDelegate(testObj, defaultValue);
                    }
                });

                Console.WriteLine($"Improved reflection: {takenTime}");

                var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance;
                takenTime = Time.MeasureExecutionTime(() =>
                {
                    for (int i = 0; i < ITER_COUNT; i++)
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

            var del = type.GetMethodInvokerDelegate("Add");
            Delegate methodDelegate = (Delegate)type.GetMethod("Add").CreateDelegate(
                typeof(Delegate));

            TimeSpan takenTime = Time.MeasureExecutionTime(() =>
            {
                for (int i = 0; i < ITER_COUNT; ++i)
                {
                    var res = del(testClass, 3, 4);
                }
            });

            Console.WriteLine($"Improved reflection: {takenTime}");

            takenTime = Time.MeasureExecutionTime(() =>
            {
                for (int i = 0; i < ITER_COUNT; ++i)
                {
                    var res = type.GetMethod("Add").Invoke(testClass, new object[] { 3, 4 });
                }
            });

            Console.WriteLine($"Default reflection: {takenTime}");

            takenTime = Time.MeasureExecutionTime(() =>
            {
                for (int i = 0; i < ITER_COUNT; ++i)
                {
                    var res = testClass.Add(3, 4);
                }
            });

            Console.WriteLine($"Native call: {takenTime}");

            takenTime = Time.MeasureExecutionTime(() =>
            {
                for (int i = 0; i < ITER_COUNT; ++i)
                {
                    var res = methodDelegate.DynamicInvoke(testClass, 3, 4);
                }
            });

            Console.WriteLine($"Delegate DynamicInvoke call: {takenTime}");
        }
    }
}
