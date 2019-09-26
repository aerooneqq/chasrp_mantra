namespace Singleton
{
    sealed class ThreadSafeSingleton
    {
        public static uint ObjectsCount { get; private set; } = 0;
        private static object threadLock = new object();
        private static ThreadSafeSingleton threadSafeSingleton;

        public static ThreadSafeSingleton Obj
        {
            get
            {
                lock (threadLock)
                {
                    if (threadSafeSingleton is null)
                    {
                        threadSafeSingleton = new ThreadSafeSingleton();
                    }

                    return threadSafeSingleton;
                }
            }
        }

        public string Name => "Thread safe lock singleton!";

        private ThreadSafeSingleton()
        {
            ObjectsCount++;
        }
    }
}
