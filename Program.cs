namespace RandomLunch;

internal abstract class Program
{
    static List<(string Name, bool IsHalal)> restaurants = new()
    {
        ("The Gourmet Kitchen", false),
        ("Seafood Delight", true),
        ("Mountain View Bistro", false),
        ("Urban Grill", true),
        ("Mama's Italiano", false),
        ("Sushi Palace", true),
        ("Spice Junction", true),
        ("The Vegan Spot", false),
        ("Pancake World", true),
        ("Taco Terrace", false)
    };
 
    static Queue<string> history = new();
    static string historyFilePath = "restaurant_history.txt";
 
    static void Main()
    {
        LoadHistory();
 
        while (true)
        {
            Console.WriteLine("Do you want to include halal restaurants? (yes/no)");
            var userChoice = Console.ReadLine()?.ToLower();
 
            var includeHalal = userChoice == "yes";
 
            // Filter the list based on user choice
            var filteredRestaurants = restaurants.Where(r => includeHalal ? r.IsHalal : !r.IsHalal).ToList();
 
            // Exclude the last 3 selected restaurants
            filteredRestaurants.RemoveAll(r => history.Contains(r.Name));
 
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
 
            Console.WriteLine("Press any key to continue, or type 'exit' to quit.");
            if (Console.ReadLine()?.ToLower() == "exit") break;
        }
    }

    private static void UpdateHistory(string selectedRestaurant)
    {
        history.Enqueue(selectedRestaurant);
 
        if (history.Count > 3)
        {
            history.Dequeue();
        }
    }

    private static void SaveHistory()
    {
        File.WriteAllLines(historyFilePath, history);
    }

    private static void LoadHistory()
    {
        if (!File.Exists(historyFilePath))
        {
            return;
        }
        
        var savedHistory = File.ReadAllLines(historyFilePath);
        foreach (var item in savedHistory)
        {
            history.Enqueue(item);
        }
    }
}