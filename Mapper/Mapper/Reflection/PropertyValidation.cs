using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Mapper.Contracts;

namespace Mapper.Reflection
{
    public class PropertyValidation : IValidator
    { 
        private static readonly Lazy<PropertyValidation> instance = new Lazy<PropertyValidation>( () => new PropertyValidation() );
        
        private PropertyValidation()
        {
            InitializeCastDictionary();
        }

        public static PropertyValidation GetInstance()
        {
            return instance.Value;
        }

        private Dictionary<Type, List<Type>> castDictionary;

        public PropertyInfo[] GetProperties(Type type)
        {
            return type.GetProperties(BindingFlags.Public
                                                | BindingFlags.SetProperty
                                                | BindingFlags.Instance);
        }

        public bool IsCanMap(PropertyInfo sourceProperty, PropertyInfo destinationProperty)
        {
            if (!sourceProperty.Name.Equals(destinationProperty.Name))
            {
                return false;
            }
            if (destinationProperty.GetSetMethod() == null)
            {
                return false;
            }
            if (!IsCorrectTypes(sourceProperty.PropertyType, destinationProperty.PropertyType))
            {
                return false;
            }
            return true;
        }

        private bool IsCorrectTypes(Type source, Type destination)
        {
            if ( IsValueTypes(source, destination)
                || IsReferenceTypes(source, destination)
                || IsValidCast(source, destination) )
            {
                return true;
            }
            return false;
        }

        private bool IsValueTypes(Type source, Type destination)
        {
            if (!source.IsValueType || !destination.IsValueType)
            {
                return false;
            }
            return source == destination;
        }

        private bool IsReferenceTypes(Type source, Type destination)
        {
            if (!source.IsClass || !destination.IsClass)
            {
                return false;
            }
            return source == destination;
        }

        private bool IsValidCast(Type source, Type destination)
        {
            if (castDictionary.ContainsKey(source))
            {
                var list = castDictionary[source];
                foreach (var type in list)
                {
                    if (destination == type)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void InitializeCastDictionary()
        {
            castDictionary = new Dictionary<Type, List<Type>>
            {
                {
                    typeof(byte), new List<Type>()
                    {
                        typeof(short),
                        typeof(ushort),
                        typeof(int),
                        typeof(uint),
                        typeof(long),
                        typeof(ulong),
                        typeof(double),
                        typeof(float),
                        typeof(char),
                        typeof(decimal)
                    }
                },
                {
                    typeof(char), new List<Type>()
                    {
                        typeof(short),
                        typeof(ushort),
                        typeof(int),
                        typeof(uint),
                        typeof(long),
                        typeof(ulong),
                        typeof(double),
                        typeof(float),
                        typeof(byte),
                        typeof(decimal)
                    }
                },
                {
                    typeof(short), new List<Type>()
                    {
                        typeof(int),
                        typeof(long),
                        typeof(double),
                        typeof(float),
                        typeof(decimal)
                    }
                },
                {
                    typeof(ushort), new List<Type>()
                    {
                        typeof(uint),
                        typeof(ulong),
                        typeof(double),
                        typeof(float),
                        typeof(decimal)
                    }
                },
                {
                    typeof(int), new List<Type>()
                    {
                        typeof(long),
                        typeof(double),
                        typeof(float),
                        typeof(decimal)
                    }
                },
                {
                    typeof(uint), new List<Type>()
                    {
                        typeof(ulong),
                        typeof(double),
                        typeof(float),
                        typeof(decimal)
                    }
                },
                {
                    typeof(long), new List<Type>()
                    {
                        typeof(double),
                        typeof(float),
                        typeof(decimal)
                    }
                },
                {
                    typeof(ulong), new List<Type>()
                    {
                        typeof(double),
                        typeof(float),
                        typeof(decimal)
                    }
                },
                {
                    typeof(float), new List<Type>()
                    {
                        typeof(double),
                        typeof(decimal)
                    }
                },
                {
                    typeof(double), new List<Type>()
                    {
                        typeof(decimal)
                    }
                }
            };
        }
    }
}
