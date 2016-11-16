namespace DtoMapper
{
    public class Mapper : IMapper
    {
        public TDestination Map<TSource, TDestination>(TSource source) where TDestination : new()
        {
            throw new System.NotImplementedException();
        }
    }
}
