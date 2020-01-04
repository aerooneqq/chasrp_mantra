using System;

using NUnit.Framework;

using EffectiveReflection;

namespace EffectiveReflectionTests
{
    class A {}
    class Operations
    {
        public static int XStatic { get; set; }
        private static int YStatic { get; } = 1;

        public A XInstance { get; set; }
        private int YInstance { get; } = 2;

        private int AddInstancePrivate(int x, int y) => x + y;
        public int AddInstancePublic(int x) => x + 5;

        public static Operations PublicStaticMethod() => null;
        private static double PrivateStaticMethod(int x, int y) => double.NegativeInfinity;

        public A ReturnA(A a) => a;
    }

    [TestFixture]
    public class TypeExtensionsTests
    {
        private Operations operations;
        private static Type operationsType = typeof(Operations);


        [SetUp]
        public void SetUp()
        {
            operations = new Operations();
        }

        [Test]
        public void TestGettingXInstanceProperty()
        {
            A value = new A();
            operations.XInstance = value;

            Assert.AreEqual((A)operationsType.GetGetPropValueDel("XInstance").Invoke(operations), value);
        }

        [Test]
        public void TestSettingXInstanceProperty()
        {
            A value = new A();
            operations.XInstance = new A();
            operationsType.GetSetPropValueDel("XInstance").Invoke(operations, value);

            Assert.AreEqual(operations.XInstance, value);
        }

        [Test]
        public void TestSettingYInstanceProperty()
        {
            int value = 123;
            Assert.Throws<ArgumentException>(() => operationsType.GetSetPropValueDel("YInstance").Invoke(operations, value));
        }

        [Test]
        public void TestGettingXStaticProperty()
        {
            int value = 123;
            Operations.XStatic = value;

            Assert.AreEqual(operationsType.GetGetPropValueDel("XStatic").Invoke(null), value);
        }

        [Test]
        public void TestSettingXStaticProperty()
        {
            int value = 123;
            Operations.XStatic = 231233;
            operationsType.GetSetPropValueDel("XStatic").Invoke(operations, value);

            Assert.AreEqual(Operations.XStatic, value);
        }

        [Test]
        public void TestSettingYStaticProperty()
        {
            int value = 123;
            Assert.Throws<ArgumentException>(() => operationsType.GetSetPropValueDel("YStatic").Invoke(operations, value));
        }

        [Test]
        public void TestInvokingPrivateAddMethod()
        {
            int x = 1;
            int y = 123;

            var del = operationsType.GetMethodInvokerDelegate("AddInstancePrivate");
            Assert.AreEqual((int)del(operations, x, y), x + y);
        }

        [Test]
        public void TestInvokingPublicAddMethod()
        {
            int x = 1;

            var del = operationsType.GetMethodInvokerDelegate("AddInstancePublic");
            Assert.AreEqual((int)del(operations, x), x + 5);
        }

        [Test]
        public void TestInvokingPrivateStaticMethod()
        {
            var del = operationsType.GetMethodInvokerDelegate("PrivateStaticMethod");
            Assert.AreEqual((double)del(operations, 4, 4), double.NegativeInfinity);
        }

        [Test]
        public void TestInvokingPublicStaticMethod()
        {
            var del = operationsType.GetMethodInvokerDelegate("PublicStaticMethod");
            Assert.AreEqual(del(operations), null);
        }

        [Test]
        public void TestInvokeReturnAMethod()
        {
            A a = new A();

            var del = operationsType.GetMethodInvokerDelegate("ReturnA");
            Assert.AreEqual(del(operations, a), a);
        }

        [Test]
        public void TestInvokingNonexistentMethod()
        {
            Assert.Throws<ArgumentException>(() => operationsType.GetMethodInvokerDelegate("blablabla"));
        }
    }
}