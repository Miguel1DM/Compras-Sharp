// using SalesSystem.Models.Product;
using System.Formats.Asn1;
using System.Security.Cryptography.X509Certificates;
using SalesSystem.Models.Product;

namespace SalesSystem.Database.Products
{
    public class DatabaseProducts
    {

        // Criando lista de produtos e populando
        public static List<Models.Product.Product> products = new List<Models.Product.Product>
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
                Console.WriteLine($"Nome: {product.Name}, Valor: {product.Price} R$. Quantidade disponível: {product.Amount}");
            }
            return true;
        }

        // Método para bucar um apenas um produto
        public bool GetOneProduct(string productName, out string message)
        {

            foreach (var product in products)
            {
                if (product.Name == productName)
                {
                    message = "ok";
                    return true;
                }
            }

            message = "nome errado";
            return false;
        }

        // Método de compra
        public bool BuyProduct(string name, int amount, out string message)
        {
            var product = products.FirstOrDefault(u => u.Name == name);

            if (product == null)
            {
                message = "nome errado";
                return false;
            }

            if (product.Amount - amount <= 0)
            {
                message = "quantidade errada";
                return false;
            }

            product.Amount = product.Amount - amount;

            message = "ok";
            return true;
        }

    }
}