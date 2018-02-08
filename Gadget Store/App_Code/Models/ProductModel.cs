using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
/// <summary>
/// Summary description for ProductModel
/// </summary>
public class ProductModel
{
    public string InsertProduct(Product product)
    {
        try
        {
            StoreDBEntities db = new StoreDBEntities();
            db.Products.Add(product);
            db.SaveChanges();

            return product.Name + " was seccesfullt inserted"; 
        }
        catch(Exception e)
        {
            return "Error:" + e;
        }
    }


    public string UpdateProduct(int id, Product product)
    {
        try
        {
            StoreDBEntities db = new StoreDBEntities();

            //fetch object from DB
            Product p = db.Products.Find(id);

            p.Name = product.Name;
            p.Price = product.Price;
            p.TypeID = product.TypeID;
            p.Description = product.Description;
            p.Image = product.Image;

            db.SaveChanges();
            return product.Name + " was seccesfullt updated";



        }
        catch (Exception e)
        {
            return "Error:" + e;
        }
    }


    public string DeleteProduct(int id)
    {
        try
        {
            StoreDBEntities db = new StoreDBEntities();
            Product product = db.Products.Find(id);

            db.Products.Attach(product);
            db.Products.Remove(product);
            db.SaveChanges();

            return product.Name + " was seccesfullt deleted";

        }
        catch (Exception e)
        {
            return "Error:" + e;
        }
    }


    public Product GetProduct(int id)
    {
        try
        {
            using (StoreDBEntities db = new StoreDBEntities())
            {
                Product product = db.Products.Find(id);
                return product;
            }
        }
        catch(Exception)
        {
            return null;
        }
    }

    public List<Product> GetAllProducts()
    {
        try
        {
            using(StoreDBEntities db = new StoreDBEntities())
            {
                List<Product> products = (from x in db.Products
                                          select x).ToList();

                return products;
            }
        }
        catch (Exception)
        {
            return null;
        }
    }

    public List<Product> GetProductsByType(int typeId)
    {
        try
        {
            using(StoreDBEntities db = new StoreDBEntities())
            {
                List<Product> products = (from x in db.Products
                                          where x.TypeID == typeId
                                          select x).ToList();

                return products;
            }
        }
        catch (Exception)
        {
            return null;
        }
    }

}