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
    public void updateUsers(List<User> users)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("userid", typeof(int));
        dt.Columns.Add("fullname", typeof(string));
        dt.Columns.Add("username", typeof(string));
        dt.Columns.Add("password", typeof(string));
        dt.Columns.Add("mobilenumber", typeof(string));

        foreach (var user in users)
        {
            dt.Rows.Add(user.UserId, user.FullName, user.Username, user.Password, user.MobileNumber);
        }
        DAL.updateUsers(dt);
    }
}
