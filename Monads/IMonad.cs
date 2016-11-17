using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monads
{
    public interface IMonad<A>
    {
        IMonad<A> Return<A>(A value); // Monadic constructor (psuedo code since interfaces to not support constructors).
        IMonad<B> Bind<A, B>(Func<A, IMonad<B>> func);
    }
}
