namespace DtoMapper.Tests
{
    internal class Source
    {
        public string SameType { get; set; }
        public bool NotSameType { get; set; }
        public int Convertible { get; set; }
        public double NonConvertible { get; set; }
        public int ThereIsNoSuchPropertyInDestination { get; set; }
    }
}
