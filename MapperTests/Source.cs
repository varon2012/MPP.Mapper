namespace Mapper.Tests
{
    internal sealed class Source
    {
        public int CanConvert { get; set; }
        public string SameType { get; set; }
        public double CantConvert { get; set; }
        public FooSubclass SubclassAndClass { get; set; }
        public string Name { get; set; }
        public string CantAssign { get; set; }
    }
}