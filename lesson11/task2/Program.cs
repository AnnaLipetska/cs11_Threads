using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace task2
{
    // Создайте консольное приложение, которое в различных потоках сможет получить доступ к 2-м
    // файлам.Считайте из этих файлов содержимое и попытайтесь записать полученную
    // информацию в третий файл.Чтение/запись должны осуществляться одновременно в каждом
    // из дочерних потоков.Используйте блокировку потоков для того, чтобы добиться корректной
    // записи в конечный файл.
    class Program
    {
        static object block = new object();
        static StreamWriter writer = File.CreateText("FoodAndDrinks.txt");

        static void AddDrinksToList()
        {
            string drinks;

            using (var reader = File.OpenText("Drinks.txt"))
            {
                drinks = reader.ReadToEnd();
            }

            lock (block)
            {
                writer.WriteLine(drinks);
                writer.WriteLine(new string('-', 30));
            }
        }

        static void AddFoodToList()
        {
            string food;

            using (var reader = File.OpenText("Food.txt"))
            {
                food = reader.ReadToEnd();
            }

            lock (block)
            {
                writer.WriteLine(food);
                writer.WriteLine(new string('-', 30));
            }
        }

        static void Main(string[] args)
        {
            var drinksThread = new Thread(AddDrinksToList);
            var foodThread = new Thread(AddFoodToList);

            using (writer)
            {
                drinksThread.Start();
                foodThread.Start();

                drinksThread.Join();
                foodThread.Join();
            }

            Console.WriteLine("All items are on the same list now.");
            Console.ReadKey();
        }
    }
}
