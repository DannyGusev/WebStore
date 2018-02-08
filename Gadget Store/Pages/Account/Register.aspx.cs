using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;



public partial class Pages_Account_Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        UserStore<IdentityUser> userStore = new UserStore<IdentityUser>();

        userStore.Context.Database.Connection.ConnectionString =
            System.Configuration.ConfigurationManager.ConnectionStrings["StoreDBconnectionString"].ConnectionString;

        UserManager<IdentityUser> manager = new UserManager<IdentityUser>(userStore);

        //create new user and try to store in DB
        IdentityUser user = new IdentityUser();
        user.UserName = txtUserName.Text;

        if(txtPassword.Text == txtConfirm.Text)
        {
            try
            {
                //create user object
                //DB will be created / expanded automatically
                IdentityResult result = manager.Create(user, txtPassword.Text);
                if (result.Succeeded)
                {
                    Customer info = new Customer
                    {
                        Address = txtAddress.Text,
                        First_Name = txtFirstName.Text,
                        Last_Name = txtLastName.Text,
                        PostalCode = Convert.ToInt32(txtPostalCode.Text),
                        GUID = user.Id
                    };

                    CustomerModel model = new CustomerModel();
                    model.InsertCustomer(info);

                    //store user in DB
                    var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;

                    //set to log in new user by cookie
                    var userIdentity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

                    //login the new user and redirect to homepage
                    authenticationManager.SignIn(new AuthenticationProperties(), userIdentity);
                    Response.Redirect("~/Index.aspx");
                }
                else
                {
                    litStatus.Text = result.Errors.FirstOrDefault();
                }
            }
            catch(Exception ex)
            {
                litStatus.Text = ex.ToString();
            }
        }
        else
        {
            litStatus.Text = "Password must match";
        }
    }
}