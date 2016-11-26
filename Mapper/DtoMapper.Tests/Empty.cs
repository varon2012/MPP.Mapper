namespace DtoMapper.Tests
{
    internal class Empty
    {
        public override bool Equals(object obj)
        {
            return obj is Empty;
        }

        public override int GetHashCode()
        {
            return 2;
        }
    }
}
