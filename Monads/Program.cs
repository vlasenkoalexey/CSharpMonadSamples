using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonadsExtensions;

namespace Monads
{
    class Program
    {
        static void Main(string[] args)
        {
            TaskMonadExtensionsSample.TaskMonadSampleWithAsync();
            TaskMonadExtensionsSample.TaskMonadSampleWithoutAsync();
            TaskMonadExtensionsSample.TaskSampleWithAsync();
            TaskMonadExtensionsSample.TaskSampleWithoutAsync();

            Console.ReadKey();
        }
    }
}
