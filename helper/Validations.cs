namespace SalesSystem.Helper.Validations
{
    public class Validations()
    {

        public static int IntValidation(string message = "")
        {
            int value;
            string input;

            if (message != "")
            {
                 Console.WriteLine(message);   
            }

            while (true)
            {
                input = Console.ReadLine() ?? "";

                if (int.TryParse(input, out value))
                    return value;

                Console.WriteLine("Entrada inválida! Digite um número válido.");
            }
        }
    }
}