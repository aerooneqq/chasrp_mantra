using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Linq;

namespace EffectiveReflection
{
    public delegate object GetPropValueDel(object obj);
    public delegate void SetPropertyValueDel(object obj, object newValue);

    public static class TypeExtensions
    {
        private static BindingFlags PropertySelectionFlags = BindingFlags.Public | BindingFlags.NonPublic | 
                                                             BindingFlags.Static | BindingFlags.Instance;

        private static OpCode[] LdArgCodes = { OpCodes.Ldarg_0, OpCodes.Ldarg_1, OpCodes.Ldarg_2, OpCodes.Ldarg_3 };

        public static GetPropValueDel GetGetPropValueDel(this Type type, string propName)
        {
            PropertyInfo property = type.GetProperty(propName, PropertySelectionFlags);

            if (property is null)
            {
                throw new ArgumentException($"The property with name {propName} does not exist in {type.FullName}");
            }

            MethodInfo getterMethod = property.GetGetMethod(true);
            DynamicMethod getterDynamicMethod = new DynamicMethod("GetPropValue", typeof(object),
                new[] { typeof(object) }, typeof(object), true);
            ILGenerator getterILGenerator = getterDynamicMethod.GetILGenerator();

            if (!property.GetMethod.IsStatic)
            {
                getterILGenerator.Emit(OpCodes.Ldarg_0);
            }

            getterILGenerator.Emit(OpCodes.Call, getterMethod);
            
            if (getterMethod.ReturnType.IsValueType)
            {
                getterILGenerator.Emit(OpCodes.Box, getterMethod.ReturnType);
            }

            getterILGenerator.Emit(OpCodes.Ret);

            return getterDynamicMethod.CreateDelegate(typeof(GetPropValueDel)) as GetPropValueDel;
        }

        public static SetPropertyValueDel GetSetPropValueDel(this Type type, string propName)
        {
            PropertyInfo property = type.GetProperty(propName, PropertySelectionFlags);

            if (property is null)
            {
                throw new ArgumentException($"The property with name {propName} does not exist in {type.FullName}");
            }

            MethodInfo setterMethod = property.GetSetMethod(true);

            if (setterMethod is null)
            {
                throw new ArgumentException($"The property with name {property.Name} does not have set method");
            }

            DynamicMethod setterDynamicMethod = new DynamicMethod("SetPropValue", typeof(void),
                new[] { typeof(object), typeof(object) }, type, true);

            ILGenerator setterILGenerator = setterDynamicMethod.GetILGenerator();

            setterILGenerator.Emit(OpCodes.Ldarg_0);
            setterILGenerator.Emit(OpCodes.Ldarg_1);

            setterILGenerator.Emit(OpCodes.Call, setterMethod);

            setterILGenerator.Emit(OpCodes.Ret);

            return setterDynamicMethod.CreateDelegate(typeof(SetPropertyValueDel)) as SetPropertyValueDel;
        }

        public static TDelegate GetMethodFunc<TDelegate>(this Type type, string methodName)
            where TDelegate : Delegate
        {
            MethodInfo method = type.GetMethod(methodName);

            if (method is null)
            {
                throw new ArgumentException($"The method {methodName} does not exist in {type.FullName}");
            }

            IList<Type> paramsTypes = method.GetParameters().Select(p => p.ParameterType).ToList();

            if (paramsTypes.Count > 3)
            {
                throw new NotSupportedException();
            }

            Type o = typeof(object);
            DynamicMethod dynamicMethod = new DynamicMethod(methodName, o, 
                                                            paramsTypes.Prepend(o).Select(_ => o).ToArray(),
                                                            type.Module, true);

            ILGenerator iLGenerator = dynamicMethod.GetILGenerator();

            iLGenerator.Emit(OpCodes.Ldarg_0);
            iLGenerator.Emit(OpCodes.Castclass, type);

            for (int i = 0; i < paramsTypes.Count; ++i)
            {
                iLGenerator.Emit(LdArgCodes[i + 1]);

                if (paramsTypes[i].IsValueType)
                {
                    iLGenerator.Emit(OpCodes.Unbox_Any, paramsTypes[i]);
                }
            }

            iLGenerator.Emit(OpCodes.Call, method);

            if (method.ReturnType.IsValueType)
            {
                iLGenerator.Emit(OpCodes.Box, method.ReturnType);
            }

            iLGenerator.Emit(OpCodes.Ret);

            return dynamicMethod.CreateDelegate(typeof(TDelegate)) as TDelegate;
        }

        public static object GetDefaultValue(this Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }

            return null;
        }
    }
}
