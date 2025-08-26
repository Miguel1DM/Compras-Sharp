using SalesSystem.Models.Product;
using SalesSystem.Models.User;
namespace SalesSystem.Models.Sale
{

    public class Sale
    {
        public User.User User { get; set; }
        public Product.Product Product{ get; set; }
        public int AmountProduct { get; set; }
        public float SaleValue { get; set; }
        public Sale(User.User user, Product.Product product, int amountProduct)
        {
            User = user;
            Product = product;
            AmountProduct = amountProduct;
            SaleValue = amountProduct * Product.Price;
        }
    }
}