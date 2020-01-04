using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Linq;

namespace EffectiveReflection
{
    public delegate object GetPropValueDel(object obj);
    public delegate void SetPropertyValueDel(object obj, object newValue);

    public static class TypeExtensions
    {
        public static BindingFlags PropertySelectionFlags { get; } = BindingFlags.Public | BindingFlags.NonPublic |
                                                                      BindingFlags.Static | BindingFlags.Instance;

        public static BindingFlags MethodSelectionFlags { get; } = BindingFlags.Public | BindingFlags.NonPublic |
                                                                    BindingFlags.Static | BindingFlags.Instance;

        public static GetPropValueDel GetGetPropValueDel(this Type type, string propName)
        {
            PropertyInfo property = type.GetProperty(propName, PropertySelectionFlags);

            if (property is null)
            {
                throw new ArgumentException($"The property with name {propName} does not exist in {type.FullName}");
            }

            // Get the property getter method
            MethodInfo getterMethod = property.GetGetMethod(true);

            if (getterMethod is null)
            {
                throw new ArgumentException($"There is no getter method for property {property.Name}");
            }

            // Create the dynamic method which represents the getter method
            DynamicMethod getterDynamicMethod = new DynamicMethod("GetPropValue", typeof(object),
                new[] { typeof(object) }, type.Module, true);
            ILGenerator getterILGenerator = getterDynamicMethod.GetILGenerator();

            // The first argument is an object in context of which the getter method is invoked. 
            // If the method is static we don't need this object
            if (!property.GetMethod.IsStatic)
            {
                getterILGenerator.Emit(OpCodes.Ldarg_0);
            }

            //Call the getter method
            getterILGenerator.Emit(OpCodes.Call, getterMethod);
            
            //If the returned type is a value type then we should box it
            if (getterMethod.ReturnType.IsValueType)
            {
                getterILGenerator.Emit(OpCodes.Box, getterMethod.ReturnType);
            }

            //Return from the method 
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
            
            // Create the dynamic method which returns void, because we just need to set the value
            // and accepts two objects params: the context object and new value 
            DynamicMethod setterDynamicMethod = new DynamicMethod("SetPropValue", typeof(void),
                new[] { typeof(object), typeof(object) }, type, true);

            ILGenerator setterILGenerator = setterDynamicMethod.GetILGenerator();

            // If the method is static we don't need the context object
            if (!setterMethod.IsStatic)
            {
                setterILGenerator.Emit(OpCodes.Ldarg_0);
            }

            // Load the new value
            setterILGenerator.Emit(OpCodes.Ldarg_1);

            // If the type of the property is value type then we should unbox the new value
            if (property.PropertyType.IsValueType)
            {
                setterILGenerator.Emit(OpCodes.Unbox_Any, property.PropertyType);
            }

            // Call the setter method
            setterILGenerator.Emit(OpCodes.Call, setterMethod);

            setterILGenerator.Emit(OpCodes.Ret);

            return setterDynamicMethod.CreateDelegate(typeof(SetPropertyValueDel)) as SetPropertyValueDel;
        }

        public static MethodInvokerDelegate GetMethodInvokerDelegate(this Type type, string methodName)
        {
            MethodInfo method = type.GetMethod(methodName, MethodSelectionFlags);

            if (method is null)
            {
                throw new ArgumentException($"The method {methodName} does not exist in {type.FullName}");
            }

            IList<Type> paramsTypes = method.GetParameters().Select(p => p.ParameterType).ToList();

            // Creating the dynamic method
            // The type of return value and all method parameters is object, so then we 
            // can cast it to Func<object, object ... object> 
            Type o = typeof(object);
            DynamicMethod dynamicMethod = new DynamicMethod(methodName, o, 
                                                            paramsTypes.Prepend(o).Select(_ => o).ToArray(),
                                                            type.Module, true);

            ILGenerator iLGenerator = dynamicMethod.GetILGenerator();
            
            // Load the first argument (that is the object of the class, in context of which we invoke method)
            // If the method is static we dont need to load the first parameter. 
            if (!method.IsStatic)
            {
                iLGenerator.Emit(OpCodes.Ldarg, 0);
            }

            // Load the method params. If the param type is a value type we should unbox it with
            // Unbox_Any instruction, because otherwise this won't work
            for (int i = 0; i < paramsTypes.Count; ++i)
            {
                iLGenerator.Emit(OpCodes.Ldarg, i + 1);

                if (paramsTypes[i].IsValueType)
                {
                    iLGenerator.Emit(OpCodes.Unbox_Any, paramsTypes[i]);
                }
            }

            // Call the method
            iLGenerator.Emit(OpCodes.Call, method);

            // If the result is a value type then we should box it.
            if (method.ReturnType.IsValueType)
            {
                iLGenerator.Emit(OpCodes.Box, method.ReturnType);
            }

            // Return the result. 
            iLGenerator.Emit(OpCodes.Ret);

            return MethodDelegateBuilder.GetMethodInvokerDelegate(dynamicMethod);
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
