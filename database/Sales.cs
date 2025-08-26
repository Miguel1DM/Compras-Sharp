using SalesSystem.Models.Sale;
using SalesSystem.Models.Product;
using SalesSystem.Models.User;
using Microsoft.VisualBasic;
namespace SalesSystem.Database.Sales
{
    public class Sales
    {

        private static List<Sale> _sales = new List<Sale> { };

        public bool GetAllSales(User user)
        {
            bool userHasSale = false;
            foreach (var sale in _sales)
            {

                if (sale.User.Name == user.Name)
                {
                    userHasSale = true;
                    int index = _sales.IndexOf(sale);
                    Console.WriteLine($"\nIndice da compra: {index} - Nome do Produto: {sale.Product.Name} - Quantidade: {sale.AmountProduct} - Valor unitário: {sale.Product.Price} R$ - Valor total: {sale.SaleValue} R$.");
                }
            }

            return userHasSale;
        }

        public static Sale GetOneSale(string productName)
        {

            Sale oneSale = null!;
            foreach (var sale in _sales)
            {
                if (sale.Product.Name == productName)
                {
                    oneSale = sale;
                    return oneSale;
                }
            }

            return oneSale;
        }

        public bool NewSale(User user, Product product, int amountProduct, out string message)
        {
            _sales.Add(new Sale(user, product, amountProduct));
            message = "ok";
            return true;
        }

        public bool DeleteSale(int index, out string message)
        {

            if (index < 0 || index >= _sales.Count)
            {
                message = "Índice inválido";
                return false;
            }
            _sales.RemoveAt(index);

            message = "ok";
            return true;
        }

        public static bool AlterSale(Product product, int amount)
        {
            foreach (var sale in _sales)
            {
                if (sale.Product.Name == product.Name)
                {
                    sale.AmountProduct = amount;
                }
            }
            return true;
        }

        public float GeneralSalesValue(User user)
        {
            float value = 0;

            foreach (var sale in _sales)
            {
                if (sale.User.Name == user.Name)
                {
                    value += sale.SaleValue;
                }
            }

            return value;
        }
    }
}