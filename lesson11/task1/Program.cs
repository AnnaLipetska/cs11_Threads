using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace task1
{
    // Используя конструкции блокировки, создайте метод, который будет в цикле for (допустим, на 10
    // итераций) увеличивать счетчик на единицу и выводить на экран счетчик и текущий поток.
    // Метод запускается в трех потоках. Каждый поток должен выполниться поочередно, т.е.в
    // результате на экран должны выводиться числа (значения счетчика) с 1 до 30 по порядку, а не в
    // произвольном порядке.
    class Program
    {
        static object block = new object();
        static int counter;
        static void Method()
        {
            lock (block)
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId}, счетчик - {++counter}");
                }
            }
        }
        static void Main(string[] args)
        {
            Thread[] threads = new Thread[3];

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(Method);
            }

            foreach (var thread in threads)
            {
                thread.Start();
            }

            Console.ReadKey();
        }
    }
}
