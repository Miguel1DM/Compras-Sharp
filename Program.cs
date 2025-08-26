using System;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;
using SalesSystem.Database.Users;
using SalesSystem.Database.Products;
using SalesSystem.Helper.Validations;
using System.Collections;
using SalesSystem.Models.Product;
using SalesSystem.Models.User;
using SalesSystem.Database.Sales;
using SalesSystem.Models.Sale;
using System.Linq.Expressions;
using System.Diagnostics.CodeAnalysis;
class Program
{
    public static void Main(string[] args)
    {

        // Definindo as variáveis que serão globais no código todo
        int decision = 0;
        string name = "";
        string password = "";
        string message;
        User? user = null;
        Product? product = null;
        var nameProduct = "";

        // Primeiras iterações com o usuário, apresentação do sistema e opções de escolha
        Console.WriteLine("\n==============================");
        Console.WriteLine("   Bem-vindo ao Compras Sharp");
        Console.WriteLine("==============================\n");

        Console.WriteLine("Escolha uma opção:");
        Console.WriteLine("[1] Registrar novo usuário");
        Console.WriteLine("[2] Fazer login");


        // Validação da entrada do usuário 1 ou 2
        while (true)
        {
            // Validação da entrada do usuário, se ele digitou um inteiro
            decision = Validations.IntValidation();

            if (decision == 1 || decision == 2) { break; }
            Console.WriteLine("Entrada inválida! Digite 1 ou 2.");
        }

        //  Tratamento da escolha, 1 registro, 2 login
        while (decision == 1 || decision == 2)
        {
            switch (decision)
            {
                case 1:

                    Console.WriteLine("Digite o nome de usuário, será usado para fazer login");
                    name = Console.ReadLine() ?? "";

                    Console.WriteLine("Digite uma Senha");
                    password = Console.ReadLine() ?? "";

                    DatabaseUsers.NewUser(name, password, out message);

                    if (message == "ja existe um usuario com esse nome")
                    {
                        Console.WriteLine("Ja existe um usuário com esse nome, escolha outro");
                        goto case 1;
                    }
                    Console.WriteLine("\n✅ Cadastro concluído com sucesso!");
                    Console.WriteLine("Agora você já pode fazer login.\n");
                    goto case 2;

                case 2:

                    Console.WriteLine("Nome: ");
                    name = Console.ReadLine() ?? "";

                    Console.WriteLine("Senha: ");
                    password = Console.ReadLine() ?? "";

                    user = DatabaseUsers.Login(name, password, out message);

                    if (user != null && message == "ok")
                    {
                        Console.WriteLine("Login feito com sucesso\n");
                        decision = 0;
                        break;
                    }

                    Console.WriteLine("\n❌ Usuário ou senha incorretos. Tente novamente.\n");
                    break;
            }
        }

        //Exibição de produtos
        Console.WriteLine("\n📦 Produtos disponíveis:\n");
        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine($"{"Produto",-20} {"Valor",-15} {"Quantidade",-10}");
        Console.WriteLine("-------------------------------------------------");
        var databaseProduct = new DatabaseProducts();
        databaseProduct.PrintAllProducts();
        
        //O sistema esta fragmentado em um grande loop, como suas fases definidas dentro de cases.
        while (true)
        {
            switch ("start")
            {
                case "start":

                    Console.WriteLine("\n🛒 Digite o nome do produto que você deseja: ");

                    while (true)
                    {
                        nameProduct = Console.ReadLine() ?? "";
                        product = databaseProduct.GetOneProduct(nameProduct, out message);

                        if (product != null && product.Amount == 0)
                        {
                            Console.WriteLine("\n⚠️ O produto selecionado está esgotado!");
                            Console.WriteLine("Por favor, digite o nome de outro produto disponível no estoque.");
                        }

                        else if (message == "ok" && product != null)

                        {
                            Console.WriteLine($"\n✔ Produto encontrado!");
                            Console.WriteLine($"   {"Produto:",-12} {product.Name}");
                            Console.WriteLine($"   {"Valor:",-12} {product.Price:C}");
                            Console.WriteLine($"   {"Disponível:",-12} {product.Amount}");
                            Console.WriteLine("\nDigite a quantidade desejada: ");
                            goto case "quantidade";
                        }
                        else if (message == "nome errado")
                        {
                            Console.WriteLine("❌ O nome digitado está incorreto. Digite novamente:");

                        }

                    }

                case "quantidade":

                    int amountProduct = Validations.IntValidation();

                    if (user != null && product != null) { databaseProduct.BuyProduct(user, product, amountProduct, out message); }

                    if (message == "ok")
                    {
                        Console.WriteLine("\n✅ Produto adicionado!");
                        goto case "escolha";
                    }
                    else if (message == "quantidade errada")
                    {
                        Console.WriteLine("⚠ A quantidade informada excede a disponível. Digite novamente:");
                        goto case "quantidade";
                    }
                    return;

                case "escolha":

                    Console.WriteLine("-------------------------------------------------------------");
                    Console.WriteLine("Digite uma opção:");
                    Console.WriteLine("1 - Continuar comprando");
                    Console.WriteLine("2 - Ver carrinho");
                    Console.WriteLine("3 - Finalizar compra");
                    Console.WriteLine("-------------------------------------------------------------");

                    while (true)
                    {
                        // Validação da entrada do usuário, se ele digitou um inteiro
                        decision = Validations.IntValidation();

                        // Validando a escolha, 1, 2 ou 3
                        if (decision == 1 || decision == 2 || decision == 3) { break; }
                        Console.WriteLine("❌ Entrada inválida! Digite 1, 2 ou 3.");
                    }

                    if (decision == 1) { goto case "start"; }
                    else if (decision == 2 && user != null) { goto case "carrinho"; }
                    else { goto case "finalizarCompra"; }

                case "carrinho":

                    if (user != null)
                    {
                        var sales = new Sales();

                        if (sales.GetAllSales(user) == false)
                        {
                            Console.WriteLine("Você ainda não realizou nenhuma compra");
                        }

                        Console.WriteLine("\n🛍 O que deseja fazer?");
                        Console.WriteLine("1 - Excluir uma compra");
                        Console.WriteLine("2 - Alterar quantidade");
                        Console.WriteLine("3 - Voltar");
                        Console.WriteLine("-------------------------------------------------------------");

                        // Lidando com a escolha do usuário
                        while (true)
                        {
                            decision = Validations.IntValidation();
                            if (decision == 1)
                            {
                                Console.WriteLine("Digite o índice da venda que deseja excluir");
                                goto case "excluirVenda";
                            }
                            else if (decision == 2)
                            {
                                Console.WriteLine("Digite o nome do produto que deseja alterar a quantidade:");
                                goto case "alterarVenda";
                            }
                            else if (decision == 3)
                            {
                                goto case "escolha";
                            }
                        }
                    }
                    break;

                case "excluirVenda":

                    while (true)
                    {
                        var index = Validations.IntValidation();
                        Sales sales = new Sales();
                        sales.DeleteSale(index, out message);

                        if (message == "ok")
                        {
                            Console.WriteLine("\n🗑 Venda excluída com sucesso!");
                            goto case "escolha";
                        }
                        else
                        {
                            Console.WriteLine("❌ Índice inválido. Digite novamente:");
                            goto case "excluirVenda";
                        }
                    }

                case "alterarVenda":

                    nameProduct = Console.ReadLine() ?? "";

                    product = databaseProduct.GetOneProduct(nameProduct, out message);
                    if (message == "ok" && product != null)
                    {

                        var sale = Sales.GetOneSale(product.Name);
                        int saleAmount = sale.AmountProduct;
                        int unsoldAmount = product.Amount;
                        int realAmount = saleAmount + unsoldAmount;

                        Console.WriteLine($"\n📝 Produto encontrado!");
                        Console.WriteLine($"   {"Produto:",-12} {product.Name}");
                        Console.WriteLine($"   {"Valor:",-12} {product.Price:C}");
                        Console.WriteLine($"   {"Disponível:",-12} {realAmount}");
                        Console.WriteLine("\nDigite a nova quantidade:");

                        while (true)
                        {
                            int newAmount = Validations.IntValidation();

                            if (newAmount <= realAmount && newAmount > 1)
                            {
                                Sales.AlterSale(product, newAmount);
                                Console.WriteLine("\n✅ Quantidade alterada com sucesso!");
                                goto case "escolha";
                            }

                            Console.WriteLine("❌ Quantidade inválida. Digite novamente:");
                        }
                    }
                    else
                    {
                        Console.WriteLine("❌ Nome digitado incorretamente. Digite novamente:");
                        goto case "alterarVenda";
                    }

                case "finalizarCompra":

                    {
                        Console.WriteLine("\n🛍️ Suas compras:");
                        Console.WriteLine("-------------------------------------------------");

                        var sale = new Sales();
                        float totalValue;

                        if (user != null)
                        {
                            totalValue = sale.GeneralSalesValue(user);

                            Console.WriteLine("\n✅ Compra finalizada com sucesso!");

                            Console.WriteLine("\n🧾 Recibo da sua compra");
                            sale.GetAllSales(user);
                            Console.WriteLine("-------------------------------------------------");
                            Console.WriteLine($"💰 Valor total da compra: {totalValue:C}");
                            Console.WriteLine("-------------------------------------------------\n");
                            Console.WriteLine("O programa será encerrado em 3 segundos...");
                            Thread.Sleep(3000);
                            Environment.Exit(0);
                        }
                    }
                    break;
            }
            break;
        }
    }
}
