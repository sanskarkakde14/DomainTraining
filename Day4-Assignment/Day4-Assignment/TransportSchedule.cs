using System;
namespace Day4_Assignment
{
    public class TransportSchedule
    {
        public string TransportType { get; set; }
        public string Route { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal Price { get; set; }
        public int SeatsAvailable { get; set; }
    }

    public class TransportManager
    {
        private readonly List<TransportSchedule> schedules = new();

        public void AddSchedule(TransportSchedule schedule) => schedules.Add(schedule);

        public IEnumerable<TransportSchedule> Search(string type = null, string route = null, DateTime? departure = null) =>
            schedules.Where(s => (string.IsNullOrEmpty(type) || s.TransportType.Equals(type, StringComparison.OrdinalIgnoreCase)) &&
                                 (string.IsNullOrEmpty(route) || s.Route.Contains(route, StringComparison.OrdinalIgnoreCase)) &&
                                 (!departure.HasValue || s.DepartureTime.Date == departure.Value.Date));

        public IEnumerable<IGrouping<string, TransportSchedule>> GroupByTransportType() => schedules.GroupBy(s => s.TransportType);

        public int TotalAvailableSeats() => schedules.Sum(s => s.SeatsAvailable);
        public decimal AveragePrice() => schedules.Count > 0 ? schedules.Average(s => s.Price) : 0;

        public IEnumerable<(string Route, DateTime DepartureTime)> SelectRoutesAndDepartureTimes() =>
            schedules.Select(s => (s.Route, s.DepartureTime));
    }

    public class TransportApp
    {
        private readonly TransportManager manager = new();

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("\nTransport Management System");
                Console.WriteLine("1. Add Schedule\n2. Search\n3. Group\n4. Total Seats\n5. Avg Price\n6. Exit");
                switch (Console.ReadLine())
                {
                    case "1": AddSchedule(); break;
                    case "2": SearchSchedules(); break;
                    case "3": GroupSchedules(); break;
                    case "4": Console.WriteLine($"Total Seats: {manager.TotalAvailableSeats()}"); break;
                    case "5": Console.WriteLine($"Average Price: {manager.AveragePrice():C}"); break;
                    case "6": return;
                    default: Console.WriteLine("Invalid choice."); break;
                }
            }
        }

        private void AddSchedule()
        {
            var schedule = new TransportSchedule
            {
                TransportType = Prompt("Type (Bus/Flight): "),
                Route = Prompt("Route: "),
                DepartureTime = DateTime.Parse(Prompt("Departure (yyyy-mm-dd hh:mm): ")),
                ArrivalTime = DateTime.Parse(Prompt("Arrival (yyyy-mm-dd hh:mm): ")),
                Price = decimal.Parse(Prompt("Price: ")),
                SeatsAvailable = int.Parse(Prompt("Seats: "))
            };
            manager.AddSchedule(schedule);
            Console.WriteLine("Schedule Added.");
        }

        private void SearchSchedules()
        {
            var results = manager.Search(
                Prompt("Type (leave blank for all): "),
                Prompt("Route (leave blank for all): "),
                DateTime.TryParse(Prompt("Departure Date (leave blank for all): "), out var dt) ? dt : (DateTime?)null
            );
            foreach (var schedule in results)
                Console.WriteLine($"{schedule.TransportType}, {schedule.Route}, {schedule.DepartureTime}, {schedule.ArrivalTime}, {schedule.Price:C}, {schedule.SeatsAvailable} seats");
        }

        private void GroupSchedules()
        {
            var grouped = manager.GroupByTransportType();
            foreach (var group in grouped)
            {
                Console.WriteLine($"Type: {group.Key}");
                foreach (var s in group)
                    Console.WriteLine($"  {s.Route}, {s.DepartureTime}, {s.Price:C}, {s.SeatsAvailable} seats");
            }
        }

        private string Prompt(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }
    }
}

