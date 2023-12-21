using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDCheckingLibrary
{
    public static class InterfaceSegregation
    {
        private static object? CreateInstanceOfClass(Type classType)
        {
            var firstConstructor = classType.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).First();
            var arguments = firstConstructor.GetParameters().
                Select(p => p.ParameterType.IsValueType ? Activator.CreateInstance(p.ParameterType) : null).
                ToArray();
            
            return firstConstructor.Invoke(arguments);
        }
        
        public static bool MethodIsImplemented(MethodInfo methodInfo, Type initialClass)
        {
            var arguments = methodInfo.GetParameters().
                Select(p => p.ParameterType.IsValueType ? Activator.CreateInstance(p.ParameterType) : null).
                ToArray();

            try
            {
                var instance = CreateInstanceOfClass(initialClass);
                methodInfo.Invoke(instance, arguments);
            }
            catch(TargetInvocationException ex)
            {
                if(ex.InnerException is NotImplementedException)
                    return false;
            }
            catch (Exception ex)
            {
                return true;
            }

            return true;
        }
        public static bool PropertyGetIsImplemented(PropertyInfo propertyInfo, Type initialClass)
        {
            var getMethod = propertyInfo.GetGetMethod();
            if(getMethod == null)
                return false;

            try
            {
                var instance = CreateInstanceOfClass(initialClass);
                propertyInfo.GetValue(instance);
            }
            catch (TargetInvocationException ex)
            {
                if (ex.InnerException is NotImplementedException)
                    return false;
            }
            catch (Exception ex)
            {
                return true;
            }

            return true;
        }
        public static bool PropertySetIsImplemented(PropertyInfo propertyInfo, Type initialClass)
        {
            var getMethod = propertyInfo.GetSetMethod();
            if (getMethod == null)
                return false;

            try
            {
                var setArgument = propertyInfo.PropertyType.IsValueType ? Activator.CreateInstance(propertyInfo.PropertyType) : null;
                var instance = CreateInstanceOfClass(initialClass);
                propertyInfo.SetValue(instance,setArgument);
            }
            catch (TargetInvocationException ex)
            {
                if (ex.InnerException is NotImplementedException)
                    return false;
            }
            catch(Exception ex)
            {
                return true;
            }

            return true;
        }
        public static (bool, string) ClassMethodsFollowPrinciple(Type type)
        {
            var allMethods = type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            bool followsPrinciple = true;
            string checkLog = "";

            foreach (var method in allMethods)
            {
                if(!MethodIsImplemented(method, type))
                {
                    followsPrinciple = false;
                    checkLog += $"Method {method.ToString()} is not implemented!\n";
                }
            }
            if (followsPrinciple) checkLog = $"Methods of class {type.Name} follow the principle";
            else checkLog = $"Methods of class {type.Name} failed to follow the principle!\n" + checkLog;

            return (followsPrinciple, checkLog);
        }
        public static (bool, string) ClassPropertiesFollowPrinciple(Type type)
        {
            var allProperties = type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            bool followsPrinciple = true;
            string checkLog = "";

            foreach(var property in allProperties)
            {
                bool propertyIsImplemented = true;
                string checkPropertyLog = "";
                if(property.GetGetMethod() != null)
                {
                    if(!PropertyGetIsImplemented(property, type))
                    {
                        propertyIsImplemented = false;
                        checkPropertyLog += "Getter is not implemented!\n";
                    }
                }
                if(property.GetSetMethod() != null)
                {
                    if (!PropertySetIsImplemented(property, type))
                    {
                        propertyIsImplemented = false;
                        checkPropertyLog += "Setter is not implemented!\n";
                    }
                }

                if (!propertyIsImplemented) 
                    checkLog += $"Property {property.ToString()} is not implemented!\n" + checkPropertyLog;

                followsPrinciple &= propertyIsImplemented;
            }

            if (followsPrinciple) checkLog = $"Properties of class {type.Name} follow the principle";
            else checkLog = $"Properites of class {type.Name} failed to follow the principle!\n" + checkLog;

            return (followsPrinciple, checkLog);
        }
        public static (bool, string) ClassFollowsPrinciple(Type type)
        {
            bool followsPrinciple = true;
            string checkLog = "";
            var methodsCheck = ClassMethodsFollowPrinciple(type);
            var propertiesCheck = ClassPropertiesFollowPrinciple(type);
            if(!methodsCheck.Item1)
            {
                checkLog += methodsCheck.Item2 + "\n";
                followsPrinciple = false;
            }
            if(!propertiesCheck.Item1)
            {
                checkLog += propertiesCheck.Item2 + "\n";
                followsPrinciple = false;
            }
            if (followsPrinciple) checkLog = $"Class {type.Name} follows the principle of interface segregation";
            else checkLog = $"Class {type.Name} doesn't follows the principle of interface segregation:\n" + checkLog;
            return (followsPrinciple, checkLog);
        }
    }
}
