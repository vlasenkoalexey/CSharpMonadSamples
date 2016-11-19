using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monads
{
    public abstract class Maybe<A>
    {
        public static Maybe<A> Return(A value) //emulating Monadic constructor
        {
            return new Just<A>(value);
        }

        public Maybe<B> Bind<B>(Func<A, Maybe<B>> func)
        {
            var justa = this as Just<A>;
            return justa == null ?
                new Nothing<B>() :
                func(justa.Value);
        }
    }

    public class Nothing<A> : Maybe<A>
    {
        public override string ToString()
        {
            return "Nothing";
        }
    }

    public class Just<T> : Maybe<T>
    {
        public T Value { get; private set; }
        internal Just(T value) // Monadic constructor
        {
            Value = value;
        }
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
