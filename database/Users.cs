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

        public static User? Login(string name, string password, out string message)
        {

            var user = _users.FirstOrDefault(u => u.Name == name);

            if (user == null)
            {
                message = "login errado";
                return null;
            }
            else if (user.ValidatePassword(password))
            {
                message = "ok";
                return user;
            }

            message = "login errado";
            return null;
        }

        public static bool NewUser(string name, string password, out string message)
        {

            foreach (var user in _users)
            {
                if (user.Name == name)
                {
                    message = "ja existe um usuario com esse nome";
                    return false;
                }
            }
            _users.Add(new User(name, password));

            message = "ok";
            return true;
        }
    }
}