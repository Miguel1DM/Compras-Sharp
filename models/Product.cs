namespace SalesSystem.Models.Product
{
    public class Product

    {
        public string Name { get; set; }
        public float Price { get; set; }
        public int Amount { get; set; }
        public Product(string name, float price, int amount)
        {
            Amount = amount;
            Price = price;
            Name = name;
        }
    }
}