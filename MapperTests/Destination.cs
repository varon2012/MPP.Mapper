using System;
using System.Collections.Generic;

namespace Mapper.Tests
{
    internal sealed class Destination
    {
        private bool Equals(Destination other)
        {
            return FirstProperty == other.FirstProperty && string.Equals(SecondProperty, other.SecondProperty) && ThirdProperty.Equals(other.ThirdProperty) && FourthProperty.Equals(other.FourthProperty);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Destination && Equals((Destination) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = FirstProperty.GetHashCode();
                hashCode = (hashCode*397) ^ (SecondProperty != null ? SecondProperty.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ ThirdProperty.GetHashCode();
                hashCode = (hashCode*397) ^ FourthProperty.GetHashCode();
                return hashCode;
            }
        }

        public long FirstProperty { get; set; }
        public string SecondProperty { get; set; }
        public float ThirdProperty { get; set; }
        public DateTime FourthProperty { get; set; }
    }
}