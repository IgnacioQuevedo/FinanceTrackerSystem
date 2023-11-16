using System;
using System.Collections;
using System.Collections.Generic;

namespace DataManagers
{
    public static class Helper
    {
        #region Are the same object
        public static bool AreTheSameObject<T>(T object1, T object2)
        {
            bool areTheSame = true;

            if (object1 != null && object2 != null)
            {
                foreach (var property in typeof(T).GetProperties())
                {
                    if (IsDateTimeOrList(property.PropertyType))
                    {
                        if (!AreSameProperties(object1, object2, property.Name))
                        {
                            areTheSame = false;
                            break;
                        }
                    }
                    else if (!AreSameSimpleProperty(object1, object2, property.Name))
                    {
                        areTheSame = false;
                        break;
                    }
                }
            }
            return areTheSame;
        }

        private static bool IsDateTimeOrList(Type type)
        {
            return type == typeof(DateTime) ||
                   (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>));
        }

        private static bool AreSameProperties<T>(T object1, T object2, string propertyName)
        {
            var property1 = typeof(T).GetProperty(propertyName)?.GetValue(object1);
            var property2 = typeof(T).GetProperty(propertyName)?.GetValue(object2);

            if (property1 is DateTime && property2 is DateTime)
            {
                return ((DateTime)property1).Date == ((DateTime)property2).Date;
            }
            else if (property1 is IList && property2 is IList)
            {
                var list1 = property1 as IList;
                var list2 = property2 as IList;

                if (list1.Count != list2.Count)
                    return false;
            }

            return true;
        }

        private static bool AreSameSimpleProperty<T>(T object1, T object2, string propertyName)
        {
            if (typeof(T).IsPrimitive || typeof(T) == typeof(string))
            {
                return object1.Equals(object2);
            }

            var property1 = typeof(T).GetProperty(propertyName)?.GetValue(object1);
            var property2 = typeof(T).GetProperty(propertyName)?.GetValue(object2);

            if (property1 != null && property2 != null)
            {
                return property1.Equals(property2);
            }
            return property1 == null && property2 == null;
        }
        #endregion
    }
}
