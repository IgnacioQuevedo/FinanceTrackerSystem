using System.Collections;

namespace DataManagers;

public abstract class Helper
{
    public static void AreTheSameObject<T>(T object1, T object2)
        {
            bool areDifferent = false;

            if (object1 != null && object2 != null && !ReferenceEquals(object1, object2))
            {
                foreach (var property in typeof(T).GetProperties())
                {
                    if (IsDateTimeOrList(property.PropertyType))
                    {
                        if (!AreEqualProperties(object1, object2, property.Name))
                        {
                            areDifferent = true;
                            break;
                        }
                    }
                    else if (!AreEqualSimpleProperty(object1, object2, property.Name))
                    {
                        areDifferent = true;
                        break;
                    }
                }
            }
            else
            {
                areDifferent = true;
            }

            if (!areDifferent)
            {
                throw new ExceptionHelper("Objects are identical. Please change at least one value.");
            }
        }

        private static bool IsDateTimeOrList(Type type)
        {
            return type == typeof(DateTime) ||
                   (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>));
        }

        private static bool AreEqualProperties<T>(T object1, T object2, string propertyName)
        {
            var property1 = typeof(T).GetProperty(propertyName)?.GetValue(object1);
            var property2 = typeof(T).GetProperty(propertyName)?.GetValue(object2);

            if (property1 is DateTime && property2 is DateTime)
            {
                return ((DateTime)property1).Date != ((DateTime)property2).Date;
            }
            else if (property1 is IList && property2 is IList)
            {
                var list1 = property1 as IList;
                var list2 = property2 as IList;

                if (list1.Count != list2.Count)
                    return false;

                for (int i = 0; i < list1.Count; i++)
                {
                    if (!EqualityComparer<object>.Default.Equals(list1[i], list2[i]))
                        return false;
                }

                return true;
            }

            return true;
        }

        private static bool AreEqualSimpleProperty<T>(T object1, T object2, string propertyName)
        {
            var property1 = typeof(T).GetProperty(propertyName)?.GetValue(object1);
            var property2 = typeof(T).GetProperty(propertyName)?.GetValue(object2);

            return EqualityComparer<object>.Default.Equals(property1, property2);
        }
    }
