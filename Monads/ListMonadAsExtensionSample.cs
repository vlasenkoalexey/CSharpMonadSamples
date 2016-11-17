using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonadsExtensions
{
    public class ListMonadAsExtensionSample
    {
        public class User
        {
            public string Name { get; set; }

            public IEnumerable<User> Children { get; set; }
        }

        public IEnumerable<string> GetGrandChildrenNamesLinq(User user)
        {
            return user.Children.Bind(child => child.Children).Bind(grandChild => ListMonadAsExtensions.Return(grandChild.Name));
        }
    }
}
