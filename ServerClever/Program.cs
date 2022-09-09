using System;
using System.Threading;

namespace ServerClever
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Выберите задачу: \n 1) - GetCount / AddToCount \n2) - полусинхронный вызов делегата");
            int i = Convert.ToInt32(Console.ReadLine());
            switch(i)
            {
                case 1:
                    Task1();
                    break;
                case 2:
                    Task2();
                    break;
                 default:
                    Console.WriteLine("Ошибка");
                        break;
            }
        }

        private static void Task1()
        {
            Thread thread1 = new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    ReadCount(1);

                }
            });

            Thread thread2 = new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    ReadCount(2);

                }
            });

            Thread thread3 = new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    ReadCount(3);

                }
            });

            Thread thread4 = new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(100);
                    int count = new Random().Next(0, 1000);
                    WriteCount(1, count);

                }
            });

            Thread thread5 = new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(100);
                    int count = new Random().Next(0, 100);
                    WriteCount(2, count);

                }
            });

            thread1.Start();
            thread2.Start();
            thread3.Start();
            thread4.Start();
            thread5.Start();

        }

        private static void ReadCount(int thread)
        {
            Thread.Sleep(2000);
            Console.WriteLine(DateTime.Now.ToString("H:mm:ss.ffff") + " | " + "Читатель " + thread + " пытается получить значение значение...");
            int s = Server.GetCount();
            Console.WriteLine(DateTime.Now.ToString("H:mm:ss.ffff") + " | " + "Читатель " + thread + " получил значение: " + "\"" + s + "\"");
        }

        private static void WriteCount(int thread, int count)
        {
            Console.WriteLine(DateTime.Now.ToString("H:mm:ss.fffffff") + " | " + "Писатель " + thread + " записывает новое значение: " + "\"" + count + "\"");
            Server.AddToCount(count);
            Console.WriteLine(DateTime.Now.ToString("H:mm:ss.fffffff") + " | " + "Писатель " + thread + " записал новое значение: " + "\"" + count + "\"");

        }

        private static void Task2()
        {
            EventHandler h = new EventHandler(MyEventHandler);
            AsyncCaller ac = new AsyncCaller(h);

            Console.WriteLine("Таймаут 6 секунд, Поток 5 секунд");
            bool completedOK = ac.Invoke(6000, null, EventArgs.Empty);
            Console.WriteLine("completedOK: " + completedOK.ToString());

            Console.WriteLine("\nТаймаут 3 секунд, Поток 5 секунд");
            bool completedOK2 = ac.Invoke(3000, null, EventArgs.Empty);
            Console.WriteLine("completedOK2: " + completedOK2.ToString());

            Console.ReadLine();
        }

        private static void MyEventHandler(object sender, EventArgs e)
        {
            Console.WriteLine("Начало хэндлера");
            Thread.Sleep(5000);
            Console.WriteLine("Конец хэндлера");
        }
    }
    
}
