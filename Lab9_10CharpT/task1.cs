using System;

namespace Lab10_Task1
{
    public class TradeEventArgs : EventArgs
    {
        public double NewPrice { get; set; }
        public string ActionMessage { get; set; }
        public TradeEventArgs(double newPrice, string msg) { NewPrice = newPrice; ActionMessage = msg; }
    }

    public delegate void TradeEventHandler(object sender, TradeEventArgs e);

    public class Trader
    {
        public string Name { get; private set; }
        public string Type { get; private set; }
        public event TradeEventHandler MarketAction;

        public Trader(string name, string type) { Name = name; Type = type; }

        public void ActOnMarket(ref double currentPrice)
        {
            if (Type == "Бик") currentPrice += 10.0;
            else currentPrice -= 8.0;

            string msg = $"{Name} ({Type}) змінив ціну до {currentPrice}";
            Console.WriteLine($"[ДІЯ] {msg}");
            MarketAction?.Invoke(this, new TradeEventArgs(currentPrice, msg));
        }

        public void ReactToMarket(object sender, TradeEventArgs e)
        {
            if (sender != this)
                Console.WriteLine($"   -> {Name} отримав повідомлення про нову ціну: {e.NewPrice}");
        }
    }

    public class Lab10T1
    {
        public void Run()
        {
            Console.WriteLine("--- Запуск Завдання 1: Бики та ведмеді ---");
            double price = 100.0;
            Trader t1 = new Trader("Уоррен", "Бик");
            Trader t2 = new Trader("Джордж", "Ведмідь");

            t1.MarketAction += t2.ReactToMarket;
            t2.MarketAction += t1.ReactToMarket;

            t1.ActOnMarket(ref price);
            t2.ActOnMarket(ref price);
        }
    }
}