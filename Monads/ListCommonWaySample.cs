using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monads
{
    public class ListCommonWaySample
    {
        public class User
        {
            public string Name { get; set; }

            public IEnumerable<User> Children { get; set; }
        }

        public IEnumerable<string> GetGrandChildrenNames(User user)
        {
            List<string> results = new List<string>();
            foreach (User child in user.Children)
            {
                foreach (User grandChild in child.Children)
                {
                    results.Add(grandChild.Name);
                }
            }
            return results;
        }

        public IEnumerable<string> GetGrandChildrenNamesWithYield(User user)
        {
            foreach (User child in user.Children)
            {
                foreach (User grandChild in child.Children)
                {
                    yield return grandChild.Name;
                }
            }
        }

        public IEnumerable<string> GetGrandChildrenNamesLinq(User user)
        {
            return user.Children.SelectMany(child => child.Children).Select(grandChild => grandChild.Name);
        }
    }
}
