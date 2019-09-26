using System;

namespace Singleton
{
    sealed class TrueLazySingleton
    {
        public static uint ObjectsCount { get; private set; } = 0;

        private static Lazy<TrueLazySingleton> lazySingleton = new Lazy<TrueLazySingleton>(() => new TrueLazySingleton());
        public static TrueLazySingleton Obj => lazySingleton.Value;

        public string Name => "True lazy singleton";

        private TrueLazySingleton()
        {
            ObjectsCount++;
        }
    }
}
