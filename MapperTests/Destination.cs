using System;
using System.Collections.Generic;

namespace Mapper.Tests
{
    internal sealed class Destination
    {
        public long CanConvert { get; set; }
        public string SameType { get; set; }
        public float CantConvert { get; set; }
        public Foo SubclassAndClass { get; set; }
        public string FirstName { get; set; }
        public object CantAssign { get; } = new object();

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
                var hashCode = CanConvert.GetHashCode();
                hashCode = (hashCode*397) ^ (SameType != null ? SameType.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ CantConvert.GetHashCode();
                hashCode = (hashCode*397) ^ (SubclassAndClass != null ? SubclassAndClass.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (FirstName != null ? FirstName.GetHashCode() : 0);
                return hashCode;
            }
        }

        // Internals

        private bool Equals(Destination other)
        {
            return CanConvert == other.CanConvert && string.Equals(SameType, other.SameType) && CantConvert.Equals(other.CantConvert) && Equals(SubclassAndClass, other.SubclassAndClass) && string.Equals(FirstName, other.FirstName);
        }
    }
}