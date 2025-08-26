// using SalesSystem.Models.Product;
using System.Formats.Asn1;
using System.Security.Cryptography.X509Certificates;
using SalesSystem.Models.Product;
using SalesSystem.Database.Sales;
using Microsoft.Win32.SafeHandles;
using SalesSystem.Models.Sale;
using SalesSystem.Models.User;

namespace SalesSystem.Database.Products
{
    public class DatabaseProducts
    {
        Sales.Sales Sale = new Sales.Sales();

        // Criando lista de produtos e populando
        public static List<Product> products = new List<Product>
        {
            new Models.Product.Product("Camiseta", 49.90f, 10),
            new Models.Product.Product("Calça Jeans", 120.50f, 5),
            new Models.Product.Product("Tênis Esportivo", 199.99f, 8),
            new Models.Product.Product("Boné", 35.00f, 20),
            new Models.Product.Product("Jaqueta", 250.00f, 3)
        };

        // Método para listar os produtos
        public bool PrintAllProducts()
        {
            foreach (var product in products)
            {
                Console.WriteLine($"{product.Name,-20} {product.Price,-15:C} {product.Amount,-10}");
            }
            return true;
        }

        // Método para buscar apenas um produto
        public Product? GetOneProduct(string productName, out string message)
        {

            foreach (var product in products)
            {
                if (product.Name == productName)
                {
                    message = "ok";
                    return product;
                }
            }

            message = "nome errado";
            return null;
        }

        // Método de compra
        public bool BuyProduct(User userObj, Product productObj, int amount, out string message)
        {
            // product1 = product;
            var product = products.FirstOrDefault(p => p.Name == productObj.Name);

            if (product == null) { message = "nome errado"; return false; }

            if (product.Amount - amount < 0) { message = "quantidade errada"; return false; }

            product.Amount = product.Amount - amount;

            Sale.NewSale(userObj, productObj, amount, out message);

            message = "ok";
            return true;
        }

    }
}