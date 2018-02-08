using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

public partial class Pages_Product : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FillPage();
    }

    private void FillPage()
    {
        //get selected product data
        if (!string.IsNullOrWhiteSpace(Request.QueryString["id"]))
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);
            ProductModel productModel = new ProductModel();
            Product product = productModel.GetProduct(id);

            //fill page with data
            lblPrice.Text = "Price per unit: <br/>$ " + product.Price;
            lblTitle.Text = product.Name;
            lblDescription.Text = product.Description;
            lblItemNr.Text = id.ToString();
            imgProduct.ImageUrl = "~/Images/Products/" + product.Image;

            //fill amount dropdownlist with number 1-20
            int[] amount = Enumerable.Range(1, 20).ToArray();
            ddlAmount.DataSource = amount;
            ddlAmount.AppendDataBoundItems = true;
            ddlAmount.DataBind();
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(Request.QueryString["id"]))
        {
            string clientId = Context.User.Identity.GetUserId();

            if (clientId != null)
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);
                int amount = Convert.ToInt32(ddlAmount.SelectedValue);

                Cart cart = new Cart
                {
                    Amount = amount,
                    CustomerID = clientId,
                    Date = DateTime.Now,
                    IsInCart = true,
                    ProductID = id

                };

                CartModel model = new CartModel();
                
                lblResult.Text = model.InsertCart(cart);
                Response.Redirect("~/Pages/Product.aspx?id=" + id);

            }
            
            else
            {
                lblResult.Text = "Please log in to order items";
            }
            
        }
        

    }
}