using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMapper
{
    class Program
    {
        static void Main(string[] args)
        {
            var source = new Source()
            {
                FirstProperty = 10,
                SecondProperty = "lalka"
            };

            var dest = new Mapper.Mapper().Map<Source, Destination>(source);
            Console.ReadKey();
        }
    }

    public class Source
    {
        public byte FirstProperty { get; set; }
        public string SecondProperty { get; set; }
    }

    public class Destination
    {
        public int FirstProperty { get; set; }
        public string SecondProperty { get; set; }
    }
}
