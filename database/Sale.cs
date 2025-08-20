using SalesSystem.Models.Sale;
using SalesSystem.Models.Product;
using SalesSystem.Models.User;
namespace SalesSystem.Database.Sales
{
    public class Sales
    {

        List<Sale> sales = new List<Sale> { };

        public bool NewSale(User user, Product product, int amountProduct, out string message)
        {

            sales.Add(new Sale(user, product, amountProduct));
            message = "ok";
            return true;
        }

        public bool GetAllSales(User user, out string message)
        {

            foreach (var sale in sales)
            {

                if (sale.User.Name == user.Name)
                {
                    Console.WriteLine($"Nome do Produto: {sale.Product.Name} Quantidade: {sale.AmountProduct}. Valor unitário: {sale.Product.Price} R$. Valor total: {sale.SaleValue} R$.");
                }
                
            }
            message = "ok";
            return true;
        }

    }
}