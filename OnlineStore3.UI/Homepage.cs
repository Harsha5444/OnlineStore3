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
            Dictionary<int, int> cartProducts = new Dictionary<int, int>();
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
                        AddToCart(cart, products, cartProducts);
                        break;
                    case "3":
                        DisplayTable.DisplayList(cart, "Cart");
                        goto start;
                    case "4":
                        Checkout(products, orders, cart, BLL, cartProducts);
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
        public void AddToCart(List<Cart> cart, List<Product> products, Dictionary<int, int> cartProducts)
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
            cartProducts.Add(productId,quantity);
            DisplayTable.DisplayList(cart, "Cart");
            Console.WriteLine($"Successfully added {quantity} of {prow.ProductName} to the cart.\n");
        }
        public void Checkout(List<Product> products, List<Orders> orders, List<Cart> cart, BusinessLogic BLL, Dictionary<int, int> cartProducts)
        {
            if (cart == null || cart.Count == 0)
            {
                Console.WriteLine("Your cart is empty. Cannot proceed with checkout.");
                Console.WriteLine("\nPress any key to return to the menu.");
                Console.ReadKey();
                return;
            }
            decimal totalCost = cart.Sum(item => item.FinalPrice);
            var orderDetailsList = cart.Select(cartItem =>
            {
                var product = products.FirstOrDefault(p => p.ProductName == cartItem.ProductName);
                string productName = product.ProductName;
                int quantity = cartItem.Quantity;
                return $"{productName} x {quantity}";
            }).ToList();

            string orderDetails = string.Join(", ", orderDetailsList);
            var newOrder = new Orders
            {
                Username = Session.Username,
                TotalCost = totalCost,
                OrderDate = DateTime.Now,
                OrderDetails = orderDetails
            };
            orders.Add(newOrder);
            BLL.UpdateProducts(cartProducts);
            BLL.UpdateOrders(orders);
            cart.Clear();
            Console.Clear();
            Console.WriteLine("********** Order Confirmation **********");
            Console.WriteLine($"Order placed successfully by {Session.Username}.");
            Console.WriteLine($"Total Cost: {totalCost}");
            Console.WriteLine($"Order Date: {DateTime.Now}");
            Console.WriteLine($"Order Details: {orderDetails}");
            Console.WriteLine("\nPress any key to return to the homepage.");
            Console.ReadKey();
        }
    }
}
