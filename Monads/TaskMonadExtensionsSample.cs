using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MonadsExtensions
{
    public class TaskMonadExtensionsSample
    {
        public static async void TaskMonadSampleWithAsync()
        {
            int userId = 1;
            var userCode = await GetUserNameById(userId).Bind(name => GetUserCode(name));
            Console.WriteLine("TaskMonadSampleWithAsync, user code: {0}", userCode);
        }

        public static void TaskMonadSampleWithoutAsync()
        {
            int userId = 1;
            Task<int> task = GetUserNameById(userId).BindAlt(name => GetUserCode(name));
            task.Wait();
            Console.WriteLine("TaskMonadSampleWithoutAsync, user code: {0}", task.Result);
        }

        public static async void TaskSampleWithAsync()
        {
            int userId = 1;
            string userName = await GetUserNameById(userId);
            int userCode = await GetUserCode(userName);
            Console.WriteLine("TaskSampleWithAsync, user code: {0}", userCode);
        }

        public static void TaskSampleWithoutAsync()
        {
            int userId = 1;
            Task<int> task = GetUserNameById(userId).ContinueWith(t => GetUserCode(t.Result)).Unwrap();
            task.Wait();
            Console.WriteLine("TaskSampleWithoutAsync, user code: {0}", task.Result);
        }


        static Task<String> GetUserNameById(int userId)
        {
            return Task<String>.Factory.StartNew(
                (obj) =>
                {
                    // Simulate a slow operation
                    Thread.Sleep(100);
                    Console.WriteLine("- GetUserNameById: Thread={0}, userId={1}", Thread.CurrentThread.ManagedThreadId, obj);
                    return "aleksey";
                },
                userId);
        }

        static Task<int> GetUserCode(string name)
        {
            return Task<int>.Factory.StartNew(
                (obj) =>
                {
                    // Simulate a slow operation
                    Thread.Sleep(100);
                    Console.WriteLine("- GetUserCode: Thread={0}, name={1}", Thread.CurrentThread.ManagedThreadId, obj);
                    return name.GetHashCode();
                },
                name);
        }
    }
}
