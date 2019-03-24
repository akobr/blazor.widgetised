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
            switch (model)
            {
                case IDictionary<string, object> dictionaryModel:
                    return GetCustomisationProperties(dictionaryModel, modelType);

                case IProviderOfChangedProperties partialModel:
                    return GetCustomisationProperties(partialModel, modelType);

                default:
                    throw new InvalidOperationException($"Unknown model type [{model.GetType().Name}] for merge of customisation models.");
            }
        }

        public static IEnumerable<PropertyInfo> GetCustomisationProperties(IDictionary<string, object> dictionaryModel, Type modelType)
        {
            foreach (string propertyName in dictionaryModel.Keys)
            {
                PropertyInfo propertyInfo = modelType.GetProperty(propertyName);

                if (propertyInfo != null)
                {
                    yield return propertyInfo;
                }
            }
        }

        public static IEnumerable<PropertyInfo> GetCustomisationProperties(IProviderOfChangedProperties partialModel, Type modelType)
        {
            foreach (string propertyName in partialModel.ChangedProperties())
            {
                PropertyInfo propertyInfo = modelType.GetProperty(propertyName);

                if (propertyInfo != null)
                {
                    yield return propertyInfo;
                }
            }
        }

        public static IEnumerable<PropertyInfo> GetCustomisationProperties(Type modelType)
        {
            return modelType
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.GetMethod != null && p.GetMethod.IsPublic && p.SetMethod != null && p.SetMethod.IsPublic);
        }

        public static bool IsMergable(object? source)
        {
            return source != null
                && (source is IDictionary<string, object>
                    || source is IProviderOfChangedProperties);
        }

        public static void Merge(object into, object? from, Type modelType)
        {
            if (from == null)
            {
                return;
            }

            IDictionary<string, object>? propertyMap;
            bool isFromDictionary = (propertyMap = from as IDictionary<string, object>) != null;
            Type fromType = from.GetType();

            foreach (PropertyInfo property in GetCustomisationProperties(from, modelType))
            {
                object? valueFrom = isFromDictionary
                    ? propertyMap![property.Name]
                    : fromType.GetProperty(property.Name).GetValue(from);

                property.SetValue(into, valueFrom);
            }
        }
    }
}
