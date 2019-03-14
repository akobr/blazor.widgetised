using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Blazor.Widgetised.Configuration
{
    internal static class Customisation
    {
        public static IEnumerable<PropertyInfo> GetCustomisationProperties(object model, Type modelType)
        {
            if (model is IPartlyChanged partial)
            {
                return GetCustomisationProperties(partial, modelType);
            }
            else
            {
                return GetCustomisationProperties(modelType);
            }
        }

        public static IEnumerable<PropertyInfo> GetCustomisationProperties(IPartlyChanged partialModel, Type modelType)
        {
            foreach (string propertyName in partialModel.ChangedPropertyNames())
            {
                yield return modelType.GetProperty(propertyName);
            }
        }

        public static IEnumerable<PropertyInfo> GetCustomisationProperties(Type modelType)
        {
            return modelType
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.GetMethod != null && p.GetMethod.IsPublic && p.SetMethod != null && p.SetMethod.IsPublic);
        }

        public static void Merge(object into, object? from, Type modelType)
        {
            if (from == null)
            {
                return;
            }

            foreach (PropertyInfo property in GetCustomisationProperties(from, modelType))
            {
                object? valueFrom = property.GetValue(from);

                if (valueFrom == null)
                {
                    continue;
                }

                property.SetValue(into, valueFrom);
            }
        }
    }
}
