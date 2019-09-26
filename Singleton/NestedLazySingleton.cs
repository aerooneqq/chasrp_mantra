namespace Singleton
{
    sealed class NestedLazySingleton
    {
        class Nested
        {
            static Nested() { }

            internal static NestedLazySingleton Singleton => new NestedLazySingleton();
        }

        public static uint ObjectsCount { get; private set; } = 0;
        public static NestedLazySingleton Obj => Nested.Singleton;

        public string Name => "Nested lazy singleton!";

        private NestedLazySingleton()
        {
            ObjectsCount++;
        }
    }
}
