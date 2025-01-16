using OnlineStore3.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineStore3.UI
{
    public class LoginForm
    {
        public static void Login(List<User> users, BusinessLogic BLL)
        {
            Console.Write("Enter Username: ");
            string username = Console.ReadLine();
            Console.Write("Enter Password: ");
            string password = Console.ReadLine();
            var user = users.Where(u => u.Username == username && u.Password == password).FirstOrDefault();
            if (user != null)
            {
                Console.Clear();
                Console.WriteLine($"Login Successful, Welcome '{user.Username}'\n");
                Session.Username = username;
                Homepage menu = new Homepage();
                menu.Menu(BLL);
            }
            else
            {
                Console.WriteLine($"No credentials found for user '{username}'. Please check your username or password.");
            }
        }
        public static void Register(List<User> users, BusinessLogic BLL)
        {
            Console.Write("Enter Username: ");
            string username = Console.ReadLine();
            if (users.Any(u => u.Username == username))
            {
                Console.WriteLine($"A user with the username '{username}' already exists.");
                return;
            }
            Console.Write("Enter Full Name: ");
            string fullName = Console.ReadLine();
            Console.Write("Enter Password: ");
            string password = Console.ReadLine();
            Console.Write("Enter Mobile Number: ");
            string mobileNumber = Console.ReadLine();
            int userId = users.Max(u => u.UserId) + 1;
            var newUser = new User
            {
                UserId = userId,
                Username = username,
                FullName = fullName,
                Password = password,
                MobileNumber = mobileNumber
            };
            users.Add(newUser);
            try
            {
                BLL.UpdateUsers(users);
                Session.Username = username;
                Homepage menu = new Homepage();
                menu.Menu(BLL);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating users: {ex.Message}");
            }
        }
    }
}
