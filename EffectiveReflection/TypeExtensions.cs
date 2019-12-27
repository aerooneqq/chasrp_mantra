using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace EffectiveReflection
{
    public delegate object GetPropValueDel(object obj);
    public delegate void SetPropertyValueDel(object obj, object newValue);

    public static class TypeExtensions
    {
        private static BindingFlags PropertySelectionFlags = BindingFlags.Public | BindingFlags.NonPublic | 
                                                             BindingFlags.Static | BindingFlags.Instance;
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
                new[] { typeof(object), typeof(object) }, typeof(object), true);

            ILGenerator setterILGenerator = setterDynamicMethod.GetILGenerator();

            setterILGenerator.Emit(OpCodes.Ldarg_0);
            setterILGenerator.Emit(OpCodes.Ldarg_1);

            setterILGenerator.Emit(OpCodes.Call, setterMethod);

            setterILGenerator.Emit(OpCodes.Ret);

            return setterDynamicMethod.CreateDelegate(typeof(SetPropertyValueDel)) as SetPropertyValueDel;
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
