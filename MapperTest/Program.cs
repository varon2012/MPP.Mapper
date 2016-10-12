using System;
using Mapper;
using Mapper.Configuration;

namespace MapperTest
{
    internal static class Program
    {
        internal static void Main(string[] args)
        {
            Source src = new Source()
            {
                FirstProperty = 2,
                SecondProperty = "azaza",
                ThirdProperty = 12,
                FourthProperty = 21
            };

           IGenericMapperConfiguration<Source, Destination> mapperConfiguration = new MapperConfiguration<Source, Destination>();

           mapperConfiguration
                .Register(source => source.FirstProperty, dest => dest.FirstProperty)
                .Register(source => source.SecondProperty, dest => dest.SecondProperty)
                .Register(source => source.ThirdProperty, dest => dest.Test)
                .Register(source => source.ThirdProperty, dest => dest.Test2);
            IMapper mapper = new SimpleMapper();
            Destination destination = mapper.Map(src, mapperConfiguration);

            Console.WriteLine("yep");
            Console.ReadLine();
        }

    }
}
