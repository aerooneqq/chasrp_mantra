namespace Singleton
{
    sealed class PreinitializedThreadSafeSingleton
    {
        public static uint ObjectsCount { get; private set; } = 0;
        private static PreinitializedThreadSafeSingleton singleton = new PreinitializedThreadSafeSingleton();
        public static PreinitializedThreadSafeSingleton Obj => singleton;


        public string Name => "Preinitialized thread safe singleton!";

        static PreinitializedThreadSafeSingleton() { }
        private PreinitializedThreadSafeSingleton()
        {
            ObjectsCount++;
        }
    }
}
