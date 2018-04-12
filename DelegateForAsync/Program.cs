using System;
using System.Threading;

namespace DelegateForAsync
{
    class Program
    {
        public delegate int FooDelegate(string s);

        static void Main(string[] args)
        {
            Console.WriteLine("主线程开始运行，当前的线程id是：" + Thread.CurrentThread.ManagedThreadId);
            FooDelegate fooDelegate = Foo;
            IAsyncResult result = fooDelegate.BeginInvoke("hello world", null, null);
            Console.WriteLine("当前主线程继续做其他事情······");
            int n = fooDelegate.EndInvoke(result);
            Console.WriteLine("回到主线程，程序执行的结果是：" + n);
            Console.ReadLine();
        }

        public static int Foo(string inputStr)
        {
            Console.WriteLine("程序所在线程，当前的线程id：" + Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("异步线程开始运行：" + inputStr);
            Thread.Sleep(1000);
            return inputStr.Length;
        }
    }
}