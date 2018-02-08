using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Pages_Management_ManageProducts : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetImages();

            //check if the url contains an id parameter
            if (!string.IsNullOrWhiteSpace(Request.QueryString["id"]))
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);
                FillPage(id);
            }

        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ProductModel productModel = new ProductModel();
        Product product = CreateProduct();

        //check if the url contains an id parameter
        if (!string.IsNullOrWhiteSpace(Request.QueryString["id"]))
        {
            //ID exists -> update existing row
            int id = Convert.ToInt32(Request.QueryString["id"]);
            lblResult.Text = productModel.UpdateProduct(id, product);
        }
        else
        {
            //ID does not exist -> create a new row
            lblResult.Text = productModel.InsertProduct(product);
        }

           
    }

    private void FillPage(int id)
    {
        //get selected product from DB
        ProductModel productModel = new ProductModel();
        Product product = productModel.GetProduct(id);

        //fill texboxes
        txtDescription.Text = product.Description;
        txtName.Text = product.Name;
        txtPrice.Text = product.Price.ToString();

        //set dropdownlist values
        ddlImage.SelectedValue = product.Image;
        ddlType.SelectedValue = product.TypeID.ToString();

    }

    private void GetImages()
    {
        try
        {
            //get all filespaths
            string[] images = Directory.GetFiles(Server.MapPath("~/images/Products/"));

            //get all filenames and add to an arraylist
            ArrayList imageList = new ArrayList();
            foreach(string image in images)
            {
                string imageName = image.Substring(image.LastIndexOf(@"\", StringComparison.Ordinal) + 1);
                imageList.Add(imageName);
            }

            //set the arraylist as the dropdownview's datasource and refresh
            ddlImage.DataSource = imageList;
            ddlImage.AppendDataBoundItems = true;
            ddlImage.DataBind();
        }
        catch(Exception e)
        {
            lblResult.Text = e.ToString();
        }
    }

    private Product CreateProduct()
    {
        Product product = new Product();

        product.Name = txtName.Text;

        double p;
        if (double.TryParse(txtPrice.Text, out p))
            product.Price = (int)(double)p;
        //product.Price = Convert.ToDouble(txtPrice.Text);


        product.TypeID = Convert.ToInt32(ddlType.SelectedValue);
        product.Description = txtDescription.Text;
        product.Image = ddlImage.SelectedValue;

        return product;
    }


    
}