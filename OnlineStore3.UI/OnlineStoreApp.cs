﻿using System;

namespace OnlineStore3.UI
{
    internal class OnlineStoreApp
    {
        static void Main(string[] args) 
        {
            BusinessLogic BLL = new BusinessLogic();
            var (users, products, cart, orders) = BLL.GetAllData();
            
            //DisplayTable.DisplayList(users,"Users");
            Console.WriteLine("Welcome to Online Store!");
            Console.WriteLine("Please choose an option: ");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register");
            string choice = Console.ReadLine();
            if (choice == "1")
            {
                LoginForm.Login(users, BLL);
                //Homepage menu = new Homepage();
                //menu.Menu();
            }
            else if (choice == "2")
            {
                LoginForm.Register(users, BLL);
            }
            else
            {
                Console.WriteLine("Invalid option selected.");
            }
            Console.ReadKey();
        }
    }
}
