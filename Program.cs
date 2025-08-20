using System;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;
using SalesSystem.Database.Users;
using SalesSystem.Database.Products;
using SalesSystem.Helper.Validations;
using System.Collections;

class Program
{
    public static void Main(string[] args)
    {

        int decision = 0;
        string name = "";
        string password = "";
        string message;

        Console.WriteLine("Bem vindo ao nosso sistema de vendas\nPara comprar conosco é necessário se registrar\nCaso não tenha registro digite 2, caso contrário digite 1");
        
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

                    Console.WriteLine("Nome: ");
                    name = Console.ReadLine() ?? "";

                    Console.WriteLine("Senha: ");
                    password = Console.ReadLine() ?? "";

                    if (DatabaseUsers.Login(name, password) == true)
                    {
                        Console.WriteLine("Login feito com sucesso\n");
                        decision = 0;
                        break;
                    }
                    break;

                case 0: break;
            }   
        }

        //Exibição de produtos
        Console.WriteLine("Os nossos produtos são:");
        var databaseProduct = new DatabaseProducts();
        databaseProduct.PrintAllProducts();

        //Opção de escolha
        Console.WriteLine("Digite o nome do produto que você deseje: ");
        var nameProduct = Console.ReadLine() ?? "";

        //Validação para o número digitado
        Console.WriteLine("Digite a quantidade:");
        int amountProduct = Validations.IntValidation();

        while (true)
        {
            databaseProduct.BuyProduct(nameProduct, amountProduct, out message);

            switch (message)
            {
                case "nome errado":

                    Console.WriteLine("O nome digitado esta errado, Digite denovo:");
                    nameProduct = Console.ReadLine() ?? "";
                    databaseProduct.BuyProduct(nameProduct, amountProduct, out message);
                    if (message == "ok") { break; }
                    else if (message == "quantidade errada") { goto case "quantidade errada"; }
                    break;

                case "quantidade errada":

                    Console.WriteLine("A quantidade digitada excede a disponível, Digite denovo:");
                    amountProduct = Validations.IntValidation();
                    databaseProduct.BuyProduct(nameProduct, amountProduct, out message);
                    if (message == "ok") { break; }
                    else if (message == "quantidade errada") { goto case "quantidade errada"; }
                    break;
            }
            databaseProduct.PrintAllProducts();
            break;
        }
    }
}
