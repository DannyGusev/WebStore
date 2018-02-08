using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.SqlServer;
using System.Data.Sql;

/// <summary>
/// Summary description for PurchaseModel
/// </summary>
public class CartModel
{
    public string InsertCart(Cart cart)
    {
        try
        {
            StoreDBEntities db = new StoreDBEntities();
            db.Carts.Add(cart);
            db.SaveChanges();

            return cart.Date + " was succesfully inserted";
        }
        catch(Exception e)
        {
            return "Error:" + e;
        }
    }

    public string UpdateCart(int id, Cart cart)
    {
        try
        {
            StoreDBEntities db = new StoreDBEntities();

            //fetch object from DB
            Cart p = db.Carts.Find(id);

            p.Date = cart.Date;
            p.CustomerID = cart.CustomerID;
            p.Amount = cart.Amount;
            p.IsInCart = cart.IsInCart;
            p.Product = cart.Product;

            db.SaveChanges();

            return cart.Date + " was succesfully updated";
        }
        catch (Exception e)
        {
            return "Error:" + e;
        }
    }

    public string DeleteCart(int id)
    {
        try
        {
            StoreDBEntities db = new StoreDBEntities();
            Cart cart = db.Carts.Find(id);

            db.Carts.Attach(cart);
            db.Carts.Remove(cart);
            db.SaveChanges();


            return cart.Date + " was succesfully deleted";
        }
        catch (Exception e)
        {
            return "Error:" + e;
        }
    }

    public List<Cart> GetOrdersInCart(string userId)
    {
        StoreDBEntities db = new StoreDBEntities();
        List<Cart> orders = (from x in db.Carts
                             where x.CustomerID == userId
                             && x.IsInCart
                             orderby x.Date
                             select x).ToList();

        return orders;
    }
    public int GetAmountOfOrders(string userId)
    {
        try
        {
            StoreDBEntities db = new StoreDBEntities();
            int amount = (from x in db.Carts
                          where x.CustomerID == userId
                          && x.IsInCart
                          select x.Amount).Sum();

            return amount;
        }
        catch
        {
            return 0;
        }


    }
    public void UpdateQuantity(int id, int quantity)
    {

        StoreDBEntities db = new StoreDBEntities();
        Cart cart = db.Carts.Find(id);
        cart.Amount = quantity;

        db.SaveChanges();


    }

    public void MarkOrdersAsPaid(List<Cart> carts)
    {
        StoreDBEntities db = new StoreDBEntities();

        if(carts != null)
        {
            foreach(Cart cart in carts)
            {
                Cart oldCart = db.Carts.Find(cart.ID);
                oldCart.Date = DateTime.Now;
                oldCart.IsInCart = false;
            }

            db.SaveChanges();

        }
    }
}