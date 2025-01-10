using System;
using System.Collections.Generic;
using System.Linq;
using OnlineStore3.Models;

namespace OnlineStore3.UI
{
    public class LoginForm
    {
        public static void Login(List<User> users)
        {
            Console.Write("Enter Username: ");
            string username = Console.ReadLine();
            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            var user = users.Where(u => u.Username == username && u.Password == password).FirstOrDefault();
            if (user != null)
            {
                Console.WriteLine($"Login Successful, Welcome '{user.Username}'");
            }
            else
            {
                Console.WriteLine($"No credentials found for user '{username}'. Please check your username or password.");
            }
        }
        public static void Register(List<User> users)
        {
            Console.Write("Enter Username: ");
            string username = Console.ReadLine();            
            Console.Write("Enter Full Name: ");
            string fullName = Console.ReadLine();
            Console.Write("Enter Password: ");
            string password = Console.ReadLine();
            Console.Write("Enter Mobile Number: ");
            string mobileNumber = Console.ReadLine();
            if (users.Any(u => u.Username == username))
            {
                Console.WriteLine($"A user with the username '{username}' already exists.");
            }
            else
            {
                var newUser = new User
                {
                    Username = username,
                    FullName = fullName,
                    Password = password,
                    MobileNumber = mobileNumber
                };
                users.Add(newUser);
                Console.WriteLine($"User '{username}' registered successfully.");
                BusinessLogic BAL = new BusinessLogic();
                BAL.updateUsers(users);
            }
        }
    }
}
