using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonadsExtensions
{
    public static class ListMonadAsExtensions
    {
        public static IEnumerable<B> Bind<A, B>(this IEnumerable<A> value, 
            Func<A, IEnumerable<B>> func)
        {
            return value.SelectMany(element => func(element));
        }

        public static IEnumerable<A> Return<A>(A element)
        {
            return new List<A> { element };
        }
    }
}
