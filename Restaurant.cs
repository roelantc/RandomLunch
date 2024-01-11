namespace RandomLunch
{
    public class Restaurant
    {
        public string Name { get; set; }
        public bool IsHalal { get; set; }
        public bool HasBeer { get; set; }

        public Restaurant(string name, bool isHalal, bool hasBeer)
        {
            Name = name;
            IsHalal = isHalal;
            HasBeer = hasBeer;
        }
    }
}
