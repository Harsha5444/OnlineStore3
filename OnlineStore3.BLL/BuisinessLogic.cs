using OnlineStore3.Models;
using System.Collections.Generic;
using System.Data;
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
}
