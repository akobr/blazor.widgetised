using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace Blazor.Widgetised.Configuration
{
    public sealed class Customisation<TModel> : DynamicObject, IMergeable<TModel>, IPartlyChanged, IDictionary<string, object>
    {
        private readonly Type modelType = typeof(TModel);
        private readonly Dictionary<string, DynamicProperty> properties;

        public Customisation()
        {
            if (modelType.IsInterface)
            {
                throw new InvalidOperationException("The customisation generic type must be an interface.");
            }

            properties = BuildProperties();
        }

        public Customisation(IDictionary<string, object> properties)
            : this()
        {
            SetProperties(properties);
        }

        private void SetProperties(IDictionary<string, object> propertiesToSet)
        {
            foreach (var propertyPair in propertiesToSet)
            {
                if (!TrySetProperty(propertyPair.Key, propertyPair.Value))
                {
                    throw new ArgumentOutOfRangeException("Key", $"A property with name '{propertyPair.Key}' doesn't exist.");
                }
            }
        }

        public void Merge(TModel content)
        {
            Customisation.Merge(this, content, modelType);
        }

        public override bool TryGetMember(GetMemberBinder binder, out object? result)
        {
            return TryGetProperty(binder.Name, binder.ReturnType, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            return TrySetProperty(binder.Name, value);
        }

        public IEnumerable<string> ChangedPropertyNames()
        {
            foreach (DynamicProperty property in properties.Values.Where(p => p.WasSet))
            {
                yield return property.Name;
            }
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return properties.Keys;
        }

        public void UndoChangedProperty(string propertyName)
        {
            if (properties.TryGetValue(propertyName, out DynamicProperty property))
            {
                property.WasSet = false;
                property.Value = null;
            }
        }

        private bool TryGetProperty(string name, Type type, out object? value)
        {
            if (!properties.TryGetValue(name, out DynamicProperty property))
            {
                value = null;
                return false;
            }

            if (property.Type != type)
            {
                throw new InvalidOperationException($"The property is type of {property.Type}.");
            }

            value = property.Value;
            return true;
        }

        private bool TrySetProperty(string name, object value)
        {
            if (!properties.TryGetValue(name, out DynamicProperty property))
            {
                return false;
            }

            Type valueType = value.GetType();

            if (valueType == property.Type)
            {
                property.SetValue(value);
                return true;
            }

            if (valueType.IsAssignableFrom(property.Type))
            {
                property.SetValue(Convert.ChangeType(value, property.Type));
                return true;
            }

            throw new InvalidOperationException($"The property is type of {property.Type}.");
        }

        private Dictionary<string, DynamicProperty> BuildProperties()
        {
            Dictionary<string, DynamicProperty>  result = new Dictionary<string, DynamicProperty>();

            foreach (PropertyInfo property in Customisation.GetCustomisationProperties(modelType))
            {
                DynamicProperty dynamicProperty = new DynamicProperty(property);
                result[dynamicProperty.Name] = dynamicProperty;
            }

            return result;
        }

        #region Members of IDictionary

        private IEnumerable<KeyValuePair<string, object>> GetKeyValuePairs()
        {
            foreach (DynamicProperty property in properties.Values)
            {
                if (property.WasSet && property.Value != null)
                {
                    yield return new KeyValuePair<string, object>(property.Name, property.Value);
                }
            }
        }

        ICollection<string> IDictionary<string, object>.Keys => properties.Keys;

        ICollection<object> IDictionary<string, object>.Values => throw new NotImplementedException();

        int ICollection<KeyValuePair<string, object>>.Count => properties.Count;

        bool ICollection<KeyValuePair<string, object>>.IsReadOnly => false;

#pragma warning disable CS8603 // Possible null reference return.
        object IDictionary<string, object>.this[string key]
        {
            get
            {
                if (!properties.TryGetValue(key, out DynamicProperty property))
                {
                    throw new ArgumentOutOfRangeException(nameof(key), $"A property with name '{key}' doesn't exist.");
                }

                TryGetProperty(key, property.Type, out object? value);
                return value;
            }

            set
            {
                if (!TrySetProperty(key, value))
                {
                    throw new ArgumentOutOfRangeException(nameof(key), $"A property with name '{key}' doesn't exist.");
                }
            }
        }
#pragma warning restore CS8603 // Possible null reference return.

        void IDictionary<string, object>.Add(string key, object value)
        {
            TrySetProperty(key, value);
        }

        bool IDictionary<string, object>.ContainsKey(string key)
        {
            return properties.ContainsKey(key);
        }

        bool IDictionary<string, object>.Remove(string key)
        {
            UndoChangedProperty(key);
            return properties.ContainsKey(key);
        }

#pragma warning disable CS8617 // Nullability of reference types in type of parameter doesn't match implemented member.
        bool IDictionary<string, object>.TryGetValue(string key, out object? value)
        {
            if (!properties.TryGetValue(key, out DynamicProperty property))
            {
                value = null;
                return false;
            }

            return TryGetProperty(key, property.Type, out value);
        }
#pragma warning restore CS8617 // Nullability of reference types in type of parameter doesn't match implemented member.

        void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> item)
        {
            TrySetProperty(item.Key, item.Value);
        }

        void ICollection<KeyValuePair<string, object>>.Clear()
        {
            foreach (DynamicProperty property in properties.Values)
            {
                property.WasSet = false;
                property.Value = null;
            }
        }

        bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> item)
        {
            return ((IDictionary<string, object>)this).ContainsKey(item.Key);
        }

        void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            var sourceArray = GetKeyValuePairs().ToArray();
            Array.Copy(sourceArray, 0, array, arrayIndex, sourceArray.LongLength);
        }

        bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> item)
        {
            return ((IDictionary<string, object>)this).Remove(item.Key);
        }

        IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
        {
            return GetKeyValuePairs().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<string, object>>)this).GetEnumerator();
        }

        #endregion

        private class DynamicProperty
        {
            private readonly PropertyInfo propertyInfo;
            public object? Value;
            public bool WasSet;

            public DynamicProperty(PropertyInfo propertyInfo)
            {
                this.propertyInfo = propertyInfo;
            }

            public string Name => propertyInfo.Name;

            public Type Type => propertyInfo.PropertyType;

            public void SetValue(object newValue)
            {
                Value = newValue;
                WasSet = true;
            }
        }
    }   
}
