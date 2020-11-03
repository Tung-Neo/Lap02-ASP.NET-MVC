using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WingtipToys_1_.Models;
using WingtipToys_1_.Logic;

namespace WingtipToys_1_
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfTokenKey";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuildValue;
            if(requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuildValue))
            {
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    requestCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }
            Page.PreLoad += master_Page_PreLoad;
        }
        protected void master_Page_PreLoad (object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            using (ShoppingCartActions usersShoppingCart = new ShoppingCartActions())
            {
                string cartStr = string.Format("Cart ({0})", usersShoppingCart.GetCount());
                cartCount.InnerText = cartStr;
            }
        }

        public IQueryable<Category> GetCategories()
        {
            var _db = new WingtipToys_1_.Models.ProductContext();
            IQueryable<Category> query = _db.Categories;
            return query;
        }

        //protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        //{
        //    Context.GetOwinContext().Authentication.SignOut();
        //}
    }
}