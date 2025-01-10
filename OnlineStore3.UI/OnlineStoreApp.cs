using System;

namespace OnlineStore3.UI
{
    internal class OnlineStoreApp
    {
        static void Main(string[] args)
        {
            BusinessLogic BLL = new BusinessLogic();
            var (users, products, cart, orders) = BLL.GetAllData();

            Console.WriteLine("Welcome to Online Store!");
            Console.WriteLine("Please choose an option: ");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register");
            string choice = Console.ReadLine();
            if (choice == "1")
            {
                LoginForm.Login(users);
            }
            else if (choice == "2")
            {
                LoginForm.Register(users);
            }
            else
            {
                Console.WriteLine("Invalid option selected.");
            }
        }
    }
}
