using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

/// <summary>
/// Summary description for ProductTypeModel
/// </summary>
public class ProductTypeModel
{
    public string InsertProductType(ProductType productType)
    {
        try
        {
            StoreDBEntities db = new StoreDBEntities();
            db.ProductTypes.Add(productType);
            db.SaveChanges();

            return productType.Name + " was succesfully inserted";
        }
        catch(Exception e)
        {
            return "Error:" + e;
        }
    }

    public string UpdateProductType(int id, ProductType productType)
    {
        try
        {
            StoreDBEntities db = new StoreDBEntities();

            //fetch object from db
            ProductType p = db.ProductTypes.Find(id);

            p.Name = productType.Name;

            db.SaveChanges();
            return productType.Name + " was succesfully updated";

        }
        catch (Exception e)
        {
            return "Error:" + e;
        }
    }

    public string DeleteProductType(int id)
    {
        try
        {
            StoreDBEntities db = new StoreDBEntities();
            ProductType productType = db.ProductTypes.Find(id);

            db.ProductTypes.Attach(productType);
            db.ProductTypes.Remove(productType);
            db.SaveChanges();

            return productType.Name + " was succesfully deleted";


        }
        catch (Exception e)
        {
            return "Error:" + e;
        }
    }
}