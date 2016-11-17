﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monads
{
    public class ListMonad<A>
    {
        IEnumerable<A> elements;
       
        public ListMonad<B> Bind<B>(Func<A, ListMonad<B>> func)
        {
            List<B> results = new List<B>();

            foreach (A element in elements)
            {
                foreach (B result in func(element).elements)
                {
                    results.Add(result);
                }
            }

            return new ListMonad<B>(results);

            // Same as:
            //return new ListMonad<B>(elements.SelectMany(element => func(element).elements));
            // SelectMany is also known as flatMap
        }

        public static ListMonad<A> Return(A element)
        {
            return new ListMonad<A>(new List<A> { element });
        }

        private ListMonad(IEnumerable<A> elements)
        {
            this.elements = elements;
        }
    }
}
