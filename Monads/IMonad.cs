using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monads
{

    // Pseudo code, for demonstation purposes only.
    public interface IMonad<A>
    {
        // Monadic constructor (psuedo code since interfaces to not support constructors).
        IMonad<A> Return<A>(A value); 
        IMonad<B> Bind<A, B>(Func<A, IMonad<B>> func);
    }
}
