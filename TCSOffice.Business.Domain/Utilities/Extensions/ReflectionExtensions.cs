using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TCSOffice.Business.Domain.Utilities.Extensions
{
    public static class ReflectionExtensions
    {
        public static string GetPropertyName<T>(Expression<Func<T, object>> expression)
        {
            var expressionBody = expression.Body;

            if (expressionBody is MemberExpression)
            {
                return (expressionBody as MemberExpression).Member.Name;
            }
            else if (expressionBody is UnaryExpression)
            {
                var op = (expressionBody as UnaryExpression).Operand;
                return ((MemberExpression)op).Member.Name;
            }

            throw new NotSupportedException();
        }

        /// <summary>
        /// converts a class to a key value pair of key=prop.Name and value=class.property.value.
        /// If skipNullOrEmpty is true, then it will NOT include unpopulated properties in the KVPs
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="includeNullOrEmptyFields"> </param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<string, string>> ConvertToKeyValuePair(this Object entity, bool includeNullOrEmptyFields = true)
        {
            var kvps = entity.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(prop => prop.Name, prop => prop.GetValue(entity, null))
                .Select(item => new KeyValuePair<string, string>(item.Key, item.Value != null ? item.Value.ToString() : ""));

            if (!includeNullOrEmptyFields) kvps = kvps.Where(x => !string.IsNullOrEmpty(x.Value.ToString()));

            return kvps;
        }

        /// <summary>
        /// Gets all the property names of a class/type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<string> GetProperties<T>() where T : new()
        {
            return GetPropertyInfos<T>().Select(x => x.Name);
        }

        /// <summary>
        /// returns an array of the class' propertyInfo
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static PropertyInfo[] GetPropertyInfos<T>() where T : new()
        {
            var entity = new T();

            return entity.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public);
        }

        public static PropertyInfo GetPropertyInfo<T>(string propertyName)
        {
            return typeof(T).GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
        }

        /// <summary>
        /// Returns names of all properties which are of the specified type or are collections of the specified type.
        /// </summary>
        public static IEnumerable<string> GetPropertyNames<T>(Type propertyType)
        {
            Type type = typeof(T);

            IEnumerable<PropertyInfo> properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach (var property in properties)
            {
                if (property.PropertyType.IsAssignableFrom(propertyType))
                    yield return property.Name;

                if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>))
                {
                    if (property.PropertyType.GetGenericArguments().First() == propertyType)
                    {
                        yield return property.Name;
                    }
                }

                if (!property.PropertyType.IsGenericType && property.PropertyType.IsClass)
                {
                    yield return property.Name;
                }

            }
        }

        /// <summary>
        /// Finds all the types that implement an interface or inherit from a class
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="baseType"></param>
        /// <returns></returns>
        public static IEnumerable<Type> FindDerivedTypes(this Assembly assembly, Type baseType)
        {
            return assembly.GetTypes()
                .Where(t => baseType.IsAssignableFrom(t));
        }

        /// <summary>
        /// Finds all types across all assemblies that inherits or implements a baseClass or interface respectively
        /// </summary>
        /// <param name="baseType"></param>
        /// <returns></returns>
        public static IEnumerable<Type> FindDerivedTypesAcrossAllAssemblies(this Type baseType)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => FindDerivedTypes(x, baseType));
        }

        public static IEnumerable<Type> LoadAssemblyFromLocation()
        {
            return null;
        }

        /// <summary>
        /// Responsible for updating the objects property with a new value
        /// </summary>
        /// <typeparam name="T">The object type</typeparam>
        /// <param name="obj">The object that needs to update its property</param>
        /// <param name="propertyName">The name of the property to update </param>
        /// <param name="newPropertyValue">The new property value to set</param>
        /// <returns>The object with the updated property </returns>
        public static T UpdateProperty<T>(this T obj, string propertyName, object newPropertyValue)
        {
            var propertyInfo = obj.GetType().GetProperty(propertyName);

            if (newPropertyValue != null)
            {
                propertyInfo.SetValue(obj, Convert.ChangeType(newPropertyValue, propertyInfo.PropertyType), null);
            }
            else
            {
                propertyInfo.SetValue(obj, null, null);
            }

            return obj;
        }

        /// <summary>
        /// This gets the value of a property based on the name of the property.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static object GetPropertyValue(this object obj, string name)
        {
            foreach (var part in name.Split('.'))
            {
                if (obj == null) { return null; }

                var type = obj.GetType();
                var info = type.GetProperty(part);
                if (info == null) { return null; }

                obj = info.GetValue(obj, null);
            }
            return obj;
        }

        /// <summary>
        /// This gets the TYPE of a property based on the name of the property.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Type GetPropertyType(this object obj, string name)
        {
            var type = obj.GetType();
            var info = type.GetProperty(name);
            return info.PropertyType;
        }

        public static Type GetCollectionItemsType(this object targetResource, string collectionName)
        {
            var col = targetResource.GetType().GetProperty(collectionName).GetValue(targetResource, null) as IList;
            if (col != null)
            {
                var typeofI = col.GetType().GetGenericArguments()[0];
                return typeofI;
            }

            throw new InvalidOperationException("Not a list/collection");
        }

        public static bool IsCollection(this object o)
        {
            return o is ICollection
                || typeof(ICollection<>).IsInstanceOfType(o);
        }

        /// <summary>
        /// Checks if the property is an IEnumerable(something)
        /// NB. Even though string implements ienumerable(char) it is skipped from the list as well
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static bool IsPropertyACollection(this PropertyInfo property)
        {
            //string implements ienumerable<char>. Without this line of code, STRING would return true
            if (property.PropertyType == typeof(string)) return false;

            var propertyTypeName = property.PropertyType.GetInterface(typeof(IEnumerable<>).FullName);
            return propertyTypeName != null;
        }

        public static void AddToCollection(this object targetResource, string collectionName, object resourceToBeAdded)
        {
            var col = targetResource.GetType().GetProperty(collectionName).GetValue(targetResource, null) as IList;
            if (col != null)
                col.Add(resourceToBeAdded);
            else
                throw new InvalidOperationException("Not a list");
        }

        public static void RemoveFromCollection(this object targetResource, string collectionName, string fieldName, int key)
        {
            var col = targetResource.GetType().GetProperty(collectionName).GetValue(targetResource, null) as IList;
            var typeOfItemInCollection = GetCollectionItemsType(targetResource, collectionName);

            //var objsToRemove = new List<T>();
            var objsToRemove = CreateInstanceOfList(typeOfItemInCollection);
            if (col != null)
            {
                foreach (var obj in col)
                {
                    var keyOfObject = (int)obj.GetPropertyValue(fieldName);
                    //if (keyOfObject == key) objsToRemove.Add((T)obj);
                    if (keyOfObject == key) objsToRemove.Add(obj);
                }

                foreach (var item in objsToRemove)
                {
                    col.Remove(item);
                }
            }
        }
        //public static void RemoveFromCollection<T>(this object targetResource, string collectionName, string fieldName, int key)
        //{
        //    var col = targetResource.GetType().GetProperty(collectionName).GetValue(targetResource, null) as IList;
        //    var objsToRemove = new List<T>();
        //    if (col != null)
        //    {
        //        foreach (var obj in col)
        //        {
        //            var keyOfObject = (int)obj.GetPropertyValue(fieldName);
        //            if (keyOfObject == key) objsToRemove.Add((T)obj);
        //        }

        //        foreach (var item in objsToRemove)
        //        {
        //            col.Remove(item);
        //        }
        //    }
        //}

        /// <summary>
        /// This empties the collection on an item as you cannot call item.childrenCollection=null
        /// </summary>
        /// <param name="targetResource"></param>
        /// <param name="collectionName"></param>
        public static void ClearCollection(this object targetResource, string collectionName)
        {
            var col = targetResource.GetType().GetProperty(collectionName).GetValue(targetResource, null) as IList;
            if (col != null) col.Clear();
        }

        public static IList CreateInstanceOfList(Type t)
        {
            var listType = typeof(List<>);
            var constructedListType = listType.MakeGenericType(t);
            var instance = Activator.CreateInstance(constructedListType);
            return (IList)instance;
        }
    }
}
