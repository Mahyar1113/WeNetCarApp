using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WeNetCarApp.Models;

namespace WeNetCarApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            // Check if the current request is for the login route
            var routeData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(Context));
            if (routeData?.Values["controller"]?.ToString().ToLower() == "account" &&
                routeData.Values["action"]?.ToString().ToLower() == "login")
            {
                // Handle the login form submission
                if (Request.HttpMethod == "POST")
                {
                    string username = Request.Form["username"];
                    string password = Request.Form["password"];

                    if (IsValidUser(username, password))
                    {
                        // Redirect to the authenticated page
                        Response.Redirect("~/Home/Index");
                        return;
                    }
                    else
                    {
                        // Display an error message
                        HttpContext.Current.Session["ErrorMessage"] = "Invalid username or password.";
                    }
                }
            }
        }

        private bool IsValidUser(string username, string password)
        {
            // Implement your own logic to validate the user credentials
            // For simplicity, let's assume valid credentials are username: "admin" and password: "password"
            return username == "admin" && password == "password";
        }
    }
}