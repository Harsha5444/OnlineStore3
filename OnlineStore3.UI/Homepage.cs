using OnlineStore3.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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
                        DisplayTable.DisplayList(products, "Products");
                        goto start;
                    case "2":
                        AddToCart(cart, products);
                        break;
                    case "3":
                        DisplayTable.DisplayList(cart, "Cart");
                        goto start;
                    case "4":
                        Checkout(products, orders, cart);
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
        public void AddToCart(List<Cart> cart, List<Product> products)
        {
            Console.Clear();
            Console.WriteLine("********** Add to Cart **********");
            DisplayTable.DisplayList(products, "Products");
            Console.Write("Enter the Product ID to add to the cart: ");
            int productId = Convert.ToInt32(Console.ReadLine());
            var prow = products.Where(p => p.ProductId == productId).FirstOrDefault();
            //int availableQuantity = products.Where(p => p.ProductId == productId).Select(p => p.QuantityAvailable).FirstOrDefault();
            if (prow.QuantityAvailable <= 0)
            {
                Console.WriteLine("Product is out of stock.");
                return;
            }
            int quantity;
            Console.Write($"Enter the quantity of {prow.ProductName}: ");
            if (!int.TryParse(Console.ReadLine(), out quantity) || quantity <= 0)
            {
                Console.WriteLine("Invalid quantity. Please try again.");
                return;
            }
            if (quantity > prow.QuantityAvailable)
            {
                Console.WriteLine("Insufficient stock available. Please try again.");
                return;
            }
            string productname = prow.ProductName;
            string username = Session.Username;
            int finalPrice = quantity * prow.Price;
            cart.Add(new Cart
            {
                ProductName = productname,
                Username = username,
                Quantity = quantity,
                FinalPrice = finalPrice
            });
            prow.QuantityAvailable -= quantity;
            DisplayTable.DisplayList(cart, "Cart");
            Console.WriteLine($"Successfully added {quantity} of {prow.ProductName} to the cart.\n");
        }
        public void Checkout(List<Product> products, List<Orders> orders, List<Cart> cart)
        {

        }
    }
}
