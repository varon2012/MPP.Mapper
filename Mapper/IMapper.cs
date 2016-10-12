using Mapper.Configuration;

namespace Mapper
{
    public interface IMapper
    {
        TDestination Map<TSource, TDestination>(TSource source) where TDestination : new();
        TDestination Map<TSource, TDestination>(TSource source, IGenericMapperConfiguration<TSource, TDestination> configuration) where TDestination: new();
    }
}
