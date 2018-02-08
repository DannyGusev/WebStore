using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CustomerModel
/// </summary>
public class CustomerModel
{
    public Customer GetCustomer(string guId)
    {
        StoreDBEntities db = new StoreDBEntities();
        Customer info = (from x in db.Customers
                         where x.GUID == guId
                         select x).FirstOrDefault();

        return info;
    }

    public void InsertCustomer(Customer info)
    {
        StoreDBEntities db = new StoreDBEntities();
        db.Customers.Add(info);
        db.SaveChanges();
    }

}