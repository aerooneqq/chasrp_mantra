namespace Singleton
{
    sealed class DummySingleton
    {
        public static uint ObjectsCount { get; private set; } = 0;
        private static DummySingleton dummySingleton;

        public static DummySingleton Obj
        {
            get
            {
                if (dummySingleton is null)
                {
                    dummySingleton = new DummySingleton();
                }

                return dummySingleton;
            }
        }

        public string Name => "Dummy singleton!";

        private DummySingleton()
        {
            ObjectsCount++;
        }
    }
}
