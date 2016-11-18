using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monads
{
    public class ListMonadSample
    {
        public class User
        {
            public string Name { get; set; }

            public ListMonad<User> Children { get; set; }
        }

        public ListMonad<string> GetGrandChildrenNamesLinq(User user)
        {
            return user.Children
                .Bind(child => child.Children)
                .Bind(grandChild => ListMonad<String>.Return(grandChild.Name));
        }
    }
}
