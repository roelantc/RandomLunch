namespace RandomLunch;

internal abstract class Program
{
    private static readonly List<Restaurant> Restaurants = new()
    {
        new Restaurant("The Gourmet Kitchen", false, true),
        new Restaurant("Seafood Delight", true, false),
        new Restaurant("Mountain View Bistro", false, true),
        // ... add other restaurants similarly
    };

    static readonly Queue<string> History = new();
    private const string HistoryFilePath = "restaurant_history.txt";

    static void Main()
    {
        LoadHistory();
 
        while (true)
        {
            Console.WriteLine("Do you want to include halal restaurants? (y/n)");
            var halalChoice = Console.ReadLine()?.ToLower();
            var includeHalal = halalChoice == "y";

            Console.WriteLine("Do you want to include restaurants that serve beer? (y/n)");
            var beerChoice = Console.ReadLine()?.ToLower();
            var includeBeer = beerChoice == "y";

            // Filter the list based on user choice
            var filteredRestaurants = Restaurants
                .Where(r => includeHalal ? r.IsHalal : !r.IsHalal)
                .Where(r => includeBeer ? r.HasBeer : !r.HasBeer)
                .ToList();
 
            // Exclude the last 3 selected restaurants
            filteredRestaurants.RemoveAll(r => History.Contains(r.Name));
 
            // Check if there are any restaurants to choose from
            if (filteredRestaurants.Count == 0)
            {
                Console.WriteLine("No restaurants available based on your choice and history.");
            }
            else
            {
                // Create a random object
                var random = new Random();
 
                // Select a random restaurant from the filtered list
                var selectedRestaurant = filteredRestaurants[random.Next(filteredRestaurants.Count)];
 
                // Print the random restaurant name
                Console.WriteLine("Random Restaurant: " + selectedRestaurant.Name + (selectedRestaurant.IsHalal ? " (Halal)" : ""));
 
                // Update history
                UpdateHistory(selectedRestaurant.Name);
                SaveHistory();
            }
 
            Console.WriteLine("Press any key to continue, or type 'q' to quit.");
            if (Console.ReadLine()?.ToLower() == "q") break;
        }
    }

    private static void UpdateHistory(string selectedRestaurant)
    {
        History.Enqueue(selectedRestaurant);
 
        if (History.Count > 3)
        {
            History.Dequeue();
        }
    }

    private static void SaveHistory()
    {
        File.WriteAllLines(HistoryFilePath, History);
    }

    private static void LoadHistory()
    {
        if (!File.Exists(HistoryFilePath))
        {
            return;
        }
        
        var savedHistory = File.ReadAllLines(HistoryFilePath);
        foreach (var item in savedHistory)
        {
            History.Enqueue(item);
        }
    }
}