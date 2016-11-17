using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MonadsExtensions
{
    public static class TaskMonadExtensions
    {
        public static Task<T> Return<T>(this T value)
        {
            return Task.FromResult(value);
        }

        public static async Task<TNewResult> Bind<TResult, TNewResult>(this Task<TResult> task, Func<TResult, Task<TNewResult>> continuation)
        {
            return await continuation(await task);
        }

        public static Task<TNewResult> BindAlt<TResult, TNewResult>(this Task<TResult> task, Func<TResult, Task<TNewResult>> continuation)
        {
            // See https://blogs.msdn.microsoft.com/pfxteam/2010/11/21/processing-sequences-of-asynchronous-operations-with-tasks/ for full implementation
            return task.ContinueWith(t => continuation(t.Result)).Unwrap();
        }
    }
}
