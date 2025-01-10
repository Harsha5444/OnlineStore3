using OnlineStore3.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

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
        List<Cart> carts = new List<Cart>();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM cart";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                carts.Add(new Cart
                {
                    ProductId = Convert.ToInt32(row["productid"]),
                    Username = row["username"].ToString(),
                    Quantity = Convert.ToInt32(row["quantity"]),
                    FinalPrice = Convert.ToInt32(row["finalprice"])
                });
            }
        }
        return carts;
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
    public void updateUsers(List<User> newuser)
    {

    }
}
