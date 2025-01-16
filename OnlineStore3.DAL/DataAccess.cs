using OnlineStore3.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

public class DataAccess
{
    private string connectionString = ConfigurationManager.ConnectionStrings["MiniProjectDB"].ConnectionString;

    List<User> users = new List<User>();
    List<Product> products = new List<Product>();
    List<Cart> carts = new List<Cart>();
    List<Orders> orders = new List<Orders>();
    public (List<User>, List<Product>, List<Cart>, List<Orders>) GetAllData()
    {
        users = GetUsers();
        products = GetProducts();
        carts = GetCarts();
        orders = GetOrders();
        return (users, products, carts, orders);
    }
    private List<User> GetUsers()
    {
        List<User> users = new List<User>();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM users";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                users.Add(new User
                {
                    UserId = Convert.ToInt32(row["userid"]),
                    FullName = row["fullname"].ToString(),
                    Username = row["username"].ToString(),
                    Password = row["password"].ToString(),
                    MobileNumber = row["mobilenumber"].ToString()
                });
            }
        }
        return users;
    }
    private List<Product> GetProducts()
    {
        List<Product> products = new List<Product>();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM products";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                products.Add(new Product
                {
                    ProductId = Convert.ToInt32(row["productid"]),
                    ProductName = row["productname"].ToString(),
                    Price = Convert.ToInt32(row["price"]),
                    QuantityAvailable = Convert.ToInt32(row["quantityavailable"])
                });
            }
        }
        return products;
    }
    private List<Cart> GetCarts()
    {
        List<Cart> cart = new List<Cart>();
        //using (SqlConnection connection = new SqlConnection(connectionString))
        //{
        //    string query = "SELECT * FROM cart";
        //    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
        //    DataTable dt = new DataTable();
        //    dataAdapter.Fill(dt);
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        carts.Add(new Cart
        //        {
        //            ProductId = Convert.ToInt32(row["productid"]),
        //            Username = row["username"].ToString(),
        //            Quantity = Convert.ToInt32(row["quantity"]),
        //            FinalPrice = Convert.ToInt32(row["finalprice"])
        //        });
        //    }
        //}
        return cart;
    }
    private List<Orders> GetOrders()
    {
        List<Orders> orders = new List<Orders>();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM orders";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                orders.Add(new Orders
                {
                    Username = row["username"].ToString(),
                    TotalCost = Convert.ToDecimal(row["totalcost"]),
                    OrderDate = Convert.ToDateTime(row["orderdate"]),
                    OrderDetails = row["orderdetails"].ToString()
                });
            }
        }
        return orders;
    }
    public void UpdateUsers(List<User> users)
    {
        var existingUsers = GetUsers();
        var newUsers = users.Where(u => !existingUsers.Any(e => e.Username == u.Username)).ToList();
        //DataSet ds = new DataSet();
        DataTable dt = new DataTable("Users");
        dt.Columns.Add("UserId");
        dt.Columns.Add("FullName");
        dt.Columns.Add("Username");
        dt.Columns.Add("Password");
        dt.Columns.Add("MobileNumber");
        //ds.Tables.Add(dt);
        foreach (var user in newUsers)
        {
            dt.Rows.Add(user.UserId, user.FullName, user.Username, user.Password, user.MobileNumber);
        }
        using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MiniProjectDB"].ConnectionString))
        {
            using (var da = new SqlDataAdapter("SELECT * FROM Users", conn))
            {
                var commandBuilder = new SqlCommandBuilder(da);
                try
                {
                    da.Update(dt);
                    Console.WriteLine("User registered successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating users: {ex.Message}");
                }
            }
        }
    }
    public void UpdateProducts(List<Product> products)
    {
        DataTable dt = new DataTable("Products");
        dt.Columns.Add("ProductId", typeof(int));
        dt.Columns.Add("ProductName", typeof(string));
        dt.Columns.Add("Price", typeof(int));
        dt.Columns.Add("QuantityAvailable", typeof(int));
        foreach (var product in products)
        {
            dt.Rows.Add(product.ProductId, product.ProductName, product.Price, product.QuantityAvailable);
        }
        foreach (DataRow row in dt.Rows)
        {
            row.SetModified();
        }
        using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MiniProjectDB"].ConnectionString))
        {
            using (var da = new SqlDataAdapter("SELECT * FROM Products", conn))
            {
                var commandBuilder = new SqlCommandBuilder(da);
                try
                {
                    da.Update(dt);
                    Console.WriteLine("Products updated successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating products: {ex.Message}");
                }
            }
        }
    }

    public void UpdateOrders(List<Orders> orders)
    {
        var existingOrders = GetOrders();
        var newOrders = orders.Where(o =>
            !existingOrders.Any(existingOrder =>
                existingOrder.Username == o.Username &&
                existingOrder.TotalCost == o.TotalCost &&
                existingOrder.OrderDate == o.OrderDate &&
                existingOrder.OrderDetails == o.OrderDetails
            )
        ).ToList();
        if (newOrders.Any())
        {
            DataTable dt = new DataTable("Orders");
            dt.Columns.Add("Username");
            dt.Columns.Add("TotalCost");
            dt.Columns.Add("OrderDate");
            dt.Columns.Add("OrderDetails");
            foreach (var order in newOrders)
            {
                dt.Rows.Add(order.Username, order.TotalCost, order.OrderDate, order.OrderDetails);
            }
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MiniProjectDB"].ConnectionString))
            {
                using (var da = new SqlDataAdapter("SELECT * FROM Orders", conn))
                {
                    var commandBuilder = new SqlCommandBuilder(da);
                    try
                    {
                        da.Update(dt);
                        Console.WriteLine("New orders added successfully.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error adding new orders: {ex.Message}");
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("No new orders to update.");
        }
    }
}
