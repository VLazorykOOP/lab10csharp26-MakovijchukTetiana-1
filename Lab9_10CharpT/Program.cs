using System;
using System.Threading.Tasks;
using Lab10_Task1;
using Lab10_Task2;

class Program
{
    // Використовуємо async Task, щоб коректно працювало друге завдання
    static async Task Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        
        Console.WriteLine("=== Лабораторна робота №10 (Варіант 10) ===");
        Console.WriteLine("Оберіть завдання для запуску:");
        Console.WriteLine("1 - Задача 1: Бики та ведмеді (Базова модель)");
        Console.WriteLine("2 - Задача 2: Біржа (Пріоритети, черги, асинхронність)");
        Console.Write("\nВаш вибір: ");

        string choice = Console.ReadLine();
        Console.WriteLine();

        switch (choice)
        {
            case "1":
                Lab10T1 task1 = new Lab10T1();
                task1.Run();
                break;
            case "2":
                Lab10T2 task2 = new Lab10T2();
                await task2.RunAsync();
                break;
            default:
                Console.WriteLine("Помилка: Невірний вибір. Оберіть 1 або 2.");
                break;
        }

        Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
        Console.ReadKey();
    }
}