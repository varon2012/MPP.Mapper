using System;

namespace DtoMapper.Tests
{
    internal class Destination
    {
        public string SameType { get; set; }
        public string NotSameType { get; set; }
        public long Convertible { get; set; }
        public float NonConvertible { get; set; }
        public int ThereIsNoSuchPropertyInSource { get; set; }

        public override bool Equals(object obj)
        {
            var compareTo = obj as Destination;

            if (compareTo == null) return false;

            return 
                string.Equals(SameType, compareTo.SameType) &&
                NotSameType == compareTo.NotSameType && 
                Convertible == compareTo.Convertible &&
                NonConvertible.Equals(compareTo.NonConvertible);
        }

        public override int GetHashCode()
        {
            return 1;
        }
    }
}
