using System;
using System.Threading;

namespace DelegateForAsync
{
    class Program
    {
        public delegate int FooDelegate(string s);
        public delegate void PrintDelegate(string s);


        static void Main(string[] args)
        {
             // *使用委托实现异步*
            //Console.WriteLine("主线程开始运行，主线程id是：" + Thread.CurrentThread.ManagedThreadId);
            //FooDelegate fooDelegate = Foo;
            //IAsyncResult result = fooDelegate.BeginInvoke("hello world", null, null);
            //Console.WriteLine("当前主线程继续做其他事情······");
            //int n = fooDelegate.EndInvoke(result);
            //Console.WriteLine("回到主线程，程序执行的结果是：" + n);
            //Console.ReadLine();

            //*使用委托回调实现异步*
            Console.WriteLine("主线程开始运行，主线程id是：" + Thread.CurrentThread.ManagedThreadId);
            PrintDelegate printDelegate = Print;
            printDelegate.BeginInvoke("hello world", PrintComplete, printDelegate);
            Console.WriteLine("当前主线程继续做其他事情······");
            Console.ReadLine();
        }

        public static int Foo(string inputStr)
        {
            Console.WriteLine("程序所在线程，当前的线程id：" + Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("异步线程开始运行：" + inputStr);
            Thread.Sleep(1000);
            return inputStr.Length;
        }

        public static void Print(string inputStr)
        {
            Console.WriteLine("当前的任务开始执行："+ inputStr);
            Console.WriteLine("当前的任务线程id是：" + Thread.CurrentThread.ManagedThreadId);
        }

        /// <summary>
        /// 这里需要注意的是：1、回调函数的返回值必须为void 2、只有一个类型参数IAsyncResult
        /// </summary>
        /// <param name="result"></param>
        public static void PrintComplete(IAsyncResult result)
        {
            (result.AsyncState as PrintDelegate)?.EndInvoke(result);
            Console.WriteLine("程序执行的线程运行结束： " + result.AsyncState.ToString());
            Console.WriteLine("回调运行的函数的线程id是：" + Thread.CurrentThread.ManagedThreadId);
        }
    }
}