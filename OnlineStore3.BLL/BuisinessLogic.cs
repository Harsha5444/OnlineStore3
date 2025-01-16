using OnlineStore3.Models;
using System.Collections.Generic;
public class BusinessLogic
{
    public DataAccess DAL;
    public BusinessLogic()
    {
        DAL = new DataAccess();
    }
    public (List<User>, List<Product>, List<Cart>, List<Orders>) GetAllData()
    {
        return DAL.GetAllData();
    }
    public void UpdateUsers(List<User> users)
    {
        DAL.UpdateUsers(users);
    }
    public void UpdateProducts(List<Product> products)
    {
        DAL.UpdateProducts(products);
    }
    public void UpdateOrders(List<Orders> orders)
    {
        DAL.UpdateOrders(orders);
    }
}
