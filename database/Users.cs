using Microsoft.VisualBasic;
using SalesSystem.Models.User;
namespace SalesSystem.Database.Users
{
    //Simulação de um banco de dados, Por ter optado em não conectar com um Banco,
    //foi necessário inserir registros no próprio projeto
    public class DatabaseUsers
    {

        //Inserção de "registros", criando instâncias do objeto User, dentro de uma Lita,
        private static List<User> _users = new List<User>
        {
            new User("miguel", "123"),
            new User("joao", "456"),
            new User("maria", "789"),
            new User("ana", "abc"),
            new User("carlos", "xyz")
        };

        //Criando os serviços
        public static void PrintAllUsers()
        {
            foreach (var user in _users)
            {
                Console.WriteLine(user.Name);
            }
        }

        public static bool Login(string name, string password)
        {

            var user = _users.FirstOrDefault(u => u.Name == name);

            if (user == null)
            {
                return false;
            }
            else if (user.ValidatePassword(password))
            {
                return true;
            }
            
            return false; 
        }
    }
}