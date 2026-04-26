using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab10_Task2
{
    public class TradeEventArgs : EventArgs
    {
        public double NewPrice { get; set; }
        public TradeEventArgs(double price) { NewPrice = price; }
    }

    public delegate void TradeEventHandler(object sender, TradeEventArgs e);

    public class TradeRequest
    {
        public string TraderName { get; set; }
        public double Change { get; set; }
        public int Priority { get; set; } // 1 - високий
    }

    public class Lab10T2
    {
        private double _price = 100.0;
        private PriorityQueue<TradeRequest, int> _queue = new PriorityQueue<TradeRequest, int>();
        public event TradeEventHandler MarketUpdate;

        public async Task RunAsync()
        {
            Console.WriteLine("--- Запуск Завдання 2: Асинхронна біржа ---");
            
            // Підписка на події
            MarketUpdate += (s, e) => Console.WriteLine($"   [Broadcast] Нова ціна на ринку: {e.NewPrice}");

            // Додаємо заявки в чергу з різним пріоритетом
            _queue.Enqueue(new TradeRequest { TraderName = "Ведмідь", Change = -15, Priority = 3 }, 3);
            _queue.Enqueue(new TradeRequest { TraderName = "VIP-Бик", Change = 20, Priority = 1 }, 1);
            _queue.Enqueue(new TradeRequest { TraderName = "Бик-2", Change = 5, Priority = 2 }, 2);

            while (_queue.Count > 0)
            {
                var req = _queue.Dequeue();
                Console.WriteLine($"\nОбробка заявки від {req.TraderName} (Пріоритет: {req.Priority})");
                
                await Task.Delay(1000); // Асинхронна затримка
                _price += req.Change;
                
                MarketUpdate?.Invoke(this, new TradeEventArgs(_price));
            }
            
            Console.WriteLine($"\nТорги завершено. Фінальна ціна: {_price}");
        }
    }
}