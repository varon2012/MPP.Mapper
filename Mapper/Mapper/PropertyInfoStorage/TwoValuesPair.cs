namespace Mapper.PropertyInfoStorage
{
    public struct TwoValuesPair<TSource, TDestination>
    {
        public TwoValuesPair(TSource source, TDestination destination)
        {
            Source = source;
            Destination = destination;
        }

        public TSource Source { get; set; }
        public TDestination Destination { get; set; }
    }
}
