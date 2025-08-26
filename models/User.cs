namespace SalesSystem.Models.User
{
    public class User
    {
        public string Name { get; set; }
        private string _password { get; set; }
        public User(string name, string password)
        {

            Name = name;
            _password = password;
        }
        public bool ValidatePassword(string password)
        {
            if (_password == password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }

}