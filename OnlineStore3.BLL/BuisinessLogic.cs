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
    public void updateUsers(List<User> users)
    {
        DAL.updateUsers(users);
    }
}
