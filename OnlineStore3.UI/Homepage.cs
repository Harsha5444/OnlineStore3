using System;

namespace OnlineStore3.UI
{
    class Homepage
    {
        public void Menu(BusinessLogic BLL)
        {
            var (users, products, cart, orders) = BLL.GetAllData();
            while (true)
            {
                Console.WriteLine("********** Welcome to the Home Page **********");
            start:
                Console.WriteLine("Please choose an option from the menu below:");
                Console.WriteLine("1. View Products");
                Console.WriteLine("2. Add to Cart");
                Console.WriteLine("3. View Cart");
                Console.WriteLine("4. Checkout");
                Console.WriteLine("5. Logout");
                Console.Write("\nSelect an option: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        DisplayTable.DisplayList(products,"Products");
                        goto start;
                    case "2":
                        //AddToCart(ds);
                        break;
                    case "3":
                        DisplayTable.DisplayList(cart, "Cart");
                        goto start;
                    case "4":
                        //Checkout(ds, bll);
                        break;
                    case "5":
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid choice. Please try again.\n");
                        break;
                }
            }
        }
    }
}
