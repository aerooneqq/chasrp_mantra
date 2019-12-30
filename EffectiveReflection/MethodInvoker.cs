using System;
using System.Collections.Generic;
using System.Text;

namespace EffectiveReflection
{
    static class MethodInvoker
    {
        static object Invoke(Func<object> func) => func();

        static object Invoke(Func<object, object> func, params object[] args) 
            => func(args[0]);

        static object Invoke(Func<object, object, object> func, params object[] args) 
            => func(args[0], args[1]);

        static object Invoke(Func<object, object, object, object> func, params object[] args)
            => func(args[0], args[1], args[2]);

        static object Invoke(Func<object, object, object, object, object> func, params object[] args)
            => func(args[0], args[1], args[2], args[3]);
    }
}
